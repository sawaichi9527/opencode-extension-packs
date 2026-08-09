# PPT Master Compatibility

## Upstream Contract

- Release: `v4.5.0`
- Repository: `hugohe3/ppt-master`
- Runtime: Python 3.10 or newer according to upstream Quick Start
- Installation may require `pip install -r requirements.txt`
- The workflow requires an agent that can read/write files, execute commands, and sustain multi-turn conversation

The team must validate the selected PPT Master release on the supported Windows, WSL, Ubuntu, or macOS environment before treating it as a standard workflow.

## Installation Checks

1. Confirm Python 3.10 or newer and pip are available.
2. Confirm the selected Skill directory contains `skills/ppt-master/SKILL.md`.
3. Confirm the upstream attribution/integrity check can run successfully.
4. Install and verify `requirements.txt` from the selected upstream installation path.
5. Run a small non-sensitive PPTX smoke test or inspect the upstream deterministic example workflow.
6. Do not store provider API keys or `.env` secrets in the Extension Packs repository.

## Boundaries

- PPT Master can use model APIs and optional image/search providers; provider credentials are environment-specific.
- Output quality depends on the selected model and source material.
- The full Git clone, release ZIP, and Skill-only installation paths have different available examples and update behavior.
- The Pack only documents installation; it does not automatically download Python packages or modify provider configuration.
