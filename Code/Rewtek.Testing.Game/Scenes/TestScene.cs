namespace Rewtek.Testing.Game.Scenes
{
    using Rewtek.GameLibrary;
    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Game.Scenes;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    public class TestScene : Scene
    {
        // Properties
        public WidgetContainer Container { get; set; }
        public Font LargeFont { get; set; }
        public Font MediumFont { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }

        // Constructor
        public TestScene()
        {
            
        }

        // Methods
        #region Overrides of Scene

        public override void LoadContent()
        {
            IsLoading = true;
            
            Container = new WidgetContainer();

            Container.Widgets.Add(new Label(Container));
            var label = (Label)Container.Widgets[0];
            label.AutoSize = false;
            label.TextAlignment = TextAlignment.Center;
            label.Position = new GameLibrary.Math.Vector2(5, 200);
            label.Size = new GameLibrary.Math.Vector2(100, 25);
            label.BackgroundColor = System.Drawing.Color.FromArgb(50, 0,0,0);

            Container.Widgets.Add(new Box(Container));
            var box = (Box)Container.Widgets[2];
            box.Position = new GameLibrary.Math.Vector2(5, 240);
            box.Size = new GameLibrary.Math.Vector2(400, 100);
            box.MouseEnter += (sender, e) => { box.BackgroundColor = System.Drawing.Color.DimGray; };
            box.MouseLeave += (sender, e) => { box.BackgroundColor = System.Drawing.Color.Black; };
            box.MouseDown += (sender, e) => { box.BackgroundColor = System.Drawing.Color.LightGray; };
            box.MouseUp += (sender, e) => { box.BackgroundColor = System.Drawing.Color.DimGray; };

            Container.Widgets.Add(new Box(Container));
            var boxx = (Box)Container.Widgets[4];
            boxx.Position = new GameLibrary.Math.Vector2(5, 150);
            boxx.Size = new GameLibrary.Math.Vector2(40, 40);
            boxx.Click += (sender, e) => { box.ToggleVisibility(); };

            Container.Widgets.Add(new Label(Container));
            var labell = (Label)Container.Widgets[6];
            labell.AutoSize = false;
            labell.Text = "Exit";
            labell.TextAlignment = TextAlignment.Center;
            labell.Position = new GameLibrary.Math.Vector2(200, 200);
            labell.Size = new GameLibrary.Math.Vector2(100, 25);
            labell.BackgroundColor = System.Drawing.Color.FromArgb(50, 0, 0, 0);
            labell.MouseEnter += (sender, e) => { labell.BackgroundColor = System.Drawing.Color.FromArgb(75, 0, 0, 0); };
            labell.MouseLeave += (sender, e) => { labell.BackgroundColor = System.Drawing.Color.FromArgb(50, 0, 0, 0); };
            labell.Click += (sender, e) => { GraphicsDevice.Window.Close(); };

            Container.Widgets.Add(new ContextMenu(Container));
            var contextMenu = (ContextMenu)Container.Widgets[8];
            contextMenu.Position = new GameLibrary.Math.Vector2(400, 400);
            contextMenu.Size = new GameLibrary.Math.Vector2(120, 20);

            LargeFont = new Font("Verdana", 24);
            MediumFont = new Font("Verdana", 18);

            PosX = 1000;
            PosY = 5;

            IsLoading = false;
            IsLoaded = true;
        }

        public override void OnLeave()
        {
            LargeFont.Dispose();
            MediumFont.Dispose();
        }

        #endregion

        #region Overrides of SceneContainer

        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            PosX -= 1.5f;
            if (PosX < -500) PosX = 1000;

            ((Label)Container.Widgets[0]).Text = Container.Widgets[2].State.ToString();

            Container.Tick(elapsed);
            base.Tick(elapsed);
        }

        /// <summary>
        /// Handles a game render.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render()
        {
            Font.Default.Draw("Test Sequence (테스트) [" + GraphicsDevice.Viewport.Width + "x" + GraphicsDevice.Viewport.Height + "]", 10, 10);
            Font.Default.Draw("GameLibrary: " + Core.Version, 10, 30);
            Font.Default.Draw("Frame: " + Core.Components.Require<GameLoop>().FrameIndex, 10, 50);
            Font.Default.Draw("Frame-Rate: " + Core.Components.Require<GameLoop>().FramesPerSecond, 10, 70);
            Font.Default.Draw("Mouse: " + Mouse.Position, 10, 90);
            Font.Default.Draw("Mouse-Button: " + Mouse.PressedButton, 10, 110);

            LargeFont.Draw("Super-Hero : ᕙ(ˉ˛ˉˋ)ᕗ", 200, 250);
            MediumFont.Draw("... just kidding シ", 210, 290);
            
            //GraphicsDevice.Gemoetry.DrawRectangle(PosX, PosY, 360, 20);
            //Font.Default.Draw("NOTICE  You! Yes you! You are very, very cure! Trust me! シ", (int)(PosX + 10), (int)(PosY + 3));

            Container.Render();
            base.Render();
        }

        #endregion
    }
}
