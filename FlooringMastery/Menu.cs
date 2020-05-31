using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Workflows;

namespace FlooringMastery
{
    public static class Menu
    {
        public static  void Start()
        {

            while (true)
            {

            Console.Clear();
            Console.WriteLine("Flooring Mastery Application");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1. Display Orders");
            Console.WriteLine("2. Add Order");
            Console.WriteLine("3. Edit Order");
            Console.WriteLine("4. Remove Order");
            Console.WriteLine("\n Q to  Quit");

            Console.Write("\n Enter Selection");

            string userinput = Console.ReadLine();

            WorkflowHelper.MenuInputValidation(userinput, "That was not a valid entry");


                switch (userinput)
                {

                    case "1":
                        //Display Workflow:
                        //* query user for date
                        //* load order.txt file for that date
                        //* if file does not exist, display an error message return user to main menu
                        break;
                    case "2":
                        AddOrderWorkflow workflow = new AddOrderWorkflow();
                        if (!workflow.Execute()) 
                        {
                            continue;
                        }

                        else
                        {
                            break;
                        }
                        

           
                    case "3":
                        //edit order workflow
                        break;
                    case "4":
                        //remove order workflow
                        break;
                    case "Q":
                        return;
                    
                    case "q":
                        return;
                }


            }

        }


    }
}
