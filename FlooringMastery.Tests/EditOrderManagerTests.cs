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
        [Test]
        public void StateDoesNotChangeIfNull()
        {
            string input = "";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.State = States.OH;
            response = EditManager.ValidateState(input);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewState, States.OH);

        }


        [Test]
        public void StateUpdatesIfNotNull()
        {
            string input = "PA";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.State = States.OH;
            response = EditManager.ValidateState(input);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewState, States.PA);

        }







        //ValidateProduct
        //*can be empty
        [Test]
        public void ProductDoesNotChangeIfNull()
        {
            string input = "";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.Product = new Product();
            EditManager.OrderToEdit.Product.ProductType = "Carpet";
            EditManager.OrderToEdit.Product.CostPerSquareFoot = 2.25M;
            EditManager.OrderToEdit.Product.LaborCostPerSquareFoot = 2.10M;
            EditManager.OrderToEdit.ProductType = "Carpet";
            response = EditManager.ValidateProduct(input);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewProduct.ProductType, "Carpet");
            Assert.AreEqual(EditManager.NewProduct.CostPerSquareFoot, 2.25M);
            Assert.AreEqual(EditManager.NewProduct.LaborCostPerSquareFoot, 2.10M);
            Assert.AreEqual(EditManager.NewProductType, "Carpet");

        }

        //ValidateProduct
        //*can be empty

        [Test]
        public void ProductUpdatesIfNotNull()
        {
            string input = "Wood";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.Product = new Product();
            EditManager.OrderToEdit.Product.ProductType = "Carpet";
            EditManager.OrderToEdit.Product.CostPerSquareFoot = 2.25M;
            EditManager.OrderToEdit.Product.LaborCostPerSquareFoot = 2.10M;
            EditManager.OrderToEdit.ProductType = "Carpet";
            response = EditManager.ValidateProduct(input);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewProduct.ProductType, "Wood");
            Assert.AreEqual(EditManager.NewProduct.CostPerSquareFoot, 5.15M);
            Assert.AreEqual(EditManager.NewProduct.LaborCostPerSquareFoot, 4.75M);
            Assert.AreEqual(EditManager.NewProductType, "Wood");

        }


        //ValidateArea
        //*can be empty
        [Test]
        public void AreaDoesNotChangeIfNull() {
        string input = "";
        Response response = new Response();
        OrderRepository OrderRepo = new OrderRepository();
        EditOrderManager EditManager = new EditOrderManager(OrderRepo);
        EditManager.OrderToEdit.Area = 200M;
     
        response = EditManager.ValidateArea(input);
        Assert.AreEqual(response.Success, true);
        Assert.AreEqual(EditManager.NewArea, 200M);
        }

        //ValidateArea
        //*updates if not null
        [Test]
        public void AreaUpdatesIfNotNull()
        {
            string input = "300";
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            EditManager.OrderToEdit.Area = 200M;

            response = EditManager.ValidateArea(input);
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(EditManager.NewArea, 300M);
        }

        //CalculateNewMaterialCost
        //ProductType,CostPerSquareFoot,LaborCostPerSquareFoot
        //Carpet,2.25,2.10 // MaterialCost = 200 X 2.25 = 450, 300 x 2.25 = 6.75
        //Laminate,1.75,2.10
        //Tile,3.50,4.15
        //Wood,5.15,4.75

        [TestCase("", "Wood") ]
        [TestCase("", "Laminate")]
        [TestCase("", "Tile")]
        [TestCase("", "Carpet")]
        [TestCase("175", "Wood")]
        [TestCase("400", "Laminate")]
        [TestCase("300", "Tile")]
        [TestCase("115", "Carpet")]

        public void NewMaterialCostUpdatesCorrectly(string newArea, string newProductName)
        {
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            ProductRepository ProductRepo = new ProductRepository();
            //order product started with this
            EditManager.OrderToEdit.Product = new Product();
            EditManager.OrderToEdit.Product.ProductType = "Carpet";
            EditManager.OrderToEdit.Product.CostPerSquareFoot = 2.25M;
            EditManager.OrderToEdit.Product.LaborCostPerSquareFoot = 2.10M;
            EditManager.OrderToEdit.Area = 200M;
            EditManager.OrderToEdit.ProductType = "Carpet";
            EditManager.ValidateArea(newArea);
            EditManager.ValidateProduct(newProductName);

            //this product object will be used to determine test calculations
            Product testProduct = ProductRepo.ProductList.Find(p => p.ProductType == newProductName);
            decimal testMaterialCost = EditManager.NewArea * testProduct.CostPerSquareFoot;

            EditManager.CalculateNewMaterialCost();
            Assert.AreEqual(EditManager.NewMaterialCost, testMaterialCost);


            //now change it -- how do I do that
            //user updates product


        }

        //CalculateNewLaborCost

        [TestCase("", "Wood")]
        [TestCase("", "Laminate")]
        [TestCase("", "Tile")]
        [TestCase("", "Carpet")]
        [TestCase("175", "Wood")]
        [TestCase("400", "Laminate")]
        [TestCase("300", "Tile")]
        [TestCase("115", "Carpet")]



        public void NewLaborCostUpdatesCorrectly(string newArea, string newProductName)
        {
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            ProductRepository ProductRepo = new ProductRepository();
            //order product started with this
            EditManager.OrderToEdit.Product = new Product();
            EditManager.OrderToEdit.Product.ProductType = "Carpet";
            EditManager.OrderToEdit.Product.CostPerSquareFoot = 2.25M;
            EditManager.OrderToEdit.Product.LaborCostPerSquareFoot = 2.10M;
            EditManager.OrderToEdit.Area = 200M;
            EditManager.OrderToEdit.ProductType = "Carpet";
            EditManager.ValidateArea(newArea);
            EditManager.ValidateProduct(newProductName);

            //this product object will be used to determine test calculations
            Product testProduct = ProductRepo.ProductList.Find(p => p.ProductType == newProductName);
            decimal testLaborCost = EditManager.NewArea * testProduct.LaborCostPerSquareFoot;

            EditManager.CalculateNewLaborCost();
            Assert.AreEqual(EditManager.NewLaborCost, testLaborCost);


            //now change it -- how do I do that
            //user updates product


        }

      
        [TestCase("OH")]
        [TestCase("PA")]
        [TestCase("MI")]
        [TestCase("IN")]

        public void TaxRateUpdatesCorrectly(string newState)
        {
            OrderRepository OrderRepo = new OrderRepository();
            EditOrderManager EditManager = new EditOrderManager(OrderRepo);
            TaxRateRepository TaxRateRepo = new TaxRateRepository();
            

            
            //this will update the "new state" field after called
            EditManager.ValidateState(newState);
            EditManager.OrderToEdit.TaxRate = 1;
            EditManager.CalculateNewTaxRate();
            TaxRate newTestTaxRate = EditManager.TaxRateRepo.TaxRateList.Find(t => t.StateAbbreviation == newState);
            decimal rate = newTestTaxRate.Rate;

            Assert.AreEqual(EditManager.NewTaxRate, rate);




        }

        //Start Here 6/27/2020
        //CalculateNewTax
        //CalculateNewTotal


        //ConvertDateToFileName
        //UpdateOrder






        //DisplayOrderInformation - Test??
        //DisplayOrderEdits - Test??
        //ValidateYesNo - Had issue - recvd erroe message

        //ConfirmChanges - Test in Repo
        //UpdateDataSource --Test in Order Repo
        //ValidateOrderNumber - Test in Order Repo
        //ValidateOrderGroup - Test in Order Repo
        //ValidateSpecificOrderExists - Test in Order Repo
    }
}
