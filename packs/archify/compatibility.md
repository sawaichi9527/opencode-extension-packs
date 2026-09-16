# Archify Compatibility

## Upstream Contract

- Release: `v2.16.0`
- Repository: `tt-a1i/archify`
- Runtime: Node.js (used via `npx`) per upstream Quick Start
- Installation: `npx skills add tt-a1i/archify -g`
- The workflow requires an agent that can read/write files, execute commands, and sustain multi-turn conversation

The team must validate the selected Archify release on the supported Windows, WSL, Ubuntu, or macOS environment before treating it as a standard workflow.

## Installation Checks

1. Confirm Node.js and npx are available.
2. Confirm the selected Skill directory contains the Skill manifest and SKILL.md from the upstream installation path.
3. Confirm the upstream attribution/integrity check can run successfully.
4. Verify the selected upstream release tag matches the release recorded here (`v2.16.0`).
5. Run a small non-sensitive Archify smoke test or render a deterministic example diagram that produces self-contained HTML.
6. Confirm the generated HTML opens in a browser and exports cleanly before treating Archify as a standard workflow.
7. Do not modify provider credentials, model API keys, or `.env` secrets in the Extension Packs repository.

## Boundaries

- Archify can use model APIs and optional image/search providers; provider credentials are environment-specific.
- Output quality depends on the selected model and source material.
- The Pack only documents installation; it does not automatically download npm packages or modify provider configuration.
- Updates must be reviewed against the upstream release before re-validating.
