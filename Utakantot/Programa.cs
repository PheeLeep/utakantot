using Utakantot.MgaEskandalo;

namespace Utakantot {
    internal static class Programa {
        static void Main(string[] args) {
            ShowTitle();
            if (args.Length == 0) {
                Console.WriteLine("Wala po kayo nilalagay ng kahit ano. :(\n(Use -h to show help.)");
                return;
            }
            FileInfo? bold = null;
            bool pwedeGumawaScript = false;
            
            for (int i = 0; i < args.Length; i++) {
                switch (args[i]) {
                    case "--mga-utos":
                        Console.WriteLine(Properties.Resources.Sintaks);
                        return;
                    case "-bataan":
                        if (Utakantot.FallOfBataan) {
                            Console.WriteLine("Kanina mo na isinuko ang Bataan. (Parameter already specified)");
                            break;
                        }
                        Utakantot.FallOfBataan = true;
                        break;
                    case "--gawa-script":
                        if (TignanKungOksNa(bold, pwedeGumawaScript)) return;
                        pwedeGumawaScript = true;
                        break;
                    case "--wag-boso":
                        Utakantot.PwedeMambozo = false;
                        break;
                    case "-h":
                        ShowHelp();
                        return;
                    case "-f":
                        if (TignanKungOksNa(bold, pwedeGumawaScript)) return;
                        if ((i + 1) >= args.Length) {
                            Console.WriteLine("Ba't ka nag-run kung wala ka maibigay? :(\n(No File Specified. Use -h to show help.)");
                            return;
                        }
                        bold = new(args[i + 1]);
                        i++;
                        break;
                    default:
                        Console.WriteLine("Kung ano-ano pinaglalagay mo dito, pre. HAHAHHAHAHAHAHHA\n(Invalid parameter passed. Use -h to show help.)");
                        return;
                }
            }

            if (bold != null) {
                bold.Refresh();
                if (!bold.Exists) {
                    Console.WriteLine("Wala sya dito, pre. Dito ba talaga sya? :(\n(File not found.)");
                    return;
                }
                if (!bold.Extension.Equals(".utak")) {
                    Console.WriteLine("Iba ito eh, t*ngina mo ka. :(\n(Invalid file found.)");
                    return;
                }
                try {
                    Utakantot.Ibuka(bold);
                } catch (Eskandalo esk) {
                    Ipagkalat(esk.Message + "\n(English Context: " + esk.RasongIngles + ")");
                } catch (Exception ex) {
                    Ipagkalat(KwadernoNgBano.Ibunyag(ex));
                }
                return;
            }
            if (!pwedeGumawaScript) {
                Console.WriteLine("Bugok! Walang gamit itong programa na ito.\n(No specified input. Use -h to show help.)");
                return;
            }
            while (!Utakantot.TaposNaAngTipan) {
                try {
                    Console.Write("taena >>");
                    Utakantot.MagAlamMoNaHehe(Console.ReadLine());
                } catch (Eskandalo esk) {
                    Ipagkalat(esk.Message + " (English Context: " + esk.RasongIngles + ")");
                } catch (Exception ex) {
                    Ipagkalat(KwadernoNgBano.Ibunyag(ex));
                }
            }
        }

        private static bool TignanKungOksNa(FileInfo? bold, bool gawaScript) {
            if (bold != null || gawaScript) {
                Console.WriteLine("Teka. Okay na, di ba?\n(Parameter already specified, or -f was already set.)");
                return true;
            }
            return false;
        }

        private static void ShowTitle() {
            // Ang programang ito ay hatid sa inyo ng... (EN: This program was brought to you by...)
            Console.Title = "Utakantot Interpreter";
            Console.WriteLine("\n'Utakantot' Interpreter (Henerasyong: " + Utakantot.Henerasyon + ")");
            Console.WriteLine("May-akda (Author): PheeLeep\n" + new string('-', 50) + "\n");
        }

        private static void Ipagkalat(string isyu) {
            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("HOYYYY G*GO!!! NABABANO KA NA!!!\n\n" + isyu);
            Console.WriteLine(new string('-', 50));
        }

        private static void ShowHelp() {
            Console.WriteLine(Properties.Resources.TanongNaDapatAlamMoNaDatiPa);
        }
    }
}