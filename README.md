# OpenCode Extension Packs

提供依角色、工具與專案需求選擇安裝的 OpenCode 擴充能力。

本 Repository 與 `opencode-essential-core` 分離，避免新安裝 OpenCode 時一次加入大量
不需要的雲端服務、MCP 或特定測試工具。

內容以 OpenCode 原生 `SKILL.md` 與 Markdown Custom Commands 為主，不要求 Claude Code／Codex Plugin、跨 Agent Hook 或模式狀態管理。

> 狀態：小型 OpenCode Team 的選配擴充基準。

## Skills

| Skill | 用途 | 狀態 |
|---|---|---|
| `swqa-automation` | Python、UART／TTY、PCAP、API、CLI、Device 等 SWQA 自動化結構與規則 | Draft |
| `test-failure-triage` | 分層分析 Python、UART、封包、DUT、環境與 Timing 造成的測試失敗 | Draft |
| `forgejo-integration` | 本地 Git + Forgejo + 可選 Forgejo MCP | Draft |
| `github-integration` | 本地 Git + GitHub CLI/API 基本協作 | Draft |
| `file-toolkit` | 文件、影音與 Python 工具能力檢查 | Draft |
| `browser-automation` | 瀏覽器自動化的安全起始規則 | Draft |
| `lean-code-review` | 審查目前 Git diff 的過度設計與不必要複雜度 | Draft |

## OpenCode Commands

| Command | 用途 |
|---|---|
| `/grill-me` | 一次一題釐清需求、術語、範圍、Acceptance Criteria 與驗證證據，確認共同理解前不實作 |

本版刻意不包含 OpenSpec-tw、SpecTest、Superpowers Plugin、NotebookLM、Google Apps Script、Supabase、Groq、Netlify 等工具。使用者部署 Core 與 Extension 後，可依個別專案需求自行安裝其他外部工具。

## 安裝單一 Skill

建議直接指定子目錄，避免 Repository 根目錄影響 Skill 搜尋：

```powershell
$env:DISABLE_TELEMETRY = "1"
npx skills add `
  https://github.com/sawaichi9527/opencode-extension-packs/tree/main/skills/swqa-automation `
  -g -a opencode --copy -y
```

例如安裝測試失敗分析：

```powershell
$env:DISABLE_TELEMETRY = "1"
npx skills add `
  https://github.com/sawaichi9527/opencode-extension-packs/tree/main/skills/test-failure-triage `
  -g -a opencode --copy -y
```

也可以手動複製 Skill 到：

```text
~/.config/opencode/skills/
```

若只希望某個專案使用，則放到：

```text
<project>/.opencode/skills/
```

## 安裝 `/grill-me`

OpenCode Custom Command 不使用 `npx skills add`。將 `commands/grill-me.md` 複製到全域 Command 目錄：

### Windows PowerShell

```powershell
New-Item -ItemType Directory -Force "$HOME\.config\opencode\commands" | Out-Null
Copy-Item ".\commands\grill-me.md" "$HOME\.config\opencode\commands\grill-me.md" -Force
```

### WSL / Ubuntu / macOS

```bash
mkdir -p ~/.config/opencode/commands
cp ./commands/grill-me.md ~/.config/opencode/commands/grill-me.md
```

若只希望某個專案使用，複製到：

```text
<project>/.opencode/commands/grill-me.md
```

重新啟動 OpenCode 後執行：

```text
/grill-me <想釐清的功能或計畫>
```

`/grill-me` 只做訪談與共同理解摘要，不會自行建立程式碼、`CONTEXT.md`、ADR 或其他規格框架。

## `test-failure-triage` 的定位

測試失敗時，先判定問題位於：

```text
Requirement / Expected Result
→ Python Harness / Fixture
→ UART Transport / Parser
→ Packet Capture / Protocol
→ DUT / Firmware
→ Environment / Timing
```

它要求保留完整 Traceback、原始 UART TX／RX、PCAP／PCAPNG、關鍵 Frame 與測試報告，再以單一假設和最小實驗確認 Root Cause。預設先調查與報告，不直接修改測試期待值或增加 Retry／Timeout 來隱藏失敗。

## `lean-code-review` 的定位

這個 Skill 只有在使用者明確要求 OpenCode 檢查程式碼是否過度設計時才載入：

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
├── Fresh Validation Evidence
└── Git Basic

Extension Packs
├── /grill-me
├── SWQA Automation
├── Test Failure Triage
├── Forgejo / GitHub
├── File Toolkit
├── Browser Automation
└── Lean Code Review
```

## 授權與來源

本專案依 MIT License 發布。上游來源與刪減原則請參閱 [UPSTREAM.md](UPSTREAM.md)。
