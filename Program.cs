using static System.Console;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Assignment03
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            var context = new Northwind();
            List<Customer> customerCollection = context.Customers.ToList();
            List<Product> productCollection = context.Products.ToList();
            ToJson(customerCollection, "customerdata.json");
            ToXml(customerCollection, "customerdata.xml");
            ToBinary(customerCollection, "customerdata.dat");
            ToJson(productCollection, "productdata.json");
            ToXml(productCollection, "productdata.xml");
            ToBinary(productCollection, "productdata.dat");

            List<SerializedFile> files = new List<SerializedFile>
            {
                new SerializedFile
                {
                    Name ="customerdata.json",
                    Size = new FileInfo("customerdata.json").Length
                },
                new SerializedFile
                {
                    Name ="customerdata.xml",
                    Size = new FileInfo("customerdata.xml").Length
                },
                new SerializedFile
                {
                    Name ="customerdata.dat",
                    Size = new FileInfo("customerdata.dat").Length
                },
                new SerializedFile
                {
                    Name ="productdata.json",
                    Size = new FileInfo("productdata.json").Length
                },
                new SerializedFile
                {
                    Name ="productdata.xml",
                    Size = new FileInfo("productdata.xml").Length
                },
                new SerializedFile
                {
                    Name ="productdata.dat",
                    Size = new FileInfo("productdata.dat").Length
                }
            };
            files.Sort();

            foreach (var file in files)
            {
                WriteLine($"{file.Name}: {file.Size}b");
            }

            // Question 2
            ReturnCities();
        }



        // Question 2
        static void ReturnCities()
        {
            WriteLine("Please select a city from the options below: ");
            // Query to return all cities in the DB.
            using (var db = new Northwind())
            {
                var cityQuery = db.Customers
                    .Select(customer => new
                    {
                        customer.City
                    }).Distinct().ToList();

                foreach (var cityQ in cityQuery)
                {
                    WriteLine($"{cityQ.City}, ");
                }
            }
            WriteLine();

            WriteLine("Enter the name of a city: ");
            string? city = ReadLine();
            using (var db = new Northwind())
            {
                // Query to find all customers in the
                // city that was entered
                var query = db.Customers
                    .Where(customer => customer.City == city)
                    .Select(customer => new
                    {
                        customer.City,
                        customer.CompanyName
                    });

                // Prints a statement with how many customers
                // are in the city entered,
                var count = query.Count();
                WriteLine(
                "There are {0} customer(s) in {1}: ",
                   arg0: query.Count(),
                   arg1: city);
                // Returns a list of all customers in the city that was
                // entered
                foreach (var customer in query)
                {
                    WriteLine($"    {customer.CompanyName}");
                }
            }
        }


        // Question 1 To-Text Methods
        static void ToBinary<T>(T data, string filename)
        {
            using (Stream st = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(st, data);
            }
        }
        static T FromBinary<T>(string filename)
        {
            using (Stream st = File.Open("data.data", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(st);
            }
        }
        static void ToJson<T>(T obj, string file_path)
        {
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(file_path, json);
        }
        static T FromJson<T>(string file_path)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            string json = File.ReadAllText(file_path);
            return JsonSerializer.Deserialize<T>(json, options);
        }
        static void ToXml<T>(T obj, string file_path)
        {
            using (StringWriter sw = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(sw, obj);
                File.WriteAllText(file_path, sw.ToString());
            }
        }
        static T FromXml<T>(string file_path)
        {
            using (StringReader sr = new StringReader(File.ReadAllText(file_path)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(sr);
            }
        }
    }
}
