---
name: swqa-automation
description: 為 SWQA 自動化專案增加 tests、test-data、logs、reports、artifacts 等結構與工作規則；不綁定特定產品或測試框架。
---

# SWQA Automation

## 適用情境

- API、CLI、Web、Device 或整合測試自動化
- 小型 SWQA 團隊共同開發
- 個人自動化研究專案

## 建議結構

```text
project/
├── src/
├── tests/
├── test-data/
├── logs/
├── reports/
├── artifacts/
└── docs/
```

## 規則

- `test-data/` 不保存真實客戶、學生、員工或產品敏感資料。
- `logs/` 與 `artifacts/` 預設不全部提交 Git。
- 測試腳本與測試案例描述分離。
- 執行測試前先顯示目標環境與可能影響。
- 破壞性、壓力或長時間測試必須取得確認。
