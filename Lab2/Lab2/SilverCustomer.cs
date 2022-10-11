using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2;

public class SilverCustomer : Customer
{
    public SilverCustomer(string? userName, string? password) : base(userName, password)
    {
        RewardLevel = "Silver";
    }


    public override void PrintCartInfo()
    {
        base.PrintCartInfo();
        Console.WriteLine($"Silver Customer Total = {Math.Round(CartPrice*0.90,2)}");
        Console.WriteLine($"You saved {Math.Round(CartPrice -(CartPrice*0.90),2)}!");
        Console.ReadLine();
    }
}