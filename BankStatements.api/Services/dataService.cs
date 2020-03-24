using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using ExcelDataReader;

using System.Threading.Tasks;
using BankStatements.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankStatements.api.Services
{
    public class dataService
    {
        public static Statement TreatStatement(IFormFile file,string nom,string rib,string mois,string annee)
        {
            if (file == null || file.Length == 0)
                Console.WriteLine("file error");

            string fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                var rootFolder = Path.Combine("Resources", "ExcelFiles");

                var fileName = file.FileName;
                var filePath = Path.Combine(rootFolder, fileName);

                var fileLocation = new FileInfo(filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     file.CopyToAsync(fileStream);
                }

                if (file.Length <= 0)
                    Console.WriteLine("there is no file");

                var dataList = GetDataList(filePath);

                var transactinsList = GetTransactionsList(dataList);

                var TransactionDetails = populateTransactionsDetails(transactinsList,nom,rib,mois,annee);
                return TransactionDetails;

            }

            return null;
        }

        public static List<string> GetDataList(string path)
        {   
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<string> data = new List<string>();

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    do
                    {
                        while (reader.Read()) //Each ROW
                        {
                            for (int column = 0; column < reader.FieldCount; column++)
                            {
                                if (reader.GetValue(column) != null)
                                    //Console.WriteLine(reader.GetString(column));//Will blow up if the value is decimal etc. 
                                    data.Add(reader.GetValue(column).ToString());
                            }
                        }
                    } while (reader.NextResult()); //Move to NEXT SHEET
                }
            }

            return data;
        }


        public static List<string> GetTransactionsList(List<string> dataList)
        {
            List<string> opsList = new List<string>();
            for (int i = 6; i < dataList.Count; i++)
            {
                if (dataList[i] == "")
                {
                    dataList[i] = "0.00";
                }

                ;
                opsList.Add(dataList[i]);
            }

            return opsList;
        }
        public static Statement populateTransactionsDetails(List<string> transactionsList,string nom,string rib,string mois,string annee )
        {
            List<Operation> Transactions = new List<Operation>();


            for (int i = 0; i < transactionsList.Count; i += 6)
            {
                Transactions.Add(new Operation()
                {
                    Date_Operation = transactionsList[i],
                    Date_Valeur = transactionsList[i + 1],
                    Transaction = transactionsList[i + 2],
                    Reference = transactionsList[i + 3],
                    Débit = transactionsList[i + 4],
                    Crédit = transactionsList[i + 5]
                });
            }

            var StatementTable = new Statement()
            {
                nom = nom,
                RIB = rib,
                mois = mois,
                annee = annee,
                Operations = Transactions
            };

            return StatementTable;
        }
    }
}