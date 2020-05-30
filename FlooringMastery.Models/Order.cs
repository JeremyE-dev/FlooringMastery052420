using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Order
    {
        int _orderNumber;

        public int OrderNumber
        { 
            get { return _orderNumber; } 
            set {_orderNumber = value; } 
        }

        DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }

            set { _orderDate = value; }

        }

        string _customerName;

        public string CustomerName 
        { 
            get { return _customerName; } 
            set {_customerName = value; } 
        }
                     
        States _state;

        public States State 
        { 
            get {return _state ; } 
            set {_state = value; } 
        }

        decimal _taxRate;

        public decimal TaxRate 
        { 
            get {return _taxRate; } 
            set { _taxRate = value; } 
        }


        string _productType;

        public string ProductType
        {
            get { return _productType; }
            set { _productType = value; }
        }

        decimal _area;

        public decimal Area 
        { 
            get { return _area; } 
            set { _area = value; } 
        }

        decimal _costPerSquareFoot;

        public decimal CostPerSquareFoot 
        { 
            get { return _costPerSquareFoot; } 
            set{ _costPerSquareFoot = value; }
        }
        
        decimal _laborCostPerSquareFoot;

        public decimal LaborCostPerSquareFoot 
        { 
           get {return _laborCostPerSquareFoot; } 
           set {_laborCostPerSquareFoot = value; } 
        }
        
        decimal _materialCost;

        public decimal MaterialCost 
        { 
            get { return _materialCost; } 
            set { _materialCost = value; } 
        }

        decimal _laborCost;

        public decimal LaborCost 
        { 
            get { return _laborCost; } 
            set { _laborCost = value; } 
        }

        decimal _tax;

        public decimal Tax 
        { 
            get { return _tax; }  
            set { _tax = value; } 
        }
        decimal _total;

        public decimal Total 
        { 
            get { return _total; }  
            set { _total = value; }
        }

        //does not include order date because that is indicated by file name
        public string OrderToLineInFile()
        {
            string result =string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",OrderNumber, CustomerName, State.ToString(),
                TaxRate.ToString(), ProductType.ToString(), Area.ToString(), CostPerSquareFoot.ToString(),
                LaborCostPerSquareFoot.ToString(), MaterialCost.ToString(), LaborCost.ToString(), 
                Tax.ToString(), Total.ToString());
            
            return result;
        }
    }


  
}
