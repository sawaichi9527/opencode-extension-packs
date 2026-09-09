using System;
using System.Diagnostics;
using System.Text;
class Program {
    static int Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        // chcp 65001
        try {
            var chcp = Process.Start(new ProcessStartInfo("chcp", "65001") { UseShellExecute = false, CreateNoWindow = true });
            chcp.WaitForExit();
        } catch {}
        // 彈性路徑：支援任意 Windows 使用者（非僅 Sawaichi），優先 USERPROFILE，再退回相對路徑
        string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
        string exe = null;
        if (!string.IsNullOrEmpty(userProfile)) {
            exe = System.IO.Path.Combine(userProfile, @".config\opencode\pwsh7\pwsh.exe");
            if (!System.IO.File.Exists(exe)) exe = null;
        }
        if (exe == null) {
            // 退回：wrapper 同目錄下的 pwsh7\pwsh.exe（便於可攜式部署）
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            exe = System.IO.Path.Combine(baseDir, @"pwsh7\pwsh.exe");
        }
        if (!System.IO.File.Exists(exe)) {
            // 最後退回：系統 PATH 上的 pwsh
            exe = "pwsh";
        }
        string arguments = string.Join(" ", args);
        ProcessStartInfo psi = new ProcessStartInfo(exe, arguments);
        psi.UseShellExecute = false;
        var p = Process.Start(psi);
        p.WaitForExit();
        return p.ExitCode;
    }
}