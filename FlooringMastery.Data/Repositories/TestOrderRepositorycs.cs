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
        
        List<Order>_salesDayOrderList;

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

        public TestRepoDataSource TestData;

        public TestOrderRepositorycs()
        {
            _salesDayOrderList = new List<Order>();
            TestData = new TestRepoDataSource();

        }

        public void printOrders()
            {
                //foreach (var item in SalesDayOrderList)
                foreach (var item in TestData.DataSource)
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
            //SalesDayOrderList.Add(o);
            TestData.DataSource.Add(o);
            }


        //FROM DisplayOrderManager

        public Response CheckIfOrderGroupExists(DateTime d) //also used in edit functions
        {
            Response response = new Response();
            var CheckOrderDates = TestData.DataSource.Where(x => x.OrderDate == d);
            //var CheckOrderDates = SalesDayOrderList.Where(x => x.OrderDate == d);
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

        //public void AddUpdatedOrderToList(Order updatedOrder)
        //{
        //    SalesDayOrderList.Add(updatedOrder);

        //    Console.WriteLine("New Data has been added to list");
        //    Console.ReadKey();

        //    return;
        //}

       



    }





    }






