---
deck_id: fii_2026_bright
kind: deck
category: brand
summary: FII 品牌簡報模板，適用於企業內部報告、客戶簡報與專案提案
keywords: [FII, 品牌, 企業簡報, 正式, 專業]
primary_color: "#002F75"
canvas_format: ppt169
canvas_width: 1280
canvas_height: 720
canvas_viewbox: "0 0 1280 720"
source_canvas_width: 1280
source_canvas_height: 720
source_viewbox: "0 0 1280 720"
replication_mode: fidelity
native_structure_mode: structured
page_count: 5
page_types: [cover, chapter, content, content, ending]
placeholders:
  01_cover: ["{{TITLE}}", "{{SUBTITLE}}", "{{DATE}}", "{{AUTHOR}}"]
  02_chapter: ["{{CHAPTER_NUM}}", "{{CHAPTER_TITLE}}"]
  03a_content_standard: ["{{PAGE_TITLE}}", "{{CONTENT_AREA}}", "{{PAGE_NUM}}"]
  03b_content_image: ["{{PAGE_TITLE}}", "{{CONTENT_AREA}}", "{{IMAGE}}", "{{PAGE_NUM}}"]
  04_ending: ["{{THANK_YOU}}", "{{CONTACT_INFO}}"]
---

# FII 2026 亮色版 — 設計規範

## I. 範本概述

| 應用上下文 | 定義 |
|---|---|
| 適用簡報類型 | 企業內部報告、客戶提案、專案匯報、年度總結 |
| 目標受眾 | 管理層、客戶、合作夥伴 |
| 呈現方式 | 投影片簡報（presented）搭配書面閱讀（close-read） |
| 代表頁面角色 | 封面、章節過渡、內容頁（含圖片版）、結尾 |

- 設計風格：專業、簡潔、明亮。以深藍色 `#002F75` 為品牌主色，搭配高品質背景圖像與清晰字型層級。
- 主題模式：light（亮色）

## II. 色彩配置

| 角色 | HEX | 用途 |
|---|---|---|
| 主要色 | `#002F75` | 標題文字、品牌重點元素 |
| 次要色 | `#2E75B6` | 漸層覆蓋、次要強調 |
| 強調色 | `#D00034` | 連接線、重點標記 |
| 背景色 | `#FFFFFF` | 內頁內容區背景 |
| 文字主色 | `#000000` | 正文內容 |
| 次要文字 | `#D9D9D9` | 次要資訊、灰色區塊 |
| 深色文字 | `#0D0D0D` | 深色標籤文字 |

## III. 字型

| 角色 | 字型堆疊 |
|---|---|
| 封面標題 | `"思源黑体 CN Heavy", "Source Han Sans SC Heavy", "Microsoft YaHei", sans-serif` |
| 頁面標題 | `"微软雅黑", "Microsoft YaHei", "思源黑体 CN Heavy", sans-serif` |
| 副標題/章節 | `"思源黑体 CN Heavy", "Microsoft YaHei", sans-serif` |
| 正文 | `"思源黑体 CN Normal", "Microsoft YaHei", Arial, sans-serif` |
| 頁碼 | `Arial, sans-serif` |

- 主標題：48px，粗體，`#002F75`
- 正文：21.33px（16pt），一般，`#000000`
- 字型均為 Windows 常見預裝字型，無需額外安裝

## IV. 標誌性設計元素

- **全幅背景圖**：Master 提供全幅背景圖，每頁可自訂獨立背景
- **品牌 Logo**：左上角放置 FII Logo（image2.png），尺寸約 109×61px
- **頁面標題列**：標題位於頁面左上區，使用 `#002F75` 粗體 48px
- **內容區**：主標題下方為內容區域，左側對齊
- **頁碼**：右下角淺色底 `‹#›/N` 格式，16px，白色
- **連接線**：紅色 `#D00034` 細線用於資訊層級引導
- **結尾頁**：深藍漸層覆蓋搭配「感謝聆聽」字樣

## V. 頁面容納表

| 檔案 | Layout Key | PowerPoint 名稱 | 內容型態 | 插槽行為 |
|---|---|---|---|---|
| `01_cover.svg` | `cover` | 封面 | 品牌封面 | {{TITLE}} 主標題置中；{{SUBTITLE}} 副標題；{{DATE}} 日期；{{AUTHOR}} 作者/部門 |
| `02_chapter.svg` | `chapter` | 章節頁 | 章節過渡 | {{CHAPTER_NUM}} 章節編號；{{CHAPTER_TITLE}} 章節名稱 |
| `03a_content_standard.svg` | `content_standard` | 標準內容 | 標準內頁 | {{PAGE_TITLE}} 頁面標題；{{CONTENT_AREA}} 自由內容區；{{PAGE_NUM}} 頁碼 |
| `03b_content_image.svg` | `content_image` | 圖片內容 | 含圖片內頁 | {{PAGE_TITLE}} 頁面標題；{{CONTENT_AREA}} 文字內容；{{IMAGE}} 圖片插槽；{{PAGE_NUM}} 頁碼 |
| `04_ending.svg` | `ending` | 結尾 | 品牌結尾 | {{THANK_YOU}} 感謝語；{{CONTACT_INFO}} 聯絡資訊 |

## VI. 素材

| 檔案 | 尺寸 | 用途 |
|---|---|---|
| `image1.jpg` | 1280×720 | 主要背景（通用內頁背景） |
| `image2.png` | ~109×61 | FII Logo（母版左上角） |
| `image3.jpg` | 1280×720 | 結尾頁背景 |
| `image4.jpg` | 1280×720 | 封面背景 |
| `image5.png` | - | 輔助素材 |
