using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq.Extensions;

namespace FlooringMastery.BLL
{
    //will validate each field input by the user


    public class AddOrderManager
    {
        private Order newOrder = new Order();


        Product _product;

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        ProductRepository _productRepo;

        public ProductRepository ProductRepo
        {
            get { return _productRepo; }
            set { _productRepo = value; }
        }

        TaxRateRepository _taxRateRepo;

        public TaxRateRepository TaxRateRepo
        {
            get { return _taxRateRepo; }
            set { _taxRateRepo = value; }

        }

        OrderRepository _orderRepo;

        //This will need to be an interface vs a specific repository, wil need to be the same for other "Managers"
        public OrderRepository OrderRepo 
        {
            get { return _orderRepo; }
            set { _orderRepo = value; } 
        }

        public AddOrderManager()
        {
            _productRepo = new ProductRepository();
            Product = new Product();

            _taxRateRepo = new TaxRateRepository();
            _orderRepo = new OrderRepository();


        }


        //validates proper input, returns response object, sets order field if response successful
        //Checks business rule: date is in the future
        public Response ValidateDate (string userInput)
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

            else if(DateTime.TryParse(userInput, out userDate))
            {
                if((userDate < DateTime.Today))
                {
                    response.Success = false;
                    response.Message = string.Format("Error: Date must be in the future \n" +
                        "Todays Date is: { 0} The Date Entered is: { 1}" 
                        , DateTime.Today.Date.ToString("MM / dd / yyyy"),  userDate.ToString("MM / dd / yyyy"));
                    return response;
                }

                else
                {
                    response.Success = true;
                    response.Message = "Date input was in correct fromat and in the future";
                    newOrder.OrderDate = userDate; //stores userDate (in datetime format) in the new order field
                   
                }
            }

            return response;

        }

        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: customer name is not blank
        public Response ValidatesCustomerName(string userInput)
        {
            Response response = new Response();

            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = false;
                response.Message = "Error: customer name cannot be blank, press any key to continue";
                return response;            
            }

            else
            {
                response.Success = true;
                newOrder.CustomerName = userInput;
                response.Message = String.Format("Customer Name: \"{0}\"  was added to the order", userInput);
            }

