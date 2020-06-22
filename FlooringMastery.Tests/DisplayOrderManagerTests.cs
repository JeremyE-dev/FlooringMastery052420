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
    class DisplayOrderManagerTests
    {
        [TestCase("", false)]
        [TestCase("7/1/2020", true)]
        [TestCase("7-12-2020", true)]
        [TestCase("07-12-2020", true)]
        [TestCase("7/1/", false)]

        public void InvalidDateIsFalse(string date, bool expected)
        {
            Response response = new Response();
            OrderRepository OrderRepo = new OrderRepository();
            DisplayOrderManager DisplayManager = new DisplayOrderManager(OrderRepo);
            response = DisplayManager.ValidateDate(date);

            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }
        //Tests for CheckIfOrderExists and Display will be tested in order repo

    }
}
