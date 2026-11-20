# Archify Compatibility

## Upstream Contract

- Release: `v3.2.0`
- Repository: `tt-a1i/archify`
- Runtime: Node.js 18 or newer
- Diagram types: architecture, workflow, sequence, dataflow, lifecycle, erd, class, tree, timeline, waterfall, explainer
- Installation: `npx skills add tt-a1i/archify -g -a opencode --copy -y`, or the `archify.zip` release archive
- The workflow requires an agent that can read/write files, execute commands, and sustain multi-turn conversation

The team must validate the selected Archify release on the supported Windows, WSL, Ubuntu, or macOS environment before treating it as a standard workflow.

## Installation Checks

1. Confirm Node.js 18 or newer is available (`node --version`).
2. Confirm the selected Skill directory contains `SKILL.md`, `skill-release.json`, and `bin/archify.mjs`.
3. Run `node <skill-dir>/bin/archify.mjs doctor`; every renderer and runtime check must report `[ok]`.
4. Verify `skill-release.json` reports `version: 3.2.0` and `channel: stable`.
5. Render a deterministic demo (`archify demo <dir>`) and run `archify check <output.html>`; the composition must report zero errors.
6. Validate at least one diagram input (`archify validate <type> <input.json>`) for a type used by the team.
7. Confirm the generated HTML opens in a browser and exports cleanly before treating Archify as a standard workflow.
8. Do not modify provider credentials, model API keys, or `.env` secrets in the Extension Packs repository.

## Upgrade Notes (2.x to 3.x)

- Guided/story views and ordinary share-card exports were removed in 3.0; `meta.views` is accepted but ignored.
- Existing schema-v1 diagram inputs remain supported; converting existing files is not required.
- Lifecycle v2 (lane/column layout) is optional; schema-v1 lifecycle geometry is still accepted.
- 3.2 adds the `explainer` type (one-page answers) with its own writing checks (`--writing warn|strict|off`).
- Automated checks do not replace visual review; complex diagrams may still need authoring repairs.

## Boundaries

- Archify can use model APIs and optional image/search providers; provider credentials are environment-specific.
- Output quality depends on the selected model and source material.
- The Pack only documents installation; it does not automatically download npm packages or modify provider configuration.
- Updates must be reviewed against the upstream release before re-validating.
