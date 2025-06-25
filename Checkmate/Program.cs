using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using jsonreadwrite.Models;

static string Get()
{
    string jsonPath = @"Data\lists.json";
    string content = File.ReadAllText(jsonPath);

    return content;
}

static void Post(Tasklist newlist)
{
    if (string.IsNullOrWhiteSpace(newlist.Name) ||
        string.IsNullOrWhiteSpace(newlist.Color) ||
        string.IsNullOrWhiteSpace(newlist.Date))
    {
        Console.WriteLine("Invalid task — missing required fields.");
        return;
    }

    string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\lists.json");
    jsonPath = Path.GetFullPath(jsonPath);
    string content = File.ReadAllText(jsonPath);

    TasklistList tasklistList = JsonSerializer.Deserialize<TasklistList>(content) ?? new TasklistList { Lists = new() };
    int maxid = tasklistList.Lists.Any() ? tasklistList.Lists.Max(x => x.Id) : 0;

    newlist.Id = ++maxid;

    tasklistList.Lists.Add(newlist);
    string jsonstring = JsonSerializer.Serialize(tasklistList, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(jsonPath, jsonstring);

    Console.WriteLine("Data added successfully");
}

static void TypeLine(string line, int delay = 10)
{
    foreach (char c in line)
    {
        Console.Write(c);
        Thread.Sleep(1);
    }
    Console.WriteLine();
}


static void intro()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    string[] lines = new[]
    {
        @"   ______  __    __   _______   ______  __  ___ .___  ___.      ___   .___________. _______",
        @"  /      ||  |  |  | |   ____| /      ||  |/  / |   \/   |     /   \  |           ||   ____|",
        @" |  ,----'|  |__|  | |  |__   |  ,----'|  '  /  |  \  /  |    /  ^  \ `---|  |----`|  |__",
        @" |  |     |   __   | |   __|  |  |     |    <   |  |\/|  |   /  /_\  \    |  |     |   __|",
        @" |  `----.|  |  |  | |  |____ |  `----.|  .  \  |  |  |  |  /  _____  \   |  |     |  |____",
        @"  \______||__|  |__| |_______| \______||__|\__\ |__|  |__| /__/     \__\  |__|     |_______|",
        @"",
        @"                               Checkmate List Manager v1.0"
    };
    foreach (string line in lines) {
        TypeLine(line, 2);
    }
    Console.ResetColor();
}

static void listy()
{
    Console.WriteLine("Choose one of your lists to view:");
}

static void Main()
{
    intro();
    listy();
    Tasklist tasklist = new Tasklist()
    {
        Name = "Grocery",
        Color = "Green",
        Date = "2025-06-24"
    };
    //Post(tasklist);
    //Console.WriteLine(Get());
    Console.ReadKey();
}

Main();