# handoff.md — OpenCode Extension Packs

> 供接手 Session 閱讀的現況摘要。更新時間：2026-09-23

## 目前狀態

| 項目 | 值 |
|---|---|
| 版本 | `2.0.16`（`VERSION` / `manifest/packs.json`；基於 OpenCode v2.0.14 驗證。上一版 `2.0.15`） |
| HEAD | `main`（2.0.16：`token-monitor` pin 升 `v0.61.0` 並於 Ubuntu 重新驗證、Linux 使用說明；前次 2.0.15：pwsh7 強制替代、全域規則模板、註冊 `token-monitor` 並完成 Ubuntu 驗證；實際 SHA 以 `git log -1` 為準） |
| 相容性 | **僅支援 OpenCode v2.x.x**——v1 慣例（agent `permission` map、`bash`/`task` action、單數 `command/`、`mcp` 直掛 server 名、`enabled` 欄位）已全部移除 |
| 三方同步 | 本地 `main` = GitHub `origin/main` = Forgejo `forgejo/main`（`6543738`）；推送順序固定 origin → forgejo。Forgejo remote：`http://192.168.23.167:3000/BBDU28500/opencode-extension-packs.git`（repo 在 Forgejo 上屬 `BBDU28500`） |
| working tree | clean，三方 `main` 一致 |

## 專案定位

選擇性 Skill / Command / 外部整合（Optional Extension Packs），與 `opencode-essential-core` 分離。
不 fork 或 vendored 第三方原始碼；外部 plugin 只記錄來源、固定版本與安裝指導。
以 `manifest/packs.json` 為套件清單來源，分 `default` / `recommended` / `optional` 三層。

## 目錄結構

- `manifest/packs.json` — 套件清單，schemaVersion 1，version 2.0.16
- `skills/` — OpenCode 原生 Skill：
  `swqa-automation`、`test-failure-triage`、`forgejo-integration`、`github-integration`、`file-toolkit`、`browser-automation`、`lean-code-review`、`local-llm-dispatch-policy`
- `commands/` — Custom Commands：`grill-me.md`（default）
- `packs/` — Pack 文件與流程：
  `ppt-master/decks`（FII 2026 版模）、`pwsh7`、`linux-flatpak-deploy`（Ubuntu flatpak/AppImage 部署 runbook）、`archify`、`playwright-mcp`、`codebase-memory-mcp`、`token-monitor`
- `docs/PACK-GUIDE.md` — Skill / Command 撰寫最低要求（新增 Pack 時需遵守）
- `UPSTREAM.md` — 上游來源與刪減原則（lazy-packs、andrej-karpathy-skills、ponytail、mattpocock/skills、obra/superpowers）

## 最近變更（0.2.6 → 2.0.16）

