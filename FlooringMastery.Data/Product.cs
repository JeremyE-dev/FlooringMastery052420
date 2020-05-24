using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    class Product
    {
        string _productType;
        public string ProductType 
        { 
            get { return _productType; } 
            set { _productType = ProductType; } 
        }
        
        decimal _costPerSquareFoot;
        public decimal CostPerSquareFoot
        {
            get { return _costPerSquareFoot; }
            set { _costPerSquareFoot = CostPerSquareFoot; }
        }

        decimal _laborCostPerSquareFoot;

        public decimal LaborCostPerSquareFoot 
        { 
            get {return _laborCostPerSquareFoot; } 
            set {_laborCostPerSquareFoot = LaborCostPerSquareFoot; } 
        }
    }
}
