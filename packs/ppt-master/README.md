# PPT Master

This pack is **Optional**. It is listed for user selection and is not installed by default.

PPT Master is an upstream presentation workflow Skill, not source code maintained by this repository.

## Official Source

- Repository: <https://github.com/hugohe3/ppt-master>
- Release: `v6.10.0`
- License: MIT
- Runtime: Python 3.10–3.14 (upstream supports up to 3.14)
- Upstream documentation: <https://github.com/hugohe3/ppt-master#quick-start>

This repository does not fork or vendor PPT Master. Install the Skill and its dependencies from the official source after selecting this Pack.

## FII 2026 Deck Templates

This Pack bundles three team-owned FII 2026 deck templates — `fii_2026_zh_tw`,
`fii_2026_zh_cn`, and `fii_2026_en` — under [decks/](decks/). They share one visual
system (1280×720, primary `#173367`) and differ only in language and font stack.
After installing the PPT Master Skill, copy the decks you need into the installed
Skill's `templates/decks/` or `templates/local/decks/` directory so every team
member uses the same brand templates. See [decks/README.md](decks/README.md) for
the copy commands and index merge notes. The retired `fii_2026_bright` and
`fii_2026_dark` workspaces are kept under `decks/_Archive/` and are no longer
registered.

### Page totals (current/total footer)

PowerPoint has no total-slides field. The FII decks keep `/N` on the
`Title and Content` layout and keep it in sync with three helpers:

- [addins/FII_PageTotals.ppam](addins/FII_PageTotals.ppam) — PowerPoint add-in
  that updates the footer when a slide is added, saved, or presented. Install
  with [addins/install_page_totals.ps1](addins/install_page_totals.ps1); see
  [addins/README.md](addins/README.md).
- [tools/sync_page_totals.py](tools/sync_page_totals.py) — CLI sync for an SVG
  project or an existing `.pptx`; use it after export when running the upstream
  (unpatched) `svg_to_pptx.py`.
- The installed `svg_to_pptx.py` used by this team carries the page-total
  export patch, so generated decks already contain the real page count.

AI workflows that reference this Pack should run the sync helper after a
post-edit export, or rely on the add-in for interactive PowerPoint edits.

## Installation

The upstream documentation supports the Skills CLI path:

```bash
npx skills add hugohe3/ppt-master -g -a opencode --copy -y
```

PPT Master also documents Git clone and release ZIP installation. Those paths are useful when the Python workflow and example projects are required rather than only the Skill files.

After installation, install the Python dependencies from the installed PPT Master directory:

```bash
pip install -r requirements.txt
```

On Windows, follow the upstream Windows installation guide and use `python` when `python3` is unavailable.

From 6.7.2 onward, Skill-only installations create new projects in the current working folder
(`projects/<name>_<format>_<YYYYMMDD>`) instead of inside the installed Skill directory. From
6.10.0, templates registered through Create Template go to the Git-ignored
`templates/local/<kind>/` layer, and `update_repo.py` migrates older personal entries out of the
tracked `*_index.json` files.

Verify the installation from any working folder:

```bash
python scripts/project_manager.py init smoke --format ppt169
python scripts/svg_to_pptx.py --help
```

`init` must create the project under the current working folder, not inside the Skill directory.

Restart OpenCode after the Skill is installed. Use it only when the task requires presentation generation, template filling, or PPTX enhancement.

## Update Policy

Review the upstream release before updating. Update the `release` field in `manifest/packs.json` and this document together, then re-run the Python dependency and workflow smoke checks.
