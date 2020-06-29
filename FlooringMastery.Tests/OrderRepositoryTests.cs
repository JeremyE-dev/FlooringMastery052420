using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace FlooringMastery.Tests
{
    class OrderRepositoryTests
    {
        //ReadOrderByDate
        //** method param is a filename that has previously been
        //verified to exist in the file system.
        //method extracts a date from file format "Orders_06132020.txt"
        // and converts it to a useable format "06/13/2020"
        //this allows a dateTime object to be parsed from the 
        //string input and later saved to the OrderDate
        //field in the target object


        //could generalize test cases and input data via parameters - time permitting
        [TestCase("Orders_09012020.txt")]
        public void DataExtractedFromInputSource(string filename)
        {
            OrderRepository OrderRepo = new OrderRepository();
            OrderRepo.ReadOrderByDate(filename);
            ProductRepository p = new ProductRepository();


            DateTime expectedDateTime = DateTime.Parse("09/01/2020");
            int expectedOrderNumber = 1;
            string expectedCustomerName = "Richie Rich";
            States expectedState = States.OH;
            decimal expectedTaxRate = 6.25M;
            
            Product expectedProduct = p.ProductList.Where(x => x.ProductType == "Wood").First();


            expectedProduct.ProductType = "Wood";

            string expectedProductType = expectedProduct.ProductType;

            decimal expectedArea = 200M;
            decimal expectedCostPerSquareFoot = 5.15M;
            decimal expectedLaborCostPerSquareFoot = 4.75M;
            decimal expectedMaterialCost = 1030.00M;
            decimal expectedLaborCost = 950.00M;
            decimal expectedTax = 123.750000M;
            decimal expectedTotal = 2103.750000M;

            
            
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].OrderDate, expectedDateTime);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].OrderNumber, expectedOrderNumber);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].CustomerName, expectedCustomerName);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].State, expectedState);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].TaxRate, expectedTaxRate);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].ProductType, expectedProductType);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].Area, expectedArea);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].CostPerSquareFoot, expectedCostPerSquareFoot);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].LaborCostPerSquareFoot, expectedLaborCostPerSquareFoot);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].MaterialCost, expectedMaterialCost);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].LaborCost, expectedLaborCost);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].Tax, expectedTax);
            Assert.AreEqual(OrderRepo.SalesDayOrderList[0].Total, expectedTotal);

        }



        //Add Methods
        //SaveAddedOrder - could add more scenarios and check each field - time permitting
        [TestCase("Orders_09012020.txt")]
        public void OrderAddsToExistingFile(string fileName)
        {
            OrderRepository OrderRepo = new OrderRepository();
            //place orders in SalesDayOrderList
            OrderRepo.ReadOrderByDate(fileName);
            Order addedOrder = new Order();
            addedOrder.OrderNumber = 3;
            addedOrder.CustomerName = "John Smith";
            addedOrder.State = States.PA;
            addedOrder.TaxRate = 6.75M;
            addedOrder.ProductType = "Wood";
            addedOrder.Area = 200;
            addedOrder.CostPerSquareFoot = 5.15M;
            addedOrder.LaborCostPerSquareFoot = 4.75M;
            addedOrder.MaterialCost = 1030.00M;
            addedOrder.LaborCost = 950.00M;
            addedOrder.Tax = 133.650000M;
            addedOrder.Total = 2113.650000M;

            OrderRepo.SaveAddedOrder(addedOrder);

            Assert.IsNotNull(OrderRepo.SalesDayOrderList[2]);


        }
        [TestCase("Orders_09012020.txt", 3, "09 / 01 / 2020")]
        
        //need to find a new was to test this, read order by date assumes file exists, throws exception iof not
        //order number calculator does not assume file exists and checks is if does or not
        //[TestCase("Orders_09012022.txt", 1, "09 / 01 / 2022")]

        public void OrderNumberCalculatesCorrectly(string fileName, int expected, string date)
        {
            OrderRepository OrderRepo = new OrderRepository();
            //place orders in SalesDayOrderList
            
            OrderRepo.ReadOrderByDate(fileName);
            Order newOrder = new Order();
            newOrder.OrderDate = DateTime.Parse(date);
            newOrder.OrderNumber = 3;
            newOrder.CustomerName = "John Smith";
            newOrder.State = States.PA;
            newOrder.TaxRate = 6.75M;
            newOrder.ProductType = "Wood";
            newOrder.Area = 200;
            newOrder.CostPerSquareFoot = 5.15M;
            newOrder.LaborCostPerSquareFoot = 4.75M;
            newOrder.MaterialCost = 1030.00M;
            newOrder.LaborCost = 950.00M;
            newOrder.Tax = 133.650000M;
            newOrder.Total = 2113.650000M;

            Assert.AreEqual(expected, OrderRepo.CalculateOrderNumber(newOrder));


        }



        //DisplayMethods
        //CheckIfOrderGroupExists
        //DisplayExistingFile


        //Edit Methods
        //DoesOrderExistInList
        //GetOrderFromList
        //RemoveOlOrderFromList - removes and deletes if last one
        //EditRemoveOldOrderFromList - Removes does not delete last
        //AddUpdatedOrderToList
        //WriteListToFile

        //**skipped for now
        //printOrders
        //ConvertToStateEnum
        //ConverDateToFileName(Order)
        //ConverDateToFileName(DateTime)

    }
}
