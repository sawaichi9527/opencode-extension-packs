# Token Usage / Observability

This pack is **Optional**. It is listed for user selection but is not installed by default.

It installs two related pieces as one unit:

- Plugin: `@ramtinj95/opencode-tokenscope@1.8.1`
- Command: `/tokenscope`

The command invokes the plugin's `tokenscope` tool. These are not two independent packs.

## Official Source

- Repository: <https://github.com/ramtinJ95/opencode-tokenscope>
- Release: `v1.8.1`
- npm package: `@ramtinj95/opencode-tokenscope`
- Upstream installation guide: <https://github.com/ramtinJ95/opencode-tokenscope#installation>

This repository does not vendor, fork, or copy the upstream source code. Install the plugin from npm and obtain the command definition from the upstream repository.

## Installation

### npm and OpenCode configuration

Install the pinned release:

```bash
npm install -g @ramtinj95/opencode-tokenscope@1.8.1
```

Add the plugin entry to the existing OpenCode configuration without replacing other plugins:

```json
{
  "plugin": [
    "@ramtinj95/opencode-tokenscope@1.8.1"
  ]
}
```

The entry can be placed in the global OpenCode configuration or the project configuration according to the team's installation scope. Keep the version pinned for reproducible member environments.

### Command installation

Create the global OpenCode command directory if needed, then install the command file from the upstream `v1.8.1` tag:

```bash
mkdir -p ~/.config/opencode/command
curl -fsSL https://raw.githubusercontent.com/ramtinJ95/opencode-tokenscope/v1.8.1/command/tokenscope.md \
  -o ~/.config/opencode/command/tokenscope.md
```

On Windows PowerShell:

```powershell
$dir = Join-Path $HOME ".config\opencode\command"
New-Item -ItemType Directory -Force $dir | Out-Null
Invoke-WebRequest `
  -Uri "https://raw.githubusercontent.com/ramtinJ95/opencode-tokenscope/v1.8.1/command/tokenscope.md" `
  -OutFile (Join-Path $dir "tokenscope.md")
```

Inspect an existing command before replacing it. Do not overwrite local changes without confirmation.

Restart OpenCode, then run:

```text
/tokenscope
```

## Configuration

TokenScope optionally reads a user override at:

```text
~/.config/opencode/tokenscope-config.json
```

Use the upstream documented keys only. The Extension Pack does not create a configuration override automatically.

## Update Policy

Update the pack only after reviewing the upstream release and compatibility notes. Change the pinned plugin version in `manifest/packs.json` and this document together, then validate `/tokenscope` in the team's supported OpenCode versions.
