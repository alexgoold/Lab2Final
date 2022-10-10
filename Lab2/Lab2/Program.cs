using System.Globalization;
using System.Reflection.Metadata;
using System.Threading.Channels;
namespace Lab2;

public class Program
{
    static void Main()
    {
        var customer1 = new Customer("Knatte", "123");
        var customer2 = new Customer("Fnatte", "321");
        var customer3 = new Customer("Tjatte", "213");
        List<Customer?> customers = new(){customer1, customer2, customer3};
        var product1 = new Product("Milk",15);
        var product2 = new Product("Bread",29);
        var product3 = new Product("Cheese",89);
        List<Product> uniqueProducts = new() { product1, product2, product3 };
        Customer? loggedInCustomer = new(string.Empty, string.Empty);
        var path = Path.Combine(Environment.CurrentDirectory, "Customers.txt");
        //If the above doesn't work, uncomment below line
        //var path = Path.Combine(GetFolderPath(Environment.SpecialFolder.Desktop) , "Customers.txt");
        CheckForCustomers();
        Login();
        void WriteCentered(string text)
        {
            Console.WriteLine(text.PadLeft(Console.WindowWidth/2+text.Length/2));
        }
        void Login()
        {
            while (true)
            {
                Console.Clear();
                WriteCentered("Welcome to the Milk Shop");
                Console.WriteLine("1) Log in (Existing customer)");
                Console.WriteLine("2) Create new customer");
                Console.WriteLine("3) Exit Program");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine();
                    switch (input)
                    {
                        case 1: 
                            Console.WriteLine("Username:");
                            string? username = Console.ReadLine();
                            var results = customers.Where(customer => customer.UserName == username);
                            if (results.Any())
                            {
                                Console.WriteLine("Password:");
                                string? password = Console.ReadLine();
                                if (password != null && results.First().CheckPassword(password))
                                {
                                    loggedInCustomer=results.First();
                                    Console.WriteLine($"{loggedInCustomer.UserName} logged in");
                                    Shopping();
                                }
                                Console.WriteLine("Returning to menu;");
                                Thread.Sleep(1000);
                            }
                            else if (!results.Any())
                            {
                                Console.WriteLine($"Username not found.\n Would you like to add user {username}?");
                                Console.WriteLine("(Y)es  (N)o");
                                ConsoleKey yesOrNO = Console.ReadKey().Key;
                                Console.WriteLine();
                                switch (yesOrNO)
                                {
                                    case ConsoleKey.Y:
                                        RegisterCustomer();
                                        break;
                                    case ConsoleKey.N:
                                        continue;
                                }
                            }
                            break;
                        case 2:
                            RegisterCustomer();
                            break;
                        case 3:
                            CheckForCustomers();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Input not recognised");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Input not recognised");
                }
            }
        }
        void RegisterCustomer()
        {
            while(true)
            {
                Console.WriteLine("Username:");
                var user = Console.ReadLine();
                string? newUsername;
                if (user.Length > 1 && !user.Contains(' ') && !customers.Any(c => c.UserName == user))
                {
                    newUsername = user;
                }
                else if (customers.Any(c => c.UserName == user))
                {
                    Console.WriteLine("Username already in use, try again");
                    continue;
                }
                else
                {
                    Console.WriteLine("Username in use or contains whitespaces");
                    continue;
                }
                Console.WriteLine("Password:");
                var pass = Console.ReadLine();
                string? newPassword;
                if (pass.Length > 1 && !pass.Contains(' '))
                {
                    newPassword = pass;

                }
                else
                {
                    Console.WriteLine("Password cannot be blank or contain whitespace");
                    continue;
                }
                WriteCentered("Choose a customer level");
                WriteColoured(ConsoleColor.Yellow,"(G)old");
                WriteColoured(ConsoleColor.DarkGray,"(S)ilver");
                WriteColoured(ConsoleColor.DarkRed,"(B)ronze");
                Console.WriteLine("(N)ormal");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "g":
                        var goldCustomer = new GoldCustomer(newUsername, newPassword);
                        customers.Add(goldCustomer);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(goldCustomer.ToString());

                        }
                        loggedInCustomer = goldCustomer;
                        break;
                    case "s":
                        var silverCustomer = new SilverCustomer(newUsername, newPassword);
                        customers.Add(silverCustomer);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(silverCustomer.ToString());

                        }
                        loggedInCustomer = silverCustomer;
                        break;
                    case "b":
                        var bronzeCustomer = new BronzeCustomer(newUsername, newPassword);
                        customers.Add(bronzeCustomer);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(bronzeCustomer.ToString());

                        }
                        loggedInCustomer = bronzeCustomer;
                        break;
                    case "n":
                        var newCustomer = new SilverCustomer(newUsername, newPassword);
                        customers.Add(newCustomer);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(newCustomer.ToString());

                        }
                        loggedInCustomer = newCustomer;
                        break;
                    default:
                        Console.WriteLine("Input not recognised");
                        continue;
                }
                Console.WriteLine($"{loggedInCustomer.UserName} now logged in. Continuing to shopping.");
                Thread.Sleep(1500);
                Shopping();
            }
        }
        void Shopping()
        {
            bool shoppingMenu = true;
            while (shoppingMenu)
            {
                Console.Clear();
                Console.WriteLine("(S)hop");
                Console.WriteLine("(V)iew Cart");
                Console.WriteLine("(F)inalise purchases");
                Console.WriteLine("\nChange currency to:");
                Console.WriteLine("(A)ustralian dollars AUD");
                Console.WriteLine("(U)S Dollars USD");
                Console.WriteLine("(K)ronor SEK");
                Console.WriteLine("(L)og out");
                var choice = Console.ReadKey().Key;
                switch (choice)
                {
                    case ConsoleKey.S:
                        bool items = true;
                        while (items)
                        {
                            Console.Clear();
                            WriteCentered("Products");
                            WriteCentered("Press the corresponding number to add to cart, press esc to return");
                            Console.WriteLine($"1){product1.Name}................{Math.Round(product1.Price,2)} {product1.Currency}");
                            Console.WriteLine($"2){product2.Name}................{Math.Round(product2.Price,2)} {product1.Currency}");
                            Console.WriteLine($"3){product3.Name}................{Math.Round(product3.Price,2)} {product1.Currency}");
                            var input = Console.ReadKey().Key;
                            int amount;
                            switch (input)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    Console.WriteLine($"Enter an amount of {product1.Name} to add:");
                                    if (int.TryParse(Console.ReadLine(), out amount))
                                    {
                                        for (int i = 0; i < amount; i++)
                                        {
                                            loggedInCustomer.Cart.Add(product1);
                                        }
                                        Console.WriteLine($"{product1.Name} x {amount} was added to cart");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Input not recognised.");

                                    }
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();

                                    Console.WriteLine("Enter an amount to add:");
                                    if (int.TryParse(Console.ReadLine(), out amount))
                                    {
                                        for (int i = 0; i < amount; i++)
                                        {
                                            loggedInCustomer.Cart.Add(product2);
                                        }
                                        Console.WriteLine($"{product2.Name} x {amount} was added to cart");
                                        Console.ReadLine();

                                    }
                                    else
                                    {
                                        Console.WriteLine("Input not recognised.");

                                    }
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    Console.WriteLine("Enter an amount to add:");
                                    if (int.TryParse(Console.ReadLine(), out amount))
                                    {
                                        for (int i = 0; i < amount; i++)
                                        {
                                            loggedInCustomer.Cart.Add(product3);
                                        }
                                        Console.WriteLine($"{product3.Name} x {amount} was added to cart");
                                        Console.ReadLine();

                                    }
                                    else
                                    {
                                        Console.WriteLine("Input not recognised.");

                                    }
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Write("(S");
                                    items = false;
                                    break;
                                default:
                                    Console.WriteLine("Input not recognised");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.V:
                        if (loggedInCustomer.Cart.Count < 1)
                        {
                            Console.WriteLine("Your cart is empty, please add something first!");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Clear();
                            WriteCentered("View Cart");
                            loggedInCustomer.PrintCartInfo(loggedInCustomer);
                        }
                        break;
                    case ConsoleKey.F:
                        if (loggedInCustomer.Cart.Count < 1)
                        {
                            Console.WriteLine("Your cart is empty, please add something first!");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Clear();
                            WriteCentered("Complete purchases");
                            loggedInCustomer.PrintCartInfo(loggedInCustomer);
                            Console.WriteLine($"Would you like to finalise transaction?");
                            Console.WriteLine("(Y)es  (N)o");
                            ConsoleKey yesOrNO = Console.ReadKey().Key;
                            switch (yesOrNO)
                            {
                                case ConsoleKey.Y:
                                    Console.WriteLine();
                                    Console.WriteLine("Transaction completed. Thank you for your purchase!");
                                    loggedInCustomer.Cart.Clear();
                                    Console.WriteLine("Logging out");
                                    Thread.Sleep(1000);
                                    loggedInCustomer = null;
                                    Login();
                                    break;
                                case ConsoleKey.N:
                                    Console.WriteLine();
                                    Console.WriteLine("Returning to Menu");
                                    Thread.Sleep(1000);
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.A:
                        Console.Clear();
                        foreach (var product in uniqueProducts)
                        {
                            product.ConvertToAud(product);
                        }
                        Console.WriteLine("Currency will now show in Australian dollars.");
                        Thread.Sleep(1500);
                        break;
                    case ConsoleKey.U:
                        Console.Clear();
                        foreach (var product in uniqueProducts)
                        {
                            product.ConvertToUsd(product);
                        }
                        Console.WriteLine("Currency will now show in American dollars.");
                        Thread.Sleep(1500);
                        break;
                    case ConsoleKey.K:
                        Console.Clear();
                        foreach (var product in uniqueProducts)
                        {
                            product.ConvertToSek(product);
                        }
                        Console.WriteLine("Currency will now show in Kronor.");
                        Thread.Sleep(1500);
                        break;
                    case ConsoleKey.L:
                        loggedInCustomer = null;
                        Login();
                        break;
                    default:
                        Console.Write("(S");
                        break;
                }
            }
        }
        void CheckForCustomers()
        {
            if (!File.Exists(path))
            {
                StreamWriter sw = new(path);
                foreach (var customer in customers)
                {
                    sw.WriteLine(customer.ToString());
                }
                sw.Close();
            }
            else if (File.Exists(path))
            {
                var file = File.ReadAllLines(path);

                foreach (var line in file)
                {
                    if (line.Contains(','))
                    {

                    }
                    string?[] userPass = line.Split(",");
                    var existingCustomer =
                        from customer in customers where customer.UserName == userPass[0] && customer.Cart.Count != 0 select customer;
                    if (existingCustomer.Any())
                    {
                        continue;
                    }
                    var newCustomer = new Customer(userPass[0], userPass[1]);
                    customers.Add(newCustomer);

                }

            }
        }
        void WriteColoured(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}