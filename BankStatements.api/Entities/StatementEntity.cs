using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BankStatements.api.Entities
{
    public class StatementEntity
    {
        //public IFormFile ExcelFile { get; set; }
        public string nom { get; set; }
        public string rib { get; set; }
        public string mois { get; set; }
        public string annee { get; set; }
    }
}
