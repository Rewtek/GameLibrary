namespace Rewtek.GameLibrary.Game.Scenes
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Game.Handlers;

    public class SceneManager : SceneContainer, ITickHandler
    {
        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether to present the changes to the control.
        /// </summary>
        public bool PresentChanges { get; set; }

        // Methods
        /// <summary>
        /// Adds the specified <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s.
        /// </summary>
        /// <param name="scenes">The <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s.</param>
        public void Add(params Scene[] scenes)
        {
            Add((IEnumerable<Scene>)scenes);
        }

        /// <summary>
        /// Adds the specified <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s.
        /// </summary>
        /// <param name="scenes">The <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>s.</param>
        public void Add(IEnumerable<Scene> scenes)
        {
            foreach (Scene scene in scenes)
            {
                base.Add(scene);
            }
        }
    }
}