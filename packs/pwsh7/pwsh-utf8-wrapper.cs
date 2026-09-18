using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
class Program {
    // Windows 命令列引號規則（CommandLineToArgvW 反向）：將單一 argv 元素重新 quote，
    // 讓子程序再解析後能取得完全相同的 argv。雙引號、空白、反斜線全保真。
    static string QuoteForCommandLine(string arg) {
        if (arg.Length == 0) return "\"\"";
        bool needQuote = false;
        foreach (char c in arg) {
            if (c == ' ' || c == '\t' || c == '"') { needQuote = true; break; }
        }
        if (!needQuote) return arg;
        var sb = new StringBuilder();
        sb.Append('"');
        int backslashes = 0;
        foreach (char c in arg) {
            if (c == '\\') { backslashes++; continue; }
            if (c == '"') {
                sb.Append('\\', backslashes * 2 + 1);
                sb.Append('"');
                backslashes = 0;
                continue;
            }
            sb.Append('\\', backslashes);
            backslashes = 0;
            sb.Append(c);
        }
        if (backslashes > 0) sb.Append('\\', backslashes * 2);
        sb.Append('"');
        return sb.ToString();
    }
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
        var parts = new List<string>();
        foreach (string a in args) parts.Add(QuoteForCommandLine(a));
        string arguments = string.Join(" ", parts);
        ProcessStartInfo psi = new ProcessStartInfo(exe, arguments);
        psi.UseShellExecute = false;
        var p = Process.Start(psi);
        p.WaitForExit();
        return p.ExitCode;
    }
}