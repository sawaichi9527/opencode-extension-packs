# Changelog

## 0.2.0 - OpenCode clarification and test triage

- Added `/grill-me` as an OpenCode-native Markdown Custom Command using the Plan Agent.
- Added `test-failure-triage` for Python, UART／TTY, PCAP, environment, timing, and DUT failure analysis.
- Refocused `swqa-automation` on Python, raw Console evidence, packet capture, and traceable Verdicts without making Web UI the default.
- Preserved large raw artifacts while limiting conversation context to relevant ranges, filters, frames, and error sections.
- Separated failure Root Cause investigation from `lean-code-review` diff simplification.
- Adapted selected concepts from `mattpocock/skills` and `obra/superpowers` without installing their upstream Skills or plugins.
- Explicitly excluded OpenSpec-tw, SpecTest, and the full Superpowers methodology from repository dependencies.

## 0.1.1 - OpenCode lean review baseline

- Added `lean-code-review` as an OpenCode-native, on-demand Skill.
- Adapted small-team coding principles from `andrej-karpathy-skills` and selected Ponytail review concepts.
- Explicitly excluded Ponytail plugin runtime, hooks, mode state, benchmarks, and cross-agent adapters.
- Documented that the review does not replace correctness, security, or SWQA test validation.

## 0.1.0 - Initial planning baseline

- Split optional capabilities from OpenCode Essential Core.
- Added initial SWQA, Forgejo, GitHub, file toolkit, and browser automation packs.
- Excluded cloud-specific packs until a real use case is confirmed.
