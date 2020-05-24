using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class DataHelpers
    {
        //ReadTaxRateFile

        public List<TaxRate> ReadTaxRateFile(string path)
        { 
            List<TaxRate> TaxRateList = new List<TaxRate>();

            try
            {
                string[] rows = File.ReadAllLines(path);
                for (int i = 1; i < rows.Length; i++)
                {
                    TaxRate t = new TaxRate();
                    string[] columns = rows[i].Split(',');
                    t.StateAbbreviation = columns[0];
                    t.StateName = columns[1];
                    t.Rate = Decimal.Parse(columns[2]);
                    TaxRateList.Add(t);

                }
                return TaxRateList;

                

            }
            catch(Exception e)
            {
                Console.Write("There was a error the File System (ReadTaxRateFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);
                return null;

            }
        }
        //ReadOrderFile
        //ReadProductFile


    }
}
