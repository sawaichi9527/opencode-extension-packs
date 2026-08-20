# Codebase Memory MCP Compatibility

## Upstream Contract

- Release: `v0.10.8`
- Package: `codebase-memory-mcp`
- Runtime requirement: Node.js 18 or newer
- Client: OpenCode local MCP configuration
- Upstream server: `DeusData/codebase-memory-mcp`

## Supported Platforms

- macOS (Apple Silicon, Intel)
- Linux (x86_64, ARM64)
- Windows (x86_64)

## Installation Checks

1. Confirm Node.js 18 or newer and npm/npx are available.
2. Confirm the active `opencode.jsonc` contains a `codebase-memory-mcp` entry under `mcp`.
3. Confirm the package version matches the upstream release.
4. Restart OpenCode completely.
5. Verify the server starts and the 15 tools are visible via `/mcp`.
6. For UI variant, confirm `localhost:9749` is accessible.

## Security Boundaries

- Codebase Memory MCP runs entirely locally — no code leaves the machine.
- No API key or hosted service is required.
- The SQLite database is stored at `~/.cache/codebase-memory-mcp/`.
- The graph UI (if enabled) is served on localhost only.

## Troubleshooting

### MCP server does not start

Check Node.js version, the exact package name/version, `npx` versus `npx.cmd`, and the active `opencode.jsonc`. Restart OpenCode after configuration changes.

### UI not accessible

Ensure `CBM_VARIANT=ui` is set in the environment configuration. The UI variant requires the environment variable to be present at startup.

### Indexing is slow

First-time indexing downloads the native executable and assets. Subsequent indexing uses cached binaries. Check `~/.cache/codebase-memory-mcp/` for cached files.
