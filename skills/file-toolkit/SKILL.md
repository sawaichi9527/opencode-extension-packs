---
name: file-toolkit
description: 檢查文件、圖片、影音與 Python 工具處理能力；依實際任務選擇安裝，不自動下載外部 Repository 或執行 curl pipe shell。
---

# File Toolkit

## 可檢查工具

- Python / uv
- FFmpeg
- yt-dlp
- office / PDF processing libraries
- image processing libraries

## 安裝原則

- 先列出任務真正需要的工具。
- 顯示來源、版本與安裝命令。
- 不預設全域安裝所有套件。
- 不執行未審查的 `curl | bash` 或遠端 PowerShell。
- 不從未知 Repository 執行安裝腳本。
