using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    
    class TaxRateRepository
    {
        private string _path = "C:/Users/Jeremy/source/repos/FlooringMastery.Data/Taxes.txt";

        public string Path {
            get { return _path; } 
            set { _path = Path; }
        }


        private List<TaxRate> _taxRateList = new List<TaxRate>();

        public List<TaxRate> TaxRateList 
        { 
            get {return _taxRateList; } 
        }


        public TaxRateRepository()
        { 
           
            ReadTaxRateFile();
        }

        //use for testing
        public TaxRateRepository(string path)
        {
            _path = path;
            ReadTaxRateFile();
        }


        public void ReadTaxRateFile()
        {
            

            try
            {
                string[] rows = File.ReadAllLines(_path);
                for (int i = 1; i < rows.Length; i++)
                {
                    TaxRate t = new TaxRate();
                    string[] columns = rows[i].Split(',');
                    t.StateAbbreviation = columns[0];
                    t.StateName = columns[1];
                    t.Rate = Decimal.Parse(columns[2]);
                    _taxRateList.Add(t);

                }
            
            }
            catch (Exception e)
            {
                Console.Write("There was a error the File System (ReadTaxRateFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);
               
            }
        }

        //returns the tax rate for the given state
        public decimal GetTaxRateByState(string stateabbreviation)
        {
            TaxRate result = TaxRateList.Find(x => x.StateAbbreviation.Contains(stateabbreviation));

            return result.Rate;
        }
    }

}
