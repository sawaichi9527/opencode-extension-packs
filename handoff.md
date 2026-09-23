# handoff.md — OpenCode Extension Packs

> 供接手 Session 閱讀的現況摘要。更新時間：2026-09-23

## 目前狀態

| 項目 | 值 |
|---|---|
| 版本 | `2.0.15-dev`（`VERSION` / `manifest/packs.json`；基於 OpenCode v2.0.14 驗證；正式版目前為 `v2.0.14`） |
| HEAD | `main`（2.0.15-dev：pwsh7 改為 Windows 主機強制替代、新增全域規則模板；同次註冊 `token-monitor` external-tool pack；前次 2.0.14：版號對齊 OpenCode 2.0.14；實際 SHA 以 `git log -1` 為準） |
| 相容性 | **僅支援 OpenCode v2.x.x**——v1 慣例（agent `permission` map、`bash`/`task` action、單數 `command/`、`mcp` 直掛 server 名、`enabled` 欄位）已全部移除 |
| 三方同步 | 本地 `main` = GitHub `origin/main` = Forgejo `forgejo/main`；推送順序固定 origin → forgejo |
| working tree | clean，無未提交變更（提交後三方 `main` 一致） |

## 專案定位

選擇性 Skill / Command / 外部整合（Optional Extension Packs），與 `opencode-essential-core` 分離。
不 fork 或 vendored 第三方原始碼；外部 plugin 只記錄來源、固定版本與安裝指導。
以 `manifest/packs.json` 為套件清單來源，分 `default` / `recommended` / `optional` 三層。

## 目錄結構

- `manifest/packs.json` — 套件清單，schemaVersion 1，version 2.0.15-dev
- `skills/` — OpenCode 原生 Skill：
  `swqa-automation`、`test-failure-triage`、`forgejo-integration`、`github-integration`、`file-toolkit`、`browser-automation`、`lean-code-review`、`local-llm-dispatch-policy`
- `commands/` — Custom Commands：`grill-me.md`（default）
- `packs/` — Pack 文件與流程：
  `ppt-master/decks`（FII 2026 版模）、`pwsh7`、`archify`、`playwright-mcp`、`codebase-memory-mcp`、`token-monitor`
- `docs/PACK-GUIDE.md` — Skill / Command 撰寫最低要求（新增 Pack 時需遵守）
- `UPSTREAM.md` — 上游來源與刪減原則（lazy-packs、andrej-karpathy-skills、ponytail、mattpocock/skills、obra/superpowers）

## 最近變更（0.2.6 → 2.0.15-dev）

