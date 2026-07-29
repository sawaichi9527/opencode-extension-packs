# Upstream and Selection Notes

## Primary upstream

- Project: `mathruffian-dot/opencode-lazy-packs`
- Source: `https://github.com/mathruffian-dot/opencode-lazy-packs`
- License: MIT
- Original copyright: 2026 三師爸 Sense Bar

## Additional references

### `multica-ai/andrej-karpathy-skills`

- License: MIT
- Concepts adapted for OpenCode:
  - think before coding;
  - simplicity first;
  - surgical changes;
  - goal-driven verification.

The always-on principles are kept in the Essential Core `AGENTS.md` template rather than copied as another overlapping Skill.

### `DietrichGebert/ponytail`

- License: MIT
- Concepts adapted into `lean-code-review`:
  - review the current diff for unnecessary complexity;
  - prefer reuse, standard libraries, and native platform features;
  - identify speculative abstractions and unnecessary dependencies;
  - report findings without automatically applying changes.

The Ponytail npm/OpenCode plugin, lifecycle hooks, `lite/full/ultra` modes, state files, benchmark suite, and cross-agent adapters are intentionally not imported.

### `mattpocock/skills`

- License: MIT
- Concepts adapted into the OpenCode-native `/grill-me` command:
  - investigate repository facts before asking the user;
  - ask one decision question at a time;
  - provide a recommended answer with each question;
  - do not begin implementation until shared understanding is confirmed.

The upstream `grill-me`, `grilling`, `grill-with-docs`, domain-modeling Skills, `CONTEXT.md`, and ADR workflow are not installed or copied. The command is independently rewritten for OpenCode and small SWQA projects.

### `obra/superpowers`

- License: MIT
- Concepts selectively adapted into `test-failure-triage`:
  - investigate Root Cause before proposing a fix;
  - reproduce consistently and gather evidence across component boundaries;
  - test one explicit hypothesis with the smallest useful experiment;
  - fix the source and verify with fresh evidence.

The Superpowers plugin, bootstrap context injection, mandatory skill invocation, worktrees, subagent workflows, automatic task framework, and mandatory TDD process are intentionally not imported.

## Initial selection

Concepts retained or rewritten:

- file toolkit;
- Git hosting workflow;
- reusable OpenCode Skills;
- OpenCode Markdown Custom Commands;
- browser automation as an optional capability;
- Python／UART／PCAP-oriented SWQA workflow;
- test failure triage;
- lean code review.

Concepts intentionally not imported in the baseline:

- OpenSpec-tw or another specification CLI;
- SpecTest and Playwright-centered test-spec workflows;
- Superpowers plugin and full development methodology;
- OpenCode Zen-specific model setup;
- unofficial NotebookLM browser automation;
- Google Apps Script;
- Supabase;
- Groq cloud APIs;
- Netlify deployment;
- Firebase;
- non-OpenCode plugin and hook frameworks.

Users may install external tools separately after deploying Essential Core and selected Extension Packs. They are not dependencies of this repository.
