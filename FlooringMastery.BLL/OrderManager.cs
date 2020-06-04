using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    //will validate each field input by the user
    public class OrderManager
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



        //once validated through the validation method, method will set order field


        public OrderManager()
        {
            _productRepo = new ProductRepository();
            Product = new Product();

            _taxRateRepo = new TaxRateRepository();

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

            




            //if it in prper format
            //if no set response success to false
            // set message to impropere format message
            //return response - will stop exection until user enters correct format

            //if
            //{
            //    //check if date is in the future
            //    //if not set response to false
            //    //set message to n ot in future
            //    ///return response
            //}


            //if you make it this far then
            //add the date to the order
            //set reponse success to true
            //set message "adding date successful"
            //return respopnse






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
        ///
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
                response.Message = String.Format(" The product {0} has been found in the file", userInput);

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
            Console.WriteLine("Please confirm  as your product (Y/N) ");
            if(ValidateYesNo())
            {
                return true;
            }

            else
            {
                return false;
            }
            

        }

        public static bool ValidateYesNo()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("press Y to continue or N to select a different product");
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
                    newOrder.Area = output;
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

        }
        
        //Area * LaborCostPerSquareFoot
        public void CalculateLaborCost() 
        {
            decimal result;
            result = newOrder.Area * newOrder.Product.LaborCostPerSquareFoot; 
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
            decimal result = (newOrder.MaterialCost * newOrder.LaborCost) * (newOrder.TaxRate / 100);
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

        public void DisplayOrderInformation()
        {
            Console.WriteLine("Here is a summary of your order");
            //just include information without the order number right now and calculate in in a separate methd/class
            // perhaps have an order number class that has a field for an order number, a methos that generates an orderNumber
            // i.e. a order number generator
            // also stores a list of order numbers, and can search the file names and extract rhe order numbers from them
            //
            Console.WriteLine(newOrder.OrderDate.Date.ToString());
            Console.WriteLine(newOrder.CustomerName);
            Console.WriteLine(newOrder.State.ToString());
            Console.WriteLine("Product: {0}", newOrder.ProductType);
            Console.WriteLine("Materials: {0}", newOrder.MaterialCost);
            Console.WriteLine("Labor: {0}", newOrder.LaborCost);
            Console.WriteLine("Tax: {0}", newOrder.Tax);
            Console.WriteLine("Total: {0}", newOrder.Total);
            Console.WriteLine("Enter Y to confirm your order or N to cancel and return to Main Menu");
        }


    }

}
