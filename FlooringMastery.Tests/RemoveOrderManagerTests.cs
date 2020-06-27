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
    class RemoveOrderManagerTests
    {
        //ValidateDate
      
        [Test]
        [TestCase("", false)]
        [TestCase("7/1/2020", true)]
        [TestCase("7-12-2020", true)]
        [TestCase("07-12-2020", true)]
        [TestCase("7/1/", false)]

        public void InvalidDateIsFalse(string date, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository(); 
            RemoveOrderManager RemoveManager = new RemoveOrderManager(OrderRepo);
            response = RemoveManager.ValidateDate(date);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }


        //ValidateOrderNumber
        [TestCase("1", true)]
        [TestCase("100000", true)]
        [TestCase("jkdfsjkdfjhdfh", false)]
        [TestCase("", false)]
        

        public void InvalidateOrderNumberIsFalse(string number, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            RemoveOrderManager RemoveManager = new RemoveOrderManager(OrderRepo);
            response = RemoveManager.ValidateOrderNumber(number);

            bool actual = response.Success;

            Assert.AreEqual(expected, actual);



        }


        
        
        
        //ValidateOrderGroup - Test in OrderRepo
        //ValidateSpecificOrderExists - Test in Order Repo
        
        //**Save for second phase
        //DisplayOrderInformation
        
        //**receiving error messages
        //ConfirmChanges
        //ValidateYesNo

    }
}
