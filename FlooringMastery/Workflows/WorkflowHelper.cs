using FlooringMastery.Data;
using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class WorkflowHelper
    {
        public static void MenuInputValidation(string userinput, string message)
        {
            if (userinput != "1" && userinput != "2" &&
                userinput != "3" && userinput != "4" && userinput != "Q" && userinput != "q")
            {
                Console.WriteLine(message);
                Console.ReadKey();
                return;
            }
        }

        public static bool ValidateYesNo(string userInput)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("press Y to continue or N to select a different product");
                string YN = Console.ReadLine();
                if (YN != "Y" && YN != "y" && YN != "N" && YN != "n")
                {
                    Console.WriteLine("Invalid entry: press any key to continue");
                    Console.ReadKey();

                    continue;
                }

                if (YN == "y" || YN == "Y")
                {
                    return true;
                }

                else if (YN == "n" || YN == "N")
                {
                    return false;
                }



            }
        }

        //returns true if state is in sales area, false if it is not
        public static bool ValidateStateInSalesArea(States state)
        {
            TaxRateRepository TaxRateRepo = new TaxRateRepository();

            string stateString = state.ToString();
            TaxRate result = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(stateString));


            if (result == null)
            {
               return false;
            }

            else
            {

                return true;
            }



        }
    }
}

