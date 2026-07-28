---
name: forgejo-integration
description: 說明本地 Git、Forgejo Remote 與可選 Forgejo MCP 的分工，協助小型團隊安全 clone、fetch、commit、push 與建立協作流程。
---

# Forgejo Integration

## 關係

- 本地 Git：實際保存工作目錄、branch、commit 與 diff。
- Forgejo：區網內共用 Repository、Issue、PR 與 Release。
- Forgejo MCP：讓 Agent 透過受控工具讀寫 Forgejo；不能取代本地 Git。

## 安全流程

1. 先執行本地 `git status` 與 `git diff`。
2. commit 前顯示納入檔案。
3. push 前顯示 remote、branch 與 commit。
4. 不把 Forgejo Token 寫入 Repository。
5. MCP 不可預設取得所有 Repository 寫入權限。

## 初始設定

詢問使用者提供 Forgejo Repository URL；不要猜測內部網域。
