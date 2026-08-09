# Team PPT Master Templates

This directory is the team-maintained PPT Master template collection. Each template has its own independent workspace directory so future templates can be added without mixing their assets or design specifications.

## Available Templates

| Template | Workspace |
|---|---|
| FII 2026 Bright | [`fii-2026-bright/`](./fii-2026-bright/) |

Each child directory is an exact PPT Master workspace containing its own `templates/` and `images/` roots. Pass the child directory, not this collection directory, to PPT Master.

## Adding a Template

Create a new sibling directory using a stable lowercase ASCII ID:

```text
Other/ppt-master-template/<template-id>/
├── README.md
├── templates/
└── images/
```

Register the child workspace in `manifest/packs.json` with `kind: "template"`, `category: "Other"`, and its exact `sourcePath`. Keep each template's assets and validation contract self-contained.
