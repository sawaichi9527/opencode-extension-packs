# FII 2026 Deck Templates

本目錄是團隊自有的 FII 品牌 Deck 模板，隨 `ppt-master` Pack 一起提供，供團隊安裝
PPT Master Skill 後導入使用。它不是上游 `hugohe3/ppt-master` 的內容，也不是第三方
Plugin，因此直接收錄在本 Repository，方便所有成員取得一致的品牌模板。

## 內容

| Deck ID | 主題 | 用途 |
|---|---|---|
| `fii_2026_bright` | FII 2026 亮色版 | 企業內部報告、客戶簡報、專案提案、年度總結 |
| `fii_2026_dark` | FII 2026 暗色版 | 企業內部報告、客戶提案、專案匯報、教學說明 |

每個 Deck 目錄都符合 PPT Master 的 Deck workspace 規範：

- `templates/design_spec.md`：Deck 的設計規範（色彩、字型、頁面容納表、素材）
- `templates/*.svg`：每個頁面的完整 SVG 預覽與 slot 定義
- `images/`：Deck 使用的背景圖、Logo 與輔助素材
- `templates/native_payloads.json.gz` 與 `template_execution/`（僅 dark）：structured mirror
  執行所需的本機 payload 與文字 slot manifest，匯入時不可刪除

## 導入方式

安裝 PPT Master Skill 後，將此目錄的 Deck 複製到已安裝 Skill 的
`templates/decks/` 下：

### Windows PowerShell

```powershell
$decks = "$env:USERPROFILE\.config\opencode\skills\ppt-master\templates\decks"
New-Item -ItemType Directory -Force $decks | Out-Null
Copy-Item ".\decks\fii_2026_bright" $decks -Recurse -Force
Copy-Item ".\decks\fii_2026_dark" $decks -Recurse -Force
Copy-Item ".\decks\decks_index.json" $decks -Force
```

### WSL / Ubuntu / macOS

```bash
decks="$HOME/.config/opencode/skills/ppt-master/templates/decks"
mkdir -p "$decks"
cp -r ./decks/fii_2026_bright "$decks/"
cp -r ./decks/fii_2026_dark "$decks/"
cp ./decks/decks_index.json "$decks/"
```

若已安裝 Skill 的 `templates/decks/decks_index.json` 已有其他 Deck 記錄，請以 JSON
合併方式加入本 Pack 的 `fii_2026_bright` 與 `fii_2026_dark` 兩筆，不要直接覆蓋。

## 註冊

`decks_index.json` 是本目錄 Deck 的 discovery 來源。新增或修改 Deck 時必須同步更新
此索引，與 PPT Master 的 `templates/decks/decks_index.json` 格式一致。
