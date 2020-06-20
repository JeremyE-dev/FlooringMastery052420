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
    [TestFixture]
    class AddOrderManagerTests
    {
        //ValtdateDate


        [Test]

        [TestCase("", false)]
        [TestCase("7/1/2020",true )]
        [TestCase("7-12-2020", true)]
        [TestCase("07-12-2020", true)]
        [TestCase("7/1/", false)]

        public void InvalidDateIsFalse(string date, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateDate(date);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);      
        }

        [Test]
      
        [TestCase("5/1/2020", false)]
        [TestCase("4/1/2020",false)]
        [TestCase("5/1/2021", true)]
        [TestCase("7/1/2019", false)]
        public void ValidDateNotInFutureIsFalse(string date, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateDate(date);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }

        //ValidateCustomerName
        [TestCase("fritz", true)]
        [TestCase("frits1234", true)]
        [TestCase("Alice", true)]
        [TestCase("", false)]
        public void CustomerNameNotEmpty(string name, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidatesCustomerName(name);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }
        //ValidateState
        [TestCase("OH", true)]
        [TestCase("PA", true)]
        [TestCase("MI", true)]
        [TestCase("IN", true)]
        public void StateInSalesAreReturnsTrue(string state , bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateState(state);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }

        //ValidateState
        [TestCase("WI", false)]
        [TestCase("TN", false)]
        [TestCase("CA", false)]
        [TestCase("LA", false)]

        public void StateNotInSalesAreReturnsFalse(string state, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateState(state);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }
        [TestCase("W", false)]
        [TestCase("", false)]
        [TestCase("CAA", false)]
        [TestCase("A", false)]
        public void InvalidStateReturnsFalse(string state, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateState(state);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }


        //ValidateProduct
        [TestCase("Wood", true)]
        [TestCase("Tile", true)]
        [TestCase("Carpet", true)]
        [TestCase("Laminate", true)]
        [TestCase("fur", false)]
        [TestCase("", false)]

        public void InvalidProductReturnsFalse(string product, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManger = new AddOrderManager(OrderRepo);
            response = AddManger.ValidateProduct(product);
           

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }

        //product user entered = product found

        [TestCase("Wood")]
        [TestCase("Tile")]
        [TestCase("Carpet")]
        [TestCase("Laminate")]
     
        public void ValidProductEnteredEqualsProductFound(string product)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
            AddManager.ValidateProduct(product);

            Assert.AreEqual(AddManager.NewOrder.Product.ProductType, product);
        }

        //ConfirmProduct
        //[Test]
        //[TestCase("Y", true)]
        //[TestCase("N", false)]
        //public void ValidateYesNoReturnsCorrectResponse(string YN, bool expected)
        //{
        //    OrderRepository OrderRepo = new OrderRepository();
        //    BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);


        //    bool actual = AddManager.ValidateYesNo("message", YN);
        //    Assert.AreEqual(expected, actual);

        //}
        //ValidateYesNo - SKIP For Now
        //ValidateArea

        //CalculateMaterialCost
        //CalculateLaborCost
        //CalculateTaxRate
        //CalculateTax
        //CalculateTotal
        //GenerateOrderNumber
        //DisplayOrderInformation
        //ConfirmOrder
        //Save








    }
}
