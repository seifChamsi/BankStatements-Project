using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using GrapeCity.Documents.Pdf;
using Microsoft.VisualBasic;
using OfficeOpenXml;

namespace BankStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataList = DataService.GetDataList(@"C:\Users\saifc\Pictures\ops.xls");

            var transactinsList = DataService.GetTransactionsList(dataList);

            var TransactionDetails = DataService.populateTransactionsDetails(transactinsList);
            

            Console.WriteLine("done");
        }
    }
}