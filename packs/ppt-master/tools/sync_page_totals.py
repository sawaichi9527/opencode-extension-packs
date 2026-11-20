#!/usr/bin/env python3
"""Synchronise the "current/total" page footer of an FII deck.

PowerPoint has no total-slides field, so the total has to be written as
literal text.  This helper keeps that literal in sync with the real page
count for both authoring inputs and delivered PPTX files.

Usage
-----
    python sync_page_totals.py <project-dir>          # before svg_to_pptx
    python sync_page_totals.py <deck.pptx>            # after export/edit
    python sync_page_totals.py <path> --total 12      # explicit total
    python sync_page_totals.py <path> --dry-run

The page-total shape is identified by a ``page-total`` name (PPTX) or by the
``layout-page-total`` id / ``data-pptx-page-total="true"`` marker (SVG).
"""

from __future__ import annotations

import argparse
import re
import sys
from pathlib import Path

_PAGE_TOTAL_RE = re.compile(r"page[-_ ]?total", re.IGNORECASE)


def _is_page_total_name(name: str) -> bool:
    return bool(name and _PAGE_TOTAL_RE.search(name))
_PAGE_TOTAL_TEXT_RE = re.compile(
    r'(<text\b(?=[^>]*(?:id="[^"]*page[-_]?total[^"]*"'
    r'|data-pptx-page-total="(?:1|true|yes)"))[^>]*>)'
    r"(.*?)"
    r"(</text>)",
    re.IGNORECASE | re.DOTALL,
)


def sync_svg_text(svg_path: Path, total: int, dry_run: bool) -> bool:
    text = svg_path.read_text(encoding="utf-8")
    value = f"/{total}"

    def _replace(match: re.Match[str]) -> str:
        return f"{match.group(1)}{value}{match.group(3)}"

    updated, count = _PAGE_TOTAL_TEXT_RE.subn(_replace, text)
    if count == 0 or updated == text:
        return False
    if not dry_run:
        svg_path.write_text(updated, encoding="utf-8")
    return True


def sync_project(project: Path, total: int | None, dry_run: bool) -> int:
    svg_dir = project / "svg_output"
    if not svg_dir.is_dir():
        svg_dir = project
    svg_files = sorted(svg_dir.glob("*.svg"))
    if not svg_files:
        raise SystemExit(f"No SVG pages found under {svg_dir}")
    effective_total = total or len(svg_files)
    updated = 0
    for svg_path in svg_files:
        if sync_svg_text(svg_path, effective_total, dry_run):
            updated += 1
            print(f"{'would update' if dry_run else 'updated'} {svg_path}")
    print(f"page total: /{effective_total} ({updated} SVG file(s))")
    return 0


def sync_pptx(pptx_path: Path, total: int | None, dry_run: bool) -> int:
    try:
        from pptx import Presentation
        from pptx.enum.shapes import PP_PLACEHOLDER
        from pptx.enum.text import MSO_ANCHOR
    except ImportError as exc:  # pragma: no cover
        raise SystemExit(
            "python-pptx is required: pip install python-pptx"
        ) from exc

    prs = Presentation(str(pptx_path))
    effective_total = total or len(prs.slides)
    updated = 0

    def update_shape(shape) -> bool:
        nonlocal updated
        name = (shape.name or "")
        if not _is_page_total_name(name) or not shape.has_text_frame:
            return False
        text_frame = shape.text_frame
        if not text_frame.paragraphs:
            return False
        paragraph = text_frame.paragraphs[0]
        value = f"/{effective_total}"
        if paragraph.text == value:
            return False
        if not paragraph.runs:
            paragraph.text = value
        else:
            paragraph.runs[0].text = value
            for run in paragraph.runs[1:]:
                run._r.getparent().remove(run._r)
        updated += 1
        return True

    def align_placeholder(placeholder, reference) -> bool:
        changed = False
        if placeholder.top != reference.top:
            placeholder.top = reference.top
            changed = True
        if placeholder.height != reference.height:
            placeholder.height = reference.height
            changed = True
        if placeholder.text_frame.vertical_anchor != MSO_ANCHOR.TOP:
            placeholder.text_frame.vertical_anchor = MSO_ANCHOR.TOP
            changed = True
        return changed

    def align_layout(layout) -> int:
        total_shape = next(
            (sh for sh in layout.shapes if _is_page_total_name(sh.name)),
            None,
        )
        if total_shape is None:
            return 0
        count = 0
        for placeholder in layout.placeholders:
            if placeholder.placeholder_format.type == PP_PLACEHOLDER.SLIDE_NUMBER:
                if align_placeholder(placeholder, total_shape):
                    count += 1
        return count

    aligned = 0
    for master in prs.slide_masters:
        for layout in master.slide_layouts:
            for shape in layout.shapes:
                update_shape(shape)
            aligned += align_layout(layout)

    for slide in prs.slides:
        for shape in slide.shapes:
            update_shape(shape)
        total_shape = next(
            (
                sh
                for sh in slide.slide_layout.shapes
                if _is_page_total_name(sh.name)
            ),
            None,
        )
        if total_shape is None:
            continue
        for placeholder in slide.placeholders:
            if placeholder.placeholder_format.type == PP_PLACEHOLDER.SLIDE_NUMBER:
                if align_placeholder(placeholder, total_shape):
                    aligned += 1

    if (updated or aligned) and not dry_run:
        prs.save(str(pptx_path))
    print(
        f"{'would update' if dry_run else 'updated'} {updated} page-total "
        f"shape(s) and aligned {aligned} slide-number placeholder(s) in "
        f"{pptx_path} to /{effective_total}"
    )
    return 0


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("path", type=Path, help="Project directory or .pptx file")
    parser.add_argument("--total", type=int, default=None, help="Override the page total")
    parser.add_argument("--dry-run", action="store_true")
    args = parser.parse_args(argv)

    path: Path = args.path
    if path.is_dir():
        return sync_project(path, args.total, args.dry_run)
    if path.suffix.lower() == ".pptx":
        if not path.is_file():
            raise SystemExit(f"Not found: {path}")
        return sync_pptx(path, args.total, args.dry_run)
    raise SystemExit("Expected a project directory or a .pptx file")


if __name__ == "__main__":
    sys.exit(main())
