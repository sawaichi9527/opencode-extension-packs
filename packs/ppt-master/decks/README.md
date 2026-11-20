# FII 2026 Deck Templates

本目錄是團隊自有的 FII 品牌 Deck 模板，隨 `ppt-master` Pack 一起提供，供團隊安裝
PPT Master Skill 後導入使用。它不是上游 `hugohe3/ppt-master` 的內容，也不是第三方
Plugin，因此直接收錄在本 Repository，方便所有成員取得一致的品牌模板。

## 內容（2026 三語系版）

| Deck ID | 語言 | 用途 |
|---|---|---|
| `fii_2026_zh_tw` | 繁體中文 | 企業內部報告、客戶提案、專案匯報、年度總結 |
| `fii_2026_zh_cn` | 简体中文 | 企业内部报告、客户提案、项目汇报、年度总结 |
| `fii_2026_en` | English | Internal reports, client proposals, project updates, annual summaries |

三個版本共用同一套視覺與版式（封面、目錄、內容、結尾），只差在語言與字型堆疊；
皆為 1280×720（`ppt169`）、主色 `#173367`。

## 退役版本（`_Archive/`）

`_Archive/fii_2026_bright/` 與 `_Archive/fii_2026_dark/` 是 2026 舊版（亮色／暗色），
已由上述三語系版本取代，**不再列入 `decks_index.json`**。保留僅供歷史追溯與舊簡報
對照；除非明確需要舊版，不要複製到已安裝 Skill。

## Workspace 結構

- `templates/design_spec.md`：Deck 的設計規範（色彩、字型、頁面容納表、素材）
- `templates/01_cover.svg`、`02_toc.svg`、`03_content.svg`、`04_ending.svg`：完整 SVG 原型與 slot 定義
- `images/`：背景圖與 FII 標誌

## 導入方式

安裝 PPT Master Skill 後，將需要的語系 Deck 複製到已安裝 Skill 的 `templates/decks/`
（或個人模板層 `templates/local/decks/`，見下方說明）：

### Windows PowerShell

```powershell
$decks = "$env:USERPROFILE\.config\opencode\skills\ppt-master\templates\decks"
New-Item -ItemType Directory -Force $decks | Out-Null
Copy-Item ".\decks\fii_2026_zh_tw" $decks -Recurse -Force
Copy-Item ".\decks\fii_2026_zh_cn" $decks -Recurse -Force
Copy-Item ".\decks\fii_2026_en" $decks -Recurse -Force
Copy-Item ".\decks\decks_index.json" $decks -Force
```

### WSL / Ubuntu / macOS

```bash
decks="$HOME/.config/opencode/skills/ppt-master/templates/decks"
mkdir -p "$decks"
cp -r ./decks/fii_2026_zh_tw "$decks/"
cp -r ./decks/fii_2026_zh_cn "$decks/"
cp -r ./decks/fii_2026_en "$decks/"
cp ./decks/decks_index.json "$decks/"
```

若已安裝 Skill 的 `templates/decks/decks_index.json` 已有其他 Deck 記錄，請以 JSON
合併方式加入這三筆，不要直接覆蓋。

> 6.10.0 起另支援 Git-ignored 的個人模板層 `templates/local/<kind>/`（含
> `templates/local/decks/`），內建層與個人層會在記憶體中合併。Skill-only 安裝（非 Git
> checkout）可把團隊 Deck 複製到 `templates/decks/`，或放進 `templates/local/decks/`。

> 路徑提醒：已安裝的 Skill 可能在原生 `~/.config/opencode/skills/ppt-master/`，或相容
> `~/.agents/skills/ppt-master/`（`npx skills add -g -a opencode` 的實際落點）。上例使用原生路徑，
> 若是用 `npx skills add` 安裝，請把 `decks` 變數改為 `"$HOME/.agents/skills/ppt-master/templates/decks"`。

## 註冊

`decks_index.json` 是本目錄 Deck 的 discovery 來源。新增或修改 Deck 時必須同步更新
此索引，與 PPT Master 的 `templates/decks/decks_index.json` 格式一致。`_Archive/`
內的舊版不註冊。
