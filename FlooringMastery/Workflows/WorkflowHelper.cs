using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class WorkflowHelper
    {
        public static void MenuInputValidation (string userinput, string message)
        {
            if(userinput != "1" && userinput != "2" && 
                userinput != "3" && userinput != "4" && userinput != "Q" && userinput != "q")
            {
                Console.WriteLine(message);
                Console.ReadKey();
                return;
            }
        }



    }
}

