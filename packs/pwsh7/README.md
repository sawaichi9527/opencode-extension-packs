# pwsh7 — PowerShell 7.4.6 for Windows 10 (UTF-8)

Alternative to Windows 10 built-in Windows PowerShell 5.1, which lacks UTF-8 support and modern syntax.

## Source

- **Upstream**: https://github.com/PowerShell/PowerShell/releases/tag/v7.4.6
  - Asset: `PowerShell-7.4.6-win-x64.zip` (.NET 8.0 LTS)
  - Bundled from: `~/.config/opencode/pwsh7/` (official ZIP, portable, no MSI, no Admin)
- **Packaged**: `pwsh7-7.4.6-win-x64.zip` via `Compress-Archive` on 2026-08-24
  - Size: 111,172,647 bytes (106 MB)
  - SHA256: `44D6870B9FBC5376A7E541E4B2268AC97A9C7ABE438B91F5B3102452413A3713`
  - MD5: `A618D8B18F4B09AA5A317E4CA96B8133`
- **Release**: https://github.com/sawaichi9527/opencode-extension-packs/releases/tag/pwsh7-v7.4.6
  - Assets: `.zip` + `.sha256` + `.md5` + `.txt` (GitHub-computed digest matches)

## Why

Windows 10 `powershell.exe` = 5.1 (.NET Framework 4.5, frozen since 2016, Big5 default). `opencode`'s `bash` tool and `llama.cpp` multilingual output require UTF-8. `pwsh7` (7.4.6, .NET 8.0 LTS) provides `??` / `?.` / ternary / `ForEach-Object -Parallel` and UTF-8 `Console.OutputEncoding`.

Windows 11 still ships 5.1 (see `AGENTS.md §3`), so this pack applies to both Windows 10 and 11.

## Usage — Any Windows User (portable, no hard-coded `C:\Users\Sawaichi`)

1. Download (option A: via this repo, recommended; option B: direct from Microsoft):

```powershell
# A: via extension-packs release (with hash)
Invoke-WebRequest -Uri "https://github.com/sawaichi9527/opencode-extension-packs/releases/download/pwsh7-v7.4.6/pwsh7-7.4.6-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
# B: direct from Microsoft
Invoke-WebRequest -Uri "https://github.com/PowerShell/PowerShell/releases/download/v7.4.6/PowerShell-7.4.6-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
```

2. Unzip to per-user location (no Admin, uses `%USERPROFILE%`):

```powershell
Expand-Archive -Path "$env:TEMP\pwsh7.zip" -DestinationPath "$env:USERPROFILE\.config\opencode\pwsh7" -Force
& "$env:USERPROFILE\.config\opencode\pwsh7\pwsh.exe" -NoProfile -Command '$PSVersionTable'
# Expected: Major 7 Minor 4 Patch 6
Get-FileHash "$env:TEMP\pwsh7.zip" -Algorithm SHA256  # must match 44D6870B...
```

3. Configure `opencode.jsonc` (global `~/.config/opencode/opencode.jsonc`):

```jsonc
{
  "$schema": "https://opencode.ai/config.json",
  "shell": "C:/Users/<you>/.config/opencode/pwsh-utf8-wrapper-v2.exe",
  // wrapper internally resolves %USERPROFILE%\.config\opencode\pwsh7\pwsh.exe + chcp 65001 + UTF8
  // Do NOT use hard-coded C:\Users\Sawaichi\...
}
```

4. UTF-8 wrapper (required, otherwise Big5 garble):

- Source: `packs/pwsh7/pwsh-utf8-wrapper.cs` (C# , flexible `USERPROFILE` → relative `pwsh7\pwsh.exe` → `pwsh` fallback)
- Binary: `pwsh-utf8-wrapper-v2.exe` (4,608 bytes, compiled via `csc.exe /nologo`, see `AGENTS.md § Platform spec`)
- Build: `csc.exe /nologo /out:"%USERPROFILE%\.config\opencode\pwsh-utf8-wrapper-v2.exe" pwsh-utf8-wrapper.cs`
- Also available as release asset on `pwsh7-v7.4.6` (optional)

5. Restart OpenCode and verify:

```powershell
Write-Host '測試中文 UTF-8：測試通過'; $PSVersionTable.PSVersion; [Console]::OutputEncoding.EncodingName
# Expected: 7.4.6 / Unicode (UTF-8) / Chinese displayed correctly
```

## AGENTS.md

Global spec lives in `~/.config/opencode/AGENTS.md` § Platform spec + §1-3 (origin, deployment guide, Windows 11 vs pwsh7). It mandates `pwsh7` for all Windows 10 PowerShell operations and `shell: pwsh-utf8-wrapper-v2.exe` for any user (`%USERPROFILE%`).

## Notes

- `pwsh7` coexists side-by-side with 5.1 (`powershell.exe` vs `pwsh.exe`, separate `PSModulePath`/`PROFILE`).
- Windows 11 still needs this pack (see AGENTS.md §3, Microsoft Learn).
