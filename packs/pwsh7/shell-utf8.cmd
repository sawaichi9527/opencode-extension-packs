@echo off
rem shell-utf8.cmd — opencode 備用 shell 包裝（cmd 入口版）
rem 作用: chcp 65001 + 轉發 pwsh7 (彈性路徑，任意 Windows 使用者)
rem 注意: opencode.jsonc 的 "shell" 需指向 .exe；此 .cmd 供手動 cmd /c 呼叫或除錯用
chcp 65001 > nul 2>&1
set PYTHONIOENCODING=utf-8

rem 彈性解析 pwsh7: %USERPROFILE% -> 同目錄 -> PATH
if exist "%USERPROFILE%\.config\opencode\pwsh7\pwsh.exe" (
    "%USERPROFILE%\.config\opencode\pwsh7\pwsh.exe" -NoLogo %*
    goto :eof
)
if exist "%~dp0pwsh7\pwsh.exe" (
    "%~dp0pwsh7\pwsh.exe" -NoLogo %*
    goto :eof
)
pwsh.exe -NoLogo %*
