using System.Text;
using Utakantot.MgaEskandalo;

namespace Utakantot {

    /// <summary>
    /// Ang puso ng kabalastugan, na naglalaman ng mga gawain para umandar ang kodigo.
    /// (EN: The heart of 'kabalastugan', containing methods in order to run the code.)
    /// </summary>
    internal static class Utakantot {

        /// <summary>
        /// Ang bersyon ng programa.
        /// <br />
        /// (EN: The version of a program.)
        /// </summary>
        internal static readonly string Henerasyon = "0.0.1.0";

        /// <summary>
        /// Tinutukoy ang laki ng array ng mga byte (Inayos sa 64kb).
        /// <br />
        /// (EN: Determines the size of an array of bytes (Fixed in 64kb)).
        /// </summary>
        internal static readonly int Dami = 1024 * 64; // 64kb

        /// <summary>
        /// Tinutukoy kung ang kodigo ay tapos na.
        /// <br />
        /// (EN: Determines whether a code was finished running.)
        /// </summary>
        internal static bool TaposNaAngTipan { get; private set; } = false;

        /// <summary>
        /// Tinutukoy kung ang programa ay gagamitan ng format na ASCII.
        /// <br />
        /// (EN: Determines whether a program will use an ASCII Format.)
        /// </summary>
        internal static bool FallOfBataan { get; set; } = false;

        /// <summary>
        /// Tinutukoy kung payag ilabas ng programa, ang karakter pag may nilagay
        /// ang user na halaga at ipinasok sa kasalukuyang byte.
        /// <br />
        /// (EN: Determines whether a program was allowed to output a character,
        /// if a user enters a value to the current byte.)
        /// </summary>
        internal static bool PwedeMambozo { get; set; } = true;

        /// <summary>
        /// Ang lalagyanan ng mga byte, kung saan nakalagay ang mga halaga habang
        /// umaandar pa ang kodigo.
        /// <br />
        /// (EN: An array of bytes that stores values during running a code.)
        /// </summary>
        private static readonly int[] ekup = new int[Dami];

        /// <summary>
        /// Tinutukoy kung gaano kalaki ang pwede ilagay sa array ng byte.
        /// <br />
        /// (EN: Determines the maximum value to be allowed to store in byte array.)
        /// </summary>
        private static int luwag = int.MaxValue;

        /// <summary>
        /// Ang pointer na pantukoy sa kasalukuyang byte sa array kung saan ka gumagawa.
        /// <br />
        /// (EN: A pointer that points a current byte in an array where you're work at.)
        /// </summary>
        private static int hinlalato = 0;

        /// <summary>
        /// Paandarin ang mga kodigo mula sa file.
        /// <br />
        /// (EN: Run codes from a file.)
        /// </summary>
        /// <param name="f">
        /// Ang kasalukuyang <see cref="FileInfo"/>.
        /// <br />
        /// (EN: A current <see cref="FileInfo"/> object.)
        /// </param>
        internal static void Ibuka(FileInfo f) {
            MagingBirhen(); // Linisin bago dumeretso sa susunod. (Clean first before moving next.)

            // Ibuksan ang file. (Open a file.)
            using FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader sr = new(fs);
            List<string> mga_DOs = new();

            // Umpisahang basahin sa unang linya. (Start reading from the first line.)
            string? boolbol = sr.ReadLine();

            // Hihinto ang pagbabasa pag nakita ang 'blessing'. (Stops reading when it detects 'blessing' keyword.)
            bool nagblessing = false;

            while (!string.IsNullOrEmpty(boolbol)) {
                string[] mga_boorneek = boolbol.Split(' ');
                for (int i = 0; i < mga_boorneek.Length; i++) {
                    // Ikunsidera bilang komento at umalis sa for loop. (Consider it as comment, and break the for loop.)
                    if (mga_boorneek[i].Equals("maglaplap")) break;
                    mga_DOs.Add(mga_boorneek[i]);
                    if (mga_boorneek[i].Equals("blessing")) {
                        nagblessing = true;
                        break;
                    }
                }
                if (nagblessing) break;
                boolbol = sr.ReadLine(); // Basahin sa susunod na linya. (Read the next line.)
            }

            // Paandarin ang kodigo.
            MagAlamMoNaHehe(mga_DOs.ToArray());
        }

        /// <summary>
        /// Lilinisin ang array at aayusin ang option base sa <see cref="FallOfBataan"/>.
        /// <br />
        /// (EN: Cleans the array and fixes the option based on <see cref="FallOfBataan"/>.)
        /// </summary>
        private static void MagingBirhen() {
            luwag = FallOfBataan ? 255 : int.MaxValue;
            Array.Clear(ekup, 0, ekup.Length);
            hinlalato = 0;
        }

