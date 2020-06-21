﻿using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq.Extensions;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    //will validate each field input by the user


    public class AddOrderManager
    {

        IOrderRepository _orderRepo;

        

        private Order _newOrder = new Order();

        public Order NewOrder
        {
            get { return _newOrder; }
            set { _newOrder = value; }
        }

        Product _product;

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        ProductRepository _productRepo;

        public ProductRepository ProductRepo
        {
            get { return _productRepo; }
            set { _productRepo = value; }
        }

        TaxRateRepository _taxRateRepo;

        public TaxRateRepository TaxRateRepo
        {
            get { return _taxRateRepo; }
            set { _taxRateRepo = value; }

        }


        public AddOrderManager(IOrderRepository OrderRepo)
        {
            _productRepo = new ProductRepository();
            Product = new Product();

            _taxRateRepo = new TaxRateRepository();
            
            _orderRepo = OrderRepo;


        }


        //validates proper input, returns response object, sets order field if response successful
        //Checks business rule: date is in the future
        public Response ValidateDate (string userInput)
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
                    response.Message = "Error: Date must be in the future";
                    //response.Message = string.Format("Error: Date must be in the future \n" +
                    //    "Todays Date is: { 0} The Date Entered is: { 1}" 
                    //    , DateTime.Today.Date.ToString("MM / dd / yyyy"),  userDate.ToString("MM / dd / yyyy"));
                    return response;
                }

                else
                {
                    response.Success = true;
                    response.Message = "Date input was in correct fromat and in the future";
                    _newOrder.OrderDate = userDate; //stores userDate (in datetime format) in the new order field
                   
                }
            }

            return response;

        }

        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: customer name is not blank
        public Response ValidatesCustomerName(string userInput)
        {
            Response response = new Response();

            if (String.IsNullOrEmpty(userInput))
            {
                response.Success = false;
                response.Message = "Error: customer name cannot be blank, press any key to continue";
                return response;            
            }

            else
            {
                response.Success = true;
                _newOrder.CustomerName = userInput;
                response.Message = String.Format("Customer Name: \"{0}\"  was added to the order", userInput);
            }

            return response;

         
        }

        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: State is a valid State enum,State is in sales area
        public Response ValidateState(string userInput)
        {
            Response response = new Response();
            States state;


            if (!Enum.TryParse(userInput, true, out state) || !Enum.IsDefined(typeof(States), state)) // userInput = value to try, true means ignore case, state is the output value
            {
                response.Success = false;
                response.Message = "Error: That was not a valid State, press any key to continue";
                return response;   
            }

            //check if states is in sales area

            if (Enum.TryParse(userInput, true, out state) || Enum.IsDefined(typeof(States), state))
            {
                string stateString = state.ToString();
                TaxRate result = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(stateString));
                if(result == null)
                {
                    response.Success = false;
                    response.Message = string.Format(" {0} is not in our sales area", stateString);
                    return response;

                }

                else
                {
                    response.Success = true;
                    _newOrder.State = state;
                    response.Message = String.Format("State \"{0}\" was sucessfully added to order", stateString);
                }

            }

           
            return response;
        }
        
        //validates proper input, returns response object, sets order field if response successful
        ////Checks business rule: Product must not be empty, Product must be on list, confirm user wants product
     
        public Response ValidateProduct(string userInput)
        {
            Response response = new Response();
            Product productFromUser = new Product();

           if (String.IsNullOrEmpty(userInput))
                {
                response.Success = false;
                response.Message = "the product must not be blank";
                return response;
            }

            if (!ProductRepo.ProductList.Any(p => p.ProductType == userInput))
            {

                response.Success = false;
                response.Message = "the product you requested is not in the list";
                return response;
            }

            else
            {
                //finds product in list, stores in a variable
                response.Success = true;

                productFromUser = ProductRepo.ProductList.Find(p => p.ProductType == userInput);
                response.Message = String.Format("The product {0} has been found in the file", userInput);

               
                _newOrder.ProductType = productFromUser.ProductType;
                _newOrder.Product = productFromUser; //stores product in Order Manger to use 
                

            }



            return response;
                            
            
        }

        //returns true if customer wants product, false otherwise
        public bool ConfirmProduct()
        {
            if (ValidateYesNo(String.Format("Enter Y to confirm  product: {0} or N to choose different product", _newOrder.ProductType)))
                //if (ValidateYesNo("Enter Y to confirm  product: {0} or N to choose different product"))
            {
                return true;
            }

            else
            {
                return false;
            }
            

        }
       
        public /*static*/ bool ValidateYesNo(string message)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine(message);
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

        //METHOD FOR TESTING ONLY -
        //public /*static*/ bool ValidateYesNo(string message,string input)
        //{

        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(message);
        //        string YN = input;


        //        if (YN != "Y" && YN != "y" && YN != "N" && YN != "n")
        //        {
        //            Console.WriteLine("Invalid entry: press any key to continue");
        //            Console.ReadKey();

        //            continue;
        //        }

        //        if (YN == "y" || YN == "Y")
        //        {
        //            return true;
        //        }

        //        else if (YN == "n" || YN == "N")
        //        {
        //            return false;
        //        }



        //    }
        //}


        public Response ValidateArea(string userInput)
        {
            decimal output;

            Response response = new Response();

            if (!Decimal.TryParse(userInput, out output))
            {

                response.Success = false;
                response.Message="Invalid Entry: Area must be a number and not blank";
                return response;

            }

            else if (Decimal.TryParse(userInput, out output))
            {
                if (output <= 100)
                {
                    response.Success = false;
                    response.Message = "Invalid Entry: Area must greater than 100";
                    return response;

                }

                else
                {
                    response.Success = true;
                    _newOrder.Area = output;
                    response.Message = String.Format("The flooring area: {0} has been added to your order", _newOrder.Area);
                }

            }  

            return response;

        }


            
        
        public void CalculateMaterialCost() 
        {
            decimal result;
            result = _newOrder.Area * _newOrder.Product.CostPerSquareFoot;
            _newOrder.MaterialCost = result;
            _newOrder.CostPerSquareFoot = _newOrder.Product.CostPerSquareFoot;

        }
        
        
        public void CalculateLaborCost() 
        {
            decimal result;
            result = _newOrder.Area * _newOrder.Product.LaborCostPerSquareFoot;
            _newOrder.LaborCost = result;
            _newOrder.LaborCostPerSquareFoot = _newOrder.Product.LaborCostPerSquareFoot;
        }
        public void CalculateTaxRate() 
        {
            decimal result;
            string state = _newOrder.State.ToString();
            TaxRate rate = TaxRateRepo.TaxRateList.Find(x => x.StateAbbreviation.Contains(state));
            result = rate.Rate;
            _newOrder.TaxRate = result;
        }
        public void CalculateTax() 
        { 
            decimal result = (_newOrder.MaterialCost + _newOrder.LaborCost) * (_newOrder.TaxRate / 100);
            _newOrder.Tax = result;
        }
        public void CalculateTotal() 
        {
            decimal result = _newOrder.MaterialCost + _newOrder.LaborCost + _newOrder.Tax;
            _newOrder.Total = result;
        }

        // once calculation are completed
        // show summary of order
        // ask user if they want to place the order
        //if yes - data will be written to the orders file - with the appropriate date
        // create a new file if it is the first order on the date;
       
        public void GenerateOrderNumber()
        {

            _newOrder.OrderNumber = _orderRepo.CalculateOrderNumber(_newOrder);
            
         

        }
        
 
        


        public void DisplayOrderInformation()
        
        {
            Console.Clear();
            Console.WriteLine("ORDER SUMMARY");
          

            Console.WriteLine("**************************************************************");
            Console.WriteLine("[{0}] [{1}]", _newOrder.OrderNumber, _newOrder.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("[{0}]", _newOrder.CustomerName);
            Console.WriteLine("[{0}]", _newOrder.State);
            Console.WriteLine("Product : [{0}]", _newOrder.ProductType);
            Console.WriteLine("Materials : [{0:c}]", _newOrder.MaterialCost);
            Console.WriteLine("Labor : [{0:c}]", _newOrder.LaborCost);
            Console.WriteLine("Tax : [{0:c}]", _newOrder.Tax);
            Console.WriteLine("Total : [{0:c}]", _newOrder.Total);
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ReadLine();

        }



   
        public bool ConfirmOrder()
        {
            if (ValidateYesNo("Press Y to confirm Your order or N to cancel"))
            {
                
                Console.WriteLine("Your order has been confirmed, press any key to continue to main menu");
                
                Console.ReadKey();
                return true;
            }

            else
            {
                Console.WriteLine("Your order will be cancelled, press any key to return to main menu");
                Console.ReadKey();
                return false;
            }
        }

        public void Save()
        {
            _orderRepo.SaveAddedOrder(_newOrder);
        }

        

        }

      


    }


