---
deck_id: fii_2026_en
kind: deck
category: brand
summary: FII branded presentation template for internal reports, client proposals, and project updates; formal visuals with white ground, navy type, and AI technology illustration.
keywords: [FII, brand, presentation, 2026]
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

# FII 2026 English — Design Specification

## I. Template Overview

| Application context | Definition |
| --- | --- |
| Recurring presentation family | Internal reports, client proposals, project updates, and annual summaries |
| Intended audiences and outcomes | Internal leaders, project teams, and client decision makers; helps the audience grasp the topic, proposal, and next step quickly |
| Delivery and reading assumptions | Presented in meetings and also read or forwarded afterwards |
| Representative narrative/page roles | Cover, agenda, content, and closing prototypes; page selection, order, and content are decided by the current project's Strategist |

- White ground and navy typography dominate; cover and closing use full-bleed AI technology illustration, while content pages stay light with a pale blue background.

## II. Color Scheme

| Role | Color | Application |
| --- | --- | --- |
| Deep navy | #173367 | Titles, footer, and primary text |
| FII red | #D00034 | Accent rules and emphasis |
| Text black | #000000 | Body content |
| Light blue | #D5E4FF | Content-page background accents |
| White | #FFFFFF | Negative space and reversed text |

## III. Typography

| Role | Font stack | Application |
| --- | --- | --- |
| Title | `Arial, "Microsoft YaHei", sans-serif` | Cover title, page titles, closing line |
| Body | `Arial, "Microsoft YaHei", sans-serif` | Body copy, agenda items, descriptions |
| Label / folio | `Arial, "Microsoft YaHei", sans-serif` | Footer, page number, labels |

Uses Arial, which ships with Windows and macOS; no extra font installation is required.

字級基準：封面 72 px、目錄 58.67 px、頁標題 32 px、正文 18 px、頁碼 13.33 px、結尾 48 px。頁碼採 `{{PAGE_NUM}}/N`，`N` 由作者改為該簡報總頁數。

## IV. Signature Design Elements

- Cover and closing use a full-bleed AI technology illustration (lighthouse, cloud, robot, data center) anchored right or center, leaving the left side for the title.
- Agenda and content pages use a pale blue technology background, the FII horizontal logo at top right, and a fixed copyright footer at bottom left.
- Titles use deep navy `#173367` at heavy weight; red `#D00034` is reserved for small emphasis, never large fills.
- All pages share roughly 42 px safe margins; titles stay left-aligned and the content zone stays open rather than pre-drawn as cards or tables.

## V. Page Roster

| File | Layout key | PowerPoint picker name | Visual character | Reusable slots |
| --- | --- | --- | --- | --- |
| `01_cover.svg` | cover | Cover | Full-bleed illustration, FII logo top left, large title and subtitle at left | Title, subtitle (name / date) |
| `02_toc.svg` | toc | Agenda | Pale blue background, logo top right, agenda title and four numbered items | Page title, four agenda items |
| `03_content.svg` | content | Title and Content | Pale blue background, logo top right, page title and a large open content zone | Page title, content area, page number |
| `04_ending.svg` | ending | Section / Closing | Full-bleed illustration, logo top left, centered closing line | Closing message |

## VI. Assets

| File | Intended usage |
| --- | --- |
| image1.png | Cover full-bleed background illustration |
| image2.png | FII horizontal logo (fixed on every page) |
| image3.png | Pale blue background for agenda and content |
| image4.png | Closing full-bleed background illustration |
