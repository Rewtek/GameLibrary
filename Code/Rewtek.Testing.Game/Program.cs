namespace Rewtek.Testing.Game
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary;
    using Rewtek.GameLibrary.Achivements;
    using Rewtek.GameLibrary.Common;
    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Environment;
    using Rewtek.GameLibrary.Environment.Entities;
    using Rewtek.GameLibrary.Environment.Items;
    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Game.Scenes;
    using Rewtek.GameLibrary.Input;
    using Rewtek.GameLibrary.Localization;
    using Rewtek.GameLibrary.Logging;
    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Surfaces;

    using Rewtek.Testing.Game.Scenes;

    public static class Program
    {
        [STAThread]
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
            Core.Components.Install(new LocalizationManager());
            Core.Components.Install(new WindowSurface(800, 600, "Rewtek Graphics Test"));
            Core.Components.Install(new SceneManager());
            Core.Components.Install(new GraphicsDevice());
            Core.Components.Install(new Mouse());

            // Localize and set default langauge
            Logger.Log("Identified {0} ({1}) as language", SystemHelper.CultureNativeName, SystemHelper.CultureName);
            Core.Components.Require<LocalizationManager>().LoadLanguages("Lang");
            Core.Components.Require<LocalizationManager>().ChangeLanguage(SystemHelper.CultureName);

            // Manage achievments
            var manager = Core.Components.Require<AchievementManager>();
            manager.Add(new Achievement("ACHV_TEST"));
            manager.Add(new Achievement("ACHV_RUFFY"));
            
            manager.Unlock("ACHV_TEST");

            // Initialize scene manager
            Core.Components.Require<SceneManager>().Initialize();
            Core.Components.Require<SceneManager>().Add(new TestScene());

            Core.Run();
            Core.Destroy();
        }
    }
}
