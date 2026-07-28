---
name: github-integration
description: 提供小型團隊與個人專案的 GitHub 基本協作流程，區分本地 Git、GitHub CLI/API 與 Pull Request 操作。
---

# GitHub Integration

## 原則

- 本地 Git 處理檔案、branch、commit、diff。
- GitHub 處理遠端 Repository、Issue、Pull Request 與 Release。
- `gh` 或 GitHub API 只在需要遠端操作時使用。
- 建立 Repository、push、公開內容或開 PR 前先確認目標帳號與可見性。
- 不在命令列或文件中輸出 Token。
