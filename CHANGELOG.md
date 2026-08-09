# Changelog

## 0.2.1 - hybrid-workflow naming

- Renamed the Skill from `other-hybrid-workflow` to `hybrid-workflow`.
- Kept `other` as the manifest category and `recommended` as the installation tier.
- Updated the Skill path to `skills/hybrid-workflow` while retaining Pack documentation under `packs/other/hybrid-workflow`.

## 0.2.0 - Generic and team-specific hybrid backends

- Added generic `workflow_local_builder` for users whose local hardware is unknown.
- Documented `workflow_local_builder_aeon` as the team 28500 `DGX Spark/aeon` preset.
- Renamed `workflow_cheap_builder` to `workflow_cloud_cheap_builder`.
- Added setup guidance to ask users to select an existing local or cloud provider/model.
- Added `/grill-me` as an OpenCode-native Markdown Custom Command using the Plan Agent.
- Added `test-failure-triage` for Python, UART／TTY, PCAP, environment, timing, and DUT failure analysis.
- Refocused `swqa-automation` on Python, raw Console evidence, packet capture, and traceable Verdicts without making Web UI the default.
- Preserved large raw artifacts while limiting conversation context to relevant ranges, filters, frames, and error sections.
- Separated failure Root Cause investigation from `lean-code-review` diff simplification.
- Adapted selected concepts from `mattpocock/skills` and `obra/superpowers` without installing their upstream Skills or plugins.
- Explicitly excluded OpenSpec-tw, SpecTest, and the full Superpowers methodology from repository dependencies.

## 0.1.0 - hybrid-workflow baseline

- Added the `hybrid-workflow` workflow in the `other` category.
- Added optional `workflow_local_builder`, `workflow_local_builder_aeon`, and `workflow_cloud_cheap_builder` backend guidance.
- Preserved OpenCode native Plan and Build routing unless the user confirms delegation.
- Standardized namespaced workflow Agent IDs to avoid menu confusion with built-in agents.
- Defined single-task, single-dispatch, linear Builder execution.

## 0.0.1 - Versioned baseline

- Added the first versioned Extension Packs baseline.
- Added `manifest/packs.json` with Default, Recommended, and Optional tiers.
- Promoted `file-toolkit` and `browser-automation` to Recommended packs with audience metadata.
- Added Optional PPT Master and Playwright MCP integrations with official-source installation and compatibility guidance.
- Added the Optional Token Usage / Observability pack for upstream TokenScope installation guidance.
- Documented that external plugins are installed from their original repositories or npm packages and are not vendored here.

## 0.1.1 - OpenCode lean review baseline

- Added `lean-code-review` as an OpenCode-native, on-demand Skill.
- Adapted small-team coding principles from `andrej-karpathy-skills` and selected Ponytail review concepts.
- Explicitly excluded Ponytail plugin runtime, hooks, mode state, benchmarks, and cross-agent adapters.
- Documented that the review does not replace correctness, security, or SWQA test validation.

## 0.1.0 - Initial planning baseline

- Split optional capabilities from OpenCode Essential Core.
- Added initial SWQA, Forgejo, GitHub, file toolkit, and browser automation packs.
- Excluded cloud-specific packs until a real use case is confirmed.
