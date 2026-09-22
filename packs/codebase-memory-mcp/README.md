# Codebase Memory MCP

This pack is **Optional**. It is listed for user selection and is not installed by default.

Codebase Memory MCP is an upstream MCP server that indexes codebases into a persistent knowledge graph for structural code analysis. It is not an OpenCode Skill and is not vendored by this repository.

## Official Source

- Repository: <https://github.com/DeusData/codebase-memory-mcp>
- Release: `v0.11.0`
- npm package: `codebase-memory-mcp@0.11.0`
- License: MIT

## Features

- 162 languages via tree-sitter AST analysis
- Hybrid LSP semantic type resolution for Python, TypeScript, JavaScript, Go, Rust, etc.
- 15 MCP tools: search_graph, trace_call_path, detect_changes, query_graph, get_architecture, get_code_snippet, search_code, manage_adr, and more
- Persistent SQLite-backed knowledge graph at `~/.cache/codebase-memory-mcp/`
- Bundled 3D graph UI at `localhost:9749` (the old `CBM_VARIANT=ui` opt-in is obsolete upstream)

## OpenCode Installation

### Standard (headless)

Add the server under `mcp.servers` in `opencode.jsonc` (OpenCode v2 shape) without replacing other MCP servers:

```json
{
  "mcp": {
    "servers": {
      "codebase-memory-mcp": {
        "type": "local",
        "command": [
          "npx",
          "-y",
          "codebase-memory-mcp@0.11.0"
        ]
      }
    }
  }
}
```

Servers connect automatically in v2; to keep one configured but disconnected, set `"disabled": true` (v2 does not use an `enabled` field).

### Graph UI

The graph UI ships with the default runtime set — no extra environment variable is required. If an older guide told you to set `CBM_VARIANT=ui`, remove it; upstream marked that opt-in as obsolete.

On Windows, use `npx.cmd` instead of `npx` if the OpenCode runtime cannot resolve the command.

## Verification

1. Restart OpenCode after configuration changes.
2. Run `/mcps` to verify the server appears with 15 tools.
3. Open `http://localhost:9749` in a browser to view the graph UI.
4. Index a project by saying "Index this project" in a conversation.

## Indexing a Project

Once installed, the server provides tools for code intelligence:

- `index_repository` — Index a repository into the knowledge graph
- `search_graph` — Search for functions, classes, routes, and variables
- `trace_call_path` — Trace callers and callees through the code graph
- `get_architecture` — Get high-level architecture overview
- `detect_changes` — Map git diff to affected symbols and blast radius

## Update Policy

Review the upstream release and compatibility notes before changing the pinned package version in `manifest/packs.json`. Validate the MCP server after every version change.
