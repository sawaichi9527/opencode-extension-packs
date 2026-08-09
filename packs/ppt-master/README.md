# PPT Master

This pack is **Optional**. It is listed for user selection and is not installed by default.

PPT Master is an upstream presentation workflow Skill, not source code maintained by this repository.

## Official Source

- Repository: <https://github.com/hugohe3/ppt-master>
- Release: `v4.5.0`
- License: MIT
- Upstream documentation: <https://github.com/hugohe3/ppt-master#quick-start>

This repository does not fork or vendor PPT Master. Install the Skill and its dependencies from the official source after selecting this Pack.

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

Restart OpenCode after the Skill is installed. Use it only when the task requires presentation generation, template filling, or PPTX enhancement.

## Update Policy

Review the upstream release before updating. Update the `release` field in `manifest/packs.json` and this document together, then re-run the Python dependency and workflow smoke checks.
