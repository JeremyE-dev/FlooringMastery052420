using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models.Responses;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    class EditOrderManagerTests
    {
        //will likely 
        //need to instantiate a new order to edit

        //checks if date is in valid format
        //ValidateDate
        [TestCase("", false)]
        [TestCase("7/1/2020", true)]
        [TestCase("7-12-2020", true)]
        [TestCase("07-12-2020", true)]
        [TestCase("7/1/", false)]

        public void InvalidDateIsFalse(string date, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            response = EditManager.ValidateDate(date);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }

    
        //ValidateCustomerName
        [TestCase("fritz", true)]
        [TestCase("frits1234", true)]
        [TestCase("Alice", true)]
        [TestCase("", true)]
        public void CustomerNameCanBeEmpty(string name, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            response = EditManager.ValidatesCustomerName(name);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
           
        }

        [Test]
        public void CustomerNameDoesNotChangeIfNull()
        {
            string name = "";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.CustomerName = "Sam";
            response = EditManager.ValidatesCustomerName(name);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewCustomerName, "Sam");

        }

        [Test]
        public void CustomerNameUpdatesIfNotNull()
        {
            string name = "Sally";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.CustomerName = "Sam";
            response = EditManager.ValidatesCustomerName(name);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewCustomerName, "Sally");

        }

        //ValidateState
        //*can be empty
        //*updates if not null

        //ValidateProduct
        //*can be empty
        //*updates if not null

        //ValidateArea
        //*can be empty
        //*updates if not null

        //DisplayOrderInformation - Test??
        //DisplayOrderEdits - Test??

        
        //ValidateYesNo
        //CalculateNewMaterialCost
        //CalculateNewLaborCost
        //CalculateNewTaxrate
        //CalculateNewTax
        //CalculateNewTotal
        //ConvertDateToFileName
        //UpdateOrder

        //ConfirmChanges - Test in Repo
        //UpdateDataSource --Test in Order Repo
        //ValidateOrderNumber - Test in Order Repo
        //ValidateOrderGroup - Test in Order Repo
        //ValidateSpecificOrderExists - Test in Order Repo
    }
}
