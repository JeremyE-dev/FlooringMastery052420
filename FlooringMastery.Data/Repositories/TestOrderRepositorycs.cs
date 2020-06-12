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


    class TestOrderRepositorycs:IOrderRepository
    {
      

        //This is a repo that contains all orders in One file Not all orders that exist in the Folder
        

            public List<Order> SalesDayOrderList { get; set; } = new List<Order>();



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
            }


        //FROM DisplayOrderManager

        public Response CheckIfOrderGroupExists(DateTime d)
        {
            Response response = new Response();
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

        


    }





    }






