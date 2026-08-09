# hybrid-workflow

Optional Builder backends for OpenCode's native Plan and Build flow.

## Scope

This Pack belongs to the `other` category and is named `hybrid-workflow`. It adds optional delegation choices; it does not replace or rename OpenCode's built-in `Plan` and `Build` agents.

The backends are independently selectable and may be installed together:

| Agent ID | Backend | Model location |
|---|---|---|
| `workflow_local_builder` | Generic Local Builder | Selected by the user from configured local providers |
| `workflow_local_builder_aeon` | team 28500 Local Builder preset | Pre-paired `DGX Spark/aeon` |
| `workflow_local_builder_<backend>` | Additional Local Builder | User-configured local provider |
| `workflow_cloud_cheap_builder` | Cheap Cloud Builder | Selected by the user from configured cloud providers |

## Default Behavior

Installing this Pack does not change OpenCode's native model assignment. When an implementation request reaches the Plan decomposition or Build execution boundary, the workflow may ask once whether to delegate the current request. Declining keeps execution on the current Plan/Build agent.

Explicit Agent IDs take precedence for the current request:

```text
workflow_local_builder_aeon
workflow_cloud_cheap_builder
```

The documentation terms `builder` and `cheap-builder` describe the roles, but the namespaced Agent IDs are the selectable names.

## Local Builder Variants

Use the generic Agent for unknown local hardware. Use one Agent file per local backend when more than one local model is available:

```text
workflow_local_builder
workflow_local_builder_aeon
workflow_local_builder_w540
workflow_local_builder_cuda
```

`workflow_local_builder_aeon` is a pre-paired preset for team 28500 and uses `DGX Spark/aeon`. Other teams should normally start with `workflow_local_builder`, which asks them to select an existing local provider/model during setup.

Do not put IP addresses, API keys, or internal hostnames in Agent IDs or this Pack.

## Agent Templates

The public templates are stored under:

```text
packs/other/hybrid-workflow/agents/
```

Copy only the backend that the user has configured into the global Agent directory:

```text
~/.config/opencode/agent/workflow_local_builder_aeon.md
~/.config/opencode/agent/workflow_cloud_cheap_builder.md
```

The generic local template requires a user-selected local provider/model. The team 28500 aeon template requires a configured `DGX Spark/aeon` model. The cheap cloud template requires a user-selected existing cloud model. Show the diff and ask for confirmation before changing any Agent file.

## Linear Dispatch

The workflow dispatches one Builder task at a time. It waits for validation and the Builder report before dispatching the next task. Parallel Builder dispatch is not part of this workflow.

## Installation Safety

Show the target Agent files and model fields before changing local configuration. Preserve existing providers, plugins, MCP servers, and secrets. If a required provider or model is not configured, report it instead of guessing or adding credentials.
