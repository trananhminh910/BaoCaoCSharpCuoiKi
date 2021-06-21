using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Customer.Models
{
    [Serializable]
    public class CartItem
    {
        public  Product Product{ set; get; }
        public int Quantity { set; get; }
    }
}