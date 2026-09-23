# Token Monitor Compatibility

## Upstream Contract

- Release: `v0.61.0`
- Repository: `Javis603/token-monitor`
- License: MIT
- Author: Javis (`@Javis603`)
- Distribution: GitHub Releases only — Windows x64 NSIS installer and portable `.exe`, macOS `.dmg`
  (signed and notarized), Linux x64 `.AppImage`. Not published to winget or the Microsoft Store.
- Bundled scan engine: `Javis603/tokscale` @ `06a9f162` (release tag `token-monitor-06a9f162`)
- Data sources are read-only local files; nothing in OpenCode is configured or modified

## Verification Status

| Platform | Status | Notes |
|---|---|---|
| Windows 10 IoT Enterprise LTSC 2021 (19044 / 21H2), x64, **no WSL installed** | **Verified** on `v0.60.0` (2026-09-23) | Portable, NSIS installer, and headless agent all exercised end to end; `v0.61.0` not yet re-verified on Windows |
| Windows 11 | Not verified by the team | Supported upstream, same installer and prerequisites |
| Ubuntu desktop (x86_64, X11, glibc) | **Verified** on `v0.60.0` and `v0.61.0` (2026-09-23) | AppImage launched (direct via FUSE and via `--appimage-extract`); widget window rendered, OpenCode usage non-zero, WSL correctly reported as absent; evidence below |
| macOS | Not verified by the team | Supported upstream; out of scope for this pack |

Treat the pack as validated only on rows marked Verified.

## Verified Environment

