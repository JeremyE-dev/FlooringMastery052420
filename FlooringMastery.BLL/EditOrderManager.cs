using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class EditOrderManager
    {
        OrderRepository _orderRepo;
        public OrderRepository OrderRepo
        {
            get { return _orderRepo; }
            set { _orderRepo = value; }
        }

        Order _orderToEdit;

        public Order OrderToEdit 
        { get { return _orderToEdit; }
            set { _orderToEdit = value; } 
        }


        DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        int _orderNumber;

        public int OrderNumber {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }


        string _newCustomerName;
        public string NewCustomerName {
            get {return _newCustomerName; }
            set { _newCustomerName = value; }
        }

        States _newState;
        public States NewState 
        {
            get { return _newState; }
            set { _newState = value; } 
        }



        TaxRateRepository _taxRateRepo;

        public TaxRateRepository TaxRateRepo 
        {
            get { return _taxRateRepo; }
            set { _taxRateRepo = value; } 
        }


        DateTime _newDate;
        public DateTime NewDate 
        {
            get {return _newDate; }
            set {_newDate = value; } 
        }

        string _newProductType;
        public string NewProductType
        {
            get {return _newProductType; }
            set {_newProductType = value; } 
        }
        
        
        int _newArea;
        public int NewArea 
        {
            get {return _newArea; }
            set {_newArea = value; }
        }

        string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public EditOrderManager()
        {
            _orderRepo = new OrderRepository();
            _orderToEdit = new Order();
            _taxRateRepo = new TaxRateRepository();
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
            
            if (!Int32.TryParse(userInput, out orderNumber))
            {
                response.Success = false;
                response.Message = "Error: Order number must be an integer";
                return response;

            }

            else if (Int32.TryParse(userInput, out orderNumber))
            {
                response.Success = true;
                response.Message = "The date entered was in the valid format";
                OrderNumber = orderNumber;

            }

            return response;


        }

        public Response ValidateFile()
        {
            string fileName = ConvertDateToFileName(OrderDate);


            string path = OrderRepo.FolderPath + fileName;
            Response response = new Response();

            //string OrderAsString = newOrder.OrderToLineInFile();

            if (!File.Exists(path))
            {
                response.Success = false;
                response.Message = String.Format("Error: There were no orders for the date given: {0}", OrderDate.ToString("MM/dd/yyyy"));

            }

            else
            {

                response.Success = true;
                FileName = fileName;
                //loads file into OrderRepo/places orders in orderList
                OrderRepo.ReadOrderByDate(FileName);
                response.Message = String.Format("An order file for {0} has been found", OrderDate);

            }

            return response;
        }


        //get Order from OrderRepo and store in field in this orderManager
        public Response ValidateOrder()
        {
            Response response = new Response();
            var orderToFind = OrderRepo.SalesDayOrderList.Where(o => o.OrderNumber == OrderNumber);

            if(!orderToFind.Any())
            {
                response.Success = false;
                response.Message = String.Format("The order number you entered {0} was not found", OrderNumber);
                return response;
            }
            
            else
            {
                //start here 6/10/2020
                response.Success = true;
                OrderToEdit = OrderRepo.SalesDayOrderList.Where(o => o.OrderNumber == OrderNumber).First();
                response.Message = String.Format("The order you entered {0} has been located", OrderNumber);

            }

            return response;
            

        }

        //Validate CustomerName
        public Response ValidatesCustomerName(string userInput)
        {
            Response response = new Response();

            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                response.Message = "Customer Name will not be updated";
                return response;
            }

            else
            {
                response.Success = true;
                NewCustomerName = userInput;
                response.Message = String.Format("Customer Name: \"{0}\"  was added to the order", userInput);
            }

            return response;


        }
        public Response ValidateState(string userInput)
        {
            Response response = new Response();
            States state;

            if(String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                response.Message = "new information was entered, no changes to State will be made";
                return response;

            }

            if (!Enum.TryParse(userInput, true, out state) || !Enum.IsDefined(typeof(States), state)) // userInput = value to try, true means ignore case, state is the output value
            {
                response.Success = false;
                response.Message = "Error: That was not a valid State, press any key to continue";
                return response;
            }

            //check if states is in sales area

            if (Enum.TryParse(userInput, true, out state) || Enum.IsDefined(typeof(States), state))
            {
                string stateString = state.ToString();
                TaxRate result = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(stateString));
                if (result == null)
                {
                    response.Success = false;
                    response.Message = string.Format(" {0} is not in our sales area", stateString);
                    return response;

                }

                else
                {
                    response.Success = true;
                    NewState = state;
                    response.Message = String.Format("State in order will be changed to  \"{0}\" after confirmation", stateString);
                }


            }


            return response;
        }

        public void DisplayOrderInformation()
        {
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
        
        
        
        public void DisplayExistingFile()
        {//1. Load the file
            OrderRepo.ReadOrderByDate(FileName);
            //print all orders in the file
            OrderRepo.printOrders();
            Console.ReadLine();
        }

        public string ConvertDateToFileName(DateTime date)
        {

            string result = "Orders_" + date.ToString("MMddyyyy") + ".txt";
            Console.WriteLine();
            return result;
        }
    }
}
