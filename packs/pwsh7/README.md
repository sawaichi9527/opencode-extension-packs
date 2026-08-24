# pwsh7 — PowerShell 7.4.6 for Windows 10/11 (UTF-8)

Alternative to built-in Windows PowerShell 5.1, which lacks UTF-8 support and modern syntax.

## Source

- **Upstream**: https://github.com/PowerShell/PowerShell/releases/tag/v7.4.6
  - Asset: `PowerShell-7.4.6-win-x64.zip` (.NET 8.0 LTS)
  - Bundled from: `%USERPROFILE%\.config\opencode\pwsh7\` (official ZIP, portable, no MSI, no Admin)
- **Packaged**: `pwsh7-7.4.6-win-x64.zip` via `Compress-Archive` on 2026-08-24
  - Size: 111,172,647 bytes (106 MB)
  - SHA256: `44D6870B9FBC5376A7E541E4B2268AC97A9C7ABE438B91F5B3102452413A3713`
  - MD5: `A618D8B18F4B09AA5A317E4CA96B8133`
- **Release**: https://github.com/sawaichi9527/opencode-extension-packs/releases/tag/pwsh7-v7.4.6
  - Assets: `pwsh7-7.4.6-win-x64.zip` + `.sha256` + `.md5` + `.txt` + **`pwsh-utf8-wrapper.exe`** (prebuilt, 4,608 bytes, SHA256 `FF5E34E917196322399F082889CA06CB35ECBC44601C751DA831FBD5D04014E4`)

## Why

Windows 10 (and 11) `powershell.exe` = 5.1 (.NET Framework 4.5, frozen since 2016, Big5 default on zh-TW locales). `opencode`'s `bash` tool and `llama.cpp` multilingual output require UTF-8. `pwsh7` (7.4.6, .NET 8.0 LTS) provides `??` / `?.` / ternary / `ForEach-Object -Parallel` and UTF-8 `Console.OutputEncoding`.

Windows 11 still ships only 5.1 built-in — this pack applies to both Windows 10 and 11.

## Usage — Any Windows User (portable, no hard-coded paths)

1. Download (option A: via this repo release, recommended; option B: direct from Microsoft):

```powershell
# A: via extension-packs release (with hash)
Invoke-WebRequest -Uri "https://github.com/sawaichi9527/opencode-extension-packs/releases/download/pwsh7-v7.4.6/pwsh7-7.4.6-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
# B: direct from Microsoft
Invoke-WebRequest -Uri "https://github.com/PowerShell/PowerShell/releases/download/v7.4.6/PowerShell-7.4.6-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
```

2. Unzip to per-user location (no Admin):

```powershell
Expand-Archive -Path "$env:TEMP\pwsh7.zip" -DestinationPath "$env:USERPROFILE\.config\opencode\pwsh7" -Force
& "$env:USERPROFILE\.config\opencode\pwsh7\pwsh.exe" -NoProfile -Command '$PSVersionTable'
# Expected: Major 7 Minor 4 Patch 6 PSEdition Core
Get-FileHash "$env:TEMP\pwsh7.zip" -Algorithm SHA256  # must match 44D6870B...
```

3. Install the UTF-8 wrapper (required — plain pwsh7 spawns with Big5 console encoding and garbles CJK output):

```powershell
# Option A: download prebuilt exe from this release
Invoke-WebRequest -Uri "https://github.com/sawaichi9527/opencode-extension-packs/releases/download/pwsh7-v7.4.6/pwsh-utf8-wrapper.exe" -OutFile "$env:USERPROFILE\.config\opencode\pwsh-utf8-wrapper.exe"
# Option B: compile from source (packs/pwsh7/pwsh-utf8-wrapper.cs) with any Windows 10/11 built-in csc
& "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\csc.exe" /nologo /out:"$env:USERPROFILE\.config\opencode\pwsh-utf8-wrapper.exe" pwsh-utf8-wrapper.cs
```

Wrapper path resolution order (any user works, nothing hard-coded):
1. `%USERPROFILE%\.config\opencode\pwsh7\pwsh.exe`
2. `<wrapper dir>\pwsh7\pwsh.exe` (portable layout)
3. `pwsh` on PATH

4. Configure `opencode.jsonc` (global `~/.config/opencode/opencode.jsonc`) — use forward slashes:

```jsonc
{
  "$schema": "https://opencode.ai/config.json",
  "shell": "C:/Users/<you>/.config/opencode/pwsh-utf8-wrapper.exe"
}
```

5. Restart OpenCode and verify:

```powershell
Write-Host '測試中文 UTF-8：測試通過'; $PSVersionTable.PSVersion; [Console]::OutputEncoding.EncodingName
# Expected: profile loaded UTF8 / 7.4.6 / Unicode (UTF-8) / Chinese displayed correctly
```

## Files in this pack

| File | Purpose |
|---|---|
| `pwsh-utf8-wrapper.cs` | Wrapper source (chcp 65001 + UTF-8 console → forward to pwsh7; flexible path resolution) |
| `shell-utf8.cmd` | cmd-entry fallback wrapper for manual/debug invocation (`cmd /c`), not for opencode.jsonc (`shell` must be an `.exe`) |
| Prebuilt `pwsh-utf8-wrapper.exe` | Release asset — skip compiling |

## Notes

- `pwsh7` coexists side-by-side with 5.1 (`powershell.exe` vs `pwsh.exe`, separate `PSModulePath`/`PROFILE`).
- Optional per-profile UTF-8 hardening: create `%USERPROFILE%\Documents\PowerShell\Microsoft.PowerShell_profile.ps1` containing `[Console]::OutputEncoding = [System.Text.Encoding]::UTF8`.
- Global agent spec lives at `%USERPROFILE%\.config\opencode\AGENTS.md`: mandates pwsh7 for all PowerShell operations on Windows 10/11 opencode hosts.
