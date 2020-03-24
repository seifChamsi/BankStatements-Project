using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankStatements
{
    public class Bank
    {
        public string Date_Operation { get; set; }
        public string Date_Valeur { get; set; }

        public string Operation { get; set; }

        public string Reference { get; set; }

        public string Débit { get; set; }
        public string Crédit { get; set; }
    }
}
