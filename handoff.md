# handoff.md — OpenCode Extension Packs

> 供接手 Session 閱讀的現況摘要。更新時間：2026-09-18

## 目前狀態

| 項目 | 值 |
|---|---|
| 版本 | `0.2.6`（`VERSION`） |
| HEAD | `8d0717d` — fix(pwsh7): quote-preserving pwsh-utf8-wrapper rebuild (0.2.6) |
| 三方同步 | 本地 `main` = GitHub `origin/main` = Forgejo `forgejo/main` = `8d0717d` |
| working tree | clean，無未提交變更 |

## 專案定位

選擇性 Skill / Command / 外部整合（Optional Extension Packs），與 `opencode-essential-core` 分離。
不 fork 或 vendored 第三方原始碼；外部 plugin 只記錄來源、固定版本與安裝指導。
以 `manifest/packs.json` 為套件清單來源，分 `default` / `recommended` / `optional` 三層。

## 目錄結構

- `manifest/packs.json` — 套件清單，schemaVersion 1，version 0.2.6
- `skills/` — OpenCode 原生 Skill：
  `swqa-automation`、`test-failure-triage`、`forgejo-integration`、`github-integration`、`file-toolkit`、`browser-automation`、`lean-code-review`、`hybrid-workflow`
- `commands/` — Custom Commands：`grill-me.md`（default）、`other-working-flow.md`
- `packs/` — Pack 文件與流程：
  `other/hybrid-workflow`、`ppt-master/decks`（FII 2026 版模）、`pwsh7`、`archify`、`playwright-mcp`、`codebase-memory-mcp`、`token-usage`
- `docs/PACK-GUIDE.md` — Skill / Command / Workflow 撰寫最低要求（新增 Pack 時需遵守）
- `UPSTREAM.md` — 上游來源與刪減原則（lazy-packs、andrej-karpathy-skills、ponytail、mattpocock/skills、obra/superpowers）

## 最近變更（0.2.4 → 0.2.6）

1. **0.2.6（9/18）**：修正 `pwsh7/pwsh-utf8-wrapper.cs` 引號保真問題；重建 5,120-byte `pwsh-utf8-wrapper.exe`（SHA256 `906AA0...`），取代 release 中兩個 deprecated 4,608-byte assets。同步 README、manifest、`packs/pwsh7/README.md`。
2. **0.2.5（9/16）**：註冊 `archify` external-skill pack，pin `tt-a1i/archify@v2.16.0`。
3. **0.2.4（9/16）**：註冊 `pwsh7` tooling pack；刷新外部 pin：`ppt-master@v6.4.0`、`playwright-mcp@0.0.81`、`codebase-memory-mcp@0.11.0`。

## 目前 Pin（外部來源）

- `ppt-master` → `hugohe3/ppt-master@v6.4.0`
- `archify` → `tt-a1i/archify@v2.16.0`
- `playwright-mcp` → microsoft/playwright-mcp，`@playwright/mcp@0.0.81`
- `codebase-memory-mcp` → DeusData/codebase-memory-mcp，`codebase-memory-mcp@0.11.0`
- `token-usage` → ramtinJ95/opencode-tokenscope，`@ramtinj95/opencode-tokenscope@1.8.1`

## 發布與驗證

- `release/pwsh7-v7.4.6` 已更新：單一 fix 版 `pwsh-utf8-wrapper.exe`（5,120 bytes，SHA256 `906AA017CDE4F36DB2BC5D3C38C976E76035E0E40703DB30D81F4F5F7F9FB8F0`，見 `packs/pwsh7/README.md`）。
- 變更流程慣例：改動時同步更新 `VERSION`、`manifest/packs.json`、`CHANGELOG.md`、相關 README / Pack 文件。

## 待辦 / 注意事項

- 無已知未完成項目；推送到開源 GitHub 前先跑 secret scan（`docs/PACK-GUIDE.md` 規則 6）。
- 若新增外部整合，同時更新 manifest pin 與對應 `packs/<id>/compatibility.md`。
- `/teamwork-update-check`（Essential Core）以本 repo `main` 的 manifest raw 網址作為更新比對來源。