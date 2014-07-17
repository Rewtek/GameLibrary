using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rewtek.GameLibrary.Game.Scenes
{
    public abstract class LoadingScene : Scene
    {
        // Properties
        /// <summary>
        /// Gets a value indicating whether loading the content is completed.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Gets the current element.
        /// </summary>
        public string CurrentElement { get; private set; }
        /// <summary>
        /// Gets the completed elements.
        /// </summary>
        public IList<string> CompletedElements { get; private set; }
        /// <summary>
        /// Gets the scene.
        /// </summary>
        public Scene Target { get; private set; }

        // Methods
        /// <summary>
        /// Called when the scene is loaded.
        /// </summary>
        protected virtual void OnCompleted()
        {
            TransitionTo(Target);
        }

        #region Overrides of SceneContainer

        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);

            if (Target.IsLoaded && !IsCompleted)
            {
                IsCompleted = true;
                OnCompleted();
            }
        }

        #endregion
    }
}
