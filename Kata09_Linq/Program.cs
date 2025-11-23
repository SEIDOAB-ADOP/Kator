using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata09_Linq
{
    static class LinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }

    class Program
    {
        const int NrOfCustomers = 10;
        const int MaxNrOfOrdersPerCustomer = 5;

        static void Main(string[] args)
        {
            //Create Order and customer Lists
            List<IOrder> OrderList = new List<IOrder>();
            List<ICustomer> CustomerList = new List<ICustomer>();

            var rnd = new Random();
            for (int c = 0; c < NrOfCustomers; c++)
            {
                var cus = Customer.Factory.CreateRandom();
                CustomerList.Add(cus);

                //Create a random number of order for the customer. Could be 0
                for (int o = 0; o < rnd.Next(0, MaxNrOfOrdersPerCustomer+1); o++)
                {
                    OrderList.Add(Order.Factory.CreateRandom(cus.CustomerID));
                }
            }

            QueryCustomersWithLinq(CustomerList);
            QueryOrdersWithLinq(CustomerList, OrderList);
        }

        private static void QueryCustomersWithLinq(IEnumerable<ICustomer> customers)
        {
            Console.WriteLine($"Nr of Customer: {customers.Count()}");
            Console.WriteLine("\nFirst 5 Customer:");
            customers.Take(5).Print();

            Console.WriteLine($"\nNumber of Customers in Sweden {customers.Where(cust => cust.Country == "Sverige").Count()}");
            Console.WriteLine($"\nOldest customer is born {customers.OrderBy(cust => cust.BirthDate).First().BirthDate:d}");
            Console.WriteLine($"Youngest customer is born {customers.OrderBy(cust => cust.BirthDate).Last().BirthDate:d}");

            Console.WriteLine($"\nNumber of customers per country");
            var groups = customers.GroupBy(cust => cust.Country);
            foreach (var item in groups)
            {
                Console.WriteLine($"{item.Key} has {item.Count()} number of customers");
            }

            Console.WriteLine($"\nNumber of customers that ends LastName with 'son': {customers.Where(cust => cust.LastName.EndsWith("son")).Count()}");
        }

        private static void QueryOrdersWithLinq(IEnumerable<ICustomer> customers, IEnumerable<IOrder> orders)
        {
            Console.WriteLine($"\nNr of orders: {orders.Count()}");
            Console.WriteLine($"Total order value: {orders.Sum(order => order.Total):C2}");

            Console.WriteLine("\ntop 5 Order list:");
            orders.OrderByDescending(order => order.Value).Take(5).Print();

            Console.WriteLine("\ntop 5 Orders with customer joined in via Join:");
            var orderCustomer = orders.Join(customers, o => o.CustomerID, c => c.CustomerID, (o, c) => new { o, c });
            orderCustomer.OrderByDescending(oc => oc.o.Value).Take(5).Print();

            Console.WriteLine("\ntop 5 Customers from order value via GroupJoin:");
            var CustomerOrders = customers.GroupJoin(orders, c => c.CustomerID, o => o.CustomerID, (cust, orders) => new { cust, orders });
            foreach (var co in CustomerOrders.OrderByDescending(co => co.orders.Sum(o => o.Value)).Take(5))
            {
                Console.WriteLine($"Cust: {co.cust.CustomerID}, OrderValue: {co.orders.Sum(o => o.Value):C2}");
            }
        }
    }
}

