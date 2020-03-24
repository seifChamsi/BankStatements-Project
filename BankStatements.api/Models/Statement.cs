using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BankStatements.api.Models
{
    public class Statement
    {
        public string nom { get; set; }
        public string RIB { get; set; }
        public string mois { get; set; }
        
        public string annee { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
