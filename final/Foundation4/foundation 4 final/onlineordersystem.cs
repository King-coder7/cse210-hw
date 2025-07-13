using System;
using System.Collections.Generic;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string GetAddressLabel()
    {
        return $"{street}\n{city}, {state}, {country}";
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string GetName() => name;
    public Address GetAddress() => address;
}

class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public double GetTotalCost() => price * quantity;
    public string GetPackingLabel() => $"{name} (ID: {productId}) x{quantity}";
}

class Order
{
    private Customer customer;
    private List<Product> products = new List<Product>();

    public Order(Customer customer)
    {
        this.customer = customer;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalCost()
    {
        double subtotal = 0;
        foreach (var product in products)
        {
            subtotal += product.GetTotalCost();
        }

        double shipping = customer.GetAddress().IsInUSA() ? 5 : 35;
        return subtotal + shipping;
    }

    public void DisplayPackingLabel()
    {
        Console.WriteLine("Packing Label:");
        foreach (var product in products)
        {
            Console.WriteLine(product.GetPackingLabel());
        }
    }

    public void DisplayShippingLabel()
    {
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(customer.GetName());
        Console.WriteLine(customer.GetAddress().GetAddressLabel());
    }
}

class Program
{
    static void Main()
    {
        var address1 = new Address("123 Main St", "Rexburg", "ID", "USA");
        var customer1 = new Customer("John Doe", address1);
        var order1 = new Order(customer1);
        order1.AddProduct(new Product("Keyboard", "KB001", 50.0, 1));
        order1.AddProduct(new Product("Mouse", "MS001", 25.0, 2));

        var address2 = new Address("22 Rue Cler", "Paris", "Île-de-France", "France");
        var customer2 = new Customer("Marie Curie", address2);
        var order2 = new Order(customer2);
        order2.AddProduct(new Product("Laptop", "LP001", 800.0, 1));

        Console.WriteLine("ORDER 1:");
        order1.DisplayPackingLabel();
        order1.DisplayShippingLabel();
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}\n");

        Console.WriteLine("ORDER 2:");
        order2.DisplayPackingLabel();
        order2.DisplayShippingLabel();
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}
