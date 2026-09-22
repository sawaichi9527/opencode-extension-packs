# Changelog

## 2.0.12 - OpenCode v2-only realignment and token-usage removal

- Adopted the Essential Core versioning scheme: the version now equals the OpenCode release this pack was validated against (2.0.12); a plain OpenCode release does not bump this pack by itself.
- Removed the `token-usage` pack and deleted `packs/token-usage/`: upstream TokenScope targets OpenCode v1 (its accuracy contracts were verified against OpenCode v1.17.18) and its install guide uses the legacy singular `command/` directory, so it does not fit the OpenCode v2.x.x windowed release. The historical `0.0.1` changelog entry is kept as-is.
- Fixed README command install paths from the legacy singular `command/` directories to `commands/` (all six occurrences), matching `docs/PACK-GUIDE.md` rule 8.
- Converted the three hybrid-workflow agent templates from the v1 `permission` map (`task`, `bash`) to the v2 `permissions` rule list with `action`/`resource`/`effect` (`subagent`, `shell`), per the v2 Permissions and Agents documentation.
- Rewrote the `playwright-mcp` and `codebase-memory-mcp` install guides to the v2 `mcp.servers` configuration shape; removed `enabled` fields (v2 connects automatically and uses `disabled` instead) and changed `/mcp` to `/mcps`.
- Corrected `codebase-memory-mcp` docs against the current upstream README: 162 languages, `trace_call_path` tool name, graph UI bundled by default (`CBM_VARIANT=ui` is obsolete upstream).
- Refreshed external pins: `ppt-master` `v6.4.0` → `v6.6.0`, `@playwright/mcp` `0.0.81` → `0.0.82`. `archify` (`v2.16.0`) and `codebase-memory-mcp` (`0.11.0`) were already current at review time.
- Kept `pwsh7` on PowerShell 7.4.6: the packaged zip, published hashes, and wrapper exe are bound to that release; upstream latest (7.6.6) is recorded in `packs/pwsh7/README.md` as a pending upgrade requiring asset re-packaging.
- Replaced the v1 tool name `bash` with `shell` in `packs/pwsh7/README.md` prose.
- Aligned all eight Skill `description` fields with `docs/PACK-GUIDE.md` rule 2: six descriptions only stated purpose and now also state an explicit trigger situation ("當使用者…時載入"); `test-failure-triage` and `lean-code-review` already complied and were left unchanged. The `github-integration` and `forgejo-integration` descriptions were then expanded to name the concrete remote operations (clone／fetch／pull／push, remote setup, repository creation) instead of the generic "operate the remote" wording.
- Removed the `hybrid-workflow` Pack and the `/other-working-flow` command: OpenCode v2 already ships a native multi-agent dispatch framework (primary/subagent agent types, the Task tool, description-based auto-invocation and `@` mentions, child sessions, `subagent` permission gating, and command-level `agent`/`subtask` dispatch), so the Pack duplicated built-in behavior and ran against `docs/PACK-GUIDE.md` rule 7. Deleted `skills/hybrid-workflow/`, `commands/other-working-flow.md`, and `packs/other/hybrid-workflow/`; removed the manifest entry and replaced the README section with native-delegation guidance plus a delegation-permission snippet that reproduces the old "confirm before delegating" behavior. The now-unused `Workflow Pack` section was also dropped from `docs/PACK-GUIDE.md`; historical changelog entries are kept.
- Added the `local-llm-dispatch-policy` Skill summarising the local-compute dispatch rules formerly covered by the removed `hybrid-workflow`/`other-working-flow`: one request one decision and linear dispatch, the Orchestrator + heavy-Builder split, the 3-strike hand-off, and the c1/c2 escalation (goal-scoped cloud fallback when the Orchestrator is local; direct takeover when it is cloud). It only states dispatch principles and relies on the native v2 subagent/`permissions` framework.
- Corrected the README native-delegation example from the v1 `permission` map (`task`) to the v2 `permissions` rule list (`subagent`) and renamed its example agent to `heavy-builder`.
- Reclassified `local-llm-dispatch-policy` (skill) and `pwsh7` (tooling) from `recommended` to `optional`: both depend on a specific local setup (configured local LLM providers; the packaged PowerShell 7.4.6 wrapper), so they are listed but not confirmed by default. README tier tables, the status line, and the Essential Core relationship tree were updated accordingly (14 packs: Default 1 / Recommended 5 / Optional 8).
- Expanded the `/grill-me` command with the upstream `grill-me` concepts that were previously missing: stop and verify by prototype when a question cannot be settled by talking (ungrillable), the user owns scope and is expected to push back, and the interview ends when nothing is left silently assumed rather than after a fixed number of questions. The no-file rule now also names plan/spec files. The adapted concepts are recorded in `UPSTREAM.md`.
- Updated `handoff.md` status (HEAD, three-way sync state) and bumped `VERSION` plus manifest version to 2.0.12.

