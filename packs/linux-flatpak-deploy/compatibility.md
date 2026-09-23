# linux-flatpak-deploy — 相容性與驗證證據

> 對照 `packs/linux-flatpak-deploy/README.md`。收錄 2026-09-23 在 Ubuntu 24.04.5 desktop
> 上完成 flatpak/flathub 修復、Gear Lever 安裝、AppImage 圖示修正的本機實測證據。

## 測試平台

| 項目 | 值 |
|---|---|
| OS | Ubuntu 24.04.5 desktop |
| 桌面 / 顯示 | GNOME，X11（`DISPLAY=:0`、`WAYLAND_DISPLAY` 空白）、`XDG_CURRENT_DESKTOP=ubuntu:GNOME` |
| 架構 | x64，glibc |
| Flatpak | 1.14.6 |
| Gear Lever | `it.mijorus.gearlever` v4.6.2（stable，system 範圍） |
| Token Monitor | AppImage v0.61.0（`token_monitor.appimage`，widget 視窗 `token-monitor.token-monitor`） |

## 驗證矩陣

| 步驟 | 指令 / 檢查點 | 預期結果 | 實測 |
|---|---|---|---|
| flathub summary 可達 | `curl -sI https://dl.flathub.org/repo/summary.idx` | `200` | ✅ 200 |
| flathub summary 404（舊 base） | `curl -sI https://flathub.org/summary.idx` | `404` | ✅ 404 |
| GPG key 可達 | `curl -sI https://dl.flathub.org/repo/flathub.gpg` | `200` | ✅ 200 |
| remote base 已切換 | `grep -A3 'remote "flathub"' /var/lib/flatpak/repo/config` | `url=https://dl.flathub.org/repo/` | ✅ |
| update 無 no-summary 錯誤 | `flatpak update --verbose` | 只剩「無事可做」，無 `no summary found` | ✅ |
| search 可查套件 | `flatpak search gearlever` | 回傳 `it.mijorus.gearlever 4.6.2 stable flathub` | ✅ |
| Gear Lever 已裝 | `flatpak list` | `Gear Lever it.mijorus.gearlever 4.6.2 stable system` | ✅ |
| desktop 捷徑匯出 | `ls /var/lib/flatpak/exports/share/applications/` | 有 `it.mijorus.gearlever.desktop` | ✅ |
| XDG_DATA_DIRS 含 flatpak | `echo "$XDG_DATA_DIRS"`（relog 後） | 含 `/var/lib/flatpak/exports/share` | ✅（reboot 後） |
| AppImage icon PNG 有效 | `file …/token-monitor.png` | `PNG image data, 1024 x 1024, 8-bit/color RGBA` | ✅ |
| Icon= 絕對路徑解析 | `grep '^Icon=' …/token_monitor.desktop` | 指向存在的 `~/.local/share/icons/.../token-monitor.png` | ✅ |
| 實際視窗 class | `wmctrl -lx \| grep -i token` | `token-monitor.token-monitor` | ✅ |
| StartupWMClass 對上 | `grep '^StartupWMClass=' …/token_monitor.desktop` | `StartupWMClass=token-monitor` | ✅ |
| 最終圖示 | 選單 + dock（重開程式 / relog 後） | Gear Lever、Token Monitor 皆非齒輪 | ✅ |

## 關鍵證據細節

### summary 路徑拼法（問題 1 根因）

- flatpak 1.14 在 remote base URL 後接 `summary.idx`。
- `flathub.org` → `flathub.org/summary.idx` → **404**；`dl.flathub.org/repo/` → `dl.flathub.org/repo/summary.idx` → **200**。
- `.flatpakrepo` 內建 `Url=https://dl.flathub.org/repo/`，重加後 config 的 `url=` 即為該值。

### AppImage 圖示兩層問題（問題 3b）

- **選單（grid）**：取決於 `.desktop` `Icon=` 是否指向**存在**檔案。原指向 `/home/ubuntu/Applications/.icons/token_monitor`（不存在）→ 齒輪；改絕對路徑至 `~/.local/share/icons/hicolor/1024x1024/apps/token-monitor.png`（AppImage 內建 1024×1024 RGBA 複製品）後解析。
- **dock（開窗）**：取決於 `StartupWMClass` 是否匹配實際視窗 class。`wmctrl -lx` 顯示實際 class = `token-monitor`，原 `.desktop` 為 `Token Monitor`（不匹配）→ 齒輪；對齊後 dock 顯示正確圖示。

### XDG_DATA_DIRS（問題 3a）

- GNOME 只掃描 `XDG_DATA_DIRS/applications/`；本機原本不含 flatpak exports → 選單看不到 flatpak 應用。
- 寫入 `/etc/environment`（含兩個 exports 目錄，置前）後，**logout/relogin**（PAM 在登入時讀取；`Alt+F2:r` 不生效）方可生效。

## Agent sandbox 限制實測（dsh 特別記錄）

- dsh harness 給 agent shell 設 `no_new_privs=1`：`sudo -n true` 回報 *"The 'no new privileges' flag is set, which prevents sudo from running as root"*——agent **無法自行 sudo**。
- sandbox（workspace-write）擋 home 下 flatpak 路徑寫入：`flatpak remote-add --user` 與 `sed` 改 `~/.local/share/applications/*.desktop` 皆回報「拒絕不符權限的操作」——agent **無法自行寫 home flatpak 路徑**。
- 用戶本機 terminal 的 `sudo`／`apt` 正常：本次實測 `sudo apt install wmctrl` 成功。
- 結論：privileged 與 home-path 編輯由用戶在本機執行；agent 負責診斷與給出精確指令。

## 已知限制與注意事項

- macOS 未由團隊驗證本 runbook（平台為 Linux desktop）。
- 若 AppImage 重新解壓縮／更新到不同路徑，`squashfs-root` 內的 icon 複製品可能失效；建議把 icon 固定在 `~/.local/share/icons/`（已這麼做），並在建構流程裡同步。
- `XDG_DATA_DIRS` 手動加入 flatpak exports 後，日後若 system 層 flatpak 改用其他 mechanism 管理，注意不要重複加入（指令已用 `grep -q` 防重）。
