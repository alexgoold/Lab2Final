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

        public double NewPrice;
        public double ConvertToAud(Product product)
        {
            NewPrice = product.Price / 7.15;
            Currency = "AUD";
            return NewPrice;
        }

        public double ConvertToUsd(Product product)
        {
            NewPrice = product.Price / 11.25;
            Currency = "USD";
            return NewPrice;
        }


        public override string ToString()
        {
            return Name;
        }




    }
}
