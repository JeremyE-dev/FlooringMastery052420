using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models;
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

  

        [TestCase("100", false)]
        [TestCase("110",  true)]
        [TestCase("one", false)]
        [TestCase("", false)]

        [TestCase("150", true)]
        [TestCase("200", true)]
        [TestCase("300", true)]
        [TestCase("400", true)]

        public void InValidateAreaResponseIsAccurate(string input, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
            bool actual = AddManager.ValidateArea(input).Success;

            Assert.AreEqual(expected, actual);     

        }

        [TestCase("150")]
        [TestCase("200")]
        [TestCase("300")]
        [TestCase("400")]
        public void OrderAreaIsCorrect(string input)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
           

            AddManager.ValidateArea(input);
            Decimal actual = AddManager.NewOrder.Area;
            decimal expected = Decimal.Parse(input);

            Assert.AreEqual(expected, actual);
        }


        //CalculateMaterialCost
        [TestCase("Carpet", "200", 450)]
        [TestCase("Laminate", "200", 350)]
        [TestCase("Tile", "200", 700)]
        [TestCase("Wood", "200", 1030)]

        public void MaterialCostCalculatesCorrectly(string product, string area, decimal expectedMaterialCost)
        {   //area always 200
            //CarpetMaterial Cost = 200 * 2.25 = 450
            //Laminate Material Cost = 200 * 1.75 = 350
            //Tile Material Cost = 200 *3.5 = 700
            //Wood Material Cost = 200 * 5.15 = 1030
            
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
            
            AddManager.ValidateProduct(product);
            AddManager.ValidateArea(area);
            AddManager.CalculateMaterialCost();
            decimal actual = AddManager.NewOrder.MaterialCost;
            Assert.AreEqual(expectedMaterialCost, actual);

        }

        //CalculateLaborCost
        [TestCase("Carpet", "200", 420)]
        [TestCase("Laminate", "200", 420)]
        [TestCase("Tile", "200", 830)]
        [TestCase("Wood", "200", 950)]

        public void LaborCostCalculatesCorrectly(string product, string area, decimal expectedLaborCost)
        {   //area always 200
            //CarpetLabor Cost = 200 * 2.10 = 420
            //Laminate Labor Cost = 200 * 2.10 = 420
            //Tile Labor Cost = 200 *4.15 = 830
            //Wood Labor Cost = 200 * 4.75 = 950

            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);

            AddManager.ValidateProduct(product);
            AddManager.ValidateArea(area);
            AddManager.CalculateLaborCost();
            decimal actual = AddManager.NewOrder.LaborCost;
            Assert.AreEqual(expectedLaborCost, actual);

        }


        //CalculateTaxRate
        [TestCase("OH", 6.25)]
        [TestCase("PA", 6.75)]
        [TestCase("MI", 5.75)]
        [TestCase("IN", 6.00)]
        public void TaxRateAndStatMatch(string state, decimal taxrate)
        {
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
            AddManager.ValidateState(state);
            AddManager.CalculateTaxRate();

            Assert.AreEqual(AddManager.NewOrder.TaxRate, taxrate);
        }
        //CalculateTax
        //CalculateTotal
        [TestCase("OH", "Carpet", "200", 54.375, 924.375)]
        [TestCase("PA", "Carpet", "200", 58.725, 928.725)]
        [TestCase("MI", "Carpet", "200", 50.025, 920.025)]
        [TestCase("IN", "Carpet", "200", 52.200, 922.2)]
        //add test cases fro the other three products

        public void TaxAndTotalCalculatesCorrectly(string state, string product, string area, decimal expectedTax, decimal expectedTotal)
        {//Calculate Tax:(_newOrder.MaterialCost + _newOrder.LaborCost) * (_newOrder.TaxRate / 100);
         // state: OH, material:Carpet, Area: 200, materialcost: area * costsqrft, laborcost: area *labor cost/sqrfoot
         //
            OrderRepository OrderRepo = new OrderRepository();
            BLL.AddOrderManager AddManager = new AddOrderManager(OrderRepo);
            AddManager.ValidateState(state);
            AddManager.ValidateProduct(product);
            AddManager.ValidateArea(area);
            AddManager.CalculateMaterialCost();
            AddManager.CalculateLaborCost();
            AddManager.CalculateTaxRate();
            AddManager.CalculateTax();
            AddManager.CalculateTotal();
            Assert.AreEqual(AddManager.NewOrder.Tax, expectedTax);
            Assert.AreEqual(AddManager.NewOrder.Total, expectedTotal);

        }

        
        //GenerateOrderNumber - test in order repo
        //DisplayOrderInformation - no need to test
        //ConfirmOrder - test in orderrepo
        //Save - test in order repo








    }
}
