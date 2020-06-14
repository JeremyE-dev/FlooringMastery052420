using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq.Extensions;
using System.Threading.Tasks;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Data
{

    //This is a repo that contains all orders in One file Not all orders that exist in the Folder
    public class OrderRepository : IOrderRepository
    {
        //The FOLDER where all orders are located
        //The specific file is entered 
        private string _folderpath = "C:/Users/Jeremy/source/repos/FlooringMastery052420/FlooringMastery.Data/AllOrders/";
        public string FolderPath
        {
            get { return _folderpath; }
            set { _folderpath = value; }
        }

        string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public List<Order> SalesDayOrderList { get; set; } = new List<Order>();

        Order _orderToEdit;

        public Order OrderToEdit
        {
            get { return _orderToEdit; }
            set { _orderToEdit = value; }
        }


        DateTime _dateOfOrderToRemove;
        public DateTime DateOfOrderToRemove 
        {
            get { return _dateOfOrderToRemove; }
            set { _dateOfOrderToRemove = value; } 
        }
        public void ReadOrderByDate(string fileName)
        {
            //ex: Orders_06132020.txt

            ProductRepository p = new ProductRepository();

            // will need to extract date from filename
            string s = fileName.Remove(0, 7);
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
                    o.Product = p.ProductList.Where(x => x.ProductType == o.ProductType).First();

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

            if (String.IsNullOrEmpty(s))
            {
                Console.WriteLine("Error: the state field was null or empty, please contact IT");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);

            }

            if (Enum.TryParse(s, out output))
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


        //*****ADD METHODS *****


        public void SaveAddedOrder(Order o)
        {
            //takes in date of "newOrder" and converts it to a filename
            string filename = ConvertDateToFileName(o);
            //constructs the complete filepath with name of file
            string path = FolderPath + filename;
            // converts order information so it can be written as one line to the file
            string OrderAsString = o.OrderToLineInFile();

            // if there is a file with that name already
            // append the given line to that file
            if (File.Exists(path))
            {


                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(OrderAsString);
                }
            }

            //if there is not a file with that name
            //create a new one
            //add header line
            // write the order to the file
            else
            {

                var myFile = File.Create(path);
                myFile.Close();

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    writer.WriteLine(OrderAsString);
                }
            }

            //after order is written Read from the file to load it to the OrderRepository
            //places orderin OrderRepo - orderlist
            //this places the new order in the order list
            ReadOrderByDate(filename);




        }

        public string ConvertDateToFileName(Order o)
        {

            string result = "Orders_" + o.OrderDate.ToString("MMddyyyy") + ".txt";
            Console.WriteLine("result");
            return result;
        }

        public string ConvertDateToFileName(DateTime d)
        {

            string result = "Orders_" + d.ToString("MMddyyyy") + ".txt";
            Console.WriteLine("result");
            return result;
        }



        public int CalculateOrderNumber(Order o)
        {
            //does a file exist with the date entered
            string filename = ConvertDateToFileName(o); //returns a filename
            string path = FolderPath + filename;


            if (!File.Exists(path))
            {
                o.OrderNumber = 1;
            }

            else
            { //if the file does exists load the file to the order repository
                ReadOrderByDate(filename); //this should load everything in that file to this list, 
                                           //hopefully will resolve null issue 


                o.OrderNumber = SalesDayOrderList.MaxBy(x => x.OrderNumber).First().OrderNumber + 1;

            }

            return o.OrderNumber;


        }

        //*******DISPLAY METHODS*****

        //also used in EditOrderManager

        public Response CheckIfOrderGroupExists(DateTime d)
        {
            string fileName = ConvertDateToFileName(d);


            string path = FolderPath + fileName;
            Response response = new Response();

            //string OrderAsString = newOrder.OrderToLineInFile();

            if (!File.Exists(path))
            {
                response.Success = false;
                response.Message = String.Format("Error: There were no orders for the date given: {0}", d.ToString("MM/dd/yyyy"));

            }

            else
            {

                response.Success = true;
                FileName = fileName;
                response.Message = String.Format("An order file for {0} has been found", d.ToString("MM/dd/yyyy"));

            }

            return response;
        }

        public void DisplayExistingFile()
        {//Load the file
            ReadOrderByDate(FileName);
            //print all orders in the file
            printOrders();
            Console.ReadLine();
        }

        //*****EDIT Methods*****

      

        public bool DoesOrderExistInList(int number)
        {
            var orderToFind = SalesDayOrderList.Where(o => o.OrderNumber == number);
            if (!orderToFind.Any())
            {
                return false;
            }

            else
            {
                OrderToEdit = orderToFind.First();
                return true;

            }


        }

        public Order GetOrderFromList(int orderNumber)
        {
            Order result = new Order();
            result = SalesDayOrderList.Where(o => o.OrderNumber == orderNumber).First();
            
            return result;
        }

       

        public void RemoveOldOrderFromList()
        {
            //findit and remove
            var orderToFind = SalesDayOrderList.Where(o => o.OrderNumber == OrderToEdit.OrderNumber).First();
            DateOfOrderToRemove = orderToFind.OrderDate;
            SalesDayOrderList.Remove(orderToFind);
            WriteListToFile(DateOfOrderToRemove);
            Console.WriteLine("Old data has been removed from list");
            Console.ReadKey();

            return;
        }

        public void AddUpdatedOrderToList(Order updatedOrder)
        {
            SalesDayOrderList.Add(updatedOrder);
            WriteListToFile(updatedOrder.OrderDate);
            Console.WriteLine("New Data has been added to list");
            Console.ReadKey();

            return;
        }

        public void WriteListToFile(DateTime date)
        {
            string filename = ConvertDateToFileName(date); //returns a filename

            string path = FolderPath + filename;



            string OrderAsString;

            if (File.Exists(path))
            {



                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                    foreach (var order in SalesDayOrderList)
                    {
                        OrderAsString = order.OrderToLineInFile();
                        writer.WriteLine(OrderAsString);
                    }

                }
            }

            //places orderin OrderRepo - orderlist
            ReadOrderByDate(filename);


        }



    





    }
}