## 0.2.6 - pwsh-utf8-wrapper quote-preserving fix

- Fixed a quote-preservation bug in `pwsh7/pwsh-utf8-wrapper.cs`: the old string-based rebuild dropped embedded double quotes, corrupting arguments passed to `pwsh` by `opencode` (e.g. `-Command "..."`).
- Rebuilt `pwsh-utf8-wrapper.exe` from the fixed source; release asset is now 5,120 bytes with SHA256 `906AA017CDE4F36DB2BC5D3C38C976E76035E0E40703DB30D81F4F5F7F9FB8F0`.
- Replaced the two deprecated 4,608-byte wrapper assets (`pwsh-utf8-wrapper.exe` and `pwsh-utf8-wrapper-v2.exe`) in the `pwsh7-v7.4.6` release with the single fixed prebuilt exe.
- Updated `packs/pwsh7/README.md` with the new asset hash and resolution note.
- Updated the README status marker and the Optional Pack summary to `v0.2.6`.
- Bumped manifest version to 0.2.6.

## 0.2.5 - archify external skill pack registration

- Registered the upstream `archify` external skill pack in `manifest/packs.json` with the `external-skill` kind and `optional` tier.
- Pointed documentation and compatibility to `packs/archify/README.md` and `packs/archify/compatibility.md`.
- Pinned `archify` to release `v2.16.0` from the official `tt-a1i/archify` source.
- Updated the README status marker and the Optional Pack summary to `v0.2.5`.
- Bumped manifest version to 0.2.5.

## 0.2.4 - pwsh7 pack registration and External release pin refresh

- Registered the self-hosted `pwsh7` pack in `manifest/packs.json` (Prebuilt `pwsh7/pwsh-utf8-wrapper.exe` + PowerShell 7.4.6 deployment guidance for Windows UTF-8 shells).
- Wired `pwsh7` Kind to `tooling` and pointed documentation to `packs/pwsh7/README.md`.
- Updated the `ppt-master` pin from `v4.5.0` to `v6.4.0`.
- Updated the `playwright-mcp` package pin from `@playwright/mcp@0.0.79` to `@playwright/mcp@0.0.81`.
- Pinned `codebase-memory-mcp` to `codebase-memory-mcp@0.11.0` and aligned the pack documentation.
- Updated the README status marker and the Optional Pack summary to `v0.2.4`.
- Bumped manifest version to 0.2.4.

## 0.2.3 - codebase-memory-mcp pack registration

- Registered the upstream `codebase-memory-mcp` external MCP pack via the `external-mcp` kind.
- Pointed documentation and compatibility to `packs/codebase-memory-mcp/README.md` and `compatibility.md`.
- Bumped manifest version to 0.2.3.

## 0.2.2 - FII 2026 ppt-master decks

- Bundled team-owned FII 2026 deck templates (`fii_2026_bright` and `fii_2026_dark`) under `packs/ppt-master/decks/`.
- Documented how to copy the decks into an installed PPT Master Skill and merge the deck index.
- Added deck index and deck README consistent with the PPT Master deck workspace contract.

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