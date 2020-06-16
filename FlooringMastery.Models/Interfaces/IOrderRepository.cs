using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        void SaveAddedOrder(Order o);
        Response CheckIfOrderGroupExists(DateTime orderDate);
        void DisplayExistingFile();
        Order GetOrderFromList(int orderNumber);
        int CalculateOrderNumber(Order newOrder);
        //void ReadOrderByDate(DateTime orderDate);
        //void ReadOrderByDate(object fileName);
        bool DoesOrderExistInList(int orderNumber);
        void RemoveOldOrderFromList();
        void AddUpdatedOrderToList(Order order);
        //Order GetOrderFromList(int orderNumber);
        //void WriteListToFile(DateTime orderDate);
        //void AddUpdatedOrderToList(Order order);
        //void RemoveOldOrderFromList();
    }
}
