using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2;

public class GoldCustomer : Customer
{

    public GoldCustomer(string? userName, string? password) : base(userName, password)
    {

    }


    public override void PrintCartInfo(Customer? customer)
    {
        base.PrintCartInfo(customer); 
        Console.WriteLine($"Gold Customer Total = {Math.Round(CartPrice*0.85)}");
        Console.WriteLine($"You saved {Math.Round(CartPrice -(CartPrice*0.85),2)}!");
        Console.ReadLine();
    }
}