1. **2.0.15-dev（9/23）**：`pwsh7` 由「可選便利」改為 Windows 主機的**強制替代**——Windows 10 內建只有 5.1，必須以 pack 的 7.4.6 取代；Windows 11 內建同樣只有 5.1，若 `pwsh` 不存在或版本低於 pack 提供版本（7.4.6）亦必須取代（`packs/pwsh7/README.md` 新增 Mandate 段：需求表、禁止寫法、`7.4.6 / utf-8` 驗證指令、例外須先取得同意）。新增 `packs/pwsh7/templates/global-AGENTS.md`：可直接複製到 `%USERPROFILE%\.config\opencode\AGENTS.md` 的全域規則（明訂適用所有 session 與 repo、與專案層衝突時以全域為準），並在 pack README 檔案表、Notes 與 README 外部整合段引用。manifest `pwsh7` description 同步改為強制語意；版號 `2.0.14` → `2.0.15-dev`（正式版仍為 `v2.0.14`）。動機實例：Windows PowerShell 5.1（zh-TW Big5）無法解析「UTF-8 無 BOM + 中文」的 `.ps1`（`ParserError: The string is missing the terminator`），pwsh7 執行同檔正常。同次亦註冊 `token-monitor` 為新的 `external-tool` pack（pin `Javis603/token-monitor@v0.60.0`）：Token Monitor 是第三方 Electron 桌面 widget，只讀取本機用量資料檔、不設定 OpenCode，因此與已移除的 `token-usage`（TokenScope，僅 OpenCode v1）不同，不受 v1／v2 差異影響；`packs/token-monitor/README.md` 收錄 Windows 10/11 完整部署流程（前置條件、安裝路徑、驗證、解除安裝），`packs/token-monitor/compatibility.md` 收錄實測矩陣與原始證據。實測平台為 Windows 10 IoT Enterprise LTSC 2021（19044 / 21H2，**無 WSL**），portable、NSIS 安裝版與 headless agent 三條路徑皆通過；**Ubuntu desktop 尚未安裝驗證**，待團隊驗證後更新本文件與該 pack 文件。README 的 Pack 表同步改為 15 個（Default 1／Recommended 5／Optional 9）。
2. **2.0.14（9/23）**：版號對齊 OpenCode 2.0.14（過渡的 `2.0.13-dev` 未發布）；文件標明 OpenCode v2 實際的 Skill discovery 目錄——原生全域 `~/.config/opencode/skills/` 與相容全域 `~/.agents/skills/`（`npx skills add -g -a opencode` 的實際落點）皆會被載入，專案端 `.opencode/skills/` 與 `.agents/skills/` 同理；同步更新 README 安裝段、`packs/ppt-master/decks/README.md`。
2. **2.0.12（9/22）**：改為僅支援 OpenCode v2——移除 `token-usage` pack（上游 TokenScope 僅驗證 v1.17.18、安裝指南用單數 `command/`，不適用 v2 視窗版）；README 安裝路徑單數 `command/` → `commands/`（6 處，符合 PACK-GUIDE 規則 8）；3 個 hybrid-workflow agent 模板改 v2 `permissions` 清單制（`task`→`subagent`、`bash`→`shell`）；playwright-mcp／codebase-memory-mcp 安裝指南改 v2 `mcp.servers` 格式、移除 `enabled`、`/mcp`→`/mcps`；外部 pin 刷新：ppt-master v6.6.0、@playwright/mcp 0.0.82（archify v2.16.0、codebase-memory-mcp 0.11.0 檢視時已最新）；pwsh7 維持 7.4.6（zip／hash／wrapper 綁定，上游 7.6.6 記為待升級）。版號方案改為「版號 = 該次檢討時基於驗證的 OpenCode 版本」（本次 2.0.12）；其後補做 8 個 SKILL.md 與 2 個 command 的 v2 逐條 review（PACK-GUIDE 全條通過），6 個 skill description 補上觸發情境；README 新增「外部整合（External Packs）」總覽表；同日再移除 `hybrid-workflow` Pack 與 `/other-working-flow` command（理由：OpenCode v2 已內建多 agent 分派——`primary`/`subagent`、Task tool、依 description 自動呼叫、`@` 指名、child session、`subagent` 權限、command `subtask`），改以原生機制達成，README 補上原生委派與 `permissions` 取代說明；同日新增 `local-llm-dispatch-policy` Skill（tier optional），把原 hybrid-workflow 的本地算力調用經驗收斂成純原則（Orchestrator＝角色、可為本地 light-weight 或雲端，heavy-Builder＝本地 heavy-duty；一請求一決策、線性 dispatch、3-strike、c1 goal-scoped 雲端接手／c2 直接接手），依賴 v2 原生 subagent/`permissions`、不重造框架；同時把 README 原生委派範例由 v1 `permission` map（`task`）更正為 v2 `permissions` 清單（`subagent`）並改稱 `heavy-builder`；同次再重排 README 首頁為「目錄結構／分層安裝／Pack 清單（Default/Recommended/Optional）／外部整合／安裝／運作說明／Manifest／Core 關係」分層結構，集中安裝與深度說明、消除重複（狀態列更新為 v2-only、14 packs＝1/5/8）。另將 `local-llm-dispatch-policy`（skill）與 `pwsh7`（tooling）由 recommended 改列 optional（前者依賴本地 LLM provider 設定、後者綁定打包的 PowerShell 7.4.6 wrapper）；同次補強 `/grill-me`（納入上游 ungrillable→先原型驗證、使用者擁有範圍並可反駁、以「已無未處理分支」為結束條件；禁寫清單加入 plan 檔），並在 `UPSTREAM.md` 記錄。
3. **0.2.6（9/18）**：修正 `pwsh7/pwsh-utf8-wrapper.cs` 引號保真問題；重建 5,120-byte `pwsh-utf8-wrapper.exe`（SHA256 `906AA0...`），取代 release 中兩個 deprecated 4,608-byte assets。同步 README、manifest、`packs/pwsh7/README.md`。
4. **0.2.5（9/16）**：註冊 `archify` external-skill pack，pin `tt-a1i/archify@v2.16.0`。
5. **0.2.4（9/16）**：註冊 `pwsh7` tooling pack；刷新外部 pin：`ppt-master@v6.4.0`、`playwright-mcp@0.0.81`、`codebase-memory-mcp@0.11.0`。

