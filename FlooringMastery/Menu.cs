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
            Console.WriteLine("********************************************************");
            Console.WriteLine("* Flooring Program");
            Console.WriteLine("*");
            Console.WriteLine("* 1. Display Orders");
            Console.WriteLine("* 2. Add an Order");
            Console.WriteLine("* 3. Edit an Order");
            Console.WriteLine("* 4. Remove an Order");
            Console.WriteLine("* 5. Quit");
            Console.WriteLine("*");
            Console.WriteLine("********************************************************");


            Console.Write("\n Enter Selection: ");

            string userinput = Console.ReadLine();

            WorkflowHelper.MenuInputValidation(userinput, "That was not a valid entry");


                switch (userinput)
                {

                    case "1":
                        DisplayWorkflow displayWorkflow = new DisplayWorkflow();
                        displayWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addWorkflow = new AddOrderWorkflow();
                        addWorkflow.Execute();
                        break;
           
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
