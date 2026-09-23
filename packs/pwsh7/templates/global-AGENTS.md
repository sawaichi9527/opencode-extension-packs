# pwsh7 template — global AGENTS.md snippet

Copy the block below into the **global** OpenCode instructions file:

```text
%USERPROFILE%\.config\opencode\AGENTS.md      (Windows)
~/.config/opencode/AGENTS.md                  (WSL / Linux / macOS)
```

OpenCode loads that file for **every session and every working directory**, so the rule applies to all projects on the host — not just one repository. If a project-level `AGENTS.md` still shows `powershell ...` examples, the global rule wins; fix the project copy as well.

---

# OpenCode 全域規則（%USERPROFILE%\.config\opencode\AGENTS.md）

## 適用範圍（全域）

本檔是 OpenCode 的**全域 instructions**，路徑固定在 `%USERPROFILE%\.config\opencode\AGENTS.md`；OpenCode 啟動時不依工作目錄載入它，因此：

- 適用於**所有 session、所有 repo／工作目錄**（本機 Windows 10/11 主機）。
- 與專案層（`<repo>\AGENTS.md`）規則**同時載入**；若專案層有舊寫法（例如範例仍寫 `powershell ...`）與本檔衝突，**以本檔為準**，並請順手在該專案的文件中改正。
- 新增專案或 clone 新 repo 時不需額外設定即生效。

## PowerShell：一律使用 pwsh7，禁止內建 powershell.exe 5.1

**規則**：本機任何 PowerShell 操作（執行腳本、查詢、管線、產出檔案）一律使用 **opencode-extension-packs 安裝的 pwsh7**；**不得**使用 Windows 內建 `powershell.exe`（Windows PowerShell 5.1）。

- 禁止形式（任何路徑寫法皆同）：`powershell ...`、`powershell.exe ...`、`%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe ...`
- 正確形式（opencode shell 已由 `opencode.jsonc` 的 `shell` 指向 UTF-8 wrapper，一般直接寫指令即可）：

```powershell
# 一般（PATH 上有 pwsh7 時）
pwsh -NoProfile -ExecutionPolicy Bypass -File <script.ps1> [args]

# 明確指定 pack 安裝路徑（PATH 不確定時）
& "$env:USERPROFILE\.config\opencode\pwsh7\pwsh.exe" -NoProfile -ExecutionPolicy Bypass -File <script.ps1> [args]
```

- 文件、註解、交接說明中的指令範例，一律寫 `pwsh`，**不要**再寫 `powershell`。

**理由（實測）**：內建 5.1 是 .NET Framework 4.5 且 zh-TW 預設 Big5；執行「UTF-8 無 BOM + 含中文」的 `.ps1` 會解析失敗：

```text
At <script>.ps1:107 char:73
The string is missing the terminator: ".
CategoryInfo: ParserError
```

pwsh7（7.4.6，.NET 8.0 LTS）提供 UTF-8 主控台與現代語法。

**驗證 pwsh7 生效**：

```powershell
pwsh -NoProfile -Command '$PSVersionTable.PSVersion.ToString(); [Console]::OutputEncoding.WebName'
# 預期：7.4.6 / utf-8
```

**例外處理**：若確實需要只有 Windows PowerShell 5.1 才有的模組或行為（例如部分 COM / 舊式 Windows PowerShell 專用模組），**先說明原因與影響並取得使用者同意**，不得默默退回 5.1；能改寫成 pwsh7 相容者（例如改以 `pwsh` 執行同一支腳本、或修正腳本編碼/語法）優先。

## 安裝來源

- **Windows 版本要求**：Windows 10 內建只有 PowerShell 5.1 → **必須**改用本 pack 提供的 pwsh7（7.4.6）替代；Windows 11 亦僅內建 5.1，若主機上的 `pwsh` **不存在或版本低於 pack 提供版本（7.4.6）**，同樣要**一律以 pack 版本替代**（升級或覆蓋）。
- Pack 文件：`packs/pwsh7/README.md`（本 repository）
- 實體：`%USERPROFILE%\.config\opencode\pwsh7\pwsh.exe`（官方 PowerShell 7.4.6 portable ZIP，免 Admin）
- UTF-8 wrapper：`%USERPROFILE%\.config\opencode\pwsh-utf8-wrapper.exe`（`opencode.jsonc` 的 `shell` 指向此檔；路徑解析順序：pack pwsh7 → wrapper 同層 `pwsh7\` → PATH 上的 `pwsh`）
