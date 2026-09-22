---
name: local-llm-dispatch-policy
description: 規範本地 LLM provider（prefill 慢、TTFT 慢、無併發）的調用與升級原則：一請求一決策、線性 dispatch、3-strike 退回、雲端接手。當使用者要在本地模型上執行任務、要用「Orchestrator（可為本地 light-weight 或雲端）＋ heavy-Builder（本地 heavy-duty、slow）」協作、或要在 heavy-Builder 連續失敗後決定改由雲端接手時載入。
---

# 本地 LLM 調用原則（Local LLM Dispatch Policy）

## 適用範圍

只處理本地算力的三種情境：

- **a**：本地單一模型、單一任務，不涉及多 agent／subagent。
- **b**：Orchestrator ＋ heavy-Builder，重型子任務派遣到另一台本地算力。
- **c1／c2**：b 連續失敗後的升級，依 **Orchestrator 的算力位置** 分支。

不適用一般雲端多 agent 規劃，也不重新定義 OpenCode v2 原生 subagent tool、`permissions` 或 command `subtask`；本 Skill 只在原生機制之上補「本地算力才需要的派遣與升級規則」。

角色（角色與算力位置無關；導入時把實際 provider/model 對應到角色）：

- **Orchestrator**：負責拆解、派遣與升級判斷的角色。算力位置可能是**本地**（＝ light-weight LLM provider（fast））或**雲端模型**；c1／c2 的分支依 Orchestrator 的算力位置區分。
- **heavy-Builder**：**本地** heavy-duty LLM provider（slow），重型執行者。

## 前提（本地模型特性）

1. 內容 prefill 慢 —— 送入大量脈絡代價高。
2. TTFT 慢 —— 來回次數是主要成本。
3. 不具同時併發能力 —— 同時多請求會排隊拖慢甚至超時。

因此每當流程涉及本地 provider（情境 a 的單一本地模型、情境 b 的 heavy-Builder），一律：**一請求一決策、線性 dispatch**。

## 核心原則

1. **一請求一決策**：每次請求只做一次派遣決策，不預先規劃多輪或多次派遣。
2. **線性 dispatch**：一次只派一個子任務；不得同時對本地 provider 發出多個任務。
3. **prefill 最小化**：派給 heavy-Builder 的簡報須自帶脈絡、可獨立執行；不重送整段對話或無關檔案。
4. **減少來回**：能一次講清楚的，不拆成多次短派遣。
5. **驗證證據**：heavy-Builder 每次嘗試都要重新產生驗證證據，不得沿用前次；Orchestrator 只在證據成立時接受成功。

## 情境 a：本地單一模型

- 不建立、不派遣 subagent。
- 線性直接完成；需要多步驟時在單次回應內完成，不為「分工」而開 subagent。

## 情境 b：Orchestrator ＋ heavy-Builder

- **b1 線性派遣**：Orchestrator 拆解後，一次只派一個子任務給 heavy-Builder。
- **b2 heavy-Builder 迴圈**：只做「思考理解需求 → 實做 → 驗證」；驗證成功才回傳《結論 + 驗證證據》供 Orchestrator 判斷。
- **b3 3-strike**：同一子任務的迴圈失敗 3 次即停止，回報失敗與已嘗試內容。
- **b4 重拆再派（最多一次）**：Orchestrator 二次檢視、重新拆解並提供新思路，再次線性派遣；成功同樣回傳《結論 + 驗證證據》。
- **再次 3-strike**：第二次 3-strike 後，停止再派遣該子任務給 heavy-Builder，進入升級判斷。

「失敗」以驗證為準：heavy-Builder 必須實際執行驗證（指令、輸出、exit code、測試結果），無證據不得宣稱成功；Orchestrator 也不得在無證據時接受結論。重拆以一次為限，不做無限重試。

## 升級判斷（c1／c2）

- **c2（Orchestrator 為雲端模型）**：由 Orchestrator 直接接手解決該子任務；解決後續拆解回到 b1–b4。

- **c1（Orchestrator 為本地模型）**：
  1. **首次 c1（本任務目標內）**：停在重新拆解步驟，暫停並建議使用者切換到特定雲端模型後再繼續。
     - 使用者不同意 → 停在該子任務，等待使用者指示，不自動接手。
     - 使用者同意並指定雲端模型 → 記錄該模型，作為**本次任務目標範圍內**的雲端接手（goal-scoped cloud fallback）；由它解決該子任務後，剩餘拆解回到 b1–b4，仍由本地 Orchestrator 繼續調度。
  2. **同任務目標內再次 c1**：不再停下來詢問，直接使用**先前已同意的同一個雲端模型**接手解決，解決後回到 b1–b4；如此直到本地 Orchestrator 完成本次任務目標。
  3. **下一輪任務目標**：同意重設。新任務目標拆解時第一次遇到 c1，重新暫停並再次徵求使用者同意後才繼續。
  4. **安全停損**：若已同意的雲端模型仍無法解決，停下來回報使用者，不重複自動接手。
  5. 為透明起見，每次自動接手時 Orchestrator 應標明「使用已同意的 <cloud-model> 處理本目標的 c1」。

  「本任務目標」＝使用者當次請求要達成的目標；其拆解出的所有子任務同屬一個任務目標。使用者提出新的請求即為下一輪任務目標。

## heavy-Builder 的角色契約

heavy-Builder 是 OpenCode v2 原生 subagent（`mode: subagent`），`model` 指向 heavy-duty provider：

- 只做「理解 → 實做 → 驗證」，不額外規劃、不再次派遣。
- 同一子任務最多嘗試 3 次；每次失敗記錄原因。
- 每次嘗試都重新產生驗證證據（指令、輸出、exit code），不得沿用前次。
- 回傳固定格式：結論、變更、驗證指令與原始輸出、殘留風險。
- 3-strike 後明確回報「失敗 + 已嘗試內容」，讓 Orchestrator 決定是否重拆。

本 Skill 不定義 agent 檔；heavy-Builder 由使用者依 OpenCode v2 原生方式建立（範例見 README）。

## 邊界

- 不取代、不重造 OpenCode v2 原生多 agent 框架。
- 不在本 Skill 寫入 provider 內部 URL、Token 或環境機密。
- 使用前需已設定好本地 provider；本 Skill 本身不需帳號、Token、MCP 或付費服務。
- 不宣稱能保證本地模型品質；3-strike 與升級僅是控制成本、避免卡死的流程規則。
