using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class BronzeCustomer : Customer
    {
        public BronzeCustomer(string? userName, string? password) : base(userName, password)
        {

        }


        public override void PrintCartInfo(Customer? customer)
        {
            base.PrintCartInfo(customer);
            Console.WriteLine($"Bronze Customer Total = {CartPrice*0.95} ({CartPrice} before gold discount)");
            Console.WriteLine($"You saved {String.Format("{0:0.##}",CartPrice -(CartPrice*0.95))}!");
            Console.ReadLine();
        }
    }
}

  