1. **2.0.16（9/23）**：`token-monitor` pin 由 `v0.60.0` 升為 **`v0.61.0`**（依 Update Policy 於 Ubuntu desktop 重新驗證：AppImage sha512 相符、FUSE 直接啟動、視窗 `340x650`、引擎 OpenCode messages 651、anchor today 38.2M／$0.6201；Windows 端 `v0.61.0` 尚未重新驗證，仍沿用 `v0.60.0` 證據）。同步更新 `packs/token-monitor/README.md`（Release 與下載版本）、`compatibility.md`（Release、Verification Status、Ubuntu `v0.61.0` 證據段、Linux Notes 檔名）、README pin 表與本文件。版號 `2.0.15` → `2.0.16`。
2. **2.0.15（9/23）**：`pwsh7` 由「可選便利」改為 Windows 主機的**強制替代**——Windows 10 內建只有 5.1，必須以 pack 的 7.4.6 取代；Windows 11 內建同樣只有 5.1，若 `pwsh` 不存在或版本低於 pack 提供版本（7.4.6）亦必須取代（`packs/pwsh7/README.md` 新增 Mandate 段：需求表、禁止寫法、`7.4.6 / utf-8` 驗證指令、例外須先取得同意）。新增 `packs/pwsh7/templates/global-AGENTS.md`：可直接複製到 `%USERPROFILE%\.config\opencode\AGENTS.md` 的全域規則（明訂適用所有 session 與 repo、與專案層衝突時以全域為準），並在 pack README 檔案表、Notes 與 README 外部整合段引用。manifest `pwsh7` description 同步改為強制語意；版號 `2.0.14` → `2.0.15`。動機實例：Windows PowerShell 5.1（zh-TW Big5）無法解析「UTF-8 無 BOM + 中文」的 `.ps1`（`ParserError: The string is missing the terminator`），pwsh7 執行同檔正常。同次亦註冊 `token-monitor` 為新的 `external-tool` pack（pin `Javis603/token-monitor@v0.60.0`）：Token Monitor 是第三方 Electron 桌面 widget，只讀取本機用量資料檔、不設定 OpenCode，因此與已移除的 `token-usage`（TokenScope，僅 OpenCode v1）不同，不受 v1／v2 差異影響；`packs/token-monitor/README.md` 收錄 Windows 10/11 完整部署流程（前置條件、安裝路徑、驗證、解除安裝），`packs/token-monitor/compatibility.md` 收錄實測矩陣與原始證據。實測平台為 Windows 10 IoT Enterprise LTSC 2021（19044 / 21H2，**無 WSL**），portable、NSIS 安裝版與 headless agent 三條路徑皆通過；**Ubuntu desktop（x64, X11, glibc）亦已完成驗證**（AppImage 以 `--appimage-extract` 啟動、widget 視窗 `340x650`、OpenCode messages 非零、WSL 正確回報不存在），`packs/token-monitor/compatibility.md`、`packs/token-monitor/README.md`（新增 Linux 前置條件與安裝段）與本文件已同步。README 的 Pack 表同步改為 15 個（Default 1／Recommended 5／Optional 9）。
3. **2.0.14（9/23）**：版號對齊 OpenCode 2.0.14（過渡的 `2.0.13-dev` 未發布）；文件標明 OpenCode v2 實際的 Skill discovery 目錄——原生全域 `~/.config/opencode/skills/` 與相容全域 `~/.agents/skills/`（`npx skills add -g -a opencode` 的實際落點）皆會被載入，專案端 `.opencode/skills/` 與 `.agents/skills/` 同理；同步更新 README 安裝段、`packs/ppt-master/decks/README.md`。
4. **2.0.12（9/22）**：改為僅支援 OpenCode v2——移除 `token-usage` pack（上游 TokenScope 僅驗證 v1.17.18、安裝指南用單數 `command/`，不適用 v2 視窗版）；README 安裝路徑單數 `command/` → `commands/`（6 處，符合 PACK-GUIDE 規則 8）；3 個 hybrid-workflow agent 模板改 v2 `permissions` 清單制（`task`→`subagent`、`bash`→`shell`）；playwright-mcp／codebase-memory-mcp 安裝指南改 v2 `mcp.servers` 格式、移除 `enabled`、`/mcp`→`/mcps`；外部 pin 刷新：ppt-master v6.6.0、@playwright/mcp 0.0.82（archify v2.16.0、codebase-memory-mcp 0.11.0 檢視時已最新）；pwsh7 維持 7.4.6（zip／hash／wrapper 綁定，上游 7.6.6 記為待升級）。版號方案改為「版號 = 該次檢討時基於驗證的 OpenCode 版本」（本次 2.0.12）；其後補做 8 個 SKILL.md 與 2 個 command 的 v2 逐條 review（PACK-GUIDE 全條通過），6 個 skill description 補上觸發情境；README 新增「外部整合（External Packs）」總覽表；同日再移除 `hybrid-workflow` Pack 與 `/other-working-flow` command（理由：OpenCode v2 已內建多 agent 分派——`primary`/`subagent`、Task tool、依 description 自動呼叫、`@` 指名、child session、`subagent` 權限、command `subtask`），改以原生機制達成，README 補上原生委派與 `permissions` 取代說明；同日新增 `local-llm-dispatch-policy` Skill（tier optional），把原 hybrid-workflow 的本地算力調用經驗收斂成純原則（Orchestrator＝角色、可為本地 light-weight 或雲端，heavy-Builder＝本地 heavy-duty；一請求一決策、線性 dispatch、3-strike、c1 goal-scoped 雲端接手／c2 直接接手），依賴 v2 原生 subagent/`permissions`、不重造框架；同時把 README 原生委派範例由 v1 `permission` map（`task`）更正為 v2 `permissions` 清單（`subagent`）並改稱 `heavy-builder`；同次再重排 README 首頁為「目錄結構／分層安裝／Pack 清單（Default/Recommended/Optional）／外部整合／安裝／運作說明／Manifest／Core 關係」分層結構，集中安裝與深度說明、消除重複（狀態列更新為 v2-only、14 packs＝1/5/8）。另將 `local-llm-dispatch-policy`（skill）與 `pwsh7`（tooling）由 recommended 改列 optional（前者依賴本地 LLM provider 設定、後者綁定打包的 PowerShell 7.4.6 wrapper）；同次補強 `/grill-me`（納入上游 ungrillable→先原型驗證、使用者擁有範圍並可反駁、以「已無未處理分支」為結束條件；禁寫清單加入 plan 檔），並在 `UPSTREAM.md` 記錄。
5. **0.2.6（9/18）**：修正 `pwsh7/pwsh-utf8-wrapper.cs` 引號保真問題；重建 5,120-byte `pwsh-utf8-wrapper.exe`（SHA256 `906AA0...`），取代 release 中兩個 deprecated 4,608-byte assets。同步 README、manifest、`packs/pwsh7/README.md`。
6. **0.2.5（9/16）**：註冊 `archify` external-skill pack，pin `tt-a1i/archify@v2.16.0`。
7. **0.2.4（9/16）**：註冊 `pwsh7` tooling pack；刷新外部 pin：`ppt-master@v6.4.0`、`playwright-mcp@0.0.81`、`codebase-memory-mcp@0.11.0`。
8. **2.0.16-docs（9/23）**：新增 `linux-flatpak-deploy` tooling pack（[packs/linux-flatpak-deploy/](packs/linux-flatpak-deploy/README.md)、[compatibility.md](packs/linux-flatpak-deploy/compatibility.md)）——收錄本機 Ubuntu 24.04.5 desktop（GNOME X11、Flatpak 1.14.6）上「flathub 修復 → Gear Lever 安裝 → AppImage／Gear Lever 圖示修正」全鏈經驗，版號沿用 `2.0.16`（pure docs/runbook 增補，不改變 OpenCode 驗證版本）。三問題根因與解法：(1) flathub `no summary found`＝base URL 決定 summary 路徑，`flathub.org/summary.idx` 回 404、`dl.flathub.org/repo/summary.idx` 回 200，用 `.flatpakrepo` 把 base 固定成 `/repo/`（注意 `remote-add --force-toggle` 的 `--force-toggle` 對 `remote-add` 非法）；(2) Gear Lever（`it.mijorus.gearlever`）裝好後選單看不到＝`XDG_DATA_DIRS` 不含 flatpak exports，寫入 `/etc/environment` 後必須 logout/relogin（PAM 在登入時讀；`Alt+F2:r` 不生效）；(3) Token Monitor AppImage 齒輪分兩層——選單齒輪是 `.desktop` `Icon=` 指向不存在路徑（改絕對路徑至 `~/.local/share/icons`），dock 齒輪是 `StartupWMClass=Token Monitor` 與實際視窗 class `token-monitor` 不匹配（`wmctrl -lx` 破案，對齊後徹底重開程式）。新增 dsh（本機本地 session：agent sandbox 的 `no_new_privs` 擋 sudo、擋 home flatpak 路徑寫入，privileged/home edits 由用戶在本機執行）與 opencode（遠端）佈署對照表；同步更新 README（Pack 數 15→16、Optional 9→10、關係樹與備註段）、manifest（新增 optional tooling pack）、CHANGELOG。

