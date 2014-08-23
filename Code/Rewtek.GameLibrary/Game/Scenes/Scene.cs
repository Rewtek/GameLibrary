namespace Rewtek.GameLibrary.Game.Scenes
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Scene : SceneContainer, IEnumerable<Scene>
    {
        // Properties
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public SceneContainer Parent { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> is loaded.
        /// </summary>
        public bool IsLoaded { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> is loading.
        /// </summary>
        public bool IsLoading { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> is paused.
        /// </summary>
        public bool IsPaused { get; set; }
        /// <summary>
        /// Gets a value indicating the index during a game tick.
        /// </summary>
        public virtual int TickIndex
        {
            get { return 0; }
        }
        /// <summary>
        /// Gets a value indicating the index during the rendering process.
        /// </summary>
        public virtual int RenderIndex
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        protected SceneManager SceneManager
        {
            //get { return XGL.Components.Get<SceneManager>(); }
            get { throw new NotImplementedException(); }
        }

        // Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> class.
        /// </summary>
        protected Scene()
        {
            IsVisible = true;
            IsPaused = false;
        }

        // Methods
        ///// <summary>
        ///// Transitions the scene to the specified one.
        ///// </summary>
        ///// <typeparam name="TTransition">The type of the transition.</typeparam>
        ///// <param name="scene">The scene.</param>
        //protected void TransitionTo<TTransition>(Scene scene) where TTransition : ITransition, new()
        //{
        //    TransitionTo(scene, new TTransition());
        //}

        ///// <summary>
        ///// Transitions the scene to the specified one.
        ///// </summary>
        ///// <param name="scene">The scene.</param>
        ///// <param name="transition">The transition.</param>
        //protected void TransitionTo(Scene scene, ITransition transition)
        //{
        //    TransitionTo(new TransitionScene(transition, this, scene));
        //}

        /// <summary>
        /// Transitions the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> to the specified one.
        /// </summary>
        /// <param name="scene">The <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/>.</param>
        protected void TransitionTo(Scene scene)
        {
            SceneManager.Add(scene);
            Remove();
        }

        /// <summary>
        /// Brings the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> to the front.
        /// </summary>
        public void BringToFront()
        {
            SceneManager.Remove(this);
            SceneManager.Add(this);
        }

        /// <summary>
        /// Removes this scene.
        /// </summary>
        public void Remove()
        {
            if (Parent != null)
            {
                Parent.Remove(this);
            }
        }

        /// <summary>
        /// Loads the content if needed.
        /// </summary>
        public void LoadContentIfNeeded()
        {
            if (IsLoaded || IsLoading) return;

            LoadContent();
        }

        /// <summary>
        /// Loads the content including textures, brushes, fonts, pens etc.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public virtual void LoadContent()
        {
            IsLoaded = true;
        }

        /// <summary>
        /// Called when the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> was added to a parent.
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// Called when the <see cref="Rewtek.GameLibrary.Game.Scenes.Scene"/> gets removed from it's parent.
        /// </summary>
        public virtual void OnLeave()
        {
        }

        #region IEnumerable<Scene> Member

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Scene> GetEnumerator()
        {
            return Scenes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
