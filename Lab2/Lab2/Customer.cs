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

    public string? Password { get; set; }
    public double CartPrice;

    private List<Product> _cart;
    public List<Product> Cart
    {
        get { return _cart; }
    }

    public override string ToString()
    {
        return UserName + "," + Password;
    }

    public Customer(string? userName, string? password)
    {
        UserName = userName;
        Password = password;
        _cart = new List<Product>();
    }
    public virtual void PrintCartInfo(Customer? customer)
    {
        IEnumerable<Product>? result = customer?.Cart.Distinct();
        CartPrice = 0;
        if (result != null)
            foreach (var product in result)
            {
                var productAmount = customer.Cart.Count(p => p.Name == product.Name);
                double total = Math.Round(product.Price * productAmount, 2);

                Console.WriteLine($"{product}......{Math.Round(product.Price,2)} x {productAmount}......Total:{total}");
                CartPrice += total;
            }
        Console.WriteLine($"Total = {Math.Round(CartPrice, 2)}");
        Console.ReadLine();
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