using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        Order newOrder;

        public Response validateDate (string userInput)
        {
            //add this to the order in order manager if successful
            DateTime userDate;
            Response response = new Response();

            if (!DateTime.TryParse(userInput, out userDate))
            {
                response.Success = false;
                response.Message = "Error: that was not a valid date";
                return response;

            }

            else if(DateTime.TryParse(userInput, out userDate))
            {
                if((userDate < DateTime.Today))
                {
                    response.Success = false;
                    response.Message = string.Format("Error: Date must be in the future \n" +
                        "Todays Date is: { 0} The Date Entered is: { 1}" 
                        , DateTime.Today.Date.ToString("MM / dd / yyyy"),  userDate.ToString("MM / dd / yyyy"));
                    return response;
                }

                else
                {
                    response.Success = true;
                    response.Message = "Date input was in correct fromat and in the future";
                    newOrder.OrderDate = userDate;
                   

                }
            }

            return response;

            




            //if it in prper format
            //if no set response success to false
            // set message to impropere format message
            //return response - will stop exection until user enters correct format

            //if
            //{
            //    //check if date is in the future
            //    //if not set response to false
            //    //set message to n ot in future
            //    ///return response
            //}


            //if you make it this far then
            //add the date to the order
            //set reponse success to true
            //set message "adding date successful"
            //return respopnse






        }
       //will validate each field input by the user
       //datetime
       // proper format - return response
       // in the future - return response
       // if it is successful will aa
       //product, etc
    }
}
