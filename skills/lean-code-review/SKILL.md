---
name: lean-code-review
description: 使用 OpenCode 檢查目前 Git diff 是否有過度設計、不必要依賴、重複實作或可直接簡化的程式碼；預設只提出建議，不自動修改。當使用者說「檢查是否過度設計」「精簡程式碼 review」「哪些可以刪除」「review current diff」時載入。
license: MIT
---

# Lean Code Review for OpenCode

## 目的

針對 OpenCode 目前協助修改的程式碼，找出「需求沒有要求、但實作卻額外增加」的複雜度。

這不是一般 Correctness Review，也不取代既有測試、Security Review 或 SWQA Test Plan。

## 預設範圍

1. 確認目前位於 Git Repository。
2. 執行 `git status --short`。
3. 讀取目前未提交的 `git diff`；若有 staged 內容，再讀取 `git diff --cached`。
4. 只審查這次變更，不掃描整個 Repository。
5. 只有使用者明確要求「檢查整個 Repository」時，才擴大範圍。

## 檢查順序

對每項新增或修改先依序判斷：

1. 專案內是否已有相同 Helper、Utility、Pattern 或共用函式可重用？
2. Standard Library 是否已經提供相同能力？
3. 作業系統、語言或框架的原生功能是否已經涵蓋？
4. 已安裝的 Dependency 是否已經能完成，不需再增加新套件？
5. 是否加入了目前沒有第二個使用者的 Interface、Factory、Wrapper、Config 或抽象層？
6. 是否只修了單一路徑症狀，而真正 Root Cause 位於共用函式？
7. 是否修改了與需求無關的檔案、格式、命名或註解？

## Finding 類型

使用下列標記，並以繁體中文說明：

- `delete`：這次變更加入但實際不需要，可直接移除。
- `reuse`：專案內已有功能，應重用而不是重新實作。
- `stdlib`：可改用語言標準函式庫。
- `native`：可改用平台、框架或資料庫原生能力。
- `dependency`：新增套件不必要，現有能力已足夠。
- `yagni`：為尚未存在的需求加入抽象、彈性或設定。
- `shrink`：相同邏輯可用更直接、較少的程式碼表達。
- `root-cause`：應修正共用根因，而不是只補單一 Caller。

## 輸出格式

依影響程度排序，每項一行：

```text
<file>:L<line> [tag] 問題。建議的較簡單作法。
```

最後摘要：

```text
結論：可精簡 N 項；預估可移除約 M 行／K 個 Dependency。
```

若沒有明顯問題：

```text
目前變更已足夠精簡，可以進入一般 Correctness 與測試驗證。
```

## 邊界

- 預設只提出建議，不直接修改檔案。
- 不把必要的錯誤處理、輸入驗證、安全措施或資料遺失防護視為過度設計。
- 不因追求較少程式碼而移除 SWQA 要求的測試 Coverage、Log、Report、Verdict、Traceability、Timeout／Retry 或硬體校正。
- 不檢查純文件內容，除非文件變更本身造成不必要的開發流程複雜度。
- 修正建議仍須符合專案 `AGENTS.md` 與使用者明確需求。
