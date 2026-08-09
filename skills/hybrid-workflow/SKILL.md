---
name: hybrid-workflow
description: Use the optional hybrid-workflow backends in the other category to offer local or low-cost cloud Builder delegation after planning, without changing OpenCode's native routing by default.
---

# hybrid-workflow

This is the `hybrid-workflow` Skill in the `other` category. It does not replace OpenCode's built-in Plan or Build agents and does not change native model assignment unless the user confirms a delegation for the current request.

## Available Backends

- `workflow_local_builder`: generic local AI Builder; ask the user to select an existing local provider/model.
- `workflow_local_builder_aeon`: team 28500 local AI Builder preset using `DGX Spark/aeon`.
- `workflow_local_builder_<backend>`: another explicitly installed local AI Builder, such as `workflow_local_builder_w540`.
- `workflow_cloud_cheap_builder`: low-cost cloud AI Builder; ask the user to select an existing cloud provider/model.

The role names `builder` and `cheap-builder` are documentation shorthand. Use the full Agent IDs when selecting or dispatching an agent so they remain distinct from OpenCode's built-in `Plan` and `Build` entries.

## Native Routing First

Apply these rules in order:

1. If this Skill is not installed, do nothing and use OpenCode's native model assignment.
2. If no workflow backend is installed, do nothing and use OpenCode's native model assignment.
3. If one or more backends are installed, still use native routing by default.
4. After the Plan has decomposed an actionable implementation request, or immediately before Build starts implementation, ask once whether to delegate the current request.
5. If the user declines, continue with the current Plan/Build agent.
6. If the user explicitly names a full Agent ID, use that backend without asking the same question again.

Do not ask for workflow selection for ordinary questions, explanations, reviews that do not create implementation tasks, or requests that the current agent can complete without delegation.

## One Decision Per Request

Ask once for the whole request, not once per subtask. If the user selects a backend, the selected backend applies to all independent subtasks in the current request. Keep dispatch linear: dispatch one Builder task, wait for its report and validation, then continue to the next task.

When multiple backends are installed, present only the installed choices plus the native execution option. Recommend `workflow_local_builder` to general users; recommend the aeon preset only when the user identifies as team 28500 or confirms the matching setup:

```text
Workflow backends available for this request:
1. workflow_local_builder
2. workflow_local_builder_aeon (team 28500 preset)
3. workflow_cloud_cheap_builder
4. Continue with the current OpenCode Plan/Build agent
```

## Builder Contract

Every delegated task must include exact files, change scope, acceptance criteria, and validation commands. The selected Builder must:

- read relevant files before editing;
- complete only one task;
- not dispatch another agent;
- run the requested validation;
- report files, commands, exit codes, results, and unresolved issues;
- never commit or push.

## Installation Detection

Treat a backend as installed only when its workflow-managed Agent file or installation state explicitly identifies the `hybrid-workflow` Skill in the `other` category. Do not infer workflow installation from an unrelated file named `builder.md`.

## Backend Setup

Before using `workflow_local_builder`, read configured local providers and ask the user to select one. Before using `workflow_cloud_cheap_builder`, read configured cloud providers and ask the user to select one. Do not infer cost from a model name. The selected model is written only to that backend's Agent file after confirmation.

## Configuration Safety

Before installing or changing a backend, show the affected files and model fields and ask for confirmation. Preserve unrelated providers, plugins, MCP servers, and user-specific values. Never publish API keys, internal URLs, or a complete local `opencode.jsonc`.
