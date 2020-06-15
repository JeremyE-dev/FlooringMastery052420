using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Repositories
{
    public class TestRepoDataSource
    {
        static List<Order> _dataSource;

        public List<Order> DataSource 
        { 
            get { return _dataSource; }

            set { _dataSource = value; }
        
        }

        public TestRepoDataSource()
        {
            _dataSource = new List<Order>();

        }

    }
}
