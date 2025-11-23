using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata08_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IOrder> OrderList = new List<IOrder>();
            for (int i = 0; i < 50_000; i++)
            {
                OrderList.Add(Order.Factory.CreateRandom());
            }

            Console.WriteLine($"Nr of orders: {OrderList.Count}");
            Console.WriteLine($"Order value total: {OrderList.Sum(order => order.Value):C2}");

            Console.WriteLine("\n5 largest orders:");
            OrderList.Take(5).OrderByDescending(order=>order.Value)
                .ToList().ForEach(order => Console.WriteLine(order));

            Console.WriteLine($"\nNr of orders< 1000kr: " +
                $"{OrderList.Where(order => order.Value < 1000).Count()}");

            Console.WriteLine($"Sum of Value of orders< 1000kr: " +
                 $"{OrderList.Where(order => order.Value < 1000).Sum(order => order.Value):C2}");

            Console.WriteLine($"Sum of Freight of orders< 1000kr: " +
                 $"{OrderList.Where(order => order.Value < 1000).Sum(order => order.Freight):C2}");

            
            Console.WriteLine("\nCountries in orderlist");
            var countryList = OrderList.Select(order => order.Country).Distinct();
            countryList.ToList().ForEach(country => Console.WriteLine(country));

            Console.WriteLine($"\nFinland order number: {OrderList.Where(order => order.Country == "Finland").Count()}");
            Console.WriteLine($"Finland order value total: {OrderList.Where(order => order.Country == "Finland").Sum(order => order.Value):C2}");
            
            Console.WriteLine("\nOrder count and value by Country");
            var orderbyCountry = OrderList.GroupBy(order => order.Country);
            foreach (IGrouping<string, IOrder> orderGroups in orderbyCountry)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine($"{orderGroups.Key} Count: {orderGroups.Count()} Value: {orderGroups.Sum(order => order.Value):C2}");
            }

            var nrOfOrders = OrderList.Where(predicate).Count();
            Console.WriteLine($"\nNr of orders with delivery later than 15 days after order date: {nrOfOrders}");

            Console.WriteLine("\nValue of 5 largest Orders by Country");
            orderbyCountry = OrderList.GroupBy(order => order.Country);
            foreach (IGrouping<string, IOrder> orderGroups in orderbyCountry)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine($"{orderGroups.Key} Value: {orderGroups.OrderByDescending(order => order.Value).Take(5).Sum(order => order.Value):C2}");
            }

            Console.WriteLine("\n5 largest Orders by Country");
            orderbyCountry = OrderList.GroupBy(order => order.Country);
            foreach (IGrouping<string, IOrder> orderGroups in orderbyCountry)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine($"\n{orderGroups.Key}");
                Console.WriteLine("----------------");

                orderGroups.OrderByDescending(order => order.Value)
                  .Take(5)
                  .Select(order => new Order(order))
                  .ToList().ForEach(order => Console.WriteLine(order));
            }

            var avgDeliverTime = OrderList.Average(order =>
            {
                var ts_days = order.DeliveryDate - order.OrderDate;
                int days = ts_days.Value.Days;
                return days;
            });
            Console.WriteLine($"Average delivery time {avgDeliverTime:N0} days");


            //Example that you can usa a local Method as a delegate instead of a Lambda Expression
            const int MaxNrOfDeliveryDays = 15;
            bool predicate (IOrder order)
            {
                var ts_days = order.DeliveryDate - order.OrderDate;
                int days = ts_days.Value.Days;
                return days > MaxNrOfDeliveryDays;
            };
        }
    }
}
