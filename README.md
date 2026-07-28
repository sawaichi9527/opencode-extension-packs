# OpenCode Extension Packs

提供依角色、工具與專案需求選擇安裝的 OpenCode 擴充能力。

本 Repository 與 `opencode-essential-core` 分離，避免新安裝 OpenCode 時一次加入大量
不需要的雲端服務、MCP 或特定測試工具。

所有 Pack 都以 OpenCode 的 `SKILL.md` 為主，不要求 Claude Code／Codex Plugin、跨 Agent Hook 或模式狀態管理。

> 狀態：初始轉化版本（v0.1 planning baseline）。

## 初始 Packs

| Pack | 用途 | 狀態 |
|---|---|---|
| `swqa-automation` | SWQA 自動化專案結構與基本工作流程 | Draft |
| `forgejo-integration` | 本地 Git + Forgejo + 可選 Forgejo MCP | Draft |
| `github-integration` | 本地 Git + GitHub CLI/API 基本協作 | Draft |
| `file-toolkit` | 文件、影音與 Python 工具能力檢查 | Draft |
| `browser-automation` | 瀏覽器自動化的安全起始規則 | Draft |
| `lean-code-review` | 使用 OpenCode 審查目前 Git diff 的過度設計與不必要複雜度 | Draft |

本版刻意未納入 NotebookLM、Google Apps Script、Supabase、Groq、Netlify 等 Packs。
這些能力未來可在有明確使用需求時再經過評估後加入。

## 安裝單一 Pack

建議直接指定子目錄，避免 Repository 根目錄 Skill 影響搜尋：

```powershell
$env:DISABLE_TELEMETRY = "1"
npx skills add `
  https://github.com/sawaichi9527/opencode-extension-packs/tree/main/skills/swqa-automation `
  -g -a opencode --copy -y
```

例如安裝精簡程式審查：

```powershell
$env:DISABLE_TELEMETRY = "1"
npx skills add `
  https://github.com/sawaichi9527/opencode-extension-packs/tree/main/skills/lean-code-review `
  -g -a opencode --copy -y
```

也可以手動複製到：

```text
~/.config/opencode/skills/
```

若只希望某個專案使用，則放到該專案：

```text
<project>/.opencode/skills/
```

## `lean-code-review` 的定位

這個 Pack 只有在使用者明確要求 OpenCode 檢查程式碼是否過度設計時才載入：

```text
檢查這次修改是否過度設計
Review current diff for unnecessary complexity
哪些新增程式碼可以刪除或改用既有功能
```

預設只讀取目前 Git diff、提出建議，不直接套用修改，也不取代 Correctness、Security 或 SWQA 測試驗證。

## 與 Essential Core 的關係

```text
Essential Core
├── 環境檢查
├── OpenCode Project Init
├── AGENTS.md 共用規則
├── Session Start / Close
└── Git Basic

Extension Packs
├── SWQA Automation
├── Forgejo / GitHub
├── File Toolkit
├── Browser Automation
└── Lean Code Review
```

## 授權與來源

本專案依 MIT License 發布。上游來源與刪減原則請參閱 [UPSTREAM.md](UPSTREAM.md)。
