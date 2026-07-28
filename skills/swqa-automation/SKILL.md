---
name: swqa-automation
description: 為 Python、UART／TTY、Console Log、Wireshark／PCAP、API、CLI、Device 或整合測試增加通用專案結構與工作規則；不綁定特定產品或 Web UI 框架。
---

# SWQA Automation

## 適用情境

- Python 測試程式、pytest、CLI 或整合測試自動化
- UART／TTY Console 控制、原始 Log 保存與 Parser 驗證
- Wireshark／tshark Capture、PCAP／PCAPNG 與 Protocol Field 驗證
- API、Device、Network 或少量 Web 測試
- 小型 SWQA 團隊共同開發或個人自動化研究

## 建議結構

```text
project/
├── src/
├── tests/
├── test-data/
├── logs/
├── reports/
├── artifacts/
│   ├── uart/
│   └── pcap/
└── docs/
```

依專案需求調整，不為空目錄建立無用結構。

## 規則

- `test-data/` 不保存真實客戶、員工、產品機密或其他敏感資料。
- `logs/`、`reports/` 與 `artifacts/` 預設不全部提交 Git；先定義保留期限與 `.gitignore` 規則。
- 測試腳本、測試案例描述與正式 Acceptance Criteria 分離。
- 執行測試前顯示目標環境、DUT／firmware、可能影響與預估執行範圍。
- 破壞性、壓力、硬體控制或長時間測試必須取得確認。
- UART 測試保存適用的 Port 設定、原始 TX／RX Log 與 Parser 結果；Parser 摘要不能取代原始 Log。
- Packet 測試保存原始 PCAP／PCAPNG、Capture Interface、時間範圍、Filter 與關鍵 Frame Number；Display Filter 不能取代原始封包。
- 跨 Python、UART、PCAP 與 DUT Log 的驗證應記錄共同時間 Anchor 或已知 Clock Offset。
- Verdict 必須可追溯到明確條件與原始證據，不只依賴模型摘要。
