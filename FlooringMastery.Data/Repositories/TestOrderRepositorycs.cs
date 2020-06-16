using FlooringMastery.Models;

using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Data.Repositories
{


    public class TestOrderRepositorycs:IOrderRepository
    {


        //This is a repo that contains all orders in One file Not all orders that exist in the Folder

        ProductRepository ProductRepo;
        static List<Order>_salesDayOrderList;

        public List<Order> SalesDayOrderList
        { 
            get { return _salesDayOrderList; } 
            set { _salesDayOrderList = value; } }//} = new List<Order>();

        Order _orderToEdit;

        public Order OrderToEdit
        {
            get { return _orderToEdit; }
            set { _orderToEdit = value; }
        }

       

        public TestOrderRepositorycs()
        {
            _salesDayOrderList = new List<Order>();
            ProductRepo = new ProductRepository();

            Order order1 = new Order();
            order1.OrderNumber = 1;
            order1.CustomerName= "Bob";
            order1.State = States.OH;
            order1.TaxRate = 2;
            order1.Product = ProductRepo.ProductList.Where(p => p.ProductType == "Wood").First();
            order1.ProductType = "Wood";
            order1.Area = 200;
            order1.CostPerSquareFoot = 4;
            order1.LaborCostPerSquareFoot = 2;
            order1.MaterialCost = 100;
            order1.LaborCost = 200;
            order1.Tax = 61;
            order1.Total = 1051;
            order1.OrderDate = new DateTime(2020,7,1);

            _salesDayOrderList.Add(order1);

            //Menu.TestData.Add(order1);



            

        }

        public void printOrders()
            {
                foreach (var item in _salesDayOrderList)
                //foreach (var item in TestData.DataSource)
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


        public int CalculateOrderNumber(Order o)
        {

            if (!SalesDayOrderList.Any())
            {
                o.OrderNumber = 1;
            }

            else
            {
                o.OrderNumber = SalesDayOrderList.MaxBy(x => x.OrderNumber).First().OrderNumber + 1;

            }

            return o.OrderNumber;


        }


        public void SaveAddedOrder(Order o)
            {
            SalesDayOrderList.Add(o);
            //TestData.DataSource.Add(o);
            }


        //FROM DisplayOrderManager

        public Response CheckIfOrderGroupExists(DateTime d) //also used in edit functions
        {
            Response response = new Response();
            //var CheckOrderDates = TestData.DataSource.Where(x => x.OrderDate == d);
            var CheckOrderDates = SalesDayOrderList.Where(x => x.OrderDate == d);
            //string OrderAsString = newOrder.OrderToLineInFile();

            if (!CheckOrderDates.Any())
            {
                response.Success = false;
                response.Message = String.Format("Error: There were no orders for the date given: {0}", d.ToString("MM/dd/yyyy"));

            }

            else
            {

                response.Success = true;
                //FileName = fileName;
                response.Message = String.Format("An order file for {0} has been found", d.ToString("MM/dd/yyyy"));

            }

            return response;
        }

        public void DisplayExistingFile()
        {   
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

        public void ReadOrderByDate(DateTime orderDate)
        {
            

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
            SalesDayOrderList.Remove(orderToFind);
            Console.WriteLine("Old data has been removed from list");
            Console.ReadKey();

            return;
        }

        public void AddUpdatedOrderToList(Order updatedOrder)
        {
            SalesDayOrderList.Add(updatedOrder);

            Console.WriteLine("New Data has been added to list");
            Console.ReadKey();

            return;
        }





    }





    }






