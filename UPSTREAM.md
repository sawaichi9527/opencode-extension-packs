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

## Initial selection

Concepts retained or rewritten:

- file toolkit;
- Git hosting workflow;
- reusable workflow skills;
- browser automation as an optional capability;
- OpenCode-native lean code review.

Concepts intentionally not imported in the initial baseline:

- OpenCode Zen-specific model setup;
- unofficial NotebookLM browser automation;
- Google Apps Script;
- Supabase;
- Groq cloud APIs;
- Netlify deployment;
- Firebase;
- non-OpenCode plugin and hook frameworks.

These may be reconsidered later as separate packs when a concrete OpenCode project needs them.
