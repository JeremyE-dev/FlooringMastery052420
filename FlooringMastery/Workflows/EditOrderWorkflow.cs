using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class EditOrderWorkflow
    {


       EditOrderManager _manager;
        public EditOrderManager Manager

        {
            get { return _manager; }
            set { _manager = value; }

        }

        public EditOrderWorkflow()
        {
            _manager = new EditOrderManager();
        }
        //1. Edit will query the user for a date and order number. 
        // -If the order exists in the file for that date

        //validates That the date is in the correct format
        //the orderDate is set/stored in the Manager

        public void Execute()
        {
            GetDateFromUser();
            
            if (!CheckIfFileExists())
            {
                return;
            }

            //checks if valid format
            // if in wrong format ask until integer
            //if in correct format - check that it existst
            ValidateOrderNumberFormat();

            if(!CheckIfOrderNumberExists())
            {
                return;
            }

            GetCustomerNameFromUser();
            GetStateFromUser();
            Manager.CalculateNewTaxRate(); // sets Resets tax rate based on userinput
            GetProductTypeFromUser();//sets newProduct and New Product Type
            GetNewAreaFromUser();
            Manager.CalculateNewLaborCost();
            Manager.CalculateNewMaterialCost();
            Manager.CalculateNewTax();
            Manager.CalculateNewTotal();
            

            if (Manager.ConfirmChanges())
            {
                Manager.RemoveOldOrderFromList();
                Manager.AddUpdatedOrderToList(); // Expect List to be in correct state
                Manager.WriteListToFile();//expect file to match list
                Console.WriteLine("Your Order Has Been Updated, Press any Key To Cointinue");
                Console.ReadKey();
                return;
            }

            else
            {
                return;
            }


            //recalculate all dependent fields
            // Display new order details
            //ask to confirm
            // if yes save to file - print save message
            //if no do not write to file and return to main men     


        }
        public void GetDateFromUser()
        {
            while (true)
            {
                // clear old text from screen
                Console.Clear();
                //write text to ask user for input
                Console.WriteLine("Enter an OrderDate : ex \"5/26/2022\"");
                //Console.WriteLine("Date must be after today: {0}, {1}",

                //TODO: implement using UTC and converting to timeszone
                //DateTime.Today.DayOfWeek, DateTime.Today.ToString("MM/dd/yyyy"));

                string userInput = Console.ReadLine();

                //validate user input - in BLL
                Response response = Manager.ValidateDate(userInput);


                //validation response object success field evaluates to false - return the message associated with that response.
                if (!response.Success)

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
                    return;
                }

            }


        }

        //if ordernumber is an integer return true, esle keep asking
        public bool ValidateOrderNumberFormat()
        {
            
           
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter an Order Number (must be an integer) : ex \"2\"");

                string userInput = Console.ReadLine();

                Response response = Manager.ValidateOrderNumber(userInput);

                if(!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press Any Key To Continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press Any Key To Continue");
                    Console.ReadKey();
                    return response.Success;
                }

               

            }
            
        }


        

        public bool CheckIfFileExists()
        {
            Response response = Manager.ValidateFile();
            //Response orderExistsResponse = Manager.ValidateOrder();

            while (true)
            {


                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return response.Success;

                }

                

                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    //Manager.DisplayOrderInformation();
                    break;
                }
            }

       
            return true;



        }

        public bool CheckIfOrderNumberExists()
        {
            

            Response response = Manager.ValidateSpecificOrderExists();

            while (true)
            {


                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return response.Success;

                }



                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Manager.DisplayOrderInformation();
                    Console.ReadLine();
                    return response.Success;
                }
            }




        }

        public void GetCustomerNameFromUser()
        {
            Response response = new Response();
            Console.WriteLine("Please Enter The New Customer Name or Press Enter to skip");
            string userInput = Console.ReadLine();
            //response will validate to true if blank or not black, but messages will be different 
            response = Manager.ValidatesCustomerName(userInput);
            Console.WriteLine(response.Message);

        }

        public void GetStateFromUser()
        {
            Response response = new Response();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please Enter The New State or Press Enter to skip");
                string userInput = Console.ReadLine();
                response = Manager.ValidateState(userInput);
                if(!response.Success)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                else
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }
        
            }

            //validation code

        }

        public void GetProductTypeFromUser()
        {
            Response response = new Response();
            Console.WriteLine("Please Enter The New ProductType or Press Enter to skip");
            string userInput = Console.ReadLine();
            response = Manager.ValidateProduct(userInput);
            Console.WriteLine(response.Message);
            Console.ReadLine();
        }

        public void GetNewAreaFromUser()
        {
            Response response = new Response();
            Console.WriteLine("Please Enter The New Area or Press Enter to skip");
            string userInput = Console.ReadLine();
            response =  Manager.ValidateArea(userInput);
            Console.WriteLine(response.Message);
            Console.ReadLine();
        }



        //2. it will query the user for each piece of order data but display the existing data.
        //--If the user enters something new, it will replace that data;
        //--if the user hits Enter without entering data, it will leave the existing data in place.

        //Only (CustomerName, State, ProductType,and Area can be changed)
        // If state, product type, of area are changes order will need to be recalculated.
        // OrderDate May not be changed

        //3. After querying for each editable field,
        //-- display a summary of the new order information
        //-- prompt for whether the edit should be saved.
        //-- If yes, replace data in the file
        //-- if no, do not save and return to main menu
    }
}
