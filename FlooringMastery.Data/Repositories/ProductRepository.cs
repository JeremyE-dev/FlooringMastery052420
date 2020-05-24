using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    class ProductRepository
    {
        private string _path = "C:/Users/Jeremy/source/repos/FlooringMastery.Data/Products.txt";
        public string Path
        {
            get { return _path; }
            set { _path = Path; }
        }


        List<Product> _productList = new List<Product>();
        
        public List<Product> ProductList
        {
            get { return _productList; }
        }

        public ProductRepository()
        {

            ReadProductFile();
        }

        public ProductRepository(string path)
        {
            _path = path;
            ReadProductFile();
        }

        public void ReadProductFile()
        {


            try
            {
                string[] rows = File.ReadAllLines(_path);
                for (int i = 1; i < rows.Length; i++) //each row of file
                {
                    Product p = new Product();
                    string[] columns = rows[i].Split(',');//ProductType[0] , costsqrft[1], Labcostsqrfoot[2]
                    p.ProductType = columns[0];
                    p.CostPerSquareFoot = Decimal.Parse(columns[1]);
                    p.LaborCostPerSquareFoot = Decimal.Parse(columns[2]);
                    _productList.Add(p);

                }

            }
            catch (Exception e)
            {
                Console.Write("There was a error the File System (ReadProductFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);

            }
        }





    }


}
