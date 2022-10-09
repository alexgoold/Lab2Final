using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public string Currency = "SEK";

        public string Name { get; set; }

        public double Price { get; set; }

        public double ConvertToAud(Product product)
        {
            double AUDPrice;
            switch (Currency)
            {
                case "SEK":
                    AUDPrice = product.Price / 7.15;
                    Currency = "AUD";
                    Price = AUDPrice;
                    return Price;
                case "USD":
                    AUDPrice = product.Price * 1.36;
                    Currency = "AUD";
                    Price = AUDPrice;
                    return Price;
                default:
                    Console.WriteLine("Currency already displaying in Australian Dollars");
                    return Price;
            }
        }

        public double ConvertToUsd(Product product)
        {
            double USDPrice;
            switch (Currency)
            {
                case "SEK":
                    USDPrice = product.Price / 11.25;
                    Currency = "USD";
                    Price = USDPrice;
                    return Price;
                case "AUD":
                    USDPrice = product.Price / 1.36;
                    Currency = "USD";
                    Price = USDPrice;
                    return Price;
                default:
                    Console.WriteLine("Currency already displaying in US Dollars");
                    return Price;
            }
        }

        public double ConvertToSek(Product product)
        {
            double SEKPrice;
            switch (Currency)
            {
                case "USD":
                    SEKPrice = product.Price * 11.25;
                    Currency = "SEK";
                    Price = SEKPrice;
                    return Price;
                case "AUD":
                    SEKPrice = product.Price * 7.15;
                    Currency = "SEK";
                    Price = SEKPrice;
                    return Price;
                default:
                    Console.WriteLine("Currency already displaying in Swedish Krona");
                    return Price;
            }
        }
        public override string ToString()
        {
            return Name;
        }




    }
}
