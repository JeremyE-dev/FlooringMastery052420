﻿using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace FlooringMastery.Workflows
{
    class DisplayWorkflow
    {
      
        DisplayOrderManager _manager;
        public DisplayOrderManager Manager

        {
            get { return _manager; }
            set { _manager = value; }

        }

        public DisplayWorkflow()
        {
       

            _manager = DIContainer.Kernel.Get<DisplayOrderManager>();
        }

        public void Execute()
        {
            GetDateFromUser();
            ValidateAndDisplayOrders();
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
                    
                    Console.WriteLine("Press any key to continue");
                  
                    Console.ReadKey();
                    Console.Clear();

                    return;
                }

            }

            
        }

     
        public void ValidateAndDisplayOrders()
        {
            Response response = Manager.CheckIfOrderExists();

            if(!response.Success)
            {
                Console.WriteLine(response.Message);
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                return;
            }

            else
            {
                Manager.DisplayOrders();
            }

        }



    }
}