## 部署經驗（dsh vs opencode）

兩次都在同一台 Ubuntu 24.04.5 desktop 完成「flatpak → Gear Lever / AppImage → 圖示」鏈，但**權限模型決定 system mode vs user mode**（完整對照見 [packs/linux-flatpak-deploy/README.md](packs/linux-flatpak-deploy/README.md) 的「dsh vs opencode 佈署對照」）：

| 維度 | dsh（本機本地 session） | opencode（受限主機） |
|---|---|---|
| root / sudo | **用戶 terminal 的 sudo 可用**（system mode flatpak、`/etc/environment`） | **`sudo` 不可用**——`NoNewPrivs: 1`，被迫 user mode + `--appimage-extract` |
| flatpak 模式 | system mode（`/var/lib/flatpak`） | user mode（`--user`、`~/.local/flatpak-env`；無 `/var/lib/flatpak`） |
| 讀取／診斷 | 強：直接讀 `/proc`、`/etc`、repo、跑 `curl`／`wmctrl`／`xwininfo` | 同左（本機執行指令） |
| agent 寫入 | harness sandbox `no_new_privs` 擋 agent 自行 sudo／寫 home flatpak 路徑 → privileged/home edits 由用戶執行 | 用戶遠端操作（具體連線／權限模型以該 session 為準） |
| 本次特別貢獻 | flathub summary 修復、Gear Lever system-mode 安裝與 `/etc/environment` 選單、Token Monitor dock `StartupWMClass` 修正（`wmctrl` 找出 class=`token-monitor`） | Token Monitor Ubuntu AppImage 可行性 + Gear Lever user-mode 安裝已 commit+push（`packs/token-monitor/compatibility.md`、README/handoff「Ubuntu desktop 實測經驗」） |

