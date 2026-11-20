# PPT Master Compatibility

## Upstream Contract

- Release: `v6.10.0`
- Repository: `hugohe3/ppt-master`
- Runtime: Python 3.10–3.14 (upstream caps support at 3.14)
- Installation may require `pip install -r requirements.txt`
- The workflow requires an agent that can read/write files, execute commands, and sustain multi-turn conversation

The team must validate the selected PPT Master release on the supported Windows, WSL, Ubuntu, or macOS environment before treating it as a standard workflow.

## Installation Checks

1. Confirm Python 3.10–3.14 and pip are available (`python --version`).
2. Confirm the selected Skill directory contains `SKILL.md`, `scripts/project_manager.py`, and `requirements.txt`.
3. Confirm the Skill frontmatter reports `metadata.version: 6.10.0`.
4. Install and verify `requirements.txt`; at minimum `python-pptx`, `PyYAML`, `Pillow`, and `lxml` must import.
5. Run `python scripts/project_manager.py init smoke --format ppt169` from a scratch folder and confirm the project is created under that folder (not inside the Skill directory).
6. Run `python scripts/project_manager.py scaffold-spec|scaffold-lock` on the new project, then `python scripts/svg_to_pptx.py --help` and `python scripts/source_to_md.py --help` to confirm the main entry points load.
7. Run a small non-sensitive PPTX smoke test or inspect the upstream deterministic example workflow. A scaffolded project fails `validate` until its `[fill]` placeholders are replaced; that is expected.
8. If using the bundled FII 2026 decks, copy the required language workspace (`fii_2026_zh_tw`, `fii_2026_zh_cn`, or `fii_2026_en`) from `packs/ppt-master/decks/` into the installed Skill's `templates/decks/` or `templates/local/decks/`, then confirm the copied IDs appear in the merged `decks_index.json`. `decks/_Archive/` holds the retired bright/dark workspaces and is not registered.
9. Do not store provider API keys or `.env` secrets in the Extension Packs repository.

## Behaviour Changes Since 6.6.0

- **Project location (6.7.2):** Skill-only installations create projects in the working folder instead of `.agents/projects/` or the plugin cache.
- **Personal templates (6.10.0):** Create Template writes to the Git-ignored `templates/local/<kind>/` layer; `update_repo.py` migrates older personal entries before `git pull --ff-only`.
- **Image backends:** OpenAI defaults to `gpt-image-2.6-sunburst`; Volcengine supports Seedream 5.0 and defaults to the Ark gateway. Both require provider credentials and are environment-specific.
- **Image to PPTX (6.9.0+):** OCR text evidence, text-removed bases, and checked image layers use `image_ocr.py` / `image_layers.py`; a vision host or `image_gen.py --reference-image` backend is required.
- **Edit Native round trip:** Office Math, icon fills, speaker-notes lines such as `#1 ...`, and non-UTF-8 source decoding received targeted fixes; re-verify existing custom workflows that depend on them.

## Boundaries

- PPT Master can use model APIs and optional image/search providers; provider credentials are environment-specific.
- Output quality depends on the selected model and source material.
- The full Git clone, release ZIP, and Skill-only installation paths have different available examples and update behavior.
- The Pack only documents installation; it does not automatically download Python packages or modify provider configuration.
