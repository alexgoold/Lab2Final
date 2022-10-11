using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2;

public class Customer
{
    public string? UserName { get; private set; }

    public string RewardLevel;
    public string? Password { get; set; }
    public double CartPrice;
    private List<Product> _cart;
    public List<Product> Cart => _cart;
    public IEnumerable<Product>? DistinctProducts => Cart.Distinct();
    public Customer(string? userName, string? password)
    {
        UserName = userName;
        Password = password;
        RewardLevel = "Normal";
        _cart = new List<Product>();
    }
    public virtual void PrintCartInfo()
    {
        CartPrice = 0;
            foreach (var product in DistinctProducts)
            {
                var productAmount = Cart.Count(p => p.Name == product.Name);
                double total = Math.Round((product.Price * productAmount), 2);
                Console.WriteLine($"{product}......{Math.Round(product.Price,2)} x {productAmount}......Total:{total}");
                CartPrice += total;
            }
        Console.WriteLine($"Total = {Math.Round(CartPrice, 2)}");
        Console.ReadLine();
    }
    public override string ToString()
    {
        string output = $"Username:{UserName}\nPassword:{Password}\nReward Level:{RewardLevel}\n";
        if (Cart.Count < 1)
        {
            Console.WriteLine(output + "Cart currently Empty");
            return output;
        }
        foreach (var product in DistinctProducts)
        {
            var productAmount = Cart.Count(p => p.Name == product.Name);

            output += $"{product} x {productAmount}\n";
        }
        Console.WriteLine(output);
        return output;
    }
    public bool CheckPassword(string password)
    {
        var attemptsRemaining = 3;
        while (attemptsRemaining > 0)
        {
            if (Password == password)
            {
                return true;
            }
            attemptsRemaining--;
            Console.WriteLine($"Password Incorrect try again. {attemptsRemaining} attempts remaining.");
            if (attemptsRemaining == 0)
            {

                return false;
            }
            Console.WriteLine("Password:");
            password = Console.ReadLine();
        }
        return false;
    }
}