結論：兩種方式都能把應用程式部署到同一台 Linux desktop，但**能否用 system mode 取決於 sudo 是否可用**。dsh 強在本機直接診斷（summary 路徑拼法、`wmctrl` 抓 class），privileged/home 編輯交由用戶本機 terminal；opencode 場次受 `NoNewPrivs` 限制只能 user mode，已落檔 AppImage 可行性驗證；本 runbook 補上 system mode 下的 flathub summary 修復、Gear Lever 安裝與 `/etc/environment`、AppImage dock `StartupWMClass` 前一場未涵蓋部分。

## 目前 Pin（外部來源）

- `ppt-master` → `hugohe3/ppt-master@v6.6.0`
- `archify` → `tt-a1i/archify@v2.16.0`
- `playwright-mcp` → microsoft/playwright-mcp，`@playwright/mcp@0.0.82`
- `codebase-memory-mcp` → DeusData/codebase-memory-mcp，`codebase-memory-mcp@0.11.0`
- `pwsh7` → PowerShell 7.4.6（packaged release 綁定；上游最新 7.6.6，升級前需重建 zip／hash／wrapper 驗證）
- `token-monitor` → `Javis603/token-monitor@v0.61.0`（`external-tool`；Ubuntu desktop（x64, X11, glibc）已驗證 `v0.60.0` 與 `v0.61.0`；Windows 10 IoT Enterprise LTSC 2021（無 WSL）已於 2026-09-23 由 App 內建 updater 更新至 `v0.61.0`）
- （已移除）`token-usage` → 原 `ramtinJ95/opencode-tokenscope@1.8.1`，2.0.12 移除（僅適用 OpenCode v1）

## 發布與驗證

- `release/pwsh7-v7.4.6` 已更新：單一 fix 版 `pwsh-utf8-wrapper.exe`（5,120 bytes，SHA256 `906AA017CDE4F36DB2BC5D3C38C976E76035E0E40703DB30D81F4F5F7F9FB8F0`，見 `packs/pwsh7/README.md`）。
- `v2.0.16` 已於 2026-09-23 發布：GitHub 與 Forgejo 皆有 `v2.0.16` release（notes 取自 CHANGELOG 2.0.16），annotated tag `v2.0.16` → `05c8aeb`，三方 tag 一致。
- `v2.0.15` 已於 2026-09-23 發布：GitHub 與 Forgejo 皆有 `v2.0.15` release（notes 取自 CHANGELOG 2.0.15），annotated tag `v2.0.15` → `b830984`，三方 tag 一致。
- `v2.0.14` 已於 2026-09-23 發布：GitHub 與 Forgejo 皆有 `v2.0.14` release（notes 取自 CHANGELOG 2.0.14），annotated tag `v2.0.14` → `54e8bd2`，三方 tag 一致。
- `v2.0.12` 已於 2026-09-22 發布：GitHub 與 Forgejo 皆有 `v2.0.12` release（notes 取自 CHANGELOG 2.0.12），annotated tag `v2.0.12` → `2d39145`，三方 tag 一致。
- 變更流程慣例：改動時同步更新 `VERSION`、`manifest/packs.json`、`CHANGELOG.md`、相關 README / Pack 文件。
- 憑證：GitHub 與 Forgejo token 存於 `~/.git-credentials`（600），不入 repo、不寫入任何追蹤檔案；Ubuntu 桌面工作機亦已完成設定並可推送（GitHub `origin`、Forgejo `forgejo`）。

## Ubuntu desktop 實測經驗（2026-09-23）

於 Ubuntu 24.04.5 LTS（x86_64、glibc 2.39、X11 `DISPLAY=:0`）工作機實測。該主機為**受限主機**：`grep NoNewPrivs /proc/self/status` → `NoNewPrivs: 1`，setuid 提權（含 `sudo`、`fusermount`）失效。

### (a) Packs 範圍：Token Monitor 在此主機的實際安裝路徑

