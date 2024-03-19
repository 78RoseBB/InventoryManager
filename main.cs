using System;
using System.Collections.Generic;
using System.IO;

public class Logger
{
    public static void LogMessage(string logFile, string message, string level)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"[{timestamp}] [{level}] {message}\n";

        try
        {
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.Write(logEntry);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}

public class InventoryManager
{
    public static List<Dictionary<string, object>> SortProducts(List<Dictionary<string, object>> products, string sortKey, bool ascending)
    {
        if (products == null || products.Count == 0)
            return new List<Dictionary<string, object>>();

        switch (sortKey)
        {
            case "name":
                products.Sort((a, b) => string.Compare((string)a["name"], (string)b["name"]));
                break;
            case "price":
                products.Sort((a, b) => ((int)a["price"]).CompareTo((int)b["price"]));
                break;
            case "stock":
                products.Sort((a, b) => ((int)a["stock"]).CompareTo((int)b["stock"]));
                break;
            default:
                throw new ArgumentException("Invalid sort key");
        }

        if (!ascending)
            products.Reverse();

        return products;
    }
}

public class Tests
{
    public static void LoggerTests()
    {
      Logger.LogMessage("test.log", "Test message", "INFO");
    }

    public static void InventoryManagerTests()
    {

        List<Dictionary<string, object>> products = new List<Dictionary<string, object>>()
        {
            new Dictionary<string, object> { { "name", "Product A" }, { "price", 100 }, { "stock", 5 } },
            new Dictionary<string, object> { { "name", "Product B" }, { "price", 200 }, { "stock", 3 } },
            new Dictionary<string, object> { { "name", "Product C" }, { "price", 50 }, { "stock", 10 } }
        };

        List<Dictionary<string, object>> sortedProducts = InventoryManager.SortProducts(products, "price", false);

    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Run tests
        Tests.LoggerTests();
        Tests.InventoryManagerTests();
    }
}