using System;

namespace UniversityApp;

internal static class Program
{
    static void Main(string[] args)
    {
        var storage = new Storage.FileDatabase(Path.Combine(AppContext.BaseDirectory, "database.txt"));
        var repo = new Storage.Repository(storage);
        var menu = new UI.ConsoleMenu(repo);
        menu.Run();
    }
}