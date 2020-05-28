using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Workflows
{
    class DisplayWorkflow
    {
        // the job 
        //ask user for input, validate it, return it

        public void GetUserInput()
        {

        }

        //when user says display orders the application should 
        // 1.) create a file with the days date in the file name and place it in the correct folder
        // 2.) Look in the Main orders file, place all orders up to todays date into that file (or should it 
        // 3.) the application shoudl check if the file exists before reading it, if it exists read that file, 
        // if it does not exist create a new one

        //call method to load the order text file
        //wheer should load order be housed?
        // create an order model in .Data
        //create an order repo in . Data that contains a list of orders
        //order repo will load file (read and write)
    }
}
