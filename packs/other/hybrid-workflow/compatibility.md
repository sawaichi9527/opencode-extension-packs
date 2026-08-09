# hybrid-workflow compatibility

## OpenCode

The workflow uses native Markdown Skills, Commands, and Agent files. It does not require a plugin, lifecycle hook, or mode-state manager.

## Model Providers

The Pack does not select a provider or invent a model ID. `workflow_local_builder` and `workflow_cloud_cheap_builder` list existing configured providers and ask the user to select one. `workflow_local_builder_aeon` is the team 28500 preset for an existing `DGX Spark/aeon` provider.

## Built-in Agents

`Plan` and `Build` remain OpenCode's native entries. The `workflow_` prefix is intentional so custom Agents are recognizable in the Agent menu.

## Existing Builder Files

An existing `builder.md` may be an unrelated custom Agent. Installation must not overwrite or delete it without confirmation. The recommended local aeon name is:

```text
workflow_local_builder_aeon.md
```

The public `workflow_local_builder_aeon` preset is explicitly labeled for team 28500. Other teams should use the generic `workflow_local_builder` unless they have the same aeon/vLLM setup.
