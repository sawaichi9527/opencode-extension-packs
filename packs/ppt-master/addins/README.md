# FII Page Totals add-in

`FII_PageTotals.ppam` keeps the **total page count** in an FII deck in sync
while you edit in PowerPoint.

PowerPoint only has a *current page* field (`slidenum`); it has no total-slides
field. The FII templates therefore keep `/N` as literal text on the
`Title and Content` layout. This add-in updates that literal whenever the deck
changes, so a slide added after `3/4` becomes `4/5` (and the total becomes `/5`)
without a manual visit to the Slide Master.

## What it updates

Every shape whose name contains `page-total` (case-insensitive), in:

- each Slide Master's custom layouts (the normal place for the `/N` text), and
- each slide (for decks that keep a slide-local total).

The FII templates use the shape name `layout-page-total`.

It runs on:

| Event | Why |
| --- | --- |
| add-in load / `Auto_Open` | fix decks already open when PowerPoint starts |
| `PresentationOpen` | fix a deck as soon as it is opened |
| `PresentationNewSlide` | update immediately after inserting a slide |
| `PresentationBeforeSave` | catch deletions and any other edits on save |
| `SlideShowBegin` | guarantee the total before presenting |
| `WindowActivate` | catch deletions when returning to the window |

## Install

```powershell
pwsh -NoProfile -ExecutionPolicy Bypass -File .\install_page_totals.ps1
```

This copies the `.ppam` to `%APPDATA%\Microsoft\AddIns` and registers it under
`HKCU\Software\Microsoft\Office\16.0\PowerPoint\AddIns\FII_PageTotals`
(`AutoLoad=1`, `LoadBehavior=3`). Restart PowerPoint.

Manual alternative: **File > Options > Add-ins > Manage: PowerPoint Add-ins >
Go > Add New**, then select `FII_PageTotals.ppam`.

## Verify

1. Open an FII deck and insert a slide after the content page.
2. The footer should change from `3/4` to `4/5` immediately.
3. Delete the slide and save; the footer returns to `3/4`.

## Uninstall

```powershell
Remove-Item "$env:APPDATA\Microsoft\AddIns\FII_PageTotals.ppam" -Force
Remove-Item "HKCU:\Software\Microsoft\Office\16.0\PowerPoint\AddIns\FII_PageTotals" -Recurse -Force
```

Then restart PowerPoint.

## Notes

- The add-in is unsigned. If macros are disabled by Trust Center, enable
  **Macro Settings > Enable VBA macros** or trust
  `%APPDATA%\Microsoft\AddIns` for this add-in.
- Decks that do not contain a `page-total` shape are left untouched.
- The add-in does not install a slide number field; current-page numbering is
  already a real `slidenum` field after `svg_to_pptx` export.
- For non-PowerPoint pipelines, use
  [`../tools/sync_page_totals.py`](../tools/sync_page_totals.py) instead.
