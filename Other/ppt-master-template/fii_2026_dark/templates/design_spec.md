---
deck_id: fii_2026_dark
kind: deck
category: brand
summary: FII 品牌暗色簡報模板，適用於企業內部報告、客戶提案、專案匯報與教學說明
keywords: [FII, 品牌, 暗色, 企業簡報, 正式]
primary_color: "#002F75"
canvas_format: ppt169
canvas_width: 1280
canvas_height: 720
canvas_viewbox: "0 0 1280 720"
source_canvas_width: 1280
source_canvas_height: 720
source_viewbox: "0 0 1280 720"
replication_mode: mirror
native_structure_mode: structured
page_count: 5
page_types: [cover, content, content, content, ending]
placeholders:
  001_cover: ["封面標題", "姓名", "202X年XX月XX日"]
  002_content: ["主標題", "副標題", "正文內容"]
  003_content: ["主標題", "副標題", "正文內容"]
  004_content: ["主標題", "副標題", "正文內容"]
  005_ending: ["感謝聆聽"]
---

# FII 2026 暗色版 — 設計規範

## I. 範本概述

| 應用上下文 | 定義 |
|---|---|
| 適用簡報類型 | 企業內部報告、客戶提案、專案匯報、教學說明 |
| 目標受眾 | 管理層、客戶、合作夥伴、內部培訓對象 |
| 呈現方式 | 投影片簡報（presented）搭配書面閱讀（close-read） |
| 代表頁面角色 | 封面、內容說明、色彩與版式示例、結尾 |

- 設計風格：正式、深色、具有高對比資訊層級。
- 主題模式：dark（暗色）
- 本 workspace 由公司提供的 `繁體-FII PPT模板2026 暗.pptx` 以 `mirror` 方式 materialize。

## II. 色彩配置

| 角色 | HEX | 用途 |
|---|---|---|
| 主要品牌色 | `#002F75` | 深藍背景、品牌結構元素 |
| 強調色 | `#D10034` | 標題標記、連接線與重點提示 |
| 主要文字 | `#FFFFFF` | 暗色背景上的標題與正文 |
| 次要文字 | `#808080` | 次要說明與示例文字 |
| 黑色文字 | `#000000` | 淺色局部或頁碼區域 |

## III. 字型

| 角色 | 字型堆疊 |
|---|---|
| 標題 | `"思源黑体 CN Heavy", "微软雅黑", sans-serif` |
| 正文 | `"思源黑体 CN Normal", "微软雅黑", sans-serif` |
| 頁碼 | `"等线", sans-serif` |

- 封面主標題：96px，粗體，白色
- 內容標題：48px，粗體，白色
- 內容正文：21.33px，白色或灰色，依頁面對比需求使用

## IV. 標誌性設計元素

- 深色全幅背景與白色高對比文字。
- FII 紅色 `#D10034` 作為資訊層級與導引強調色。
- 內容頁使用深藍與紅色的結構線、標記與示例區塊。
- 頁碼保留右下角 `‹#›/N` 顯示位置。
- 末頁使用完整深色背景與「感謝聆聽」結尾語。

## V. 頁面容納表

| 檔案 | Page Type | 用途 |
|---|---|---|
| `001_cover.svg` | `cover` | 暗色品牌封面、標題、姓名與日期 |
| `002_content.svg` | `content` | 標題和內容示例，含資訊層級與連接線 |
| `003_content.svg` | `content` | 說明頁與品牌標準色、字型示例 |
| `004_content.svg` | `content` | 投影片母版、頁碼與版式操作說明示例 |
| `005_ending.svg` | `ending` | 深色結尾頁 |

Imported native payloads and text-slot manifests under `templates/` are required for structured mirror execution; do not delete them when copying the workspace.

## VI. 素材

| 檔案 | 用途 |
|---|---|
| `image1.jpg` | 共用暗色背景 |
| `image2.jpg` | 結尾頁背景 |
| `image3.jpg` | 封面背景 |
| `image4.png` | 內容示例圖片 |
| `image5.png` | 內容示例圖片 |
| `image6.png` | 內容示例圖片 |

## VII. Import Notes

原始 PPTX import 產生 6 個 `stroke-omitted` 非阻塞警告，原因是來源使用的 DrawingML miter limit `400000` 不在 importer 支援範圍內。這些警告記錄於 source import 的 `conversion-report.json`，不將原始 PPTX 或該暫存報告納入可攜式 workspace。