| 觀察 | 證據 |
|---|---|
| `sudo` 不可用 → 無法安裝 `libfuse2` | `sudo: The "no new privileges" flag is set, which prevents sudo from running as root.`；Ubuntu 24.04 的套件名為 **`libfuse2t64`** |
| 系統無 FUSE 2 函式庫 | `ldconfig -p \| grep libfuse.so.2` 無結果（僅有 `libfuse3`） |
| 自備使用者版 `libfuse.so.2`（deb 解到 `~/.local/lib/fuse2/`）仍無法掛載 | `fusermount: mount failed: Operation not permitted`；`/dev/fuse` 為 `crw-rw-rw-`、`fusermount3` 為 setuid root，但在 `NoNewPrivs` 下均無效 |
| 改用 `--appimage-extract` + `squashfs-root/AppRun` | 成功：Electron 程序 8 個、`xwininfo` 視窗 `"Token Monitor"` `340x650`、內附 `@tokscale/cli-linux-x64-gnu` 回報 OpenCode `messages: 141`（非零）、`collector-anchor.json` today 5,240,366 tokens／$0.0721（與畫面一致） |
| 完整性 | size `155094683`、sha512 `IHy/Qg4O…cg==`，皆與 `latest-linux.yml` 相符 |

結論：在受限主機上，`--appimage-extract` **不是替代選項而是唯一可行路徑**。此經驗已寫入 README 的「Ubuntu desktop（實測注意事項）」。

驗證技巧：GTK4 應用在 X11 下直接擷取會得到全黑畫面，需 `GSK_RENDERER=cairo`（必要時加 `LIBGL_ALWAYS_SOFTWARE=1`）；Electron 應用（如 Token Monitor）不受影響。

### (b) 非本 repo 範圍：Flatpak user 模式與 Gear Lever

> 不屬本 Repository 任何 Pack，僅記錄同機環境經驗，供同類主機參考。

- **前提**：`bubblewrap 0.9.0` 已安裝，`unshare -U` 與 `unshare -rm` 均成功；但 `sudo` 因 `NoNewPrivs` 不可用 → 由 deb 解出 `flatpak`／`libostree-1-1` 至 `~/.local/flatpak-env/`，以 `~/.local/bin/flatpak` 包裝檔提供（`LD_LIBRARY_PATH`／`XDG_DATA_DIRS`／`GI_TYPELIB_PATH`）。**僅能 `--user`**，`/var/lib/flatpak` 不存在。
- **驗證**：`flatpak --version` → `1.14.6`；`remote-add flathub` 成功；`remote-ls`／`remote-info` 正常；`install --no-deps` 完成並出現在 `flatpak --user list`。
- **速度**：flathub CDN 實測約 **25 MB/s**（同機 GitHub Releases 僅 50–90 KB/s）。Gear Lever `it.mijorus.gearlever` `4.6.2`（app 11.6 MB ＋ `org.gnome.Platform//50` 419.7 MB ＋ GL／codecs／theme）**67 秒**完成；`xwininfo` 視窗 `"Gear lever"` `750x750` 正常渲染（zh-TW 介面）。
- **限制**：無 `xdg-desktop-portal` → `org.freedesktop.portal.Flatpak was not provided by any .service files`（核心功能不受影響）；GTK4 擷取需 `GSK_RENDERER=cairo`。

### 後續建議

- 若團隊 Ubuntu 主機普遍無 root 或 FUSE 掛載受限，可在下一版把「受限主機路徑」正式寫入 `packs/token-monitor/compatibility.md` 的 Verification Status／Linux Notes（本次僅文件層 README／handoff，**不 bump 版號**）。
- 本次未動 `VERSION`、`manifest/packs.json`、`CHANGELOG.md`。

## 待辦 / 注意事項

- **`token-monitor` 已完成 Windows 與 Ubuntu desktop 雙平台驗證**：Windows 10 IoT Enterprise LTSC 2021（無 WSL）通過 portable／NSIS／headless；Ubuntu desktop（x64, X11, glibc）通過 AppImage（`--appimage-extract` 啟動、widget 視窗、OpenCode messages 非零、WSL 回報不存在）。`packs/token-monitor/compatibility.md`（Verification Status 與證據）、`packs/token-monitor/README.md`（Linux 前置條件與安裝段）與本文件 Pin 備註均已更新。macOS 仍為 upstream 支援、未由團隊驗證。
- 推送到開源 GitHub 前先跑 secret scan（`docs/PACK-GUIDE.md` 規則 6）。
- 若新增外部整合，同時更新 manifest pin 與對應 `packs/<id>/compatibility.md`。
- `/teamwork-update-check`（Essential Core）以本 repo `main` 的 manifest raw 網址作為更新比對來源。
- v2 agent frontmatter 的 permission 寫法以官方 Permissions／Agents 文件為準（`permissions` 清單制）；`opencode.ai/config.json` schema 目前仍顯示 v1 `permission` map，屬 schema 滯後，勿以 schema 為依據。
