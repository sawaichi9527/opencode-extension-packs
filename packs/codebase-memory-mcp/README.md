# Codebase Memory MCP

This pack is **Optional**. It is listed for user selection and is not installed by default.

Codebase Memory MCP is an upstream MCP server that indexes codebases into a persistent knowledge graph for structural code analysis. It is not an OpenCode Skill and is not vendored by this repository.

## Official Source

- Repository: <https://github.com/DeusData/codebase-memory-mcp>
- Release: `v0.10.8`
- npm package: `codebase-memory-mcp`
- License: MIT

## Features

- 158 languages via tree-sitter AST analysis
- Hybrid LSP semantic type resolution for Python, TypeScript, JavaScript, Go, Rust, etc.
- 15 MCP tools: search_graph, trace_path, detect_changes, query_graph, get_architecture, get_code_snippet, search_code, manage_adr, and more
- Persistent SQLite-backed knowledge graph at `~/.cache/codebase-memory-mcp/`
- Optional 3D graph UI at `localhost:9749`

## OpenCode Installation

### Standard (headless)

Add the server to the existing `mcp` object in `opencode.jsonc` without replacing other MCP servers:

```json
{
  "mcp": {
    "codebase-memory-mcp": {
      "type": "local",
      "command": [
        "npx",
        "-y",
        "codebase-memory-mcp"
      ],
      "enabled": true
    }
  }
}
```

### With Graph UI

Add the `CBM_VARIANT=ui` environment variable to enable the 3D graph visualization:

```json
{
  "mcp": {
    "codebase-memory-mcp": {
      "type": "local",
      "command": [
        "npx",
        "-y",
        "codebase-memory-mcp"
      ],
      "enabled": true,
      "environment": {
        "CBM_VARIANT": "ui"
      }
    }
  }
}
```

On Windows, use `npx.cmd` instead of `npx` if the OpenCode runtime cannot resolve the command.

## Verification

1. Restart OpenCode after configuration changes.
2. Run `/mcp` to verify the server appears with 15 tools.
3. For UI variant, open `http://localhost:9749` in a browser.
4. Index a project by saying "Index this project" in a conversation.

## Indexing a Project

Once installed, the server provides tools for code intelligence:

- `index_repository` — Index a repository into the knowledge graph
- `search_graph` — Search for functions, classes, routes, and variables
- `trace_path` — Trace callers and callees through the code graph
- `get_architecture` — Get high-level architecture overview
- `detect_changes` — Map git diff to affected symbols and blast radius

## Update Policy

Review the upstream release and compatibility notes before changing the pinned package version in `manifest/packs.json`. Validate the MCP server after every version change.
