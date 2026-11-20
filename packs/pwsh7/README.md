# pwsh7 — PowerShell 7.6.10 LTS for Windows 10/11 (UTF-8)

Alternative to built-in Windows PowerShell 5.1, which lacks UTF-8 support and modern syntax.

## Source

- **Upstream**: https://github.com/PowerShell/PowerShell/releases/tag/v7.6.10
  - Asset: `PowerShell-7.6.10-win-x64.zip` (.NET 10.0 LTS)
  - Bundled from: official ZIP (portable, no MSI, no Admin)
- **Packaged**: `pwsh7-7.6.10-win-x64.zip` — byte-identical to the upstream archive, renamed for the release
  - Size: 109,626,781 bytes (104.5 MB)
  - SHA256: `7D871531806EB1646264E4BBABD9F0C30C23355591BC756BB9909448C29AE166`
  - MD5: `05A216C2B9B6FCAE0B673333485A4DD4`
- **Release**: https://github.com/sawaichi9527/opencode-extension-packs/releases/tag/pwsh7-v7.6.10
  - Assets: `pwsh7-7.6.10-win-x64.zip` + `.sha256` + `.md5` + `.txt` + **`pwsh-utf8-wrapper.exe`** (prebuilt, 5,120 bytes, SHA256 `906AA017CDE4F36DB2BC5D3C38C976E76035E0E40703DB30D81F4F5F7F9FB8F0`)
- **Upstream check (2026-11-20)**: `v7.7.0` is the latest stable (STS, .NET 11), while `v7.6.10` is the current LTS and is supported until **2028-11-14**. The previous LTS `7.4` reached end-of-support on **2026-11-10**, which is why this pack moved from 7.4.22 to 7.6.10 instead of adopting 7.7.0.

## Why

Windows 10 (and 11) `powershell.exe` = 5.1 (.NET Framework 4.5, frozen since 2016, Big5 default on zh-TW locales). `opencode`'s `shell` tool and `llama.cpp` multilingual output require UTF-8. `pwsh7` (7.6.10, .NET 10.0 LTS) provides `??` / `?.` / ternary / `ForEach-Object -Parallel` and UTF-8 `Console.OutputEncoding`.

Windows 11 still ships only 5.1 built-in — this pack applies to both Windows 10 and 11.

## Mandate — replace built-in Windows PowerShell 5.1 (Windows 10/11)

This pack is **not** an optional convenience on Windows opencode hosts: the built-in `powershell.exe` must not be used for local operations.

| System | Built-in PowerShell | Requirement |
|---|---|---|
| Windows 10 | 5.1 only | **MUST** replace it with the pwsh7 (7.6.10 LTS) provided by this pack |
| Windows 11 | 5.1 only | If `pwsh` is **missing or older than the pack version (7.6.10)**, **MUST** replace/upgrade it with the pack version |

- Forbidden invocations: `powershell ...`, `powershell.exe ...`, `%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe ...`
- Use `pwsh -NoProfile -ExecutionPolicy Bypass -File <script>` instead; documentation and script comments must show `pwsh`, never `powershell`.
- Verify the replacement:

```powershell
pwsh -NoProfile -Command '$PSVersionTable.PSVersion.ToString(); [Console]::OutputEncoding.WebName'
# Expected: 7.6.10 / utf-8
```

- Make the rule apply to every project on the host by installing the global spec: copy [`templates/global-AGENTS.md`](templates/global-AGENTS.md) into `%USERPROFILE%\.config\opencode\AGENTS.md`. OpenCode loads that file for all sessions and working directories, so a project-level `AGENTS.md` that still shows `powershell ...` examples is overridden by it (fix the project copy as well).
- Only exception: a genuinely 5.1-only module/behaviour — state the reason and get user consent first; never silently fall back to 5.1.

## Usage — Any Windows User (portable, no hard-coded paths)

1. Download (option A: via this repo release, recommended; option B: direct from Microsoft):

