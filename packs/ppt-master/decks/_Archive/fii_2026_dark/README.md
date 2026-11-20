# FII 2026 Dark

Team-provided PPT Master `deck` workspace materialized from the company source template `繁體-FII PPT模板2026 暗.pptx`.

## Workspace Contract

```text
fii_2026_dark/
├── templates/
│   ├── design_spec.md
│   ├── 001_cover.svg
│   ├── 002_content.svg
│   ├── 003_content.svg
│   ├── 004_content.svg
│   ├── 005_ending.svg
│   ├── native_payloads.json.gz
│   └── template_execution/
└── images/
    └── image1.jpg ... image6.png
```

Keep `templates/` and `images/` together. The SVG prototypes use relative `../images/` references.

## Use Without Re-importing

Use this exact workspace root when creating a deck:

```text
<path-to-opencode-extension-packs>/Other/ppt-master-template/fii_2026_dark
```

PPT Master can consume this exact workspace directly. It does not need to import the original PPTX again.

## Library Installation Path

To make this workspace appear in PPT Master's library selector, copy the complete child directory to:

```text
<ppt-master-skill>/templates/decks/fii_2026_dark/
```

For the global Windows installation:

```text
C:\Users\<user>\.config\opencode\skills\ppt-master\templates\decks\fii_2026_dark
```

From the PPT Master Skill root, validate and register it:

```bash
python3 scripts/svg_quality_checker.py \
  "templates/decks/fii_2026_dark/templates" \
  --template-mode
python3 scripts/register_template.py fii_2026_dark --kind deck
```

On Windows, use `python` when `python3` is unavailable. Registration updates `templates/decks/decks_index.json`.

## Template Identity

- `deck_id`: `fii_2026_dark`
- `kind`: `deck`
- Canvas: `ppt169`, 1280 x 720
- Replication mode: `mirror`
- Native structure: `structured`
- Page roster: cover, three content references, ending
- Primary color: `#002F75`
- Accent color: `#D10034`

The workspace preserves the imported native structure payload and the five imported SVG prototypes. The import reported six non-blocking `stroke-omitted` warnings for an unsupported DrawingML miter limit; review `conversion-report.json` from the source import if the source is re-imported.

## Validation

Run the validator after changing SVGs or asset paths. A failure blocks use of the workspace until corrected.

The original source PPTX is intentionally not committed here. The repository stores the reusable materialized workspace, not the source package.

## Provenance

This is a team-maintained workspace published in `sawaichi9527/opencode-extension-packs`. It is independent from the upstream `hugohe3/ppt-master` release and uses the template ID `fii_2026_dark`.
