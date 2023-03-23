
namespace Utakantot {
    internal static class KwadernoNgBano {

        /// <summary>
        /// Gawin ang <see cref="Exception"/> sa mas makabuluhang saysay.
        /// <br />
        /// (EN: Make <see cref="Exception"/> in a meaningful sense.)
        /// </summary>
        /// <param name="kaso">
        /// Ang kasalukuyang <see cref="Exception"/>
        /// <br />
        /// (EN: A current <see cref="Exception"/> object.)
        /// </param>
        /// <returns>
        /// Ibabalik ang string na naglalaman ng makabuluhang mensahe.
        /// <br />
        /// (EN: Returns a string containing meaningful message.)
        /// </returns>
        internal static string Ibunyag(Exception kaso) {
            if (kaso is IOException) {
                return "Hala, g*go! Di ko mabuksan. :(\n(IO Error: " + kaso.Message + ")";
            } else if (kaso is ArithmeticException) {
                return "Hala, g*go! Amb*bo mo sa math! HAHAHHAHAHAHHHA\n(ArithmeticException: " + kaso.Message + ")";
            }
            return "Pre, di kita maintindihan! :(\n(General Error: [" + kaso.GetType().Name + "] " + kaso.Message + ")";
        }
    }
}
