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
            firstPrice = price;
        }
        public string Currency = "SEK";

        public string Name { get; set; }

        public double Price { get; set; }

        public double firstPrice;
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
                    return Price;
            }
        }

        public double ConvertToSek(Product product)
        {
            Currency = "SEK";
            Price = firstPrice;
            return Price;
        }
        public override string ToString()
        {
            return Name;
        }




    }
}
