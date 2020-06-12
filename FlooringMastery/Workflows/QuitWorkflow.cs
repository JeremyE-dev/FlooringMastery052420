using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class QuitWorkflow
    {
        public void Execute ()
        {
            Console.WriteLine("Press any key to exit the application");
            Console.ReadKey();
            System.Environment.Exit(0);

        }

    }
}
