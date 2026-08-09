---
description: inspect available hybrid-workflow backends and confirm whether to delegate this request
agent: plan
---

Load and follow the `hybrid-workflow` Skill.

This command is an explicit way to inspect or select the `hybrid-workflow` Skill in the `other` category for the current request. If no backend is selected, keep OpenCode's native model assignment and execution behavior. `$ARGUMENTS` may contain `workflow_local_builder`, `workflow_local_builder_aeon`, `workflow_local_builder_<backend>`, or `workflow_cloud_cheap_builder`; with no argument, ask once for the current request after the plan is decomposed.
