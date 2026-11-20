# Archify

This pack is **Optional**. It is listed for user selection and is not installed by default.

Archify is an upstream Agent Skill for producing validated diagrams as self-contained HTML:
architecture, workflow, sequence, data-flow, lifecycle, ERD, class, tree, timeline, and
waterfall, plus one-page "explainer" answers that combine prose, charts, and native diagrams.
It is an upstream Skill, not source code maintained by this repository.

## Official Source

- Repository: <https://github.com/tt-a1i/archify>
- Release: `v3.2.0`
- License: MIT
- Runtime: Node.js 18 or newer

This repository does not fork or vendor Archify. Install the Skill from the official source
after selecting this pack.

## Installation

Install the upstream Skill globally using the Skills CLI:

```bash
npx skills add tt-a1i/archify -g -a opencode --copy -y
```

Or download `archify.zip` from the upstream `v3.2.0` release and copy its `archify/` folder to
the global skill directory (`~/.config/opencode/skills/archify` or `~/.agents/skills/archify`).

Verify the installation with the packaged CLI:

```bash
node <skill-dir>/bin/archify.mjs doctor
node <skill-dir>/bin/archify.mjs demo ./archify-demo
node <skill-dir>/bin/archify.mjs check ./archify-demo/archify-demo.html
```

`doctor` must report every renderer and runtime check as `[ok]` before Archify is treated as a
standard workflow. Then open the generated HTML in a browser to confirm the viewer renders.

## Upgrading From 2.x

Archify 3.x is a major release. Guided/story views and ordinary share-card exports were removed;
existing schema-v1 diagram inputs remain supported and already generated HTML keeps its embedded
viewer. Review the upstream `CHANGELOG.md` ("Upgrading from 2.x") before replacing an older
installation, and re-check existing automation that referenced the removed views.

## Update Policy

Review the upstream release before updating. Update the `release` field in `manifest/packs.json`
and this document together, then re-run `doctor`, a demo render, and one `check`. For major
releases, also re-read the upstream upgrade notes before updating team documentation.
