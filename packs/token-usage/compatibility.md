# TokenScope Compatibility

## Upstream Contract

- Package: `@ramtinj95/opencode-tokenscope@1.8.1`
- Required plugin API: `@opencode-ai/plugin >=1.1.48`
- Upstream verification noted in release documentation: OpenCode `v1.17.18`
- Runtime requirements: Node.js and npm for npm installation
- TokenScope is an OpenCode plugin and tool, not a standalone CLI

The team must validate the pinned release against the OpenCode version distributed by `opencode-essential-core`; do not infer compatibility from the package version alone.

## Installation Checks

1. Confirm Node.js and npm are available.
2. Confirm the pinned npm package resolves to `1.8.1`.
3. Confirm the plugin entry exists in the active `opencode.jsonc` without removing existing entries.
4. Confirm `~/.config/opencode/command/tokenscope.md` exists, or use the platform-equivalent OpenCode command directory.
5. Restart OpenCode completely.
6. Run `/tokenscope` and verify that a report path and report content are returned.

## Known Boundaries

- The report is based on OpenCode session telemetry plus separately labeled local and explanatory estimates.
- The invocation step and later report-reading/final-response steps are outside the snapshot boundary.
- Model pricing or tokenizer coverage can be incomplete and may produce visible warnings.
- Some tokenizer files may be downloaded from Hugging Face at runtime.
- Local providers may not expose billable pricing or complete metadata.
- Compaction and session reverts can make retained content differ from lifetime recorded usage.

## Troubleshooting

### `/tokenscope` is unavailable

Check the command path, plugin entry, package version, and then restart OpenCode. Do not install a second unrelated TokenScope implementation.

### Counts differ from the TUI

Read the upstream report boundary and uncertainty notes first. A difference can be caused by snapshot timing, compaction, tokenizer fallback, missing pricing, or provider metadata.

### Plugin resolution fails

Check npm connectivity, Node.js/npm versions, the exact pinned package name, and OpenCode plugin API compatibility. Avoid switching to `@latest` as a first troubleshooting step because it changes the baseline.
