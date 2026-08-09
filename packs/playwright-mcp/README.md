# Playwright MCP

This pack is **Optional**. It is listed for user selection and is not installed by default.

Playwright MCP is an upstream MCP server for controlled browser automation. It is not an OpenCode Skill and is not vendored by this repository.

## Official Source

- Repository: <https://github.com/microsoft/playwright-mcp>
- Release: `v0.0.79`
- npm package: `@playwright/mcp@0.0.79`
- License: Apache-2.0

## OpenCode Installation

Add the server to the existing `mcp` object in `opencode.jsonc` without replacing other MCP servers:

```json
{
  "mcp": {
    "playwright": {
      "type": "local",
      "command": [
        "npx",
        "-y",
        "@playwright/mcp@0.0.79"
      ],
      "enabled": true
    }
  }
}
```

On Windows, use `npx.cmd` instead of `npx` if the OpenCode runtime cannot resolve the command. Keep the package version pinned for reproducible member environments.

Restart OpenCode, then verify that the Playwright MCP tools are available. Use the `browser-automation` Skill for safe interaction rules; installing this MCP server alone does not authorize login, submission, deletion, publishing, or other side-effectful actions.

## Update Policy

Review the upstream release and compatibility notes before changing the pinned package version in `manifest/packs.json`. Validate the MCP server after every version change.
