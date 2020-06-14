using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class RemoveOrderWorkflow
    {
        RemoveOrderManager _manager;
        
        public RemoveOrderManager Manager
        {
            get { return _manager; }
            set { _manager = value; }

       }

        public RemoveOrderWorkflow()
        {
            _manager = new RemoveOrderManager();
        }

        public void Execute()
        {
            GetDateFromUser();

            if (!ExitIfOrderGroupDoesNotExist())
            {
                return;
            }

         
            ////checks if valid format
            //// if in wrong format ask until integer
            ////if in correct format - check that it existst
            ValidateOrderNumberFormat();


            //checks if exists if it does displays order
            if (!CheckIfOrderNumberExists())
            {
                return;
            }

            if (Manager.ConfirmChanges())
            {
                Manager.UpdateDataSource();
                Console.WriteLine("Your Order Has Been Removed, Press any Key To Continue");
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


        public bool ExitIfOrderGroupDoesNotExist()
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
                  
                    break;
                }
            }


            return true;



        }





        public bool ValidateOrderNumberFormat()
        {


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter an Order Number (must be an integer) : ex \"2\"");

                string userInput = Console.ReadLine();

                Response response = Manager.ValidateOrderNumber(userInput);

                if (!response.Success)
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
    }
}
