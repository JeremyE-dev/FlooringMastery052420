using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class Order
    {
        int _orderNumber;

        public int OrderNumber
        { 
            get { return _orderNumber; } 
            set {_orderNumber = OrderNumber; } 
        }

        string _customerName;

        public string CustomerName 
        { 
            get { return _customerName; } 
            set {_customerName = CustomerName; } 
        }
                     
        string _state;

        public string State 
        { 
            get {return _state ; } 
            set {_state = State; } 
        }

        decimal _taxRate;

        public decimal TaxRate 
        { 
            get {return _taxRate; } 
            set { _taxRate = TaxRate; } 
        }


        string _productType;

        public string ProductType
        {
            get { return _productType; }
            set { _productType = ProductType; }
        }

        decimal _area;

        public decimal Area 
        { 
            get { return _area; } 
            set { _area = Area; } 
        }

        decimal _costPerSquareFoot;

        public decimal CostPerSquareFoot 
        { 
            get { return _costPerSquareFoot; } 
            set{ _costPerSquareFoot = CostPerSquareFoot; }
        }
        
        decimal _laborCostPerSquareFoot;

        public decimal LaborCostPerSquareFoot 
        { 
           get {return _laborCostPerSquareFoot; } 
           set {_laborCostPerSquareFoot = LaborCostPerSquareFoot; } 
        }
        
        decimal _materialCost;

        public decimal MaterialCost 
        { 
            get { return MaterialCost; } 
            set { _materialCost = MaterialCost; } 
        }

        decimal _laborCost;

        public decimal LaborCost 
        { 
            get { return _laborCost; } 
            set { _laborCost = LaborCost; } 
        }

        decimal _tax;

        public decimal Tax 
        { 
            get { return _tax; }  
            set { _tax = Tax; } 
        }
        decimal _total;

        public decimal Total 
        { 
            get { return _total; }  
            set { _total = Total; } }
    }
}
