using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utakantot.MgaEskandalo {
#pragma warning disable S3376 // Bakit?
#pragma warning disable S3925

    public abstract class Eskandalo : Exception  {
        public string RasongIngles { get; private set; }
        protected Eskandalo() : this("Pre, nagkaroon ito ng scandal.", "General Custom Error: An error occurred during the program execution."){ }

        protected Eskandalo(string boolbol, string rasongIngles) : base(boolbol) { 
            RasongIngles= rasongIngles;
        }
    }
}
