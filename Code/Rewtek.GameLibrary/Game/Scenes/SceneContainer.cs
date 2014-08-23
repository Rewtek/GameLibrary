namespace Rewtek.GameLibrary.Game.Scenes
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Input;
    using Rewtek.GameLibrary.Rendering;

    public abstract class SceneContainer : Component
    {
        // Properties
        /// <summary>
        /// Gets the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s.
        /// </summary>
        protected List<Scene> Scenes { get; private set; }
        /// <summary>
        /// Gets the <see cref="Rewtek.GameLibrary.Rendering.GraphicsDevice"/>.
        /// </summary>
        protected GraphicsDevice GraphicsDevice
        {
            get { return Core.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the <see cref="Rewtek.GameLibrary.Input.Mouse"/>.
        /// </summary>
        protected Mouse Mouse
        {
            get { return Core.Components.Require<Mouse>(); }
        }

        // Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Game.Scenes.SceneContainer"/> class.
        /// </summary>
        protected SceneContainer()
        {
            Scenes = new List<Scene>();
        }

        // Methods
        #region Implementation of SceneContainer

        /// <summary>
        /// Adds the specified <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        /// <param name="scene">The <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.</param>
        public void Add(Scene scene)
        {
            scene.Parent = this;
            Logger.Log("Adding scene {0}.", scene.GetType().Name);

            scene.OnEnter();

            Scenes.Add(scene);
        }

        /// <summary>
        /// Removes the specified <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        /// <param name="scene">The <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.</param>
        public void Remove(Scene scene)
        {
            scene.Parent = null;
            Logger.Log("Removing scene {0}.", scene.GetType().Name);

            scene.OnLeave();

            Scenes.Remove(scene);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Orders the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s for game tick processing.
        /// </summary>
        private IEnumerable<Scene> OrderedTickScenes()
        {
            return Scenes.Where(scene => !scene.IsPaused)
                         .OrderBy(scene => scene.TickIndex);
        }

        /// <summary>
        /// Orders the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s for the rendering process.
        /// </summary>
        private IEnumerable<Scene> OrderedRenderScenes()
        {
            return Scenes.Where(scene => scene.IsLoaded && scene.IsVisible)
                         .OrderBy(scene => scene.RenderIndex);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        public T GetScene<T>() where T : Scene
        {
            return (T)GetScene(scene => scene is T);
        }

        /// <summary>
        /// Gets a <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="System.Predicate"/>.</param>
        public Scene GetScene(Predicate<Scene> predicate)
        {
            return this.Scenes.FirstOrDefault(scene => predicate(scene));
        }

        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            //using (Scenes.Protect())
            {
                foreach (Scene scene in OrderedTickScenes())
                {
                    scene.LoadContentIfNeeded();
                    if (scene.IsLoaded)
                    {
                        scene.Tick(elapsed);
                    }
                }
            }
        }

        /// <summary>
        /// Handles a game render.
        /// </summary>
        public virtual void Render()
        {
            //using (Scenes.Protect())
            {
                foreach (Scene scene in OrderedRenderScenes())
                {
                    scene.Render();
                }
            }
        }

        #endregion
    }
}
