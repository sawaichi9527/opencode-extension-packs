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
5. 互動式 Command 不應在未確認前自行修改程式碼或執行高風險操作。
6. README 必須分別說明全域 `~/.config/opencode/commands/` 與專案 `.opencode/commands/` 的安裝位置。
