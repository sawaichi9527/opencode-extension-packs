---
deck_id: fii_2026_zh_cn
kind: deck
category: brand
summary: FII 品牌演示模板，适用于企业内部报告、客户提案与项目汇报；白底、深蓝字与 AI 科技插画构成正式统一的视觉。
keywords: [FII, 品牌, 演示模板, 2026]
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

# FII 2026 简体中文 — Design Specification

## I. Template Overview

| Application context | Definition |
| --- | --- |
| Recurring presentation family | 企业内部报告、客户提案、项目汇报与年度总结 |
| Intended audiences and outcomes | 面向内部主管、项目团队与客户决策者；帮助受众快速理解主题、方案与下一步 |
| Delivery and reading assumptions | 以会议讲解为主，会后亦需可独立阅读与流转 |
| Representative narrative/page roles | 封面、目录、内容与结尾四个原型；实际选页、顺序与内容由当前项目的 Strategist 决定 |

- 视觉以白底与深蓝字为主，封面与结尾采用满版 AI 科技插画，内容页以淡蓝背景保持轻量与正式感。

## II. Color Scheme

| Role | Color | Application |
| --- | --- | --- |
| Deep navy | #173367 | 标题、页脚与主要文字 |
| FII red | #D00034 | 强调线与重点标示 |
| Text black | #000000 | 正文内容 |
| Light blue | #D5E4FF | 内容页背景装饰 |
| White | #FFFFFF | 画面留白与反白文字 |

## III. Typography

| Role | Font stack | Application |
| --- | --- | --- |
| 标题 | `"Microsoft YaHei", "Noto Sans SC", Arial, sans-serif` | 封面主标、页标题与结尾标语 |
| 正文 | `"Microsoft YaHei", "Noto Sans SC", Arial, sans-serif` | 内文、目录项目与说明 |
| 标签／页码 | `Arial, "Microsoft YaHei", sans-serif` | 页脚、页码与英文标签 |

以 Windows 内建微软雅黑为主，Noto Sans SC 为跨平台回退；不依赖额外字体安装。

字级基准：封面 72 px、目录 58.67 px、页标题 32 px、正文 18 px、页码 13.33 px、结尾 58.67 px。页码由两部分组成：`{{PAGE_NUM}}` 导出为 PowerPoint 的 slidenum 字段，会自动跟随页序；总页数 `/N` 是 Layout 上的静态文字（`Title and Content` 的 `layout-page-total`）。PowerPoint 没有自动“总页数”字段，新增或删除页面后，请在“视图 > 幻灯片母版”修改该 Layout 的 `/N` 一次，所有使用此 Layout 的页面会一起更新。

## IV. Signature Design Elements

- 封面与结尾使用满版 AI 科技插画（灯塔、云、机器人与数据中心），插画集中于画面右侧或中央，左侧保留标题区。
- 内容与目录页使用淡蓝科技背景，右上角放置 FII 横式标志，左下角固定版权页脚。
- 标题采用深蓝 `#173367` 高字重；红 `#D00034` 仅用于少量强调，不作为大面积底色。
- 所有版面共用 42 px 左右安全边界；标题靠左，内容区保持开放，避免预先画出卡片或表格。

## V. Page Roster

| File | Layout key | PowerPoint picker name | Visual character | Reusable slots |
| --- | --- | --- | --- | --- |
| `01_cover.svg` | cover | Cover | 满版 AI 插画、左上 FII 标志、左侧大型标题与副标 | 标题、副标（姓名／日期） |
| `02_toc.svg` | toc | Agenda | 淡蓝背景、右上标志、左侧目录标题与四行编号项目 | 页标题、四个目录项目 |
| `03_content.svg` | content | Title and Content | 淡蓝背景、右上标志、顶部页标题与大面積开放内容区 | 页标题、内容区、页码 |
| `04_ending.svg` | ending | Section / Closing | 满版 AI 插画、左上标志、中央结尾标语 | 结尾标语 |

## VI. Assets

| File | Intended usage |
| --- | --- |
| image1.png | 封面满版背景插画 |
| image2.png | FII 横式标志（各页固定元素） |
| image3.png | 目录与内容页淡蓝背景 |
| image4.png | 结尾页满版背景插画 |
