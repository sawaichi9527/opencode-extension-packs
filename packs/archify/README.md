# Archify

This pack is **Optional**. It is listed for user selection and is not installed by default.

Archify is an upstream Agent Skill for producing architecture, workflow, sequence, data-flow,
and lifecycle diagrams in self-contained HTML with motion and crisp export. It is an upstream
Skill, not source code maintained by this repository.

## Official Source

- Repository: <https://github.com/tt-a1i/archify>
- Release: `v2.16.0`
- License: MIT
- Runtime: Node.js 18 or newer (uses `npx` to install and run the Skill)

This repository does not fork or vendor Archify. Install the Skill and its dependencies from the
official source after selecting this pack.

## Installation

Install the upstream Skill globally using the Skills CLI:

```bash
npx skills add tt-a1i/archify -g
```

Add `-a opencode` to scope the install to the OpenCode skills directory, and use `--copy`
to copy the Skill files locally when the agent configuration requires it:

```bash
npx skills add tt-a1i/archify -g -a opencode --copy
```

Or install from the upstream release archive with the Skilled CLI:

```bash
npx skills add tt-a1i/archify -g -a opencode --copy -y
```

After installation, verify the Skill can render a small non-sensitive diagram and that the output
HTML opens in a browser before treating Archify as a standard workflow. Restart OpenCode after the
Skill is installed draw flows, sequences, data-flow, and lifecycle diagrams only when a task
requires them.

## Update Policy

Review the upstream release before updating. Update the `release` field in `manifest/packs.json`
and this document together, then re-run the repository validation checks.
