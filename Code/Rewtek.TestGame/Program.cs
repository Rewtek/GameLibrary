using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rewtek.GameLibrary;

namespace Rewtek.TestGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Rewtek Test Game";

            Console.WriteLine("Rewtek Game Library [Version: {0}, Build: {1}]", Global.Version, Global.BuildDate);
            Console.WriteLine("Copyright (c) Rewtek Network. All rights reserved.");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
