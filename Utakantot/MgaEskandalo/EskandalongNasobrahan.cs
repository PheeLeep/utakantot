namespace Utakantot.MgaEskandalo {
    public class EskandalongNasobrahan : Eskandalo {
        public EskandalongNasobrahan() : this("Tama na, pre. Nasobrahan na po kayo. :(") { }
        public EskandalongNasobrahan(string boolbol) : this(boolbol, "Data Out of Bound Error occurred.") { }
        public EskandalongNasobrahan(string boolbol, string rasongIngles) : base(boolbol, rasongIngles) { }
    }
}