```powershell
# A: via extension-packs release (with hash)
Invoke-WebRequest -Uri "https://github.com/sawaichi9527/opencode-extension-packs/releases/download/pwsh7-v7.6.10/pwsh7-7.6.10-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
# B: direct from Microsoft
Invoke-WebRequest -Uri "https://github.com/PowerShell/PowerShell/releases/download/v7.6.10/PowerShell-7.6.10-win-x64.zip" -OutFile "$env:TEMP\pwsh7.zip"
```

2. Unzip to per-user location (no Admin):

```powershell
Expand-Archive -Path "$env:TEMP\pwsh7.zip" -DestinationPath "$env:USERPROFILE\.config\opencode\pwsh7" -Force
& "$env:USERPROFILE\.config\opencode\pwsh7\pwsh.exe" -NoProfile -Command '$PSVersionTable'
# Expected: Major 7 Minor 6 Patch 10 PSEdition Core
Get-FileHash "$env:TEMP\pwsh7.zip" -Algorithm SHA256  # must match 7D871531...
```

3. Install the UTF-8 wrapper (required — plain pwsh7 spawns with Big5 console encoding and garbles CJK output):

```powershell
# Option A: download prebuilt exe from this release
Invoke-WebRequest -Uri "https://github.com/sawaichi9527/opencode-extension-packs/releases/download/pwsh7-v7.6.10/pwsh-utf8-wrapper.exe" -OutFile "$env:USERPROFILE\.config\opencode\pwsh-utf8-wrapper.exe"
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
# Expected: profile loaded UTF8 / 7.6.10 / Unicode (UTF-8) / Chinese displayed correctly
```

## Files in this pack

| File | Purpose |
|---|---|
| `pwsh-utf8-wrapper.cs` | Wrapper source (chcp 65001 + UTF-8 console → forward to pwsh7; flexible path resolution) |
| `shell-utf8.cmd` | cmd-entry fallback wrapper for manual/debug invocation (`cmd /c`), not for opencode.jsonc (`shell` must be an `.exe`) |
| `templates/global-AGENTS.md` | Ready-to-copy global spec for `%USERPROFILE%\.config\opencode\AGENTS.md` — mandates pwsh7 for every session/project on the host |
| Prebuilt `pwsh-utf8-wrapper.exe` | Release asset — skip compiling |

## LTS upgrade policy

- Follow the **LTS** line, not the STS/stable line: 7.6 is supported until **2028-11-14**; 7.4 ended support on 2026-11-10. Adopt an STS release (7.7, 7.9, …) only when a required fix or feature is unavailable in the LTS line, and record the reason in the CHANGELOG.
- **When a new LTS line ships** (the next even-minor LTS after 7.6), start an upgrade evaluation; do **not** wait for the current LTS to reach end-of-support.
- Start the concrete upgrade work at least **90 days before** the current LTS end-of-support date, and re-check the Microsoft lifecycle table (`PowerShell Support Lifecycle`) at that time.
- Each upgrade must repeat the full check: verify the upstream ZIP SHA-256, replace the packaged ZIP, update `manifest/packs.json` + this README + `templates/global-AGENTS.md` + the host's `AGENTS.md`, publish a `pwsh7-v<version>` release with the same five assets, and re-test UTF-8/CJK output through `pwsh-utf8-wrapper.exe`.
- Record the decision and dates in `handoff.md` so the next maintainer knows which LTS line is current and when the next evaluation is due.

## Notes

- `pwsh-utf8-wrapper.exe` is unchanged from earlier releases (5,120 bytes, SHA256 `906AA0...`); the wrapper source did not change.
- `pwsh7` coexists side-by-side with 5.1 (`powershell.exe` vs `pwsh.exe`, separate `PSModulePath`/`PROFILE`).
- Optional per-profile UTF-8 hardening: create `%USERPROFILE%\Documents\PowerShell\Microsoft.PowerShell_profile.ps1` containing `[Console]::OutputEncoding = [System.Text.Encoding]::UTF8`.
- Global agent spec lives at `%USERPROFILE%\.config\opencode\AGENTS.md`: mandates pwsh7 for all PowerShell operations on Windows 10/11 opencode hosts. Install it from [`templates/global-AGENTS.md`](templates/global-AGENTS.md); the same file also states that a missing or older-than-7.6.10 `pwsh` on Windows 11 must be replaced with this pack's version.
