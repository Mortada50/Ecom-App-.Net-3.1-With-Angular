using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Enitites
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {

        }

        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; } // key
        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>(); // value
    }
}
