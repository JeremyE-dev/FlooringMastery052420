using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Data;
using FlooringMastery.Data.Repositories;

namespace FlooringMastery.BLL
{
    class DIContainer
    {
        public static IKernel Kernel = new StandardKernel();

        static DIContainer()
        {
            string chooserType = ConfigurationManager.AppSettings["Mode"].ToString();

            if(chooserType == "File")
            {
                Kernel.Bind<IOrderRepository>().To<OrderRepository>();
            }

            else if(chooserType == "Test")
            {
                Kernel.Bind<IOrderRepository>().To<TestOrderRepositorycs>();
            }
                 
        }
    }
}
