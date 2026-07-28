---
name: test-failure-triage
description: 分析 Python 自動化、UART／TTY Console Log、Wireshark／PCAP、DUT 或測試環境造成的失敗；先蒐證、分層與驗證單一假設，再提出修正。當使用者說「測試失敗」「先找 root cause」「UART/PCAP 跟預期不同」「pytest 為什麼失敗」時載入。
license: MIT
---

# Test Failure Triage for OpenCode

## 目的

測試失敗不等於產品失敗。先判定問題位於需求、測試程式、通訊、封包擷取、DUT、環境或 Timing，再決定是否修改程式碼。

預設先調查與報告，不直接修改檔案。

## 1. 建立失敗基準

1. 讀取專案 `AGENTS.md`、`handoff.md` 與相關測試規格。
2. 說明預期結果、實際結果，以及兩者第一個可觀察差異。
3. 記錄可重現的完整命令、exit code、失敗 Test Case 與完整 Error／Traceback。
4. 確認失敗是固定發生、間歇發生，或尚未能重現。
5. 若證據不足，先說明缺少什麼，不猜測 Root Cause。

## 2. 記錄環境

依適用情況取得：

- Git branch、commit 與未提交變更；
- OS、Python 版本、virtual environment、主要套件版本；
- Test target、DUT 型號、firmware／build version；
- UART／TTY port、baud rate、data bits、parity、stop bits、flow control、encoding、line ending 與 timeout；
- Capture interface、PCAP／PCAPNG 路徑、capture 時段、Wireshark／tshark version 與使用的 filter；
- Python、UART、PCAP 與 DUT 的時間來源、時區或已知 clock offset。

不要把未確認的環境值當成事實。

## 3. 保存並對齊原始證據

優先保存：

- Python runner output、Traceback、JUnit 或其他 Test Report；
- UART TX／RX 原始 Log，以及 Parser 產生的事件；
- 原始 PCAP／PCAPNG、相關 Frame Number、Protocol Field 與 Display Filter；
- DUT／system log 與版本資訊。

建立最小時間線，例如：

```text
T0 Python 發出操作
T1 UART 出現對應事件
T2 PCAP 出現預期封包
T3 Python 產生 Verdict
```

若各來源時鐘不同，使用同一個可辨識事件作為 Anchor，並標示已知偏差。文字摘要不能取代原始 Log、PCAP 或 Report。

## 4. 失敗分層

依序判斷：

1. `requirement-oracle`：Acceptance Criteria 或預期結果是否矛盾、過時或無法觀察？
2. `python-harness`：測試流程、例外處理、資源清理或命令執行是否錯誤？
3. `fixture-data`：Test Data、前置狀態或環境準備是否不正確？
4. `uart-transport`：Port 設定、TX／RX、斷線、buffer、encoding、line ending 或 timeout 是否有問題？
5. `parser-verdict`：原始資料存在，但 Parser、Pattern、狀態機或 Verdict 判定錯誤？
6. `packet-capture`：抓錯 Interface、Capture 起停時間不對、封包遺失或 Filter 隱藏了證據？
7. `network-protocol`：封包存在，但 Address、Port、Sequence、Transaction、Call-ID 或 Protocol Field 不符？
8. `dut-firmware`：在 Test Harness、通訊與環境證據合理後，DUT 行為仍不符合規格？
9. `environment-dependency`：Driver、Permission、Tool Version、PATH、Firewall、Network Route 或第三方服務差異？
10. `timing-race`：非固定等待、非同步事件、Clock Offset、Race Condition 或資源競爭？

不要在尚未排除前述層級時直接宣稱是 DUT Bug。

## 5. 單一假設與最小驗證

1. 寫出一個明確假設：`我認為 X 是原因，因為證據 Y。`
2. 設計只改變一個變數的最小實驗。
3. 預先寫出若假設成立與不成立，各會觀察到什麼。
4. 執行安全且已獲授權的驗證，保留命令、exit code 與 Artifact。
5. 假設不成立時，回到證據與分層，不在原假設上繼續堆疊修正。

## 6. 修正門檻

只有在 Root Cause 有足夠證據後才建議修正：

- 修正來源，不只遮蔽單一症狀；
- 一次只做與假設直接相關的最小修改；
- 建立或更新可重現原始失敗的 Regression Test；
- 使用最新證據重新驗證原始失敗與相關測試；
- 若需要修改 Acceptance Criteria 或 Test Expectation，明確說明理由並取得使用者確認。

## 輸出格式

```text
Observed failure:
Expected result:
Reproduction:
Environment:
Evidence timeline:
Most likely layer:
Confirmed facts:
Open questions:
Current hypothesis:
Minimal verification:
Result:
Recommended next action:
Artifact paths:
```

若尚未找到 Root Cause，直接說明目前只能定位到哪一層，以及下一項最有資訊價值的檢查。

## 邊界

- 不以增加 Retry、延長 Timeout、Skip／XFail、刪除 Assertion 或修改預期值作為預設修正。
- 不刪除、覆蓋或只保留壓縮後的原始 UART Log、PCAP、JUnit 或正式測試報告。
- 硬體控制、破壞性、壓力或長時間測試必須先取得確認。
- 不順便重構無關程式碼，不把失敗分析擴大成整個 Repository 的全面改寫。
- 修正完成後仍須遵守 Essential Core 的 fresh validation evidence 與 `session-close` 流程。
