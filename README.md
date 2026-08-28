# OpenCode Extension Packs

提供依角色、工具與專案需求選擇安裝的 OpenCode 擴充能力。

本 Repository 與 `opencode-essential-core` 分離，避免新安裝 OpenCode 時一次加入大量
不需要的雲端服務、MCP 或特定測試工具。

內容以 OpenCode 原生 `SKILL.md` 與 Markdown Custom Commands 為主，不要求 Claude Code/Codex Plugin、跨 Agent Hook 或模式狀態管理。外部 plugin 只提供來源、版本、相容性與安裝指導，不將第三方原始碼 fork 或 vendored 進本 Repository。

> 狀態：v0.2.1，包含 hybrid-workflow 與三種 Builder backend profile。

## 分層安裝

Extension Packs 不採全部默認安裝，套件由 `manifest/packs.json` 分成三層：

| Tier | 行為 | 定位 |
|---|---|---|
| `default` | 可由團隊安裝流程直接安裝 | 所有成員通常都需要的低依賴能力 |
| `recommended` | 列出並由使用者確認 | 常見但依角色或工作流程而異 |
| `optional` | 列出但不預選 | 外部 plugin、額外依賴、雲端服務或特殊用途 |

使用者可執行 Core 提供的 `/teamwork-update-check`，讀取本 Repository 的 manifest 與版本，查看新增或變更後再選擇要套用的 Packs。Extension Packs 不會自行建立背景排程，也不會在未確認時安裝 Optional Pack。

## Manifest

`manifest/packs.json` 是套件清單的來源，包含：

- repository 版本與 schema 版本
- Default Pack 清單
- Pack ID、tier、kind、audience 與來源路徑
- 外部 plugin 的來源 repository、固定版本與相容性文件

新增或修改 Pack 時，必須同步更新 `VERSION`、manifest、CHANGELOG 與相關安裝文件。

## Skills

| Skill | 用途 | 狀態 |
|---|---|---|
| `swqa-automation` | Python、UART/TTY、PCAP、API、CLI、Device 等 SWQA 自動化結構與規則 | Draft |
| `test-failure-triage` | 分層分析 Python、UART、封包、環境、Timing 與 DUT 造成的測試失敗 | Draft |
| `forgejo-integration` | 本地 Git + Forgejo + 可選 Forgejo MCP | Draft |
| `github-integration` | 本地 Git + GitHub CLI/API 基本協作 | Draft |
| `file-toolkit` | 文件、影音與 Python 工具能力檢查 | Draft |
| `browser-automation` | 瀏覽器自動化的安全起始規則 | Draft |
| `lean-code-review` | 審查目前 Git diff 的過度設計與不必要複雜度 | Draft |

## OpenCode Commands

| Command | 用途 |
|---|---|
| `/grill-me` | 使用 Plan Agent 一次一題釐清需求、術語、範圍、Acceptance Criteria 與驗證證據，確認共同理解前不實作 |
| `/other-working-flow` | 查看 `hybrid-workflow` 可用 backend，並在使用者確認後選擇是否委派 Builder |

## hybrid-workflow

`hybrid-workflow` 是 `other` 分類下的可選 workflow，不會取代 OpenCode 內建的 `Plan`、`Build` 或原生模型指派。

可同時安裝下列 backend：

| Agent ID | 用途 |
|---|---|
| `workflow_local_builder` | 泛用本地 AI；導入時選擇本地 provider/model |
| `workflow_local_builder_aeon` | team 28500 預配 DGX Spark/aeon 本地 AI |
| `workflow_local_builder_<backend>` | 使用其他本地 AI backend 執行單一 Builder 任務 |
| `workflow_cloud_cheap_builder` | 導入時選擇低成本雲端 provider/model |

安裝後預設仍使用 OpenCode 原生模型指派。只有在 Plan 完成拆解或 Build 即將開始前，workflow 才會在每次請求詢問是否委派；拒絕時由目前的 Plan/Build agent 繼續執行。

一般使用者優先推薦 `workflow_local_builder`，因為它不假設本地硬體。team 28500 可使用已預先配對 `DGX Spark/aeon` 的 `workflow_local_builder_aeon`。`workflow_cloud_cheap_builder` 則在導入時從本機已配置的雲端模型中詢問使用者選擇。

