namespace Rewtek.TestGame
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary;
    using Rewtek.GameLibrary.Achivements;
    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Environment;
    using Rewtek.GameLibrary.Environment.Entities;
    using Rewtek.GameLibrary.Environment.Items;
    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Common;
    using Rewtek.GameLibrary.Logging;

    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Rewtek Test Game";

            Console.WriteLine("Rewtek Game Library [Version: {0}, Build: {1}]", Core.Version, Core.BuildDate);
            Console.WriteLine("Copyright (c) 2014 Rewtek Network. All rights reserved.");
            Console.WriteLine();

            Core.Initialize();

            //ResourceSystem.CheckFile("Data\\ItemList.xml");

            Core.Components.Install(new ItemManager());
            Core.Components.Install(new TestComponent());
            Core.Components.Install(new RuffyComponent());
            Core.Components.Install(new AchievementManager());
            
            var manager = Core.Components.Require<AchievementManager>();
            manager.Add(new Achievement("ACHV_TEST"));
            manager.Add(new Achievement("ACHV_RUFFY"));
            
            manager.Unlock("ACHV_TEST");
            
            //Core.Run();

            Console.ReadLine();
        }
    }
}
