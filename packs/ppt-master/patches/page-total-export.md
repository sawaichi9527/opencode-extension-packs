# Page-total export patch

This is a **team-local extension**, not part of upstream PPT Master
(`hugohe3/ppt-master` v6.10.0). It makes `svg_to_pptx.py` write the real total
page count into the FII `layout-page-total` shape at export time.

Upstream only converts a whole page-number text into a `slidenum` field; it has
no total-slides field and leaves `/N` as literal text.

## File

`scripts/svg_to_pptx/pptx_package/builder.py`

## 1. Add the helper

Place it next to `_convert_page_number_texts_to_fields`:

```python
def _sync_page_total_texts(extract_dir: Path, slide_count: int, *, verbose: bool = False) -> int:
    """Set literal page-total text to the exported slide count."""
    updated = 0
    page_total_re = re.compile(r"page[-_ ]?total", re.IGNORECASE)
    parts = sorted((extract_dir / "ppt" / "slideLayouts").glob("slideLayout*.xml"))
    parts += sorted((extract_dir / "ppt" / "slides").glob("slide*.xml"))
    value = f"/{slide_count}"
    for part in parts:
        try:
            tree = ET.parse(part)
        except (OSError, ET.ParseError):
            continue
        changed = False
        for shape in tree.getroot().iter(f"{{{PML_NS}}}sp"):
            cnv = shape.find(f"{{{PML_NS}}}nvSpPr/{{{PML_NS}}}cNvPr")
            name = cnv.attrib.get("name", "") if cnv is not None else ""
            if not page_total_re.search(name):
                continue
            tx_body = shape.find(f"{{{PML_NS}}}txBody")
            if tx_body is None:
                continue
            first_text = None
            for run in list(tx_body.iter(f"{{{DML_NS}}}r")):
                text = run.find(f"{{{DML_NS}}}t")
                if text is None:
                    continue
                if first_text is None:
                    first_text = text
                    text.text = value
                    changed = True
                else:
                    parent = run.getparent()
                    if parent is not None:
                        parent.remove(run)
                        changed = True
        if changed:
            _write_xml_tree(part, tree)
            updated += 1
    if verbose and updated:
        print(f"  Page totals: synced {updated} part(s) to /{slide_count}")
    return updated
```

## 2. Call it before repackaging

Find the `# Repackage PPTX to a temporary file first.` comment near the end of
the builder and insert immediately before it:

```python
        if use_native_shapes and success_count == len(svg_files):
            _sync_page_total_texts(extract_dir, public_slide_count, verbose=verbose)
```

## 3. Verify

```powershell
python scripts/svg_to_pptx.py <project> --quick-generate --no-notes -o <out>.pptx
```

The log should include:

```text
  Page totals: synced 1 part(s) to /4
```

Then inspect:

```powershell
python -c "from pptx import Presentation; p=Presentation(r'<out>.pptx'); print([sh.text_frame.text for l in p.slide_masters[0].slide_layouts for sh in l.shapes if 'page-total' in sh.name])"
```

Expected: `['/4']` for a four-page deck.

After a PPT Master update, reapply this patch or run
[`../tools/sync_page_totals.py`](../tools/sync_page_totals.py) on each exported
`.pptx`.
