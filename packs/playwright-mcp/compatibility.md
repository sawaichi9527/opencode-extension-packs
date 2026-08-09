# Playwright MCP Compatibility

## Upstream Contract

- Release: `v0.0.79`
- Package: `@playwright/mcp@0.0.79`
- Runtime requirement: Node.js 18 or newer
- Client: OpenCode local MCP configuration
- Upstream server: `microsoft/playwright-mcp`

## Installation Checks

1. Confirm Node.js 18 or newer and npm/npx are available.
2. Confirm the active `opencode.jsonc` contains a `playwright` entry under `mcp`.
3. Confirm the package version is pinned to `0.0.79`.
4. Restart OpenCode completely.
5. Verify the server starts and the browser tools are visible.
6. Use a non-sensitive test page before attempting a real workflow.

## Security Boundaries

- Playwright MCP is not a security boundary.
- Do not use real user passwords, MFA secrets, or personal data in generic team examples.
- Persistent browser profiles may retain logged-in state; prefer `--isolated` for test sessions.
- Avoid `--allow-unrestricted-file-access`, broad host allowlists, or remote browser endpoints unless explicitly required and reviewed.
- Confirm before navigation that submits, purchases, publishes, deletes, changes permissions, or uploads data.

## Troubleshooting

### MCP server does not start

Check Node.js, the exact package name/version, `npx` versus `npx.cmd`, and the active `opencode.jsonc`. Restart OpenCode after configuration changes.

### Browser executable is missing

Follow the upstream Playwright installation guidance for the required browser. Do not silently switch to an unpinned package version.