            return response;

         
        }

        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: State is a valid State enum,State is in sales area
        public Response ValidateState(string userInput)
        {
            Response response = new Response();
            States state;


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
                if(result == null)
                {
                    response.Success = false;
                    response.Message = string.Format(" {0} is not in our sales area", stateString);
                    return response;

                }

                else
                {
                    response.Success = true;
                    newOrder.State = state;
                    response.Message = String.Format("State \"{0}\" was sucessfully added to order", stateString);
                }

            }

           
            return response;
        }
        
        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: Product must not be empty, Product must be on list, confirm user wants product
     
        public Response ValidateProduct(string userInput)
        {
            Response response = new Response();
            Product productFromUser = new Product();

           if (String.IsNullOrEmpty(userInput))
                {
                response.Success = false;
                response.Message = "the product must not be blank";
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
                newOrder.ProductType = productFromUser.ProductType;
                newOrder.Product = productFromUser; //stores product in Order Manger to use 
                

            }



            return response;
                            
            
        }

        //returns true if customer wants product, false otherwise
        public bool ConfirmProduct()
        {
            //Console.WriteLine("Please confirm {0} as your product (Y/N)", newOrder.ProductType);
            //Console.ReadLine();
            if(ValidateYesNo(String.Format("Enter Y to confirm  {0}  product or N to choose different product", newOrder.ProductType)))
            {
                return true;
            }

            else
            {
                return false;
            }
            

        }

        public static bool ValidateYesNo(string message)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine(message);
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

        public Response ValidateArea(string userInput)
        {
            decimal output;

            Response response = new Response();

            if (!Decimal.TryParse(userInput, out output))
            {

                response.Success = false;
                response.Message="Invalid Entry: Area must be a decimal";
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
                    newOrder.Area = output;
                    response.Message = String.Format("The flooring area: {0} has been added to your order", newOrder.Area);
                }

            }  

            return response;

        }


            
        //Area * CostPerSquareFoot
        public void CalculateMaterialCost() 
        {
            decimal result;
            result = newOrder.Area * newOrder.Product.CostPerSquareFoot;
            newOrder.MaterialCost = result;
            newOrder.CostPerSquareFoot = newOrder.Product.CostPerSquareFoot;

        }
        
        //Area * LaborCostPerSquareFoot
        public void CalculateLaborCost() 
        {
            decimal result;
            result = newOrder.Area * newOrder.Product.LaborCostPerSquareFoot;
            newOrder.LaborCost = result;
            newOrder.LaborCostPerSquareFoot = newOrder.Product.LaborCostPerSquareFoot;
        }
        public void CalculateTaxRate() 
        {
            decimal result;
            string state = newOrder.State.ToString();
            TaxRate rate = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(state));
            result = rate.Rate;
            newOrder.TaxRate = result;
        }
        public void CalculateTax() 
        { 
            decimal result = (newOrder.MaterialCost + newOrder.LaborCost) * (newOrder.TaxRate / 100);
            newOrder.Tax = result;
        }
        public void CalculateTotal() 
        {
            decimal result = newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax;
            newOrder.Total = result;
        }

        // once calculation are completed
        // show summary of order
        // ask user if they want to place the order
        //if yes - data will be written to the orders file - with the appropriate date
        // create a new file if it is the first order on the date;
       
        public void GenerateOrderNumber()
        {

            newOrder.OrderNumber = OrderRepo.CalculateOrderNumber(newOrder);
            
            
            //does a file exist with the date entered
            //string filename = ConvertDateToFileName(); //returns a filename
            //string path = OrderRepo.FolderPath + filename;


            //if (!File.Exists(path))
            //{
            //    newOrder.OrderNumber = 1;
            //}

            //else
            //{ //if the file does exists load the file to the order repository
            //    OrderRepo.ReadOrderByDate(filename); //this should load everything in that file to this list, 
            //                                         //hopefully will resolve null issue 
                
                
            // newOrder.OrderNumber = OrderRepo.SalesDayOrderList.MaxBy(o => o.OrderNumber).First().OrderNumber + 1;
              
            //}

        }
        
        //MOVE TO ORDER REPO -- I dont think this is used - 
        //function completed by Calculate OrderNumber
        public void setOrderNumber()
        {
            //finds the order with the highest order number
            
            int newOrderNumber = 
            OrderRepo.SalesDayOrderList.OrderByDescending(i => i.OrderNumber).Max().OrderNumber + 1;
            newOrder.OrderNumber = newOrderNumber;


            // thoughts: the order repo for this order
            //find the highest order number
            // add 1 to it

        }


        public void DisplayOrderInformation()
        {
            Console.Clear();
            Console.WriteLine("ORDER SUMMARY");
          

            Console.WriteLine("**************************************************************");
            Console.WriteLine("[{0}] [{1}]", newOrder.OrderNumber, newOrder.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("[{0}]", newOrder.CustomerName);
            Console.WriteLine("[{0}]", newOrder.State);
            Console.WriteLine("Product : [{0}]", newOrder.ProductType);
            Console.WriteLine("Materials : [{0:c}]", newOrder.MaterialCost);
            Console.WriteLine("Labor : [{0:c}]", newOrder.LaborCost);
            Console.WriteLine("Tax : [{0:c}]", newOrder.Tax);
            Console.WriteLine("Total : [{0:c}]", newOrder.Total);
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ReadLine();

        }



   
        public bool ConfirmOrder()
        {
            if (ValidateYesNo("Press Y to confirm Your order or N to cancel"))
            {
                //write order to file
                Console.WriteLine("Your order has been confirmed, press any key to continue to main menu");
                Console.ReadKey();
                return true;
            }

            else
            {
                Console.WriteLine("Your order will be cancelled, press any key to return to main menu");
                Console.ReadKey();
                return false;
            }
        }

        public void Save()
        {
            OrderRepo.SaveAddedOrder(newOrder);
        }

        //MOVE TO ORDER REPO - RENAME TO "SAVE ORDER"

        //public void WriteOrderToFile()
        //{
        //    string filename = ConvertDateToFileName(); //returns a filename

        //    string path = OrderRepo.FolderPath + filename;

        //    //validate if this is valid path first??

        //    string OrderAsString = newOrder.OrderToLineInFile();

        //    if (File.Exists(path))
        //    {
               

        //        using (StreamWriter writer = File.AppendText(path))
        //        {
        //            writer.WriteLine(OrderAsString);
        //        }
        //    }

        //    else
        //    {
               
        //        var myFile = File.Create(path);
        //        myFile.Close();  
                
        //        using (StreamWriter writer = new StreamWriter(path))
        //        {
        //            writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
        //            writer.WriteLine(OrderAsString);
        //        }
        //    }

        //    //after order is written Read from the file to load it to the OrderRepository
        //    //places orderin OrderRepo - orderlist
        //    OrderRepo.ReadOrderByDate(filename);


        //}

        // after calling confirm order

        //Move t0 ORDERREPO
        //public string ConvertDateToFileName()
        //{

        //    string result = "Orders_" + newOrder.OrderDate.ToString("MMddyyyy") + ".txt";
        //    Console.WriteLine("result");
        //    return result;
        //}

        

        }

      


    }


