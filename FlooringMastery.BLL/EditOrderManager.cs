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
    public class EditOrderManager
    {
        IOrderRepository _orderRepo;
        //public OrderRepository OrderRepo
        //{
        //    get { return _orderRepo; }
        //    set { _orderRepo = value; }
        //}

        Order _orderToEdit;

        public Order OrderToEdit
        {   get { return _orderToEdit; }
            set { _orderToEdit = value; }
        }

        Order _updatedOrder;

        public Order UpdatedOrder 
        {
            get { return _updatedOrder; }
            set {_updatedOrder = value; } 
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
            get { return _newCustomerName; }
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
            get { return _newDate; }
            set { _newDate = value; }
        }



        decimal _newArea;
        public decimal NewArea
        {
            get { return _newArea; }
            set { _newArea = value; }
        }

        string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public ProductRepository _productRepo;

        public ProductRepository ProductRepo
        {
            get { return _productRepo; }
            set { _productRepo = value; }
        }

        string _newProductType;
        public string NewProductType
        {
            get { return _newProductType; }
            set { _newProductType = value; }
        }

        Product _newProduct;

        public Product NewProduct
        {
            get { return _newProduct; }
            set { _newProduct = value; }
        }

        decimal _newMaterialCost;
        
        public decimal NewMaterialCost 
        { 
            get {return _newMaterialCost;}
            set {_newMaterialCost = value;}
        }

        decimal _newCostPerSquareFoot;

        public decimal NewCostPerSquareFoot 
        {
            get { return _newCostPerSquareFoot; }
            set { _newCostPerSquareFoot = value; } 
        }
        
        
        decimal _newLaborCost;

        public decimal NewLaborCost 
        {
            get { return _newLaborCost; }
            set { _newLaborCost = value; } 
        }


        decimal _newLaborCostPerSquareFoot;

        public decimal NewLaborCostPerSquareFoot 
        {
            get {return _newLaborCostPerSquareFoot; }
            set { _newLaborCostPerSquareFoot = value;} 
        }
        
        decimal _newTaxRate;

        public decimal NewTaxRate 
        {
            get {return _newTaxRate; }
            set { _newTaxRate = value; } 
        }
        
        decimal _newTax;

        public decimal NewTax
        {
            get { return _newTax; }
            set { _newTax = value; }
        }

        decimal _newTotal;

        public decimal NewTotal 
        {
            get { return _newTotal; }
            set { _newTotal = value; } 
        }



        //Material Cost = Area * CostPerSquareFoot
        //LaborCost = Area * LaborCostperSquareFoot
        //Tax = ((MaterialCost + LaborCost)*(Taxrate/100))
        //--Tax Rates are stores as whole numnbers
        //Total = (MaterialCost + LaborCost +Tax)




        public EditOrderManager(IOrderRepository OrderRepo)
        {
            _orderRepo = OrderRepo;
            _orderToEdit = new Order();
            _updatedOrder = new Order();
            _taxRateRepo = new TaxRateRepository();
            _productRepo = new ProductRepository();
            _newProduct = new Product();

        }


        //Returns of date was valid, if it is saces to orderdate field
        //This field cannot be edited
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
        //This field cannot be edited
        public Response ValidateOrderNumber(string userInput)
        {
            int orderNumber;
            Response response = new Response();
            
            if (!Int32.TryParse(userInput, out orderNumber) || String.IsNullOrEmpty(userInput) )
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
        
        //MOVED TO ORDER REPO, possibly rename, currently same name in two classes
        public Response ValidateOrderGroup()
        {
            //    return OrderRepo.ValidateFile(OrderDate);
            Response response = (_orderRepo.CheckIfOrderGroupExists(OrderDate));

        
            return response;

           
           
        }




        //get Order from OrderRepo and store in field in this orderManager
        //MOVed TO ORDER REPO
        public Response ValidateSpecificOrderExists()
        {
            Response response = new Response();

            if(_orderRepo.DoesOrderExistInList(OrderNumber))
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

        //Validate CustomerName
        //if left blank: will be set to the name currently in the file
        public Response ValidatesCustomerName(string userInput)
        {
            Response response = new Response();

            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                response.Message = "No new information entered, Customer Name will not be updated";
                NewCustomerName = OrderToEdit.CustomerName;
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
        
        //if left blank: will be set to the name currently in the file
        public Response ValidateState(string userInput)
        {
            Response response = new Response();
            States state;

            if(String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                NewState = OrderToEdit.State;
                response.Message = "NO new information was entered, no changes to State will be made";
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
        
        //if left blank: will be set to the name currently in the file
        public Response ValidateProduct(string userInput)
        {
            Response response = new Response();
            Product productFromUser = new Product();

            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                NewProductType = OrderToEdit.ProductType;
                NewProduct = OrderToEdit.Product;
                response.Message = "Field left blank, no new information will be added";
                return response;
            }

            if (!ProductRepo.ProductList.Any(p => p.ProductType == userInput))
            {

                response.Success = false;
                response.Message = "the product you requested is not in the list";
                return response;
            }

            else
            {
                //finds product in list, stores in a variable
                response.Success = true;

                productFromUser = ProductRepo.ProductList.Find(p => p.ProductType == userInput);
                response.Message = String.Format("The product {0} has been found in the file", userInput);

                //sets the order product name to the order Product field
                //does this before asking user to confirm, so if user says no, the product they said no to will 
                //be in that6 order field until it is replaced by the new order
                // if user says no ( i.e. they want a different product) 
                //the product will be set to the next product after the method is called again
                NewProductType= productFromUser.ProductType;
                NewProduct = productFromUser; //stores product in Order Manger to use 

            }



            return response;


        }

        public Response ValidateArea(string userInput)
        {
            decimal output;

            Response response = new Response();


            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = true;
                response.Message = "No new information entered, Area will not ne changed";
                NewArea = OrderToEdit.Area;
                return response;
            }
            
            
            if (!Decimal.TryParse(userInput, out output))
            {

                response.Success = false;
                response.Message = "Invalid Entry: Area must be a decimal";
                return response;

            }

            else if (Decimal.TryParse(userInput, out output))
            {
                if (output <= 100)
                {
                    response.Success = false;
                    response.Message = "Invalid Entry: Area must greater than 100";
                    return response;

                }

                else
                {
                    response.Success = true;
                    NewArea = output;
                    response.Message = String.Format("The new flooring area: {0} will be saves upon confirmation", NewArea);
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
            //Console.ReadLine();
        }

        public void DisplayOrderEdits()
        {
            Console.WriteLine("**************************************************************");
            Console.WriteLine("[{0}] [{1}]", OrderToEdit.OrderNumber, OrderToEdit.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("[{0}]", NewCustomerName);
            Console.WriteLine("[{0}]", NewState);
            Console.WriteLine("Product : [{0}]", NewProductType);
            //Console.WriteLine("Materials : [{0:c}]", OrderToEdit.MaterialCost);
            //Console.WriteLine("Labor : [{0:c}]", OrderToEdit.LaborCost);
            //Console.WriteLine("Tax : [{0:c}]", OrderToEdit.Tax);
            //Console.WriteLine("Total : [{0:c}]", OrderToEdit.Total);
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ReadLine();
        }
        
        public bool ConfirmChanges()
        {
            Console.WriteLine("Summary of Edited Order");

            Console.WriteLine("**************************************************************");
            Console.WriteLine("[{0}] [{1}]", OrderToEdit.OrderNumber, OrderToEdit.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("[{0}]", NewCustomerName);
            Console.WriteLine("[{0}]", NewState);
            Console.WriteLine("Product : [{0}]", NewProductType);
            Console.WriteLine("Materials : [{0:c}]", NewMaterialCost);
            Console.WriteLine("Labor : [{0:c}]", NewLaborCost);
            Console.WriteLine("Tax : [{0:c}]", NewTax);
            Console.WriteLine("Total : [{0:c}]", NewTotal);
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ReadLine();

            Console.WriteLine("Press \"Y\" to save these changes and \"N\" to return to the main menu");
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

        //MOVE TO OrderRepo
        //public void DisplayExistingFile()
        //{//1. Load the file
        //    OrderRepo.ReadOrderByDate(FileName);
        //    //print all orders in the file
        //    OrderRepo.printOrders();
        //    Console.ReadLine();
        //}

        public void CalculateNewMaterialCost()
        {
            decimal result;
            result = NewArea * NewProduct.CostPerSquareFoot;//updated in precious calculation
            NewCostPerSquareFoot = NewProduct.CostPerSquareFoot;
            NewMaterialCost = result;
            

        }

        //Area * LaborCostPerSquareFoot
        public void CalculateNewLaborCost()
        {
            decimal result;
            result = NewArea * NewProduct.LaborCostPerSquareFoot;//updated in precious calculation
            NewLaborCostPerSquareFoot = NewProduct.LaborCostPerSquareFoot;
            NewLaborCost = result;
            
        }
        
        public void CalculateNewTaxRate()
        {
            decimal result;
            string state = NewState.ToString();
            TaxRate rate = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(state));
            result = rate.Rate;
            NewTaxRate = result;
        }
        public void CalculateNewTax()
        {
            decimal result = (NewMaterialCost + NewLaborCost) * (NewTaxRate / 100);
            NewTax = result;
        }
        public void CalculateNewTotal()
        {
            decimal result = NewMaterialCost + NewLaborCost + NewTax;
            NewTotal = result;
        }


        public string ConvertDateToFileName()
        {

            string result = "Orders_" + OrderToEdit.OrderDate.ToString("MMddyyyy") + ".txt";
            Console.WriteLine("result");
            return result;
        }

        

        public Order UpdateOrder()
        {
            UpdatedOrder.OrderNumber = OrderToEdit.OrderNumber;//this is because it will replace that order
            UpdatedOrder.OrderDate = OrderToEdit.OrderDate;//this field cannot be changed
            
            UpdatedOrder.CustomerName = NewCustomerName;
            UpdatedOrder.State = NewState;
            UpdatedOrder.TaxRate = NewTaxRate;
            UpdatedOrder.Product = NewProduct;
            UpdatedOrder.ProductType = NewProductType;
            UpdatedOrder.CostPerSquareFoot = NewCostPerSquareFoot;
            UpdatedOrder.LaborCostPerSquareFoot = NewCostPerSquareFoot;
            UpdatedOrder.Area = NewArea;
            UpdatedOrder.MaterialCost = NewMaterialCost;
            UpdatedOrder.LaborCost = NewLaborCost;
            UpdatedOrder.Tax = NewTax;
            UpdatedOrder.Total = NewTotal;

            return UpdatedOrder;
            
        }

        public void UpdateDataSource()
        {
            _orderRepo.RemoveOldOrderFromList();
            _orderRepo.AddUpdatedOrderToList(UpdateOrder());
           // _orderRepo.WriteListToFile(OrderDate);
        }



      
    }
}
