using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2;

public class BronzeCustomer : Customer
{
    public BronzeCustomer(string? userName, string? password) : base(userName, password)
    {
        RewardLevel = "Bronze";
    }
    public override void PrintCartInfo()
    {
        base.PrintCartInfo();
        Console.WriteLine($"Bronze Customer Total = {Math.Round(CartPrice * 0.95)}");
        // $"({Math.Round(CartPrice)} before bronze discount)");
        Console.WriteLine($"You saved {Math.Round(CartPrice -(CartPrice*0.95),2)}!");
        Console.ReadLine();
    }
}