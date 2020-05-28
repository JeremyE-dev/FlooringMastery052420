using FlooringMastery.Data;
using System;
using System.Collections.Generic;
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
                    

                }

                //Start Here 5/29/20
                //validate inout is positive and over 100

            }

            // calculate square feet based on area is the same as quare feet
            //validate order sixe over 100 square feet
          
        }

        public Order CreateOrderFromInput()
        {
        
            Order newOrder = new Order();
            newOrder.OrderDate = GetDateFromUser();
            newOrder.CustomerName = GetNameFromUser();
            newOrder.State = GetStateFromUser();
            newOrder.ProductType = GetProductFromUser().ProductType;


            Console.WriteLine();
            
           
          
          
            // productType - must enter a product type that is on file
            throw new NotImplementedException();
        }

        //checks for valid input type - helper maybe put in workflow helper folder

        public void ValidateUserInput()
        {

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

        public void AddOrderToMainOrdersList()
        {

        }

        public void ValidateDate(string userinput)
        {
            while(true)
            {

            }
        }


        
    }
}
