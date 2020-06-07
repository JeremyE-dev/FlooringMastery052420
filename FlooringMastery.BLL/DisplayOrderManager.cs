﻿using FlooringMastery.Data;
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
        
        OrderRepository _orderRepo;
        public OrderRepository OrderRepo
        {
            get { return _orderRepo; }
            set { _orderRepo = value; }
        }
        
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

        public DisplayOrderManager()
        {
           _orderRepo = new OrderRepository();
        }



        //Returns of date was valid, if it is saces to orderdate field
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

        public Response CheckIfFileExists()
        {
            string fileName = ConvertDateToFileName(OrderDate);
       

            string path = OrderRepo.FolderPath + fileName;
            Response response = new Response();

            //string OrderAsString = newOrder.OrderToLineInFile();

            if (!File.Exists(path))
            {
                response.Success = false;
                response.Message = String.Format("Error: There were no orders for the date given: {0}", OrderDate.ToString("MM/dd/yyyy"));
                      
            }

            else
            {

                response.Success = true;
                FileName = fileName;
                response.Message = String.Format("An order file for {0} has been found", OrderDate);
                
            }

            return response;
        }

        public void DisplayExistingFile()
        {//1. Load the file
            OrderRepo.ReadOrderByDate(FileName);
            //print all orders in the file
            OrderRepo.printOrders();
            Console.ReadLine();
        }

        public string ConvertDateToFileName(DateTime date)
        {

            string result = "Orders_" + date.ToString("MMddyyyy") + ".txt";
            Console.WriteLine();
            return result;
        }
    }
}