| Item | Value |
|---|---|
| OS | Windows 10 IoT Enterprise LTSC 2021, build 19044 (21H2), AMD64 |
| WSL | Not installed — `HKCU\Software\Microsoft\Windows\CurrentVersion\Lxss` absent, `wsl.exe` present only as the inbox stub |
| VC++ Redistributable | `14.51.36247.00`; `HKLM\SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64` → `Installed=1` |
| Node.js | `24.18.0` (headless path only) |
| OpenCode data | `%USERPROFILE%\.local\share\opencode\opencode.db` (SQLite, V2 schema) plus `-wal`/`-shm`; no `storage\message\` directory |
| Token Monitor | `v0.60.0` |

### Ubuntu desktop (verified 2026-09-23)

| Item | Value |
|---|---|
| OS | Ubuntu, x86_64, glibc; desktop session `x11` (`DISPLAY=:0`) |
| FUSE | `libfuse2` **not installed** — the AppImage was unpacked with `--appimage-extract` |
| Node.js | `24.21.0` (present; not required by the GUI path) |
| OpenCode data | `~/.local/share/opencode/opencode.db` (SQLite, V2 schema) plus `-wal`/`-shm` |
| Token Monitor | `v0.61.0` (Linux x64 AppImage, direct via FUSE and extracted) |
| Bundled engine | `@tokscale/cli-linux-x64-gnu` 4.17.0 |

## Verification Evidence

### Artifact integrity

| Check | Result |
|---|---|
| `Token-Monitor-Setup-0.60.0.exe` size vs `latest.yml` | `119407520` = `119407520` |
| `Token-Monitor-Setup-0.60.0.exe` `sha512` vs `latest.yml` | match |
| Authenticode, installer and portable | `Valid`; `CN=SignPath Foundation`; not after 2027-09-08; DigiCert timestamp |
| Mark-of-the-Web | absent (downloaded with `curl.exe`) |

### Scan engine reach (the decisive check)

| Engine | `tokscale clients` → OpenCode `messages` |
|---|---|
| Bundled pinned fork (`06a9f162`, based on 4.17.0) | **261** |
| Plain npm `tokscale@4.10.0` on the same machine | **0** |

Both ran against the same `opencode.db`. The older npm release scans only the legacy
`storage/message/` layout, which OpenCode 1.2+ no longer writes. Verify the bundled binary, never a
system-installed tokscale.

### Raw scan shape

`tokscale.exe --json --client opencode --group-by client,workspace,session,model --today` returned
`groupBy: "client,workspace,session,model"` with the fork-specific `sessions` and `workspaces`
arrays, five entries, `totalMessages: 179`, `totalCost: 0.216190668`, and a per-entry `performance`
block (`timedTokens`, `totalDurationMs`, `tokenCoverage: 1.0`). Workspaces decoded to
`D:/workspace/ai-plug` and `D:/workspace/opencode-self-maintenance`.

### Headless agent (`npm run agent:once -- --dry-run --clients=opencode`)

Exit code 0. Selected fields of the produced device record:

```text
deviceId        acubens
platform        win32-x64
agentRuntime    headless-agent
trackedClients  ["opencode"]
clientStatus    {"opencode":"active"}
wslStatus       {"state":"not-installed","detected":[],"withData":[]}
clientHealth    opencode -> source.detectedCount 1/1, collection "direct", overall "healthy"
periodWindows   timeZone Asia/Taipei
today           21194030 tokens / $0.218931 / 3 sessions / projects ["ai-plug","opencode-self-maintenance"]
month = allTime 28322313 tokens / $0.218931
capabilities    tokenComponents true, throughput true
watching        C:\Users\Sawaichi\.local\share\opencode (native events)
```

### Reconciliation against the source database

Read directly with `node:sqlite` (read-only, safe while OpenCode holds the WAL):

| Metric | `opencode.db` (`session_v2` sum) | Token Monitor (agent) |
|---|---|---|
| Sessions | 3 | 3 |
| Session ids / titles / directories | `ses_f362028d…`, `ses_f3786033…`, `ses_f33df9c1…` | identical |
| Total tokens | 29,938,735 | 28,322,313 |
| Cost | 0.226781 | 0.218931 |

Per-session cost matched to six decimals (`0.134820672` vs `0.134821`). The residual total
difference is live drift: the measured session was still running and accrued between the two reads.
The database schema is V2 — `session_v2` and `session_message`; the V1 `message`/`part` tables do
not exist.

### GUI behaviour

| Check | Result |
|---|---|
| Process tree | 5 Electron processes, main window titled `Token Monitor` |
| Window | `356x658`, rendered always-on-top above the editor window |
| On-screen value vs `collector-anchor.json` | `24,239,932` / `$0.2366` — identical |
| Default tracked clients | All supported tools; a DeepSeek Harness session was also collected |
| Context fields | `contextTokens: 0` / `contextWindow: 0` for OpenCode (the client states no window size; the value is deliberately not guessed) |
| Project attribution | `projectId: sha256:8201becb…` with `projectLabel: opencode-self-maintenance` |
| Session archive | `session-usage-archive.sqlite` written with `metadata` and `sessions` tables |

### Installer

| Check | Result |
|---|---|
| Silent install (`/S`) | exit code 0 |
| Install mode | per-user; no UAC, no administrator rights |
| Install directory | `%LOCALAPPDATA%\Programs\Token Monitor\` containing `Token Monitor.exe` and `Uninstall Token Monitor.exe` |
| Directory ACL | SDDL contains `(A;OICI;0x1200a9;;;S-1-15-2-2)` — allow, object+container inherit, `RX` |
| Uninstall registry key | `HKCU\…\Uninstall\481e6f3e-6ba8-5e6e-ab41-400c1e2a3de3`, `Token Monitor 0.60.0`, publisher `Javis` |
| Shortcuts | Start Menu and Desktop |
| Autostart entries | none created |
| Data retained after uninstall | `%APPDATA%\Token Monitor\`, `%APPDATA%\tokscale\cache\` |

### CRT prerequisites (measured, not assumed)

An import-table scan of every `.dll`/`.exe`/`.node` under the install directory (11 PE files read):

| File | CRT imports |
|---|---|
| `Token Monitor.exe`, `ffmpeg.dll`, `libGLESv2.dll`, `vk_swiftshader.dll` | none — Electron statically links the CRT |
| `dxil.dll` | `api-ms-win-crt-*.dll` only — the Universal CRT, an OS component |
| `resources\app.asar.unpacked\node_modules\@tokscale\cli-win32-x64-msvc\bin\tokscale.exe` | **`VCRUNTIME140.dll`** |

This is why the VC++ 2015-2022 x64 Redistributable is listed as the GUI's only hard prerequisite: the
GUI shell does not need it, but the bundled scan engine does.

### Scan latency

| Condition | `--today` scan |
|---|---|
| First run (pricing fetch times out, falls back to cache) | 30.1 s |
| After the one-hour pricing cache is warm | 0.2 s |

### Ubuntu desktop (X11) evidence — `v0.60.0`

| Check | Result |
|---|---|
| `Token-Monitor-0.60.0.AppImage` size vs `latest-linux.yml` | `154943207` = `154943207` |
| `Token-Monitor-0.60.0.AppImage` `sha512` vs `latest-linux.yml` | match |
| Launch | AppImage unpacked with `--appimage-extract` (no `libfuse2`); `squashfs-root/AppRun` launched on X11 |
| Process tree | Electron main + zygote + GPU + network + renderer + broker |
| Window | `xwininfo`: `"Token Monitor"` `340x650` (always-on-top) |
| App data | `~/.config/Token Monitor/` created with `settings.json`, `credentials.json`, `collector-anchor.json`, `daily-history-archive.json`, `exchange-rates.json`, `session-usage-archive.sqlite` |
| Collector | log `[collector] Watching /home/ubuntu/.local/share/opencode (native events)` |
| Engine reach | `@tokscale/cli-linux-x64-gnu` `clients` → OpenCode `messages: 615` (non-zero; legacy `storage/message/` absent) |
| Raw scan | `--group-by client,session,model --today` returned real per-session/per-model rows with `performance.tokenCoverage: 1.0` |
| Anchor values | `collector-anchor.json`: today `33,493,657` tokens / `$0.5908`; month = allTime `99,162,062` / `$3.705` |
| WSL status | `wslStatus: null` (no WSL — reported as absent, not an error) |
| Tracked clients | `settings.json` `clients` includes `opencode` |

Benign messages on the extracted run: `APPIMAGE env is not defined` and
`App update check failed: Update metadata missing or invalid` (both because the app was started from
an extracted tree rather than through the AppImage runtime), plus `vaInitialize failed` /
`MESA-LOADER: failed to open dri` GPU warnings that fall back to software rendering.

### Ubuntu desktop (X11) evidence — `v0.61.0` re-verification (2026-09-23)

| Check | Result |
|---|---|
| `Token-Monitor-0.61.0.AppImage` size vs `latest-linux.yml` | `155094683` = `155094683` |
| `Token-Monitor-0.61.0.AppImage` `sha512` vs `latest-linux.yml` | match |
| Launch | Run directly through FUSE (`libfuse2` installed) — process `/tmp/.mount_Token-*/token-monitor` |
| Window | `xwininfo`: `"Token Monitor"` `340x650` |
| Engine reach | `@tokscale/cli-linux-x64-gnu` 4.17.0 `clients` → OpenCode `messages: 651` |
| Raw scan | `--group-by client,session,model --today` → 2 entries |
| Anchor values | `collector-anchor.json`: today `38,216,954` tokens / `$0.6201`; month = allTime `103,885,359` / `$3.734` |
| WSL status | `wslStatus: null` |
| Notes | `v0.61.0` adds a Linux Floating Bubble fix (#756) that was not specifically exercised. The bundle still ships tokscale 4.17.0 (`gnu`/`musl`), so the gnu-vs-musl grouping note below is unchanged. |

## Installation Checks

1. Confirm the VC++ 2015-2022 x64 Redistributable is installed.
2. Confirm the installer's Authenticode status is `Valid` and its `sha512` matches `latest.yml`.
3. Confirm no administrator rights are required and no UAC prompt appears.
4. Confirm the install directory contains `Token Monitor.exe` and `Uninstall Token Monitor.exe`.
5. Confirm the install directory SDDL contains `S-1-15-2-2` with `RX`.
6. Launch the widget and confirm it shows a non-zero `OpenCode` token count.
7. Confirm `%APPDATA%\Token Monitor\settings.json` was created.
8. Confirm the engine reaches the current OpenCode layout: `tokscale.exe clients` must report a
   non-zero `messages` count for OpenCode.
9. On a host without WSL, confirm the widget reports WSL as not installed rather than as an error.
10. Do not store provider credentials, API keys, or `.env` secrets in the Extension Packs repository.

For Linux x64, the equivalent checks are:

1. The AppImage `sha512` matches `latest-linux.yml`.
2. The AppImage launches — directly when `libfuse2` is present, otherwise via `--appimage-extract` + `squashfs-root/AppRun`.
3. `~/.config/Token Monitor/settings.json` is created.
4. The bundled `@tokscale/cli-linux-x64-gnu` `clients` output reports a non-zero `messages` count for OpenCode.
5. The widget window appears and shows a non-zero OpenCode token count.
6. Do not store provider credentials, API keys, or `.env` secrets in the Extension Packs repository.

## Windows Notes

### No WSL is a fully supported configuration

The WSL scan is gated on the registry key `HKCU\Software\Microsoft\Windows\CurrentVersion\Lxss`. When
it is absent the app reports `wslStatus: {"state":"not-installed", …}` and skips the scan entirely —
`wsl.exe` is never spawned, which also avoids the inbox stub's installation prompt. No setting has to
be changed, and nothing is lost: the host's own OpenCode database is scanned natively.

### `icacls` prints a localized SID name

The installer grants `S-1-15-2-2` using the SID literal precisely because the display name is
localized. On a zh-TW host `icacls` shows `APPLICATION PACKAGE AUTHORITY\所有受限制的應用程式套件`,
so check `(Get-Acl $dir).Sddl` rather than grepping `icacls` output.

### Update checks are publisher-pinned

`electron-updater` is configured with `publisherName: "SignPath Foundation"`, the same identity that
signs the release artifacts, so a tampered update payload fails verification.

## Linux Notes

### `libfuse2` is not required if you unpack the AppImage

The AppImage runs directly when FUSE 2 is available (`libfuse2` / `libfuse.so.2`). Without it, use
the standard AppImage escape hatch instead of installing FUSE:

```bash
chmod +x Token-Monitor-0.61.0.AppImage
./Token-Monitor-0.61.0.AppImage --appimage-extract
./squashfs-root/AppRun
```

Running the extracted `AppRun` prints `APPIMAGE env is not defined` and disables the in-app updater
check (`App update check failed: Update metadata missing or invalid`). Both are cosmetic; scanning
and the widget are unaffected.

### The `gnu` scan engine supports the full grouping; the `musl` one does not

The AppImage ships both `@tokscale/cli-linux-x64-gnu` and `@tokscale/cli-linux-x64-musl` (4.17.0).
On glibc distributions the `gnu` build is used and accepts `--group-by client,workspace,session,model`;
the `musl` build rejects that value and only accepts the shorter groupings. Verify the `gnu` binary
on glibc hosts.

### GPU warnings are expected in VMs and some desktops

`vaInitialize failed` / `MESA-LOADER: failed to open dri` messages appear when hardware video
acceleration or the GBM loader is unavailable; Chromium falls back to software rendering and the
widget still renders.

## Security Boundaries

- Local-first: the app sends no telemetry to the maintainer. Prompts, responses, and source code
  never leave the machine.
- `%APPDATA%\Token Monitor\credentials.json` stores provider credentials in **plaintext**, protected
  only by filesystem permissions (on Windows, the `%APPDATA%` ACL). Treat it as a secret-bearing
  file and keep it out of any repository.
- Provider limit checks send credentials only to the matching provider and are off until enabled.
  With limits disabled the app still makes network requests for update checks, exchange rates, and
  the scan engine's pricing cache.
- Project attribution is transmitted as an opaque `sha256:` id plus a workspace folder label;
  absolute workspace paths stay on the device.
- The pack documents installation only; it downloads and installs nothing.

## Troubleshooting

### The widget shows zero OpenCode usage

Almost always a scan-engine binary that predates OpenCode 1.2's SQLite layout. Confirm with the
bundled pinned binary (see README "Scan engine"), not with a system-installed `tokscale`. The
`clients` subcommand's `messages` count for OpenCode must be non-zero.

### `ensure:tokscale` fails with "This operation was aborted"

The download uses a fixed 60-second timeout. Use the `curl.exe` workaround in the README, verifying
the recorded SHA256 before replacing the binary.

### The widget window is blank or the app crashes at startup

Check the VC++ Redistributable first, then re-run the installer so the `icacls` grant is applied.

## Update Policy

Re-run the checks above on at least the verified platform before moving the pin. An upstream release
note is not a substitute for the checks in this document.
