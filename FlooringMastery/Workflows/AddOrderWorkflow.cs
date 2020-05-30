using FlooringMastery.Data;
using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringMastery.Workflows
{
    class AddOrderWorkflow
    {
        //ask user for:
        //OrderDate
        //CustomerName
        //State
        //ProductType
        //Area

        //takes in user input and returns an order
        //calculates 

        //gets date from user
        //validates date
        //returns DateTime object of future Date



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

        }

        //used in: calculateMaterialCost method
        //set by getProduct from user
        Product productFromUser;
        public DateTime GetDateFromUser()
        {
            while (true)
            {
                Console.Clear();
                DateTime userDate;
                DateTime Today = DateTime.Today;
                Console.WriteLine("Enter an OrderDate : ex \"5/26/2022\"");
                Console.WriteLine("Date must Be after today: {0}", Today);
                string userInput = Console.ReadLine();

                if(!DateTime.TryParse(userInput, out userDate))
                {
                    Console.WriteLine("Error: that was not a valid date");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
        
                DateTime DateEntered = userDate.Date;

                if(DateTime.Compare(Today, DateEntered) != 1)
                {
                    Console.WriteLine("Error: Date must be in the future");
                    Console.WriteLine("Todays Date is: {0} The Date Entered is: {1}", Today, DateEntered);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                return userDate;

            }

        }
        
        public string GetNameFromUser()
        {
            while(true)
            {
                Console.WriteLine("Enter a customer name: ");
                string userInput = Console.ReadLine();
                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Error: That is not a valid customer name, press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                return userInput;
            }
  

        }
        
        public States GetStateFromUser()
        {
            while (true)
            {
                Console.WriteLine("Please enter a state (i.e. AL for  Alabama)");
                string userInput = Console.ReadLine();
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
            Product productToReturn;

            while (true)
            {
                Console.WriteLine("Please select a product type from the list below");
                products.printProductsList();
                Console.Write("Enter name of product here:  ");
                string userinput = Console.ReadLine();


                //Start Here 5-28-20
                //validate - not null
                if (String.IsNullOrEmpty(userinput))
                {
                    Console.WriteLine("product name canot be blank");
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

                    while (true)
                    {

                        Console.WriteLine("press Y to continue or N to select a different product");
                        string YN = Console.ReadLine().ToLower();

                        if (YN == "y")
                        {
                            productToReturn = products.ProductList.Find(p => p.ProductType == userinput);
                            break; //i expoect this will break out of while loop
                        }

                        else if (YN == "n")
                        {
                            continue;
                        }

                        else
                        {
                            Console.WriteLine("Invalid entry");
                            continue;
                        }

                    }

                }

                productFromUser = productToReturn;

                return productToReturn;

            }
         
            // you have selected""
            //press "Y" to confirm product type or "N" to return to list

   
        }

        public decimal GetAreaFromUser()
        {
           

            while (true)
            {
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

                else
                {
                    return decimal.Parse(userInput);
                }

            }

            // calculate square feet based on area is the same as quare feet
            //validate order sixe over 100 square feet
          
        }

        public Order CreateOrderFromInput()
        {
            // generate new order when instantiated and save order in a field
        
            
            //From userinput
            NewOrder.OrderDate = GetDateFromUser();
            NewOrder.CustomerName = GetNameFromUser();
            NewOrder.State = GetStateFromUser();
            NewOrder.ProductType = GetProductFromUser().ProductType;
            NewOrder.Area = GetAreaFromUser();
            
            //calculated fields
            NewOrder.MaterialCost = CalculateMaterialCost(NewOrder);
            NewOrder.LaborCost = CalculateLaborCost(NewOrder);
            NewOrder.Tax = CalculateTax(NewOrder);
            NewOrder.Total = CalculateTotal(NewOrder);

            return NewOrder;         
          
        }
        //material cost = Area* CostPerSquareFoot
        public decimal CalculateMaterialCost(Order o)
        {
            decimal materialCost = o.Area * productFromUser.CostPerSquareFoot;
            return materialCost;

        }
        //LaborCost = Area * LaborCostPerSquareFoot
        public decimal CalculateLaborCost(Order o)
        {
            decimal laborCost = o.CostPerSquareFoot * productFromUser.LaborCostPerSquareFoot;
            return laborCost;

        }
       
        
        //Tax = ((MaterialCost + LaborCost) * (TaxRate/100)) *** Tax rates stored as whole numbers
        public decimal CalculateTax(Order o)
        {

            return (CalculateMaterialCost(o) + CalculateLaborCost(o)) * (GetTaxRate(o));


        }

        //get tax rate for the state given by the user 
        public decimal GetTaxRate(Order o)
        {
            //state given by user i know it is a valid state because the state I am using was validated //
            //when enetered by user
            string state = o.State.ToString();
            TaxRate result = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(state));
            return result.Rate / 100;
            
        }

        
        //Total = (MaterialCost + LaborCost + Tax)
        public decimal CalculateTotal(Order o)
        {
            decimal result = CalculateMaterialCost(o) + CalculateLaborCost(o) + CalculateTax(o);
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

        }

    


        
    }
}
