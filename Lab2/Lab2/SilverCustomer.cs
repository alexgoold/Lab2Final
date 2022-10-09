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
            base.PrintCartInfo(customer);
            Console.WriteLine($"Silver Customer Total = {CartPrice*0.90} ({CartPrice} before gold discount)");
            Console.WriteLine($"You saved {String.Format("{0:0.##}",CartPrice -(CartPrice*0.90))}!");
            Console.ReadLine();
        }
    }
}

  
