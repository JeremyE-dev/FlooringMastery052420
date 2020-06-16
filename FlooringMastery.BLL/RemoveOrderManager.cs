using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class RemoveOrderManager
    {


        IOrderRepository _orderRepo;
        //public OrderRepository OrderRepo
        //{
        //    get { return _orderRepo; }
        //    set { _orderRepo = value; }
        //}

        Order _orderToEdit;

        public Order OrderToEdit
        {
            get { return _orderToEdit; }
            set { _orderToEdit = value; }
        }

      


        DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        int _orderNumber;

        public int OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }





        public RemoveOrderManager(IOrderRepository OrderRepo)
        {

            _orderRepo = OrderRepo;
            _orderToEdit = new Order();
         

        }
        //Returns of date was valid, if it is saces to orderdate field
        
        public Response ValidateDate(string userInput)
        {
            //add this to the order in order manager if successful
            DateTime userDate;
            Response response = new Response();

            if (!DateTime.TryParse(userInput, out userDate))
            {
                response.Success = false;
                response.Message = "Error: that was not a valid date";
                return response;

            }

            else if (DateTime.TryParse(userInput, out userDate))
            {
                response.Success = true;
                response.Message = "The date entered was in the valid format";
                OrderDate = userDate;

            }

            return response;

        }
      
        public Response ValidateOrderNumber(string userInput)
        {
            int orderNumber;
            Response response = new Response();

            if (!Int32.TryParse(userInput, out orderNumber) || String.IsNullOrEmpty(userInput))
            {
                response.Success = false;
                response.Message = "Error: Order number must be an integer and not empty";
                return response;

            }

            else if (Int32.TryParse(userInput, out orderNumber))
            {
                response.Success = true;
                response.Message = "The order entered was in the valid format";
                OrderNumber = orderNumber;

            }

            return response;


        }

        public Response ValidateOrderGroup()
        {
       
            Response response = (_orderRepo.CheckIfOrderGroupExists(OrderDate));

          
            return response;



        }



        public Response ValidateSpecificOrderExists()
        {
            Response response = new Response();

            if (_orderRepo.DoesOrderExistInList(OrderNumber))
            {
                response.Success = true;
                OrderToEdit = _orderRepo.GetOrderFromList(OrderNumber);
                response.Message = String.Format("The order you entered {0} has been located", OrderNumber);
                Console.ReadLine();
                return response;

            }

            else
            {
                response.Success = false;
                response.Message = String.Format("The order number you entered {0} was not found", OrderNumber);
                Console.ReadLine();

                return response;
            }

        }

      



        public void DisplayOrderInformation()
        {
            Console.WriteLine("Order to be Removed");
            Console.WriteLine("**************************************************************");
            Console.WriteLine("[{0}] [{1}]", OrderToEdit.OrderNumber, OrderToEdit.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("[{0}]", OrderToEdit.CustomerName);
            Console.WriteLine("[{0}]", OrderToEdit.State);
            Console.WriteLine("Product : [{0}]", OrderToEdit.ProductType);
            Console.WriteLine("Materials : [{0:c}]", OrderToEdit.MaterialCost);
            Console.WriteLine("Labor : [{0:c}]", OrderToEdit.LaborCost);
            Console.WriteLine("Tax : [{0:c}]", OrderToEdit.Tax);
            Console.WriteLine("Total : [{0:c}]", OrderToEdit.Total);
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ReadLine();
        }

     
        public bool ConfirmChanges()
        {
     

            Console.WriteLine("Press \"Y\" to Remove the order and \"N\" to return to the main menu");
            string userInput = Console.ReadLine();

            return ValidateYesNo(userInput);


        }

        public static bool ValidateYesNo(string userInput)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("press Y to confirm or N to return to main menu");
                string YN = Console.ReadLine();
                if (YN != "Y" && YN != "y" && YN != "N" && YN != "n")
                {
                    Console.WriteLine("Invalid entry: press any key to continue");
                    Console.ReadKey();

                    continue;
                }

                if (YN == "y" || YN == "Y")
                {
                    return true;
                }

                else if (YN == "n" || YN == "N")
                {
                    return false;
                }



            }
        }


   

        public void UpdateDataSource()
        {
           
            _orderRepo.RemoveOldOrderFromList();
           
        }
        

    }
        
    }



