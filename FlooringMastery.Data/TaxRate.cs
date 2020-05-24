using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class TaxRate
    {
               
        private string _stateAbbreviation;
        public string StateAbbreviation
        {
            get { return _stateAbbreviation; }
            set { _stateAbbreviation = StateAbbreviation; }
        }
        
        private string _stateName;

        public string StateName
        {
            get { return _stateName; }
            set { _stateName = StateName; }
        }
        
        private decimal _rate;

        public decimal Rate
        { 
            get {return _rate; } 
            set {_rate = Rate; } 
        }

}


    
}
