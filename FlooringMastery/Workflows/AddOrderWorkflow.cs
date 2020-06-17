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
using Ninject;

namespace FlooringMastery.Workflows
{
    class AddOrderWorkflow
    {

        AddOrderManager _manager;
        public AddOrderManager Manager

        {
            get { return _manager; }
            set { _manager = value; }

        }

   

        public AddOrderWorkflow()
        {
            
            _manager = DIContainer.Kernel.Get<AddOrderManager>();

        }


        public void Execute()
        {
            //collect and validate all user information, 
            //store user information within a newOrder within Order Manager
            GetDateFromUser();
            GetNameFromUser();
            GetStateFromUser();
            GetProductFromUser();
            GetAreaFromUser();
            Manager.CalculateMaterialCost();
            Manager.CalculateLaborCost();
            Manager.CalculateTaxRate();
            Manager.CalculateTax();
            Manager.CalculateTotal();
            Manager.GenerateOrderNumber();
            Manager.DisplayOrderInformation();

            //if this is true, then wite to file, else just return to main menu
            
            if(Manager.ConfirmOrder())
            {
                Manager.Save();

                //for 
            }
            else
            {
                return;
            }
            
        }

        //function: get a date from the user, validate it and return it
        public void GetDateFromUser()
        {
            while (true)
            {
                // clear old text from screen
                Console.Clear();
                //write text to ask user for input
                Console.WriteLine("Enter an OrderDate : ex \"5/26/2022\"");
                Console.WriteLine("Date must be after today: {0}, {1}", 
                
                //TODO: implement using UTC and converting to timeszone
                DateTime.Today.DayOfWeek,DateTime.Today.ToString("MM/dd/yyyy"));
                string userInput = Console.ReadLine();
                
                //validate user input - in BLL
                Response response = Manager.ValidateDate(userInput);


                //validation response object success field evaluates to false - return the message associated with that response.
                if(!response.Success)
                    
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                // if response object success field is set to true, return the
                // return the user input
                else
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }         

            }

        }
        
        //works in UI
        public void GetNameFromUser()
        {
            while(true)
            {
          
                Console.Clear();
                Console.WriteLine("Enter a customer name: ");
                string userInput = Console.ReadLine();

                Response response = Manager.ValidatesCustomerName(userInput);

                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                
                return;
               

            }  

        }
        
        //returns valid state, does NOT check if state is in sales areaq
        public void GetStateFromUser()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Please enter a state (i.e. AL for  Alabama)");
                string userInput = Console.ReadLine();

                Response response = Manager.ValidateState(userInput);
             
                if (!response.Success) // userInput = value to try, true means ignore case, state is the output value
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }          
               
            }      
  
        }

        //TODO - -
        ////if a product is added to the file it should show up in the application without a code change
        /// --I beleive that you this information shoudl be read from the file
        // productType - must enter a product type that is on file
        
            
            // will need to test that this does not return a Null product object
        public void GetProductFromUser()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select a product type from the list below");
                Manager.ProductRepo.printProductsList();
                Console.Write("Enter name of product here:  ");
                string userInput = Console.ReadLine();
                Response response = Manager.ValidateProduct(userInput);

                if (!response.Success)
                {

                    Console.WriteLine(response.Message);
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    continue;

                }


                else
                {

                    //Console.WriteLine(response.Message);
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    if(Manager.ConfirmProduct())
                    {

                        Console.WriteLine("Product Choice {0} has been saved", userInput);
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Press Any Key To Select a new product");
                        Console.ReadKey();
                        continue;
                    }
                }

            }

        }

    

        public void GetAreaFromUser()
        {
        
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter flooring area: ");
                string userInput = Console.ReadLine();
                Response response = Manager.ValidateArea(userInput);


                if (!response.Success)
                {
                    Console.WriteLine();
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    continue;

                }

                else 
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    return;

                }
              
            }
          
        }


       
       
        

    }



    


        
    
}