## 目前 Pin（外部來源）

- `ppt-master` → `hugohe3/ppt-master@v6.6.0`
- `archify` → `tt-a1i/archify@v2.16.0`
- `playwright-mcp` → microsoft/playwright-mcp，`@playwright/mcp@0.0.82`
- `codebase-memory-mcp` → DeusData/codebase-memory-mcp，`codebase-memory-mcp@0.11.0`
- `pwsh7` → PowerShell 7.4.6（packaged release 綁定；上游最新 7.6.6，升級前需重建 zip／hash／wrapper 驗證）
- `token-monitor` → `Javis603/token-monitor@v0.60.0`（`external-tool`；Windows 10 IoT Enterprise LTSC 2021、無 WSL 已驗證 portable／NSIS／headless 三路徑；Ubuntu desktop 待驗證）
- （已移除）`token-usage` → 原 `ramtinJ95/opencode-tokenscope@1.8.1`，2.0.12 移除（僅適用 OpenCode v1）

## 發布與驗證

- `release/pwsh7-v7.4.6` 已更新：單一 fix 版 `pwsh-utf8-wrapper.exe`（5,120 bytes，SHA256 `906AA017CDE4F36DB2BC5D3C38C976E76035E0E40703DB30D81F4F5F7F9FB8F0`，見 `packs/pwsh7/README.md`）。
- `v2.0.14` 已於 2026-09-23 發布：GitHub 與 Forgejo 皆有 `v2.0.14` release（notes 取自 CHANGELOG 2.0.14），annotated tag `v2.0.14` → `54e8bd2`，三方 tag 一致。
- `v2.0.12` 已於 2026-09-22 發布：GitHub 與 Forgejo 皆有 `v2.0.12` release（notes 取自 CHANGELOG 2.0.12），annotated tag `v2.0.12` → `2d39145`，三方 tag 一致。
- 變更流程慣例：改動時同步更新 `VERSION`、`manifest/packs.json`、`CHANGELOG.md`、相關 README / Pack 文件。
- 憑證：GitHub 與 Forgejo token 存於本機 `~/.git-credentials`（600），不入 repo、不寫入任何追蹤檔案。

## 待辦 / 注意事項

- **待驗證：`token-monitor` 尚未在 Ubuntu desktop 安裝過。** 目前只在 Windows 10 IoT Enterprise LTSC 2021（無 WSL）驗證 portable／NSIS／headless 三條路徑；Ubuntu desktop 驗證完成後，需同步更新 `packs/token-monitor/compatibility.md` 的 Verification Status 表、`packs/token-monitor/README.md`（若安裝路徑或前置條件不同）與本文件的 Pin 備註，才可視為跨平台。
- 推送到開源 GitHub 前先跑 secret scan（`docs/PACK-GUIDE.md` 規則 6）。
- 若新增外部整合，同時更新 manifest pin 與對應 `packs/<id>/compatibility.md`。
- `/teamwork-update-check`（Essential Core）以本 repo `main` 的 manifest raw 網址作為更新比對來源。
- v2 agent frontmatter 的 permission 寫法以官方 Permissions／Agents 文件為準（`permissions` 清單制）；`opencode.ai/config.json` schema 目前仍顯示 v1 `permission` map，屬 schema 滯後，勿以 schema 為依據。
