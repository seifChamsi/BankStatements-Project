using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using GrapeCity.Documents.Pdf;

namespace BankStatements
{
    public class DataService
    {
        public static List<string> GetDataList(string path)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<string> data = new List<string>();

            using (var stream = File.Open(Constants.pdfFilePath, FileMode.Open, FileAccess.Read))
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
        public static List<Bank> populateTransactionsDetails(List<string> transactionsList)
        {
            List<Bank> Transactions = new List<Bank>();

            for (int i = 0; i < transactionsList.Count; i += 6)
            {
                Transactions.Add(new Bank()
                {
                    Date_Operation = transactionsList[i],
                    Date_Valeur = transactionsList[i + 1],
                    Operation = transactionsList[i + 2],
                    Reference = transactionsList[i + 3],
                    Débit = transactionsList[i + 4],
                    Crédit = transactionsList[i + 5]
                });
            }

            return Transactions;
        }
    }
}