namespace Utakantot.MgaEskandalo {
#pragma warning disable S3925 // Bakit?
    public class EskandalongNabubulol : Eskandalo {
        public EskandalongNabubulol() : this("Pre, nabubulol ka na. HAHAHAHHAHAHA") { }
        public EskandalongNabubulol(string boolbol) : this(boolbol, "Syntax Error occurred.") { }
        public EskandalongNabubulol(string boolbol, string rasongIngles) : base(boolbol, rasongIngles) { }
    }
}
