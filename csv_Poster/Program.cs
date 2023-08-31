using ESG_CSVreader;
using System.Net.Http.Json;

string[] cmd_args = Environment.GetCommandLineArgs();

string csv_dir = "";
string endpoint = "";

try
{
    for (int i = 0; i < cmd_args.Length; i++)
    {
        if (cmd_args[i] == "--csv")
        {
            csv_dir = cmd_args[i + 1];
        }
        else if (cmd_args[i] == "--endpoint")
        {
            endpoint = cmd_args[i + 1];
        }
    }
}
catch
{
    Console.WriteLine("Could not set command line variables\nCommand line arguments:");
    foreach (string arg in cmd_args) { Console.WriteLine(arg); }
}

List<Customer> customers = new List<Customer>();

if (!File.Exists(csv_dir))
{
    Console.WriteLine("Could not find " + csv_dir);
    return;
}

using (var reader = new StreamReader(csv_dir))
{
    string? line;
    string[] fields;
    while (!reader.EndOfStream)
    {
        line = reader.ReadLine();
        if (line == null)
        {
            Console.WriteLine("Failed to read csv line @ byte position: " + reader.BaseStream.Position);
            continue;
        }

        fields = line.Split(',');
        if (fields.Length != 8)
        {
            Console.WriteLine("Misformatted csv @ byte position: " + reader.BaseStream.Position);
            continue;
        }

        customers.Add(new Customer(
            fields[0], 
            fields[1],
            fields[2],
            fields[3],
            fields[4],
            fields[5],
            fields[6],
            fields[7]
            ));
    }
}

if (customers.Count == 0) {
    Console.WriteLine("No customers read; no customers sent.");
    return;
}

using (var client = new HttpClient())
{
    HttpResponseMessage? response = null;
    try
    {
        response = await client.PostAsJsonAsync(endpoint, customers);
    }
    catch (HttpRequestException)
    {
        Console.WriteLine("Failed to post customers: Endpoint " + endpoint + " not available");
        return;
    }

    if (!(response?.IsSuccessStatusCode ?? false))
    {
        Console.WriteLine("Failed to post customers: Response code " + response?.StatusCode);
    }
    else
    {
        var writtenCustomers = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
        var count = writtenCustomers?.Count() ?? 0;
        Console.WriteLine("Successfully posted " + count + " customers with response code " + response.StatusCode);
    }
}