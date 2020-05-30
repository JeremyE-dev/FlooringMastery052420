using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Product
    {
        string _productType;
        public string ProductType 
        { 
            get { return _productType; } 
            set { _productType = value; } 
        }
        
        decimal _costPerSquareFoot;
        public decimal CostPerSquareFoot
        {
            get { return _costPerSquareFoot; }
            set { _costPerSquareFoot = value; }
        }

        decimal _laborCostPerSquareFoot;

        public decimal LaborCostPerSquareFoot 
        { 
            get {return _laborCostPerSquareFoot; } 
            set {_laborCostPerSquareFoot = value; } 
        }
    }
}
