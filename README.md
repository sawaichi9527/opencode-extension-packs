# OpenCode Extension Packs

提供依角色、工具與專案需求**選擇安裝**的 OpenCode 擴充能力。本 Repository 與 `opencode-essential-core` 分離，避免新安裝 OpenCode 時一次加入大量不需要的雲端服務、MCP 或特定測試工具。

內容以 OpenCode v2 原生 `SKILL.md` 與 Markdown Custom Commands 為主，不要求 Claude Code／Codex Plugin、跨 Agent Hook 或模式狀態管理。外部外掛只記錄來源、固定版本與安裝指導，不 fork、不 vendored 進本 Repository。

> 狀態：`v2.0.12`，**僅支援 OpenCode v2.x.x**（v1 相容已移除）。共 14 個 Pack：Default 1、Recommended 5、Optional 8。版號採「該次檢討時基於驗證的 OpenCode 版本」方案，與 `opencode-essential-core` 一致。

## 目錄結構

```text
manifest/packs.json     套件清單：version、schemaVersion、tier、kind、sourcePath、audience、外部 pin
skills/                 原生 Skill（8）
commands/               Markdown Custom Command（1）
packs/                  外部整合與工具的文件（不改第三方原始碼）
docs/PACK-GUIDE.md      新增 Pack 的撰寫最低要求
UPSTREAM.md             上游來源與刪減原則
VERSION / CHANGELOG.md / handoff.md
```

## 分層安裝

Extension Packs 不採全部默認安裝，套件由 `manifest/packs.json` 分成三層：

| Tier | 行為 | 定位 |
|---|---|---|
| `default` | 可由團隊安裝流程直接安裝 | 所有成員通常都需要的低依賴能力 |
| `recommended` | 列出並由使用者確認 | 常見但依角色或工作流程而異 |
| `optional` | 列出但不預選 | 外部 plugin、額外依賴、雲端服務或特殊用途 |

