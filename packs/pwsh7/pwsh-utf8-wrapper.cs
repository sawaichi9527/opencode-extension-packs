using System;
using System.Diagnostics;
using System.Text;
using System.IO;

// pwsh-utf8-wrapper: opencode shell 前置包裝
// 作用: chcp 65001 + Console UTF-8，再轉發給 pwsh7 (PowerShell 7.x)
// 彈性路徑解析順序 (任意 Windows 使用者可用，無硬編碼):
//   1. %USERPROFILE%\.config\opencode\pwsh7\pwsh.exe
//   2. <wrapper 同目錄>\pwsh7\pwsh.exe (可攜式部署)
//   3. PATH 上的 pwsh
// 編譯 (任意 Windows 10/11 內建 .NET Framework csc):
//   %WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe /nologo /out:"%USERPROFILE%\.config\opencode\pwsh-utf8-wrapper.exe" pwsh-utf8-wrapper.cs
class Program {
    static int Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        try {
            var chcp = Process.Start(new ProcessStartInfo("chcp", "65001") { UseShellExecute = false, CreateNoWindow = true });
            chcp.WaitForExit();
        } catch {}
        string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
        string exe = null;
        if (!string.IsNullOrEmpty(userProfile)) {
            string candidate = Path.Combine(userProfile, @".config\opencode\pwsh7\pwsh.exe");
            if (File.Exists(candidate)) exe = candidate;
        }
        if (exe == null) {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string candidate = Path.Combine(baseDir, @"pwsh7\pwsh.exe");
            if (File.Exists(candidate)) exe = candidate;
        }
        if (exe == null) {
            exe = "pwsh"; // fallback: PATH
        }
        string arguments = string.Join(" ", args);
        var psi = new ProcessStartInfo(exe, arguments) { UseShellExecute = false };
        var p = Process.Start(psi);
        p.WaitForExit();
        return p.ExitCode;
    }
}