完整規則請參閱 [hybrid-workflow](packs/other/hybrid-workflow/README.md)。

## FII 2026 ppt-master 版模

`ppt-master` Pack 隨附團隊自有的 FII 2026 Deck 版模，安裝 PPT Master Skill 後可直接導入：

| Deck | 主題 | 用途 |
|---|---|---|
| `fii_2026_bright` | FII 2026 亮色版 | 企業內部報告、客戶簡報、專案提案、年度總結 |
| `fii_2026_dark` | FII 2026 暗色版 | 企業內部報告、客戶提案、專案匯報、教學說明 |

導入指令與索引合併說明見 [packs/ppt-master/decks/](packs/ppt-master/decks/README.md)。

## Token Usage / Observability Pack

`token-usage` 是 **Optional** Pack，不會默認安裝，也不會列為兩個獨立元件。它包含：

- `@ramtinj95/opencode-tokenscope@1.8.1` plugin
- `/tokenscope` command

安裝時從 upstream repository 與 npm 官方來源取得內容；本 Repository 只保留 manifest、安裝指導與相容性文件：

- [Token Usage 安裝指導](packs/token-usage/README.md)
- [TokenScope 相容性與排錯](packs/token-usage/compatibility.md)
- [Pack manifest](manifest/packs.json)

## Team Template Assets

The repository also contains team-maintained template assets under `Other/`.
The current PPT Master template is:

- `Other/ppt-master-template/fii_2026_bright/`
- `fii_2026_bright` FII 2026 Bright deck workspace
- Use the exact workspace root directly with PPT Master, or copy it to PPT Master's `templates/decks/fii_2026_bright/` library path and register it for selector discovery.

See [FII 2026 Bright PPT Master Template](Other/ppt-master-template/fii_2026_bright/README.md) for the workspace contract and validation command.

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
New-Item -ItemType Directory -Force "$HOME\.config\opencode\command" | Out-Null
Copy-Item ".\commands\grill-me.md" "$HOME\.config\opencode\command\grill-me.md" -Force
```

### WSL / Ubuntu / macOS

```bash
mkdir -p ~/.config/opencode/command
cp ./commands/grill-me.md ~/.config/opencode/command/grill-me.md
```

若只希望某個專案使用，複製到：

```text
<project>/.opencode/command/grill-me.md
```

重新啟動 OpenCode 後執行：

```text
/grill-me <想釐清的功能或計畫>
```

`/grill-me` 固定使用 OpenCode Plan Agent，只做訪談與共同理解摘要，不會自行建立程式碼、`CONTEXT.md`、ADR 或其他規格框架。若本 Session 已讀取且檔案未變更，會沿用既有專案資訊，避免重複載入相同 Context。

## 安裝 `/other-working-flow`

OpenCode Custom Command 不使用 `npx skills add`。將 `commands/other-working-flow.md` 複製到全域 Command 目錄：

```powershell
New-Item -ItemType Directory -Force "$HOME\.config\opencode\command" | Out-Null
Copy-Item ".\commands\other-working-flow.md" "$HOME\.config\opencode\command\other-working-flow.md" -Force
```

若只希望某個專案使用，複製到：

```text
<project>/.opencode/command/other-working-flow.md
```

重新啟動 OpenCode 後執行 `/other-working-flow`。全域安裝會讓所有專案都能使用，專案安裝則只在該專案提供 workflow 選擇。

## `test-failure-triage` 的定位

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

## `lean-code-review` 的定位

這個 Skill 只有在使用者明確要求 OpenCode 檢查程式碼是否過度設計時才載入：

```text
檢查這次修改是否過度設計
Review current diff for unnecessary complexity
哪些新增程式碼可以刪除或改用既有功能
```

預設只讀取目前 Git diff、提出建議，不直接套用修改，也不取代 Correctness、Security 或 SWQA 測試驗證。若實際測試失敗的 Root Cause 尚未確認，先使用 `test-failure-triage`，不從 Diff 猜測原因。

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
├── Optional: Forgejo / GitHub
└── Optional: Token Usage / Observability
```

## 授權與來源

本專案依 MIT License 發布。上游來源與刪減原則請參閱 [UPSTREAM.md](UPSTREAM.md)。
