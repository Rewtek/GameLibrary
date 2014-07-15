namespace Rewtek.GameLibrary.Achivements
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    #endregion

    /// <summary>
    /// Provides an achivement.
    /// </summary>
    public class Achievement
    {
        // Variables

        // Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Rewtek.GameLibrary.Achivements.Achivement"/> has been unlocked.
        /// </summary>
        public bool Unlocked { get; set; }

        // Events
        public event EventHandler Unlock;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> class.
        /// <param name="name">The name of the <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>.</param>
        /// </summary>
        public Achievement(string name)
        {
            Name = name;
        }

        // Methods
        public virtual void OnUnlock(object sender, EventArgs e)
        {
            Unlock.SafeInvoke(this, EventArgs.Empty);
            Logger.Log("Achievement {0} has been unlocked", Name);
        }
    }
}
