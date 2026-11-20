---
deck_id: fii_2026_zh_tw
kind: deck
category: brand
summary: FII 品牌簡報版模，適用企業內部報告、客戶提案與專案匯報；白底、深藍字與 AI 科技插畫構成正式一致的視覺。
keywords: [FII, 品牌, 企業簡報, 2026]
primary_color: "#173367"
canvas_format: ppt169
canvas_width: 1280
canvas_height: 720
canvas_viewbox: "0 0 1280 720"
source_canvas_width: 1280
source_canvas_height: 720
source_viewbox: "0 0 1280 720"
replication_mode: fidelity
native_structure_mode: structured
page_count: 4
placeholders:
  01_cover: ["{{TITLE}}", "{{SUBTITLE}}"]
  02_toc: ["{{TITLE}}", "{{TOC_ITEM_1_TITLE}}", "{{TOC_ITEM_2_TITLE}}", "{{TOC_ITEM_3_TITLE}}", "{{TOC_ITEM_4_TITLE}}"]
  03_content: ["{{PAGE_TITLE}}", "{{CONTENT_AREA}}", "{{PAGE_NUM}}"]
  04_ending: ["{{CLOSING_MESSAGE}}"]
---

# FII 2026 繁體中文 — Design Specification

## I. Template Overview

| Application context | Definition |
| --- | --- |
| Recurring presentation family | 企業內部報告、客戶提案、專案匯報與年度總結 |
| Intended audiences and outcomes | 面向內部主管、專案團隊與客戶決策者；協助受眾快速掌握主題、方案與下一步 |
| Delivery and reading assumptions | 以會議講解為主，會後亦需可獨立閱讀與流轉 |
| Representative narrative/page roles | 封面、目錄、內容與結尾四個原型；實際選頁、順序與內容由當前專案的 Strategist 決定 |

- 視覺以白底與深藍字為主，封面與結尾採滿版 AI 科技插畫，內容頁以淡藍背景維持輕量與正式感。

## II. Color Scheme

| Role | Color | Application |
| --- | --- | --- |
| Deep navy | #173367 | 標題、頁尾與主要文字 |
| FII red | #D00034 | 強調線與重點標示 |
| Text black | #000000 | 正文內容 |
| Light blue | #D5E4FF | 內容頁背景裝飾 |
| White | #FFFFFF | 畫面留白與反白文字 |

## III. Typography

| Role | Font stack | Application |
| --- | --- | --- |
| 標題 | `"Microsoft JhengHei", "Noto Sans TC", Arial, sans-serif` | 封面主標、頁標題與結尾標語 |
| 正文 | `"Microsoft JhengHei", "Noto Sans TC", Arial, sans-serif` | 內文、目錄項目與說明 |
| 標籤／頁碼 | `Arial, "Microsoft JhengHei", sans-serif` | 頁尾、頁碼與英文標籤 |

以 Windows 內建微軟正黑體為主，Noto Sans TC 為跨平台回退；不依賴額外字型安裝。

字級基準：封面 72 px、目錄 58.67 px、頁標題 32 px、正文 18 px、頁碼 13.33 px、結尾 58.67 px。頁碼由兩部分組成：`{{PAGE_NUM}}` 匯出為 PowerPoint 的 slidenum 欄位，會自動跟隨頁序；總頁數 `/N` 是 Layout 上的靜態文字（`Title and Content` 的 `layout-page-total`）。PowerPoint 沒有自動「總頁數」欄位；FII 以 `layout-page-total` 搭配匯出 patch 或 `FII_PageTotals.ppam` 自動維護，未安裝時才需在「檢視 > 投影片母片」手動修改該 Layout 的 `/N`。

頁碼自動化：目前頁是 PowerPoint `slidenum` 欄位；總頁數 `/N` 位於 `Title and Content` Layout 的 `layout-page-total`（`data-pptx-page-total="true"`）。`svg_to_pptx.py` 若已套用 FII page-total patch，會在匯出時自動寫入實際頁數；使用原始 upstream 腳本時，請在匯出後執行 `python scripts/sync_page_totals.py <deck.pptx>`（本 pack 鏡像：`packs/ppt-master/tools/sync_page_totals.py`）。在 PowerPoint 內編輯時，`FII_PageTotals.ppam`（本 pack `packs/ppt-master/addins/`）會自動維護總頁數。

## IV. Signature Design Elements

- 封面與結尾使用滿版 AI 科技插畫（燈塔、雲端、機器人與資料中心），插畫集中於畫面右側或中央，左側保留標題區。
- 內容與目錄頁使用淡藍科技背景，右上角配置 FII 橫式標誌，左下角固定版權頁尾。
- 標題採深藍 `#173367` 高字重；紅 `#D00034` 僅用於少量強調，不作為大面積底色。
- 所有版面共用 42 px 左右安全邊界；標題靠左，內容區保持開放，避免預先畫出卡片或表格。

## V. Page Roster

| File | Layout key | PowerPoint picker name | Visual character | Reusable slots |
| --- | --- | --- | --- | --- |
| `01_cover.svg` | cover | Cover | 滿版 AI 插畫、左上 FII 標誌、左側大型標題與副標 | 標題、副標（姓名／日期） |
| `02_toc.svg` | toc | Agenda | 淡藍背景、右上標誌、左側目錄標題與四行編號項目 | 頁標題、四個目錄項目 |
| `03_content.svg` | content | Title and Content | 淡藍背景、右上標誌、頂部頁標題與大面積開放內容區 | 頁標題、內容區、頁碼 |
| `04_ending.svg` | ending | Section / Closing | 滿版 AI 插畫、左上標誌、中央結尾標語 | 結尾標語 |

## VI. Assets

| File | Intended usage |
| --- | --- |
| image1.png | 封面滿版背景插畫 |
| image2.png | FII 橫式標誌（各頁固定元素） |
| image3.png | 目錄與內容頁淡藍背景 |
| image4.png | 結尾頁滿版背景插畫 |
