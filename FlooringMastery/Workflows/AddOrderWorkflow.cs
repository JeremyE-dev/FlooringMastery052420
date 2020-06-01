using FlooringMastery.Data;
using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Workflows
{
    class AddOrderWorkflow
    {

        OrderManager _manager;
        public OrderManager Manager

        {
            get { return _manager; }
            set { _manager = value; }

        }

        OrderRepository _orderRepo;

        public OrderRepository OrderRepo 
        { 
            get {return _orderRepo; } 
            set {_orderRepo = value; } 
        }

        ProductRepository _productRepo;

        public ProductRepository ProductRepo 
        {
            get {return _productRepo; } 
            set {_productRepo = value; }
        }
        
        TaxRateRepository _taxRateRepo;

        public TaxRateRepository TaxRateRepo
        {
            get {return _taxRateRepo; }
            set { _taxRateRepo = value; } 
        
        }


        Order _newOrder;

        public Order NewOrder 
        { 
            get { return _newOrder; } 
            set {_newOrder = value; } }



        public AddOrderWorkflow()
        {
            
            _productRepo = new ProductRepository();
            _taxRateRepo = new TaxRateRepository();
            _orderRepo = new OrderRepository();
            _newOrder = new Order();
            _manager = new OrderManager();

        }

        //used in: calculateMaterialCost method
        //set by getProduct from user
        Product productFromUser;

        //public bool Execute()
        //{


        //    //this method will return false if order was not compoleted
        //    if (!CreateOrderFromInput())
        //    {
        //        return false;
        //    }
                
        //    AddOrderToMainOrdersList();

        //    return true;
        //}


        //function: get a date from the user, validate it and return it
        public void GetDateFromUser()
        {
            while (true)
            {
                Console.Clear();      
                
                Console.WriteLine("Enter an OrderDate : ex \"5/26/2022\"");
                Console.WriteLine("Date must be after today: {0}, {1}", 
                    DateTime.Today.DayOfWeek,DateTime.Today.ToString("MM/dd/yyyy"));
                string userInput = Console.ReadLine();
                Response response = Manager.validateDate(userInput);

                if(Manager.validateDate(userInput).Success == false)
                    
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    return;
                }
          

            }

        }
        
        //works in UI
        public string GetNameFromUser()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter a customer name: ");
                string userInput = Console.ReadLine();
                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Error: customer name cannot be blank, press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                return userInput;
            }
  

        }
        
        //returns valis state, does NOT check if state is in sales areaq
        public States GetStateFromUser()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Please enter a state (i.e. AL for  Alabama)");
                string userInput = Console.ReadLine();
                //validates is the state is in the states list
                States state;
                if (!Enum.TryParse(userInput, true, out state)|| !Enum.IsDefined(typeof(States), state)) // userInput = value to try, true means ignore case, state is the output value
                {
                    Console.WriteLine("Error: That was not a valid State, press any key to continue");
                    Console.ReadKey();
                    continue;
                } 
              

                return state;
            }
           
  
        }

        //TODO - -
        ////if a product is added to the file it should show up in the application without a code change
        /// --I beleive that you this information shoudl be read from the file
        // productType - must enter a product type that is on file
        
            
            // will need to test that this does not return a Null product object
        public Product GetProductFromUser()
        {
            ProductRepository products = new ProductRepository();
            Product productToReturn = new Product();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select a product type from the list below");
                products.printProductsList();
                Console.Write("Enter name of product here:  ");
                string userinput = Console.ReadLine();


                //Start Here 5-28-20
                //validate - not null
                if (String.IsNullOrEmpty(userinput))
                {
                    Console.WriteLine("product name cannot be blank");
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                //validate if product is in the list

                if (!products.ProductList.Any(p => p.ProductType == userinput))
                {
                    Console.WriteLine("the product you requested is not in the list");
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    Console.WriteLine("you have selected {0} ", userinput);
                  

                    if (WorkflowHelper.ValidateYesNo(userinput))
                    {
                        productToReturn = products.ProductList.Find(p => p.ProductType == userinput);
                        productFromUser = productToReturn;

                        return productToReturn;

                        //break; //i expoect this will break out of while loop
                    }

                    else // if its false
                    {
                        continue;
                    }       

                }

              
            }
         
            // you have selected""
            //press "Y" to confirm product type or "N" to return to list

   
        }

        public decimal GetAreaFromUser()
        {
           

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter flooring area: ");
                string userInput = Console.ReadLine();
                decimal output;
              
                if (!Decimal.TryParse(userInput, out output))
                {
                    Console.WriteLine("Invalid Entry: Area must be a decimal");
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    continue;

                }

                else if (Decimal.TryParse(userInput, out output))
                {
                    if(output <=100)
                    {
                        Console.WriteLine("Invalid Entry: Area must greater than 100");
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        continue;
                    }

                }

               
               return decimal.Parse(userInput);
                

            }

  
          
        }

        //public bool CreateOrderFromInput()
        //{
        //    // generate new order when instantiated and save order in a field
        
            
        //    //From userinput
        //    NewOrder.OrderDate = GetDateFromUser();
        //    NewOrder.CustomerName = GetNameFromUser();
            
        //    States tempState = GetStateFromUser();

        //    if(!WorkflowHelper.ValidateStateInSalesArea(tempState))
        //    {
        //        Console.WriteLine("{0} is not in the sales area, press any key to return to main menu", tempState);
        //        Console.ReadKey();
        //        return false;
        //    }

        //    NewOrder.State = tempState;
        //    NewOrder.ProductType = GetProductFromUser().ProductType;
        //    NewOrder.Area = GetAreaFromUser();
            
        //    //calculated fields
        //    NewOrder.MaterialCost = CalculateMaterialCost();
        //    NewOrder.LaborCost = CalculateLaborCost();
        //    NewOrder.Tax = CalculateTax();
        //    NewOrder.Total = CalculateTotal();

        //    return true;
               
          
        //}
        //material cost = Area* CostPerSquareFoot
        public decimal CalculateMaterialCost()
        {
            decimal materialCost = NewOrder.Area * productFromUser.CostPerSquareFoot;
            return materialCost;

        }
        //LaborCost = Area * LaborCostPerSquareFoot
        public decimal CalculateLaborCost()
        {
            decimal laborCost = NewOrder.CostPerSquareFoot * productFromUser.LaborCostPerSquareFoot;
            return laborCost;

        }
       
        
        //Tax = ((MaterialCost + LaborCost) * (TaxRate/100)) *** Tax rates stored as whole numbers
        public decimal CalculateTax()
        {

            return (CalculateMaterialCost() + CalculateLaborCost()) * (GetTaxRate());


        }

        //get tax rate for the state given by the user 
        public decimal GetTaxRate()
        {
            //state given by user i know it is a valid state because the state I am using was validated //
            //when enetered by user
            string state = NewOrder.State.ToString();
            TaxRate result = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(state));

            //fix this later 

            //place in workflow helpers - and validate when state is entered
            //also validated when state was entered, but kep here just in case

            if(result == null)
            {
                Console.WriteLine("SWCCorp does not currently sell in {0}", state);
                Console.WriteLine("press M to return to Main Menu or Q to quit");
                return -1;
            }

            else
            {
                return result.Rate / 100;
            }  
            
        }

        
        //Total = (MaterialCost + LaborCost + Tax)
        public decimal CalculateTotal()
        {
            decimal result = CalculateMaterialCost() + CalculateLaborCost() + CalculateTax();
            return result;
        }



        //takes in date checks if file with date exists
        //if it does not exist, a file is created
        //?? return true if a new file was created and false if a file existed??
        //??or return the file date that was created??
        public bool CreateFileWithDate(string date)
        {
            throw new NotImplementedException();
        }

        public void AddOrderToSalesDateList()
        {


        }

        //add one order to the mainlist (existing file)

        public void AddOrderToMainOrdersList()
        {
            //add try/catch
            
            string path = _orderRepo.Path;

            string order = NewOrder.OrderToLineInFile();
         

         
            using(StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(order);
                
            }

            Console.WriteLine(order);
            Console.ReadLine();

        }


    }



    


        
    
}
