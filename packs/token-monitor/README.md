# Token Monitor

This pack is **Optional**. It is listed for user selection and is not installed by default.

Token Monitor is an upstream **desktop widget** (Electron) that tracks token usage, cost, and
provider limits across 38+ AI coding tools, including OpenCode, Claude Code, Codex, Cursor, GitHub
Copilot, and DeepSeek Harness. It is a standalone third-party application — not an OpenCode plugin,
Skill, Command, or MCP server — and it is not source code maintained by this repository.

Token Monitor only reads the usage files those tools already write (for OpenCode:
`~/.local/share/opencode/opencode.db`). It never writes to OpenCode or changes its configuration,
so there is no OpenCode version constraint on this pack. See
[Relationship to OpenCode](#relationship-to-opencode).

## Official Source

- Repository: <https://github.com/Javis603/token-monitor>
- Release: `v0.61.0`
- License: MIT
- Author: Javis (`@Javis603`)
- Upstream documentation: <https://github.com/Javis603/token-monitor#readme>
- Bundled scan engine: [tokscale](https://github.com/junhoyeo/tokscale) (MIT), pinned to a
  downstream build — see [Scan engine](#scan-engine)

This repository does not fork, vendor, repackage, or host Token Monitor or its binaries. Download
the executables from the official GitHub Releases page and verify them as described below.

## Relationship to OpenCode

`2.0.12` removed the `token-usage` pack because upstream TokenScope depended on OpenCode v1 (its
singular `command/` directory and v1 API). Token Monitor has **no integration surface with OpenCode
at all**: it scans data files on disk with its own bundled binary. The OpenCode v1/v2 split
therefore does not apply, and this pack adds nothing to `opencode.jsonc` or the skills directory.

## Prerequisites (Windows 10 / 11)

| Requirement | GUI (portable / installer) | Headless agent |
|---|---|---|
| Administrator rights | Not required (per-user install) | Not required |
| VC++ 2015-2022 x64 Redistributable | **Required** | Required |
| Node.js | **Not required** | **Required, 22.15.0 or newer** |
| Python | Not required | Not required |
| WSL | **Not required** | Not required |
| winget / Microsoft Store | Not required | Not required |
| PowerShell | Only for the commands in this document; on Windows hosts use the `pwsh7` pack, never the built-in 5.1 | Same |

The VC++ 2015-2022 x64 Redistributable is the only hard prerequisite of the GUI install. Electron
itself statically links the C runtime — `Token Monitor.exe`, `ffmpeg.dll`, `libGLESv2.dll`, and
`vk_swiftshader.dll` import no `VCRUNTIME140.dll`/`MSVCP140.dll` — but the bundled `tokscale.exe`
does. The `api-ms-win-crt-*.dll` Universal CRT is a Windows 10/11 OS component and needs no install.

Verify before installing:

```powershell
(Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64' -ErrorAction SilentlyContinue).Installed -eq 1
Test-Path "$env:SystemRoot\System32\VCRUNTIME140.dll"
```

If either check fails, install Microsoft Visual C++ 2015-2022 Redistributable (x64) first.

### Prerequisites (Linux x64)

| Requirement | GUI (AppImage) | Headless agent |
|---|---|---|
| `libfuse2` | Optional — only to run the AppImage directly; otherwise use `--appimage-extract` | Not required |
| Node.js | Not required | Required, 22.15.0 or newer |
| Python / WSL / VC++ Redistributable | Not required | Not required |

Linux is published as a single x64 `.AppImage` (no installer). macOS ships a signed, notarized `.dmg`.

## Installation (Windows 10 / 11)

### 1. Download

```powershell
$v = "0.61.0"
$d = "$env:TEMP\token-monitor"; New-Item -ItemType Directory -Force $d | Out-Null
$base = "https://github.com/Javis603/token-monitor/releases/download/v$v"
curl.exe -L --retry 5 --retry-delay 3 -o "$d\Token-Monitor-Setup-$v.exe" "$base/Token-Monitor-Setup-$v.exe"
curl.exe -L --retry 5 --retry-delay 3 -o "$d\Token-Monitor-$v.exe"       "$base/Token-Monitor-$v.exe"
curl.exe -L --retry 5 --retry-delay 3 -o "$d\latest.yml"                 "$base/latest.yml"
```

Use `curl.exe` rather than `Invoke-WebRequest`: the release assets redirect to
`release-assets.githubusercontent.com`, where the download is slow enough that a Node `fetch` with a
short timeout aborts.

### 2. Verify

`latest.yml` carries the publisher's `sha512` for the installer — the same value `electron-updater`
checks when updating. Compare it, and confirm the Authenticode signature:

```powershell
(Get-AuthenticodeSignature "$d\Token-Monitor-Setup-$v.exe").Status      # must be Valid
$expected = (Select-String -Path "$d\latest.yml" -Pattern 'sha512: (\S+)').Matches.Groups[1].Value
$actual = [Convert]::ToBase64String(
  [System.Security.Cryptography.SHA512]::Create().ComputeHash(
    [System.IO.File]::ReadAllBytes("$d\Token-Monitor-Setup-$v.exe")))
$actual -eq $expected                                                   # must be True
```

Both executables are signed by **SignPath Foundation** with a DigiCert timestamp, matching the
`publisherName` the app verifies updates against. Downloading with `curl.exe` leaves no
Mark-of-the-Web, so SmartScreen does not prompt on first run.

### 3. Install — pick one

**A. Portable (no install).** Run the portable executable directly:

```powershell
& "$d\Token-Monitor-$v.exe"
```

No registry writes, no shortcuts. Application data still goes to `%APPDATA%\Token Monitor\`.

**B. Installer (recommended).** Interactive wizard, or `/S` for a silent install:

```powershell
& "$d\Token-Monitor-Setup-$v.exe"
& "$d\Token-Monitor-Setup-$v.exe" /S
```

The installer forces a **per-user** installation (`customInstallMode` sets `$isForceCurrentInstall`),
so there is no UAC prompt and no administrator requirement. It then grants read/execute on its own
install directory to `S-1-15-2-2`:

```text
icacls "$INSTDIR" /grant "*S-1-15-2-2:(OI)(CI)(RX)"
```

That works around Chromium AppContainer GPU/renderer failures on machines carrying explicit
AppContainer ACEs (upstream issue #487). The `icacls` exit code is only logged, so a locked-down or
non-English image cannot fail the install over it. `icacls` prints the localized display name of
that SID, which is why the check below reads the SDDL instead of grepping the output.

### 4. Install paths

| What | Path |
|---|---|
| Install directory (installer) | `%LOCALAPPDATA%\Programs\Token Monitor\` |
| Uninstaller | `%LOCALAPPDATA%\Programs\Token Monitor\Uninstall Token Monitor.exe` |
| Uninstall registry key | `HKCU\Software\Microsoft\Windows\CurrentVersion\Uninstall\481e6f3e-6ba8-5e6e-ab41-400c1e2a3de3` |
| Start Menu shortcut | `%APPDATA%\Microsoft\Windows\Start Menu\Programs\Token Monitor.lnk` |
| Desktop shortcut | `%USERPROFILE%\Desktop\Token Monitor.lnk` |
| Application data | `%APPDATA%\Token Monitor\` |
| Scan-engine cache | `%APPDATA%\tokscale\cache\` |
| Autostart entry | **Not created.** Enable in-app: Settings → General → launch at login |

The uninstall registry key is derived from the app id (`com.javis.tokenmonitor`); re-read it from
the registry if the app id ever changes.

`%APPDATA%\Token Monitor\` holds `settings.json`, `credentials.json` (plaintext — see
[Security Boundaries](compatibility.md#security-boundaries)), `collector-anchor.json`, `daily-history-archive.json`,
`session-usage-archive.sqlite`, and `exchange-rates.json`. The scan engine caches pricing metadata
and a message index under `%APPDATA%\tokscale\cache\` (roughly 8 MB). Unlike the installer payload,
these are created on first run and are shared by the portable and installed builds.

### 5. Verify after install

```powershell
& "$env:LOCALAPPDATA\Programs\Token Monitor\Token Monitor.exe"
```

The widget must appear and show a non-zero token count for `OpenCode`. To compare it against the raw
scan, run the bundled engine directly:

```powershell
$tok = "$env:LOCALAPPDATA\Programs\Token Monitor\resources\app.asar.unpacked\node_modules\@tokscale\cli-win32-x64-msvc\bin\tokscale.exe"
& $tok clients
& $tok --json --client opencode --group-by client,workspace,session,model --today
```

The first launch is slower than later ones: the scan engine fetches pricing data from LiteLLM,
OpenRouter, and models.dev, and falls back to its cache when that fetch fails. Cold start measured
about 30 s; once the one-hour pricing cache is warm, a `--today` scan takes about 0.2 s.

### 6. Uninstall

```powershell
& "$env:LOCALAPPDATA\Programs\Token Monitor\Uninstall Token Monitor.exe" /currentuser
```

Uninstalling removes the program and shortcuts but **leaves** `%APPDATA%\Token Monitor\` and
`%APPDATA%\tokscale\cache\`. Delete both by hand for a full removal.

## Installation (Linux x64)

### 1. Download and verify

```bash
v=0.61.0
d=~/token-monitor; mkdir -p "$d"; cd "$d"
base="https://github.com/Javis603/token-monitor/releases/download/v$v"
curl -L --retry 5 --retry-delay 3 -O "$base/latest-linux.yml"
curl -L --retry 5 --retry-delay 3 -O "$base/Token-Monitor-$v.AppImage"

expected=$(sed -n 's/^sha512:[[:space:]]*//p' latest-linux.yml | head -1)
actual=$(openssl dgst -sha512 -binary "Token-Monitor-$v.AppImage" | base64 -w0)
[ "$expected" = "$actual" ] && echo "sha512 match"
```

`latest-linux.yml` carries the publisher's `sha512` for the AppImage — the same value
`electron-updater` checks when updating.

### 2. Run

```bash
chmod +x Token-Monitor-0.61.0.AppImage
./Token-Monitor-0.61.0.AppImage          # requires libfuse2 (libfuse.so.2)
```

Without `libfuse2`, unpack and run the extracted tree instead (see
[compatibility.md](compatibility.md#linux-notes)):

```bash
./Token-Monitor-0.61.0.AppImage --appimage-extract
./squashfs-root/AppRun
```

### 3. Verify after install

The widget must appear and show a non-zero token count for `OpenCode`. To compare against the raw
scan, run the bundled engine (glibc hosts use the `gnu` build):

```bash
tok=squashfs-root/resources/app.asar.unpacked/node_modules/@tokscale/cli-linux-x64-gnu/bin/tokscale
"$tok" clients
"$tok" --json --client opencode --group-by client,workspace,session,model --today
```

App data lives in `~/.config/Token Monitor/` (`settings.json`, `collector-anchor.json`,
`session-usage-archive.sqlite`, …); the engine caches pricing metadata and its index under
`~/.config/tokscale/`.

### 4. Uninstall

There is no installer: delete the AppImage (or the extracted tree), and for a full removal also
`~/.config/Token Monitor/` and `~/.config/tokscale/`.

## Headless agent (optional)

Only needed on a machine without a desktop session. Requires Node.js 22.15.0 or newer (the scan
engine uses `zlib.zstdDecompressSync` to read DeepSeek Harness sessions).

```powershell
git clone --depth 1 https://github.com/Javis603/token-monitor.git
cd token-monitor
npm ci --omit=dev
npm run agent:once -- --dry-run --clients=opencode
```

`--omit=dev` is enough and skips the ~100 MB Electron download; the scan engine is a production
dependency. npm's script policy does not run koffi's install script, which is fine here — koffi is
only used by the GUI's window effects.

Known gotcha: `npm run ensure:tokscale` downloads the pinned scan-engine binary with Node `fetch`
and a fixed **60-second** timeout, which aborts on slower links (`This operation was aborted`).
Download it with `curl.exe`, verify SHA256
`d9fa18103c277e35d2cabb61c852adc795439a5c8010c7ea9a468a592a0fc1da`, place it at
`node_modules\@tokscale\cli-win32-x64-msvc\bin\tokscale.exe`, then re-run
`npm run ensure:tokscale` — it reports `already matches` and skips the download.

## Scan engine

Token Monitor bundles a pinned downstream build of tokscale — `Javis603/tokscale` @ `06a9f162`,
release tag `token-monitor-06a9f162`, `win32-x64` SHA256 `d9fa1810…` — instead of the plain npm
release, because it depends on that fork's `client,workspace,session,model` grouping and its
`sessions`/`workspaces` metadata. Do not substitute an npm tokscale release for the bundled binary:
older upstream builds read only OpenCode's legacy `storage/message/` layout and report zero usage on
OpenCode 1.2+.

The Linux AppImage bundles both `gnu` and `musl` builds of the same 4.17.0 engine
(`@tokscale/cli-linux-x64-gnu` / `-musl`). On glibc distributions the `gnu` build is selected and
supports the full `client,workspace,session,model` grouping; the `musl` build only accepts the
shorter groupings. See [compatibility.md](compatibility.md#linux-notes).

## Update Policy

- The pinned version lives in the `release` field of `manifest/packs.json` and in this document;
  update both together.
- Upstream releases very frequently (`v0.57.0` → `v0.61.0` in nine days). Re-read
  [compatibility.md](compatibility.md) before moving the pin.
- This pack tracks release tags only, never upstream commits.

## Boundaries

- This pack documents source, prerequisites, and installation. It downloads nothing, installs
  nothing, and does not modify `opencode.jsonc` or any OpenCode configuration.
- Token Monitor stores provider credentials in plaintext; see [compatibility.md](compatibility.md).
- Platforms the team has not verified yet are listed in [compatibility.md](compatibility.md).