使用者可執行 Core 提供的 `/teamwork-update-check`（詳見[更新檢查](#更新檢查)），讀取本 Repository 的 manifest 與版本，查看新增或變更後再選擇要套用的 Packs。Extension Packs 不會自行建立背景排程，也不會在未確認時安裝 Optional Pack。

## Pack 清單

### Default

| Pack | 類型 | 用途 |
|---|---|---|
| `/grill-me` | command | 使用 Plan Agent 一次一題釐清需求、術語、範圍、Acceptance Criteria 與驗證證據，確認共同理解前不實作 |

### Recommended

| Pack | 類型 | 用途 |
|---|---|---|
| `swqa-automation` | skill | Python、UART/TTY、PCAP、API、CLI、Device 等 SWQA 自動化結構與規則 |
| `test-failure-triage` | skill | 分層分析 Python、UART、封包、環境、Timing 與 DUT 造成的測試失敗 |
| `file-toolkit` | skill | 文件、影音與 Python 工具能力檢查 |
| `browser-automation` | skill | 瀏覽器自動化的安全起始規則 |
| `lean-code-review` | skill | 審查目前 Git diff 的過度設計與不必要複雜度 |

### Optional

| Pack | 類型 | 用途 |
|---|---|---|
| `forgejo-integration` | skill | 本地 Git + Forgejo + 可選 Forgejo MCP |
| `github-integration` | skill | 本地 Git + GitHub CLI/API 基本協作 |
| `local-llm-dispatch-policy` | skill | 本地算力調用與升級原則（Orchestrator ＋ heavy-Builder、線性 dispatch、3-strike、雲端接手） |
| PPT Master | external-skill | 簡報生成（含 FII 2026 deck 版模） |
| Archify | external-skill | 架構圖生成 |
| Playwright MCP | external-mcp | 瀏覽器自動化 MCP |
| Codebase Memory MCP | external-mcp | 程式碼記憶與檢索 MCP |
| `pwsh7` | tooling | PowerShell 7.4.6 UTF-8 wrapper（自託管 release） |

各 Pack 的固定版本與來源見下節；完整清單與版本以 `manifest/packs.json` 為準。

## 外部整合（External Packs）

外部外掛不 fork、不 vendored，只記錄來源、固定版本與安裝指導；安裝前依各 Pack 的 compatibility 文件驗證：

| Pack | 類型 | 來源 | Pin | 文件 |
|---|---|---|---|---|
| PPT Master | external-skill | [hugohe3/ppt-master](https://github.com/hugohe3/ppt-master) | `v6.6.0` | [packs/ppt-master/](packs/ppt-master/README.md) |
| Archify | external-skill | [tt-a1i/archify](https://github.com/tt-a1i/archify) | `v2.16.0` | [packs/archify/](packs/archify/README.md) |
| Playwright MCP | external-mcp | [microsoft/playwright-mcp](https://github.com/microsoft/playwright-mcp) | `@playwright/mcp@0.0.82` | [packs/playwright-mcp/](packs/playwright-mcp/README.md) |
| Codebase Memory MCP | external-mcp | [DeusData/codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) | `codebase-memory-mcp@0.11.0` | [packs/codebase-memory-mcp/](packs/codebase-memory-mcp/README.md) |
| pwsh7 | tooling（自託管） | [PowerShell/PowerShell](https://github.com/PowerShell/PowerShell) + 本 repo release | `7.4.6` | [packs/pwsh7/](packs/pwsh7/README.md) |

- 兩個 external-mcp 依 v2 `mcp.servers` 格式設定，安裝後以 `/mcps` 驗證（詳見各 Pack 文件）。
- `browser-automation` Skill 與 Playwright MCP 是配對組合：MCP 提供瀏覽器能力，Skill 提供安全規則（登入、提交、刪除、發布前必須確認）。
- Pin 與 `manifest/packs.json` 同步更新；升級流程見各 Pack 的 Update Policy。

## 安裝

### 安裝 Skill

OpenCode v2 原生 Skill 以 `npx skills add` 安裝；建議直接指定子目錄，避免 Repository 根目錄影響 Skill 搜尋：

```powershell
$env:DISABLE_TELEMETRY = "1"
npx skills add `
  https://github.com/sawaichi9527/opencode-extension-packs/tree/main/skills/swqa-automation `
  -g -a opencode --copy -y
```

也可手動複製到全域或專案目錄：

```text
~/.config/opencode/skills/       # 全域
<project>/.opencode/skills/      # 單一專案
```

### 安裝 Command（`/grill-me`）

OpenCode Custom Command 不使用 `npx skills add`。將 `commands/grill-me.md` 複製到 Command 目錄：

```powershell
# Windows PowerShell
New-Item -ItemType Directory -Force "$HOME\.config\opencode\commands" | Out-Null
Copy-Item ".\commands\grill-me.md" "$HOME\.config\opencode\commands\grill-me.md" -Force
```

```bash
# WSL / Ubuntu / macOS
mkdir -p ~/.config/opencode/commands
cp ./commands/grill-me.md ~/.config/opencode/commands/grill-me.md
```

只希望某個專案使用時，複製到 `<project>/.opencode/commands/grill-me.md`。重新啟動 OpenCode 後執行 `/grill-me <想釐清的功能或計畫>`。

`/grill-me` 固定使用 OpenCode Plan Agent，只做訪談與共同理解摘要，不會自行建立程式碼、`CONTEXT.md`、ADR 或其他規格框架。若本 Session 已讀取且檔案未變更，會沿用既有專案資訊，避免重複載入相同 Context。

### 安裝外部整合

依各 Pack 的 `packs/<id>/README.md` 安裝；MCP 安裝後以 `/mcps` 驗證。

### 更新檢查

已安裝的使用者執行 Core 的 `/teamwork-update-check`，會以本 manifest（`main` 分支的 raw 網址）作為比對來源，與本機 `~/.config/opencode/teamwork-install-state.json` 記錄的 skill／plugin 固定版本比較。若有版本更新，會列出「目前版本 → 最新版本」與相容性摘要，詢問使用者確認後才升級；不會在未確認時自動改寫 `opencode.jsonc` 或安裝套件。

## 運作說明

### 多 Agent 委派（OpenCode v2 原生）

OpenCode v2 已內建多 Agent 分派框架：可自訂 `primary`／`subagent` agent（`.opencode/agents/*.md` 或 `~/.config/opencode/agents/`），由 primary 依 `description` 自動呼叫或由使用者 `@` 指名，並以 `permissions` 的 `subagent` 規則控制可呼叫對象；Command 也可用 `agent`／`subtask` 直接觸發 subagent。

原 `hybrid-workflow` Pack 與 `/other-working-flow` command 已於 2026-09-22 移除（OpenCode v2 原生已涵蓋，見 CHANGELOG）。若要保留「委派前詢問」的行為，在 `opencode.jsonc` 設定：

```jsonc
{
  "$schema": "https://opencode.ai/config.json",
  "permissions": [
    { "action": "subagent", "resource": "*", "effect": "allow" },
    { "action": "subagent", "resource": "heavy-builder", "effect": "ask" }
  ]
}
```

v2 使用 `permissions` 規則清單與 `subagent` action（v1 的 `permission` map 與 `task` 已移除）；規則依序、後者覆蓋前者，因此 `heavy-builder` 需確認、其餘維持允許。將 `"ask"` 改為 `"deny"` 即可完全停用自動委派（使用者仍可透過 `@` 指名）。

### 本地 LLM 調用原則（`local-llm-dispatch-policy`）

規範本地算力模型（prefill 慢、TTFT 慢、不具併發能力）的調用與升級原則，只涵蓋三種情境，且不重造 OpenCode v2 原生多 Agent 框架：

| 角色 | 說明 |
|---|---|
| Orchestrator | 負責拆解、派遣與升級判斷；算力位置可為**本地**（light-weight LLM provider（fast））或**雲端模型** |
| heavy-Builder | **本地** heavy-duty LLM provider（slow）；只做「理解→實做→驗證」 |

- **情境 a**：本地單一模型、單一任務，不派遣 subagent。
- **情境 b**：Orchestrator 線性派遣（一次一個子任務）給 heavy-Builder；3-strike 後重拆一次，再失敗則升級。
- **c1／c2**：依 Orchestrator 的算力位置升級——本地時暫停並建議切換雲端（同意後於本次任務目標內沿用該雲端模型），雲端時直接接手。

heavy-Builder 需為 OpenCode v2 原生 subagent（`mode: subagent`），範例：

```md
---
description: heavy-Builder：本地重型執行者。只做「理解→實做→驗證」，同一子任務最多嘗試 3 次，失敗即回報，不再次派遣。
mode: subagent
model: <heavy-provider>/<heavy-model>
steps: 15
permissions:
  - action: subagent
    resource: "*"
    effect: deny
  - action: edit
    resource: "*"
    effect: allow
  - action: shell
    resource: "*"
    effect: ask
---

你只處理被派遣的單一子任務，不擴大範圍、不重新規劃、不再派遣其他 subagent。

流程：
1. 理解需求與驗收條件。
2. 以最小變更實作。
3. 執行可重現的驗證，保留原始輸出（指令、輸出、exit code）。

嘗試上限：同一子任務最多嘗試 3 次；每次失敗都記錄失敗原因與目前證據。

回傳格式（成功）：結論、變更內容、驗證指令與原始輸出、殘留風險。
回傳格式（3-strike 失敗）：失敗、已嘗試內容與原因、目前證據。
```

### `test-failure-triage` 的定位

測試失敗時，先判定問題位於：

```text
Requirement / Expected Result
→ Python Harness / Fixture
→ UART Transport / Parser
→ Packet Capture / Protocol
→ Environment / Timing
→ DUT / Firmware
```

它要求保存完整 Traceback、原始 UART TX/RX、PCAP/PCAPNG、關鍵 Frame 與測試報告，再以單一假設和最小實驗確認 Root Cause。完整原始檔保存為 Artifact，分析時優先讀取相關時間範圍、Filter、Frame 與錯誤區段，不把大型 Log 或整份 PCAP 全部塞入對話。

預設先調查與報告，不直接修改測試期待值，也不以增加 Retry/Timeout、Skip 或刪除 Assertion 隱藏失敗。只有其他可控制層級已有合理證據時，才把問題歸類為 DUT/Firmware。

### `lean-code-review` 的定位

這個 Skill 只有在使用者明確要求 OpenCode 檢查程式碼是否過度設計時才載入：

```text
檢查這次修改是否過度設計
Review current diff for unnecessary complexity
哪些新增程式碼可以刪除或改用既有功能
```

預設只讀取目前 Git diff、提出建議，不直接套用修改，也不取代 Correctness、Security 或 SWQA 測試驗證。若實際測試失敗的 Root Cause 尚未確認，先使用 `test-failure-triage`，不從 Diff 猜測原因。

### FII 2026 ppt-master 版模

`ppt-master` Pack 隨附團隊自有的 FII 2026 Deck 版模，安裝 PPT Master Skill 後可直接導入：

| Deck | 主題 | 用途 |
|---|---|---|
| `fii_2026_bright` | FII 2026 亮色版 | 企業內部報告、客戶簡報、專案提案、年度總結 |
| `fii_2026_dark` | FII 2026 暗色版 | 企業內部報告、客戶提案、專案匯報、教學說明 |

導入指令與索引合併說明見 [packs/ppt-master/decks/](packs/ppt-master/decks/README.md)。

## Manifest 與版本

`manifest/packs.json` 是套件清單的來源，包含 repository 版本、schema 版本、Default Pack 清單、Pack 的 ID／tier／kind／audience／來源路徑，以及外部 plugin 的來源 repository、固定版本與相容性文件。

新增或修改 Pack 時，必須同步更新 `VERSION`、manifest、CHANGELOG 與相關安裝文件；版號規則（版號 = 該次檢討時基於驗證的 OpenCode 版本）與目前同步狀態見 [handoff.md](handoff.md)。

## 與 Essential Core 的關係

```text
Essential Core
├── 環境檢查
├── OpenCode Project Init
├── AGENTS.md 共用規則
├── Session Start / Close
├── Fresh Validation Evidence
├── Git Basic
└── 手動 /teamwork-update-check 更新檢查

Extension Packs
├── Default: grill-me
├── Recommended: Lean Review / SWQA / Failure Triage / File Toolkit / Browser Automation
├── Optional: Forgejo / GitHub / Local LLM Dispatch / pwsh7
└── Optional: PPT Master（含 FII 2026 deck） / Archify / Playwright MCP / Codebase Memory MCP
```

## 授權與來源

本專案依 MIT License 發布。上游來源與刪減原則請參閱 [UPSTREAM.md](UPSTREAM.md)。
