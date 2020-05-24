using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderRepository
    {
          
        private string _path = "C:/Users/Jeremy/source/repos/FlooringMastery.Data/Orders_06012013.txt";
        public string Path
        {
            get { return _path; }
            set { _path = Path; }
        }


        private List<Order> _orderList = new List<Order>();

        public List<Order> OrderList
        { 
            get { return _orderList; } 
            set { _orderList = OrderList; } 
        }
        public OrderRepository()
        {

            ReadOrderFile();
        }

        public OrderRepository(string path)
        {
            _path = path;
            ReadOrderFile();
        }

        public void ReadOrderFile()
        {


            try
            {
                string[] rows = File.ReadAllLines(_path);
                for (int i = 1; i < rows.Length; i++) //each row of file
                {
                    Order o = new Order();
                    string[] columns = rows[i].Split(',');//12 fields 0-11
                    o.OrderNumber = Int32.Parse(columns[0]);
                    o.CustomerName = columns[1];
                    o.State = columns[2];
                    o.TaxRate = Decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = Decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = Decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = Decimal.Parse(columns[7]);
                    o.MaterialCost = Decimal.Parse(columns[8]);
                    o.LaborCost = Decimal.Parse(columns[9]);
                    o.Tax = Decimal.Parse(columns[10]);
                    o.Total= Decimal.Parse(columns[11]);
                    
                    _orderList.Add(o);

                }

            }
            catch (Exception e)
            {
                Console.Write("There was a error the File System (ReadOrderFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);

            }
        }






    }
}
