using Digimezzo.Foundation.Core.Packaging;
using Dopamine.Core.Base;
using System;
using System.Reflection;

namespace Dopamine.Packager
{
    class Packager
    {
        static void Main(string[] args)
        {
            bool proceed = true;

#if DEBUG
            proceed = false;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The project was built in DEBUG. Do you want to proceed? [Y/N]");
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleKeyInfo info = Console.ReadKey();
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);

            if (info.Key == ConsoleKey.Y)
            {
                proceed = true;
               
            }   
#endif

            if (proceed)
            {
                var worker = new PackageCreator(Package.FromAssembly(Assembly.GetEntryAssembly(), ProductInformation.ApplicationName));
                worker.ExecuteAsync().Wait();
            }
        }
    }
}
