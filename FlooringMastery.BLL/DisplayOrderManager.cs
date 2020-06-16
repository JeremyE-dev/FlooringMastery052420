using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class DisplayOrderManager
    {

        IOrderRepository _orderRepo;
     

        DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public DisplayOrderManager(IOrderRepository OrderRepo)
        {
            _orderRepo = OrderRepo;
        }



        //Returns if date was valid, if it is saces to orderdate field
        public Response ValidateDate(string userInput)
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

            else if (DateTime.TryParse(userInput, out userDate))
            {
                response.Success = true;
                response.Message = "The date entered was in the valid format";
                OrderDate = userDate;

            }

            return response;

        }

        public Response CheckIfOrderExists()
        {
            Response response = _orderRepo.CheckIfOrderGroupExists(OrderDate);
            return response;
        }


        public void DisplayOrders()
        {
            _orderRepo.DisplayExistingFile();
        }

    }

}