using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class SilverCustomer : Customer
    {
        public SilverCustomer(string? userName, string? password) : base(userName, password)
        {

        }


        public override void PrintCartInfo(Customer? customer)
        {
            IEnumerable<Product>? result = customer?.Cart.Distinct();
            CartPrice = 0;
            if (result != null)
                foreach (var product in result)
                {
                    var productAmount = customer.Cart.Count(p => p.Name == product.Name);
                    double total = (product.Price * productAmount);

                    Console.WriteLine($"{product}......{product.Price} x {productAmount}......Total:{total}");

                    CartPrice += total;
                }
            Console.WriteLine($"Total = {CartPrice*0.90} ({CartPrice} before gold discount)");
            Console.WriteLine($"You saved {String.Format("{0:0.##}",CartPrice -(CartPrice*0.90))}!");
            Console.ReadLine();
        }
    }
}

  
