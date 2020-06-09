using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq.Extensions;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    
    //This is a repo that contains all orders in One file Not all orders that exist in the Folder
    public class OrderRepository
    {
         //The FOLDER where all orders are located
         //The specific file is entered 
        private string _folderpath = "C:/Users/Jeremy/source/repos/FlooringMastery052420/FlooringMastery.Data/AllOrders/";
        public string FolderPath
        {
            get { return _folderpath; }
            set { _folderpath = value; }
        }

        //public List<Order> MainOrderLIst { get; set; } = new List<Order>();

        
        public List<Order> SalesDayOrderList { get; set; } = new List<Order>();

        //List that contains all orders in a given file

        // medthod that takes in a filename and writes all orders in that filename to a list
        
        //This is an order repository for a specific date
        //public OrderRepository(string fileName)
        //{

        //    ReadOrderByDate(fileName);
        //}

        //public OrderRepository(string path)
        //{
        //    _folderpath = path;
        //    ReadMainOrderFile();
        //}

        //Reads list of orders in a given file,
        //converts each order into an order object
        //places each order object in SalesDayOrderList
        public void ReadOrderByDate(string fileName)
        {
            //ex: Orders_06132020.txt



            // will need to extract date from filename
            string s = fileName.Remove(0,7);
            string[] stringArray = s.Split('.');
            string date = stringArray[0];
            string formattedDate = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4); 
           

            //this will read one file and add the order to the 
            //SalesDayOrderList
            try
            {
                string[] rows = File.ReadAllLines(_folderpath + fileName);
                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');
                    Order o = new Order();
                    o.OrderDate = DateTime.Parse(formattedDate);
                    o.OrderNumber = Int32.Parse(columns[0]);               
                    o.CustomerName = columns[1];
                    o.State = ConvertToStateEnum(columns[2]); //convert to enum
                    o.TaxRate = Decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = Decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = Decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = Decimal.Parse(columns[7]);
                    o.MaterialCost = Decimal.Parse(columns[8]);
                    o.LaborCost = Decimal.Parse(columns[9]);
                    o.Tax = Decimal.Parse(columns[10]);
                    o.Total = Decimal.Parse(columns[11]);

                    SalesDayOrderList.Add(o);

                }


            }

            catch (Exception e)
            {
                Console.WriteLine("There was a error the File System (ReadOrderFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);

            }

            
        }

        public void printOrders()
        {
            foreach (var item in SalesDayOrderList)
            {
                Console.WriteLine("**************************************************************");
                Console.WriteLine("[{0}] [{1}]", item.OrderNumber, item.OrderDate.ToString("MM/dd/yyyy"));
                Console.WriteLine("[{0}]", item.CustomerName);
                Console.WriteLine("[{0}]", item.State);
                Console.WriteLine("Product : [{0}]", item.ProductType);
                Console.WriteLine("Materials : [{0:c}]", item.MaterialCost);
                Console.WriteLine("Labor : [{0:c}]", item.LaborCost);
                Console.WriteLine("Tax : [{0:c}]", item.Tax);
                Console.WriteLine("Total : [{0:c}]", item.Total);
                Console.WriteLine("**************************************************************");
                Console.WriteLine();

            }
        }

        //
        public void CreateSalesDayOrderList()
        {
            //each time this application is run - get the date from the system

        }

        public States ConvertToStateEnum(string s)
        {
            States output;
            
            if(String.IsNullOrEmpty(s))
            {
                Console.WriteLine("Error: the state field was null or empty, please contact IT");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);

            }

            if(Enum.TryParse(s, out output))
            {
                return output;
            }

            else
            {
                Console.WriteLine("Error: could not parse state field or order, please contact IT");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return output;
            
        
        }



    }
}
