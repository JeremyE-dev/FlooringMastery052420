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
            GetOrderNumberFromUser();
            CheckIfOrderExists();
            GetCustomerNameFromUser();
            GetStateFromUser();

            //ask for Customername and Validate
            //ask for State and Validate
            //ask for Product Type and Validate
            //ask for Area and Validate


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


        public void GetOrderNumberFromUser()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Enter an Order Number (must be an integer) : ex \"2\"");

                string userInput = Console.ReadLine();

                Response response = Manager.ValidateOrderNumber(userInput);

                //validation response object success field evaluates to false - return the message associated with that response.
                if (!response.Success)

                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                // if response object success field is set to true, return the
                // orderNumber is saved in the manager
                else
                {
                    return;
                }


            }
        }

        public void CheckIfOrderExists()
        {
           Response fileExistsResponse =  Manager.ValidateFile();
            Response orderExistsResponse = Manager.ValidateOrder();

            if (!fileExistsResponse.Success)
            {
                Console.WriteLine(fileExistsResponse.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return; //return to main menu?

            }

            if (!orderExistsResponse.Success)
            {
                Console.WriteLine(orderExistsResponse.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return; 

            }

            else
            {
                Manager.DisplayOrderInformation();
            }

            return;



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
            Console.WriteLine("Please Enter The New State or Press Enter to skip");
            string userInput = Console.ReadLine();
            response = Manager.ValidateState(userInput);
            Console.WriteLine(response.Message);
            Console.ReadLine();


            //validation code

        }

        public void GetProductTypeFromUser()
        {
            Console.WriteLine("Please Enter The New ProductType or Press Enter to skip");
        }

        public void GetAreaFromUser()
        {
            Response response = new Response();
            Console.WriteLine("Please Enter The New Area or Press Enter to skip");
            string userInput = Console.ReadLine();
            response =  Manager.ValidatesCustomerName(userInput);
            Console.WriteLine(response.Message);
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
