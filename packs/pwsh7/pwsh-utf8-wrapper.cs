using System;
using System.Diagnostics;
using System.Text;
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
            exe = System.IO.Path.Combine(userProfile, @".config\opencode\pwsh7\pwsh.exe");
            if (!System.IO.File.Exists(exe)) exe = null;
        }
        if (exe == null) {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            exe = System.IO.Path.Combine(baseDir, @"pwsh7\pwsh.exe");
        }
        if (!System.IO.File.Exists(exe)) {
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
