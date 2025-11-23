using System.Text.Json;
using System.Text.Json.Serialization;
using Kata10_Serialization;
using System.Xml.Serialization;

List<Order> OrderList = new List<Order>();
for (int i = 0; i < 50_000; i++)
{
    OrderList.Add(Order.Factory.CreateRandom());
}

Console.WriteLine($"Nr of orders: {OrderList.Count}");
Console.WriteLine($"Order value total: {OrderList.Sum(order => order.Value):C2}");

var largestOrders = OrderList.OrderByDescending(order => order.Value).Take(1000).ToList();

Console.WriteLine($"\nWrite the 1000 largest orders to: {fname("Kata10.json")}");
using (Stream s = File.Create(fname("Kata10.json")))
using (TextWriter writer = new StreamWriter(s))
    writer.Write(JsonSerializer.Serialize<List<Order>>(largestOrders, new JsonSerializerOptions() { WriteIndented = true }));

Console.WriteLine($"\nWrite the 1000 largest orders to: {fname("Kata10.xml")}");
var xs = new XmlSerializer(typeof(List<Order>));
using (Stream s = File.Create(fname("Kata10.xml")))
    xs.Serialize(s, largestOrders);


#region used in Kata to find proper file location
static string fname(string name)
{
    var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    documentPath = Path.Combine(documentPath, "ADOP", "Kata10");
    if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
    return Path.Combine(documentPath, name);
}
#endregion


