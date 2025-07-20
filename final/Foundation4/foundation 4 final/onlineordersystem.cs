using System;
using System.Collections.Generic;

// The Address class encapsulates address details.
// Its internal representation (private fields) is hidden.
class Address
{
    private string _street;
    private string _city;
    private string _stateProvince; // Can be state for USA, province for Canada, etc.
    private string _country;

    // Constructor to initialize an Address object
    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        // Case-insensitive comparison for country
        return _country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    // Method to return the full address as a formatted string
    public string GetFullAddressString()
    {
        return $"{_street}\n{_city}, {_stateProvince}, {_country}";
    }
}

// The Customer class encapsulates customer name and address.
// The Address object itself is also encapsulated.
class Customer
{
    private string _name;
    private Address _address; // Customer has an Address object

    // Constructor to initialize a Customer object
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Public getter for customer name
    public string GetName()
    {
        return _name;
    }

    // Public getter for customer's address object
    public Address GetAddress()
    {
        return _address;
    }

    // Method to check if the customer lives in the USA,
    // delegating the check to the encapsulated Address object.
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

// The Product class encapsulates product details and its total cost calculation.
class Product
{
    private string _name;
    private string _productId;
    private double _pricePerUnit;
    private int _quantity;

    // Constructor to initialize a Product object
    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        _name = name;
        _productId = productId;
        _pricePerUnit = pricePerUnit;
        _quantity = quantity;
    }

    // Method to calculate the total cost for this specific product
    public double GetTotalCost()
    {
        return _pricePerUnit * _quantity;
    }

    // Method to get a string for the packing label of this product
    public string GetPackingLabelString()
    {
        return $"{_name} (ID: {_productId}) x {_quantity}";
    }
}

// The Order class encapsulates a list of products and a customer,
// and handles calculations and label generation for the entire order.
class Order
{
    private Customer _customer;
    private List<Product> _products; // Order contains a list of Product objects

    // Constructor to initialize an Order object
    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>(); // Initialize the list of products
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Method to calculate the total cost of the entire order, including shipping
    public double GetTotalOrderCost()
    {
        double subtotal = 0;
        foreach (Product product in _products)
        {
            subtotal += product.GetTotalCost();
        }

        double shippingCost = _customer.LivesInUSA() ? 5.00 : 35.00; // Conditional shipping cost

        return subtotal + shippingCost;
    }

    // Method to generate the packing label for the order
    public string GetPackingLabel()
    {
        string label = "--- Packing Label ---\n";
        foreach (Product product in _products)
        {
            label += product.GetPackingLabelString() + "\n";
        }
        return label;
    }

    // Method to generate the shipping label for the order
    public string GetShippingLabel()
    {
        string label = "--- Shipping Label ---\n";
        label += $"Customer: {_customer.GetName()}\n";
        label += $"Address:\n{_customer.GetAddress().GetFullAddressString()}";
        return label;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // --- Order 1: Customer in USA ---
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Customer customer1 = new Customer("John Doe", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Keyboard", "KB001", 49.99, 1));
        order1.AddProduct(new Product("Gaming Mouse", "MS005", 25.50, 2));
        order1.AddProduct(new Product("Webcam HD", "WC003", 75.00, 1));

        Console.WriteLine("----- ORDER 1 DETAILS -----");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("\n" + order1.GetShippingLabel());
        Console.WriteLine($"\nTotal Order Cost: ${order1.GetTotalOrderCost():F2}\n");
        Console.WriteLine("---------------------------\n");


        // --- Order 2: Customer outside USA ---
        Address address2 = new Address("789 Rue de la Paix", "Paris", "Ile-de-France", "France");
        Customer customer2 = new Customer("Marie Dubois", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Noise-Cancelling Headphones", "HP010", 199.99, 1));
        order2.AddProduct(new Product("Portable Bluetooth Speaker", "SPK002", 89.00, 1));

        Console.WriteLine("----- ORDER 2 DETAILS -----");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("\n" + order2.GetShippingLabel());
        Console.WriteLine($"\nTotal Order Cost: ${order2.GetTotalOrderCost():F2}\n");
        Console.WriteLine("---------------------------\n");

        // --- Order 3: Another customer in USA ---
        Address address3 = new Address("456 Oak Ave", "Springfield", "IL", "USA");
        Customer customer3 = new Customer("Jane Smith", address3);

        Order order3 = new Order(customer3);
        order3.AddProduct(new Product("USB-C Hub", "HUB001", 30.00, 1));
        order3.AddProduct(new Product("External SSD 1TB", "SSD002", 120.00, 1));
        order3.AddProduct(new Product("Monitor Stand", "MST001", 40.00, 1));
        order3.AddProduct(new Product("Ergonomic Chair", "CHR005", 250.00, 1));

        Console.WriteLine("----- ORDER 3 DETAILS -----");
        Console.WriteLine(order3.GetPackingLabel());
        Console.WriteLine("\n" + order3.GetShippingLabel());
        Console.WriteLine($"\nTotal Order Cost: ${order3.GetTotalOrderCost():F2}\n");
        Console.WriteLine("---------------------------\n");
    }
}