        /// <summary>
        /// Paandarin ang kodigo.
        /// <br />
        /// (EN: Run a code.)
        /// </summary>
        /// <param name="mga_boorneek">
        /// Ang array ng string na nilalaman ng mga utos ng programa.
        /// <br />
        /// (EN: An array of string containing program commands.)
        /// </param>
        /// <exception cref="EskandalongNasobrahan"></exception>
        /// <exception cref="EskandalongNabubulol"></exception>
        internal static void MagAlamMoNaHehe(string[] mga_boorneek) {
            for (int i = 0; i < mga_boorneek.Length; i++) {
                switch (mga_boorneek[i]) {
                    case "eut": // equivalent to Brainfuck's increment sign (+).
                        TignanAngDami(ekup[hinlalato], true);
                        ekup[hinlalato]++;
                        break;
                    case "bayo": // equivalent to Brainfuck's decrement sign (+).
                        TignanAngDami(ekup[hinlalato], false);
                        ekup[hinlalato]--;
                        break;
                    case "isirit": // shows a raw, integer type value, instead of a char.
                        Console.Write(ekup[hinlalato]);
                        break;
                    case "ilabas": // equivalent to Brainfuck's output value (.).
                        Console.Write(FallOfBataan ? Encoding.ASCII.GetString(new byte[] { (byte)ekup[hinlalato] }) : Convert.ToChar(ekup[hinlalato]));
                        break;
                    case "isubo": // equivalent to Brainfuck's require user input value (,).
                        int hims = Console.ReadKey(PwedeMambozo).KeyChar;
                        if (hims < 0 || hims > luwag) throw new EskandalongNasobrahan("Tama na, pre. nabo-b*b* ka na kakatype.", "Invalid data input.");
                        ekup[hinlalato] = hims;
                        break;
                    case "himas": // equivalent to Brainfuck's decrement pointer (<).
                        hinlalato--;
                        if (hinlalato < 0) hinlalato = ekup.Length - 1;
                        break;
                    case "lamas": // equivalent to Brainfuck's increment pointer (>).
                        hinlalato++;
                        if (hinlalato > ekup.Length - 1) hinlalato = 0;
                        break;
                    case "sigepa": // equivalent to Brainfuck's while loop ([).
                        if (ekup[hinlalato] == 0) {
                            int ulit = 0;
                            int isa_pang_ay = i + 1;
                            if (isa_pang_ay >= mga_boorneek.Length)
                                throw new EskandalongNasobrahan("Pre, sobra-sobra na yang kal*b*gan mo.", "Keywords after 'sigepa' does not exists.");
                            while (!mga_boorneek[isa_pang_ay].Equals("tamana") || ulit > 0) {
                                switch (mga_boorneek[isa_pang_ay]) {
                                    case "sigepa": ulit++; break;
                                    case "tamana": ulit--; break;
                                }
                                isa_pang_ay++;
                                i = isa_pang_ay;
                            }
                        }
                        break;
                    case "tamana": // equivalent to Brainfuck's loop terminator when the value is zero. (]).
                        if (ekup[hinlalato] != 0) {
                            int ulit = 0;
                            int isa_pang_ay = i - 1;
                            if (isa_pang_ay < 0)
                                throw new EskandalongNasobrahan("Duwag ka pala e. HAHAHAHHAHHAHHA", "Keywords after 'tamana' does not exists.");
                            while (mga_boorneek[isa_pang_ay] != "sigepa" || ulit > 0) {
                                switch (mga_boorneek[isa_pang_ay]) {
                                    case "sigepa": ulit--; break;
                                    case "tamana": ulit++; break;
                                }
                                isa_pang_ay--;
                                i = isa_pang_ay;
                            }
                        }
                        break;
                    case "blessing": // terminates the program.
                        TaposNaAngTipan = true;
                        return;
                    default:
                        throw new EskandalongNabubulol("Naiintindihan ko ba mga sinasabi mo? Ul*l ka ba? HAHAHAHAHHAHHHA\n\"" + mga_boorneek[i] + "\" daw amp.",
                                                       "Command Error: \"" + mga_boorneek[i] + "\" is invalid command.");
                }
            }
        }

        /// <summary>
        /// Paandarin ang kodigo sa isang string.
        /// <br />
        /// (EN: Run a code in a string)
        /// </summary>
        /// <param name="boolbol">
        /// Ang isang string na nilalaman ng mga utos ng programa.
        /// <br />
        /// (A string containing program commands.)
        /// </param>
        internal static void MagAlamMoNaHehe(string? boolbol) {
            if (string.IsNullOrEmpty(boolbol)) return;
            string[] mga_boorneek = boolbol.Split(" ");
            MagAlamMoNaHehe(mga_boorneek);
        }

        /// <summary>
        /// Tukuyin kung sakto sa itinakdang dami ng halaga ng byte. 
        /// At itataas ang <see cref="EskandalongNasobrahan"/> pag hindi natupad.
        /// <br />
        /// (EN: Determines if a current byte value is satisfied. It will raise an
        /// <see cref="EskandalongNasobrahan"/> if not to be followed.)
        /// </summary>
        /// <param name="hims">
        /// Ang kasalukuyang dami ng halaga ng byte.
        /// <br />
        /// (EN: A current amount of a byte value.)
        /// </param>
        /// <param name="pwedeEut">
        /// Tinutukoy kung isusukat kung gaano kadami o hindi.
        /// <br />
        /// (EN: Determines whether it will measures on how much or not.)
        /// </param>
        /// <exception cref="EskandalongNasobrahan"></exception>
        private static void TignanAngDami(int hims, bool pwedeEut) {
            if (!pwedeEut && hims < 0) throw new EskandalongNasobrahan("Nasobrahan na po kayo sa pagiging banal. :(");
            if (pwedeEut && hims == luwag) throw new EskandalongNasobrahan("Nasobrahan na po kayo sa pagiging fuccboi/fuccgirl, g*go. :(");
        }
    }
}
