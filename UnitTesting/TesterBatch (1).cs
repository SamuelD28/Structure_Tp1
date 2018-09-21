//#define BONUS

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TesterBatch
{
    [TestClass]
    public class TesterBatch
    {
        const string Calc = "calc";
        
        public static bool Exec(string commandLine, out string stdout, out string stderr, out int exitCode)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C " + commandLine,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                    // WorkingDirectory = @"C:\MyAndroidApp\"
                }
            };

            proc.Start();
            if(proc.WaitForExit(200)) 
            {
                stdout = proc.StandardOutput.ReadToEnd();
                stderr = proc.StandardError.ReadToEnd();
                exitCode = proc.ExitCode;
                return true;
            }
            else 
            {
                // proc.Kill();
                stdout = stderr = "";
                exitCode = 0;
                return false;
            }
        }

        public static void Valider(string commande, string expStdout, string expStderr = "", int expExitCode = 0)
        {
            IsTrue(Exec(commande, out string stdout, out string stderr, out int exitCode), $"La commande '{commande}' ne se termine pas.");
            if (expStderr != "") 
                IsTrue(stderr.Trim().ToLowerInvariant().IndexOf(expStderr) >= 0, $"La chaine '{expStderr}' était attendue dans l'erreur '{stderr.Trim()}'");
            else 
                AreEqual("", stderr.Trim());
            AreEqual(expStdout, stdout.Trim());
            AreEqual(expExitCode, exitCode);
        }

        [TestMethod]
        public void _61_CalcArgOk()
        {
            Valider($"{Calc} 10 10 +", "20");
        }

        [TestMethod]
        public void _62_CalcArgÉtat()
        {
            Valider($"{Calc} 10 20 30", "10  20  30?");
        }

        [TestMethod]
        public void _63_CalcArgErreur()
        {
            Valider($"{Calc} 10 0 \\", "", "divi", 1);
        }

        [TestMethod]
        public void _81_CalcArgFichierCommande()
        {
            var file = Path.GetTempFileName();
            File.AppendAllText(file, "=3d*4d*+");
            Valider($"{Calc} {file}", "25");
            File.Delete(file);
        }

        [TestMethod]
        public void _82_CalcArgFichierErreur()
        {
            Valider($"{Calc} not.found", "", "not.found", 1);
            Valider($"{Calc} not.found 10 20", "", "commande invalide", 1);
        }

#if BONUS
        [TestMethod]
        public void _71_CalcPipe()
        {
            Valider($"echo 1234? | {Calc} bb", "12?");
            Valider($"echo 10 20 30 40 | {Calc} ++", "10  90");
            Valider($"echo 10 20 30 40 | {Calc} + | {Calc} + | {Calc} +", "100");
            Valider($"echo 10 20 30 | {Calc} + | {Calc} | {Calc} +", "60");
        }

        [TestMethod]
        public void _72_CalcPipeÉtatInvalide()
        {
            Valider($"echo -1234? | {Calc}", "", "état invalide", 1);
            Valider($"echo 10 + | {Calc}", "", "état invalide", 1);
            Valider($"echo 99999999999999999999999999999999999999999 | {Calc}", "", "état invalide", 1);
        }

        [TestMethod]
        public void _73_CalcPipeCommandes()
        {
            Valider($"echo = 10 20 + | {Calc}", "30");
            Valider($"echo =3d*4d*+ | {Calc}", "25");
        }

        [TestMethod]
        public void _83_CalcArgFichierÉtat()
        {
            var file = Path.GetTempFileName();
            File.AppendAllText(file, "10 20 30 40?");
            Valider($"echo =+++ | {Calc} {file}", "100");
            File.Delete(file);
        }
#endif // BONUS
    }
}
