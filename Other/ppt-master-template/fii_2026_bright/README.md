# FII 2026 Bright

Team-provided PPT Master `deck` workspace for FII-branded internal reports, customer proposals, project briefings, and annual summaries.

## Workspace Contract

This directory is a complete explicit PPT Master workspace:

```text
fii_2026_bright/
├── templates/
│   ├── design_spec.md
│   └── *.svg
└── images/
    └── image1.jpg ... image5.png
```

The SVG files reference sibling assets with `../images/<filename>`. Keep the `templates/` and `images/` directories together when copying or selecting the workspace.

## Use Without Re-importing

After cloning or downloading `opencode-extension-packs`, give PPT Master the exact workspace root when creating a deck:

```text
Use this exact PPT Master template workspace:
<path-to-opencode-extension-packs>/Other/ppt-master-template/fii_2026_bright
```

PPT Master can consume an exact workspace root as an explicit template input. It does not need to run the template-import workflow again. The workspace can also be copied into a project-local `templates/` and `images/` pair when the project must retain a local copy.

Do not move only `templates/` or only `images/`; the relative SVG asset references must remain valid.

## Library Installation Path

The Extension Packs copy is the team source. It is not automatically scanned by PPT Master. To make this template appear in PPT Master's library selector, install the complete child workspace at:

```text
<ppt-master-skill>/templates/decks/fii_2026_bright/
```

For the current global Windows installation, `<ppt-master-skill>` is:

```text
C:\Users\<user>\.config\opencode\skills\ppt-master
```

The resulting installed path is:

```text
C:\Users\<user>\.config\opencode\skills\ppt-master\templates\decks\fii_2026_bright
```

Copy the child workspace as a whole, change directory to the PPT Master Skill root, then validate and register it:

```bash
cd <ppt-master-skill>
python3 scripts/svg_quality_checker.py \
  "templates/decks/fii_2026_bright/templates" \
  --template-mode
python3 scripts/register_template.py \
  fii_2026_bright --kind deck
```

On Windows, run the same commands with `python` when `python3` is unavailable. Registration updates `templates/decks/decks_index.json`; only then will the workspace be available through PPT Master's library discovery and selector.

Do not overwrite an existing installed workspace without reviewing the diff. The `fii_2026_bright` directory in this repository and the installed library directory should remain independent copies with the same workspace contract.

## Template Identity

- `deck_id`: `fii_2026_bright`
- `kind`: `deck`
- Canvas: `ppt169`, 1280 x 720
- Replication mode: `fidelity`
- Native structure: `structured`
- Page roster: cover, chapter, standard content, image content, ending
- Primary color: `#002F75`

The design specification contains the complete color, typography, placeholder, page roster, and asset contract. Use the official FII brand and asset policy when producing new public material.

## Validation

Run the validator from the installed PPT Master Skill directory before using a changed copy:

```bash
cd <ppt-master-skill>
python3 scripts/svg_quality_checker.py \
  "templates/decks/fii_2026_bright/templates" \
  --template-mode
```

On Windows, use `python` when `python3` is unavailable. A validation failure blocks use of the workspace until the source SVG or asset reference is corrected.

## Provenance

This is a team-maintained workspace published in `sawaichi9527/opencode-extension-packs`. It is not part of the upstream `hugohe3/ppt-master` repository and must be versioned with the Extension Packs repository independently from the upstream PPT Master release.
