using System;
using DataAccess;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataRepository repo = new FileDataRepository();
            var menu = new ConsoleMenu(repo);
            menu.Run();
        }
    }
}
