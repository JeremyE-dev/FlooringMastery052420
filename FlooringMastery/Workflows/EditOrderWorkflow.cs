using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using Ninject;
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
            _manager = DIContainer.Kernel.Get<EditOrderManager>();
        }
     
        public void Execute()
        {
            //1.) get the date and validate format
            GetDateFromUser();
            //2.) is there a group of orders for this date
            //call CheckIfOrderGroupExists - returns a response object


            //check if date exists in the file folder, and save filename 
            //in the order repository claSS
            //
            if (!CheckIfOrderGroupExists())
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
                Manager.UpdateDataSource();
                Console.WriteLine("Your Order Has Been Updated, Press any Key To Cointinue");
                Console.ReadKey();
                return;
            }

            else
            {
                return;
            }


          

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
                    //Console.WriteLine(response.Message);
                    Console.WriteLine("Press Any Key To Continue");
                    Console.ReadKey();
                    return response.Success;
                }

               

            }
            
        }


        

        public bool CheckIfOrderGroupExists()
        {
            Response response = Manager.ValidateOrderGroup();
            

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
                    //Console.ReadLine();
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




    }
}
