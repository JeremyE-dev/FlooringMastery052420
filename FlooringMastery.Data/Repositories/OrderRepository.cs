﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderRepository
    {
          
        private string _path = "C:/Users/Jeremy/source/repos/FlooringMastery.Data/Orders_06012013.txt";
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public List<Order> MainOrderLIst { get; set; } = new List<Order>();

        public List<Order> SalesDayOrderList { get; set; } = new List<Order>();
        public OrderRepository()
        {

            ReadMainOrderFile();
        }

        public OrderRepository(string path)
        {
            _path = path;
            ReadMainOrderFile();
        }

        //Reads MainOrder File, places contents into the OrderList
        public void ReadMainOrderFile()
        {


            try
            {
                string[] rows = File.ReadAllLines(_path);
                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');
                    Order o = new Order();
                    o.OrderNumber = Int32.Parse(columns[0]);               
                    o.CustomerName = columns[1];
                    o.State = columns[2];
                    o.TaxRate = Decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = Decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = Decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = Decimal.Parse(columns[7]);
                    o.MaterialCost = Decimal.Parse(columns[8]);
                    o.LaborCost = Decimal.Parse(columns[9]);
                    o.Tax = Decimal.Parse(columns[10]);
                    o.Total = Decimal.Parse(columns[11]);

                    MainOrderLIst.Add(o);

                }


            }

            catch (Exception e)
            {
                Console.Write("There was a error the File System (ReadOrderFile), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);

            }

            
        }

        public void printOrders()
        {
            foreach (var item in MainOrderLIst)
            {
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}"
                    , item.OrderNumber, item.CustomerName, item.State, item.TaxRate, item.ProductType, item.Area,
                    item.CostPerSquareFoot, item.LaborCostPerSquareFoot, item.MaterialCost, item.LaborCost, item.Tax,
                    item.Total);
            }
        }

        //
        public void CreateSalesDayOrderList()
        {
            //each time this application is run - get the date from the system

        }



    }
}
