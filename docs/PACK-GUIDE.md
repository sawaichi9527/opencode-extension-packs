# Pack 與 Command 撰寫最低要求

## Skill Pack

每個 Skill Pack 至少包含：

```text
skills/<pack-name>/SKILL.md
```

並遵守：

1. 資料夾名稱與 frontmatter `name` 完全一致。
2. `description` 同時說明用途與觸發情境。
3. 說明會新增、修改或執行什麼。
4. 需要外部帳號、Token、MCP 或付費服務時明確標示。
5. 危險或不可回復操作必須先詢問。
6. 不把公司內部 URL、Token 或專案機密提交到公開 GitHub。
7. 能由既有 Skill、`AGENTS.md` 或 `session-close` 處理的內容，不建立重複 Skill。

## OpenCode Custom Command

需要由使用者明確執行的互動流程，可放在：

```text
commands/<command-name>.md
```

並遵守：

1. 檔名就是 `/command-name` 的指令名稱。
2. frontmatter 至少包含清楚的 `description`。
3. Command 應使用 OpenCode 原生 Markdown 格式，不依賴其他 Agent 的 Plugin 或 Hook。
4. 使用 `$ARGUMENTS` 接收使用者輸入時，必須說明沒有參數時的行為。
5. 只做分析、訪談或規劃的 Command 應指定 `agent: plan`；需要寫入時才使用具備修改權限的 Agent。
6. 互動式 Command 不應在未確認前自行修改程式碼或執行高風險操作。
7. 若本 Session 已讀取且來源未變更，應沿用現有結果，避免重複載入相同文件與 Git 狀態。
8. README 必須分別說明全域 `~/.config/opencode/commands/` 與專案 `.opencode/commands/` 的安裝位置。

## Workflow Pack

Workflow Pack 放在 `other/<workflow-name>/` 類別下，並遵守：

1. 在 manifest 使用 `category: "other"`、`subcategory` 與 `kind: "workflow"`。
2. 說明是否會改變 OpenCode 原生模型指派；預設不得在未確認時改變。
3. 可用 backend 必須有穩定、與內建 Agent 不混淆的 Agent ID，例如 `workflow_local_builder_aeon`。
4. backend 可同時安裝，不得預設成互斥方案，除非文件明確說明。
5. 未安裝 workflow 時必須完全回到 OpenCode 原生模型指派邏輯。
6. 若 workflow 會在 Plan 或 Build 邊界詢問，必須定義詢問時機、拒絕行為與每次請求的詢問次數。
7. Agent template 不得包含 provider secret、內部 URL 或特定使用者的完整設定。
