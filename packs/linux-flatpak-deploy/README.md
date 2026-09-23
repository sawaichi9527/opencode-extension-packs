# Linux Flatpak / AppImage 桌面部署 Runbook（Ubuntu 24.04.5 desktop）

> 收錄 **2026-09-23 透過 dsh（DeepSeek Harness，本機本地 session）** 在 Ubuntu 24.04.5 desktop
> 上完成「flatpak / flathub 修復 → Gear Lever（Flatpak）安裝 → AppImage / Gear Lever 圖示修正」
> 整條鏈的經驗。對照 opencode 遠端佈署經驗見 [dsh vs opencode 佈署對照](#dsh-vs-opencode-佈署對照)。
>
> 本 pack 是**部署 / 除錯 runbook**，不是可透過 OpenCode 安裝的能力 Pack；它記錄「在 Linux desktop
> 把 Flatpak／AppImage 應用程式順利跑起來並正確顯示圖示」的可重複步驟與坑點。

## 範圍與平台

| 項目 | 值 |
|---|---|
| OS | Ubuntu 24.04.5 desktop |
| 桌面 / 顯示 | GNOME，**X11**（`DISPLAY=:0`、`WAYLAND_DISPLAY` 空白）、`XDG_CURRENT_DESKTOP=ubuntu:GNOME` |
| 架構 | x64，glibc |
| Flatpak | 1.14.6 |
| 涵蓋對象 | flathub summary 修復、Gear Lever（`it.mijorus.gearlever`）安裝與選單圖示、Token Monitor AppImage 的選單 / dock 齒輪圖示 |

三件事串成一条典型鏈：**先讓 flathub 能抓 summary → 再裝 Flatpak 應用 → 最後修桌面圖示**。
每一階段都有明確的「看不到的現象 → 根因 → 修正 → 驗證」，下面逐一拆解。

---

## 問題 1：flathub「`GPG verification enabled, but no summary found`」

### 症狀

```text
flatpak remote-add --if-not-exists flathub https://flathub.org
警告：無法更新「flathub」的額外中介資料：GPG verification enabled, but no summary found
flatpak update
正在尋找更新...
無事可做。            ← 不是真的沒更新，是 summary 沒抓到、看不到任何 app
```

### 根因（關鍵：base URL 決定 summary 路徑）

Flatpak 1.14 會把 `summary.idx` **接在 remote 的 base URL 後面**去抓：

| base URL（`.desktop`/remote 設定） | flatpak 拼出的 summary | 結果 |
|---|---|---|
| `https://flathub.org` | `https://flathub.org/summary.idx` | **404** ✗（本機問題根源） |
| `https://dl.flathub.org/repo/` | `https://dl.flathub.org/repo/summary.idx` | **200** ✓ |

- GPG key 在任何一端都拿得到：`dl.flathub.org/repo/flathub.gpg` = 200。
- 但 `dl.flathub.org/repo/flathub.summary` 與 `/repo/flathub.summary.gz` 在本機皆 404；只有
  **`dl.flathub.org/repo/summary.idx` = 200**。
- 因此「換到 `dl.` 就好」這個說法**不完整**——真正生效的是「base 以 `/repo/` 結尾，讓 flatpak
  拼出 `/repo/summary.idx`」。用 `.flatpakrepo` 描述檔即可把 base 固定成這個路徑。

### 修正（需 sudo，system 範圍）

```bash
sudo flatpak remote-delete flathub
sudo flatpak remote-add --if-not-exists flathub https://dl.flathub.org/repo/flathub.flatpakrepo
sudo rm -rf /var/lib/flatpak/repo/tmp/*          # 清下載快取（選用）
sudo flatpak update
```

`.flatpakrepo` 內建 `Url=https://dl.flathub.org/repo/`，重加後 `grep` 確認：

```text
[remote "flathub"]
url=https://dl.flathub.org/repo/
gpg-verify=true
```

### 驗證

`flatpak update --verbose` 只剩「無事可做」，**不再出現 `no summary found`**；
`flatpak search gearlever` 能回傳 `it.mijorus.gearlever 4.6.2 stable flathub`。

> ⚠️ 常見誤區：`flatpak remote-add --force-toggle …` 中的 `--force-toggle` **不是 `remote-add`
> 的合法參數**（它是 `flatpak update`／`install` 用以強制重下）。原樣貼會噴 `unknown option`。
> 另：`--if-not-exists` 在 remote 已存在時是 no-op，必須先 `remote-delete`。

---

## 問題 2：安裝 Gear Lever

Gear Lever（`it.mijorus.gearlever`，v4.6.2，「Manage AppImages」）是一個 AppImage 啟動器：
把你散落的 AppImage 整理分類成網格，從介面裡點選啟動——正是「整合快捷頁面點選啟動 AppImage」的需求。

```bash
sudo flatpak install flathub it.mijorus.gearlever
```

- 相依環境會一起下（`org.gnome.Platform@50`、`org.freedesktop.Platform.GL.*`、`codecs-extra` 等）。
- 裝完 `flatpak list` 會出現 `Gear Lever it.mijorus.gearlever 4.6.2 stable system`；
  捷徑匯出到 `/var/lib/flatpak/exports/share/applications/it.mijorus.gearlever.desktop`。
- 立即測試（不依賴選單）：`flatpak run it.mijorus.gearlever`。

---

## 問題 3a：Gear Lever 不在應用程式選單（XDG_DATA_DIRS）

### 症狀

裝好了，但 GNOME 應用程式選單（grid）與 Ubuntu Dock 都看不到 Gear Lever 圖示。

### 根因

`.desktop` 捷徑已匯出，但 GNOME 只掃描 **`XDG_DATA_DIRS`** 裡的 `applications/`。本機：

```text
XDG_DATA_DIRS=/usr/share/ubuntu:/usr/share/gnome:/usr/local/share/:/usr/share/:/var/lib/snapd/desktop
```

**沒有** `/var/lib/flatpak/exports/share` 與 `/home/ubuntu/.local/share/flatpak/exports/share`，
所以 GNOME 看不到 flatpak 應用。此即一開始那則「XDG_DATA_DIRS 不含 flatpak exports」警告的實際表現。

### 修正（寫 `/etc/environment`，需 sudo）

```bash
if ! grep -q '^XDG_DATA_DIRS=' /etc/environment; then
  printf 'XDG_DATA_DIRS="/var/lib/flatpak/exports/share:/home/ubuntu/.local/share/flatpak/exports/share:/usr/share/ubuntu:/usr/share/gnome:/usr/local/share/:/usr/share/:/var/lib/snapd/desktop"\n' | sudo tee -a /etc/environment >/dev/null
  echo "已新增 XDG_DATA_DIRS"
else
  echo "XDG_DATA_DIRS 已存在，未重複新增"
fi
```

把兩個 flatpak exports 放**最前面**（優先），並保留原有路徑以免影響 snap／GNOME 其他選單。

### 驗證（必須**完全重新登入**）

`/etc/environment` 由 PAM 在**登入時**讀取；X11 下 `Alt+F2` 輸入 `r` 重殼層**不會**重讀它，
務必 logout 再登入。重開後：

```bash
echo "$XDG_DATA_DIRS"                 # 應含 /var/lib/flatpak/exports/share
ls /var/lib/flatpak/exports/share/applications/   # 應有 it.mijorus.gearlever.desktop
```

---

## 問題 3b：Token Monitor AppImage 齒輪圖示（選單 + dock）

Token Monitor 是 AppImage。它的圖示問題分兩層，**各自有不同的根因與解法**——這是本 runbook 最容易混清的地方。

### 3b-1：應用程式選單（grid）齒輪 → `.desktop` 的 `Icon=` 指向不存在路徑

原始捷徑：

```text
Icon=/home/ubuntu/Applications/.icons/token_monitor     ← 這個目錄根本不存在 → 回退齒輪
```

真正的 icon 在 AppImage 內（有效、1024×1024 RGBA）：

```text
/home/ubuntu/Applications/Token Monitor/squashfs-root/usr/share/icons/hicolor/1024x1024/apps/token-monitor.png
```

**解法：改成絕對路徑指向一個 stable 複製品**（不依賴 icon 主題搜尋／快取，GNOME 直接讀這個 PNG）：

```bash
mkdir -p ~/.local/share/icons/hicolor/1024x1024/apps
cp "/home/ubuntu/Applications/Token Monitor/squashfs-root/usr/share/icons/hicolor/1024x1024/apps/token-monitor.png" \
   ~/.local/share/icons/hicolor/1024x1024/apps/token-monitor.png

sed -i 's|^Icon=.*|Icon=/home/ubuntu/.local/share/icons/hicolor/1024x1024/apps/token-monitor.png|' \
   ~/.local/share/applications/token_monitor.desktop
gtk-update-icon-cache -q -t ~/.local/share/icons/hicolor/
grep '^Icon=' ~/.local/share/applications/token_monitor.desktop   # Icon=/home/ubuntu/.local/.../token-monitor.png
```

> 為何不用相對名 `Icon=token-monitor`？即使檔案在標準 hicolor 路徑且快取最新，GNOME 有時仍不解析，
> 絕對路徑最穩。若要用相對名，請確認 `icon-theme.cache` 時間**晚於** icon 檔（過期快取看不到新圖）。

### 3b-2：開窗時的 Ubuntu Dock 齒輪 → `StartupWMClass` 與實際視窗類別不匹配

這是「選單正常、開窗才齒輪」的元兇。用 `wmctrl` 找出**實際視窗類別**：

```bash
sudo apt install -y wmctrl
wmctrl -lx
# 0x03400004  0 token-monitor.token-monitor  p53vm01  Token Monitor
#                 └─────── 實際 WM_CLASS ───────┘
```

原捷徑寫 `StartupWMClass=Token Monitor`（有空格、大小寫不同），與實際 class `token-monitor` **不匹配**，
GNOME 無法把視窗接到捷徑，於是 dock 顯示視窗內建的齒輪。

**解法：改成與實際 class 一致**：

```bash
sed -i 's|^StartupWMClass=.*|StartupWMClass=token-monitor|' ~/.local/share/applications/token_monitor.desktop
grep '^StartupWMClass=' ~/.local/share/applications/token_monitor.desktop   # StartupWMClass=token-monitor
```

然後**徹底重開程式**（先 `pkill -f token_monitor.appimage`，再從選單重啟，不要雙擊 AppImage），
讓 GNOME 用改好的 `.desktop` 重新接上視窗。若仍有殘留，logout 再登入最保險。

> 偵錯技巧備註：`wmctrl -lx` 的第三欄 `CLASS.CLASS` 就是 `StartupWMClass` 要對上的值。
> Electron／AppImage 常把 class 設成二進位名（小寫、連字號），與 `.desktop` 的 `Name` 不同，
> 這是 AppImage「進窗才齒輪」極常見的原因。

---

## dsh vs opencode 佈署對照

兩次都在 **Ubuntu 24.04.5 desktop** 上完成「flatpak → Gear Lever / AppImage → 圖示」鏈，但**權限模型不同，導致走 system mode 還是 user mode**——這是最關鍵的分野：

| 維度 | **dsh**（本次，本機本地 session） | **opencode**（前一場，受限主機） |
|---|---|---|
| root / sudo | **用戶 terminal 的 sudo 可用**（`sudo flatpak install`、`sudo apt install wmctrl` 皆成功） | **`sudo` 不可用**——主機 `NoNewPrivs: 1`，setuid helper（`fusermount`）也失效 |
| flatpak 模式 | **system mode**（`/var/lib/flatpak`，`sudo flatpak remote-add / install`） | **user mode**（`--user`，`~/.local/flatpak-env` 自帶 `LD_LIBRARY_PATH`／`XDG_DATA_DIRS`；`/var/lib/flatpak` 不存在） |
| AppImage 啟動 | 直接執行（系統有 FUSE）＋`--appimage-extract` 皆可用 | **只能 `--appimage-extract`**（自備 `libfuse.so.2` 仍 `fusermount: Operation not permitted`，是唯一可行路徑） |
| XDG_DATA_DIRS 修正 | 寫入 **`/etc/environment`**（需 sudo），logout/relogin 生效 | 無 system 範圍；改由 flatpak 包裝檔帶入 `XDG_DATA_DIRS` |
| 圖示修正 | `.desktop` `Icon=` 絕對路徑 + `StartupWMClass`（`wmctrl -lx` 找出 class = `token-monitor`） | 同左（`.desktop` 編輯在 home 下，由用戶執行） |
| 已知限制 | — | 無 `xdg-desktop-portal` → `org.freedesktop.portal.Flatpak was not provided`；GTK4 擷取需 `GSK_RENDERER=cairo` |
| 部署拓撲 | DeepSeek Harness **本機本地 session**（Web GUI） | 用戶在**另一台電腦**跑 opencode，遠端操作本機 |
| agent 角色 | 強診斷（直接讀 `/proc`／`/etc`、跑 `curl`／`wmctrl`／`xwininfo`）；但 harness sandbox 的 `no_new_privs` 擋 agent 自行 sudo／寫 home flatpak 路徑 → privileged / home edits **由用戶執行** | 用戶遠端操作（具體連線／權限模型以該 session 為準） |

**結論**：兩種方式都能把應用程式部署到同一台 Linux desktop，但**能否用 system mode 取決於 sudo 是否可用**。
- **dsh 本次**：sudo 可用 → system mode + `/etc/environment`；agent 負責診斷（summary 路徑拼法、`wmctrl` 抓 class、`Icon=` 指向），privileged / home 編輯由用戶執行。
- **opencode 場次**：受限主機（`NoNewPrivs: 1`）→ 被迫 user mode + `--appimage-extract`，已 commit+push 在 [README.md](../../README.md#ubuntu-desktop實測注意事項) 與 [handoff.md](../../handoff.md#ubuntu-desktop實驗經驗）（Token Monitor AppImage 可行性、Gear Lever user-mode 67 秒安裝、無 portal 限制）。

本 runbook 補上 opencode 場次未涵蓋的部分：**system mode 下的 flathub summary 修復、Gear Lever 安裝與 `/etc/environment` 選單、以及 AppImage dock 的 `StartupWMClass` 齒輪**。本 runbook 與 [`packs/token-monitor/`](../../packs/token-monitor/) 的 Ubuntu 驗證則為共同基礎。

---

## 可重複指令清單（Ubuntu 24.04.5 desktop）

```bash
# 1) flathub summary 修復（system，需 sudo）
sudo flatpak remote-delete flathub
sudo flatpak remote-add --if-not-exists flathub https://dl.flathub.org/repo/flathub.flatpakrepo
sudo flatpak update

# 2) 裝 Gear Lever
sudo flatpak install flathub it.mijorus.gearlever

# 3a) Gear Lever 進選單：XDG_DATA_DIRS 加入 flatpak exports（寫 /etc/environment，需 sudo），再 logout/relogin
# 3b-1) AppImage 選單圖示：絕對路徑 Icon= 指向 ~/.local/share/icons 的複製品 + gtk-update-icon-cache
# 3b-2) AppImage dock 圖示：wmctrl -lx 找出實際 class，改 StartupWMClass 對上，徹底重開程式
```

## 經驗與坑（含 agent sandbox 限制）

1. **summary 路徑由 base URL 決定**：flatpak 1.14 在 base 後接 `summary.idx`；base 用 `flathub.org`
   會 404，用 `dl.flathub.org/repo/` 才拼出可用的 `/repo/summary.idx`（200）。`.flatpakrepo` 是最乾淨的固定方式。
2. **選單圖示用絕對路徑最穩**：`.desktop` 的 `Icon=` 必須指向**存在**的檔案；相對名依賴主題搜尋與快取，
   容易因快取過期或搜尋路徑問題回退齒輪。
3. **dock 齒輪多半是 `StartupWMClass` 不匹配**：先 `wmctrl -lx` 抓實際 class（常是二進位小寫連字號），
   再對上 `.desktop`。這是 AppImage「進窗才齒輪」最常见原因。
4. **flatpak 選單要重登**：`XDG_DATA_DIRS` 由 PAM 在登入時讀，改 `/etc/environment` 後必須 logout/relogin，
   僅重啟 gnome-shell（`Alt+F2:r`）不會生效。
5. **agent sandbox 限制（dsh 特別注意）**：harness 給 agent shell 設 `no_new_privs=1`，**會擋掉 sudo**；
   sandbox 也會擋 home 下 flatpak 路徑的寫入。因此「需 sudo」與「改 home 下 `.desktop`／`/etc/environment`」
   這些動作由**用戶在本機 terminal 執行**最快，agent 負責診斷與給出精確指令。
6. **不要盲信建議中的參數**：例如 `remote-add --force-toggle` 的 `--force-toggle` 對 `remote-add` 非法；
   執行前先用 `flatpak <cmd> --help` 確認參數、用 curl 端點實測再動。

## 相關文件

- 本 runbook：`packs/linux-flatpak-deploy/README.md`
- 平台驗證與證據：`packs/linux-flatpak-deploy/compatibility.md`
- Token Monitor（AppImage）既有 Ubuntu 驗證：`packs/token-monitor/compatibility.md`、`packs/token-monitor/README.md`
