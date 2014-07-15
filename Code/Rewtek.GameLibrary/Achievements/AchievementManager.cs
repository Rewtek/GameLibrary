namespace Rewtek.GameLibrary.Achivements
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    using Rewtek.GameLibrary.Components;

    #endregion

    /// <summary>
    /// Provides a management for achivements.
    /// </summary>
    public class AchievementManager : Component
    {
        // Variables
        private readonly List<Achievement> _achievments;

        // Properties
        /// <summary>
        /// Gets the number of <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>s actually registered.
        /// </summary>
        public int AchivementNum { get { return _achievments.Count; } }
        /// <summary>
        /// Gets the number of unlocked <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>s actually registered.
        /// </summary>
        public int AchivementUnlokedNum { get { return _achievments.FindAll(achv => achv.Unlocked).Count; } }
        /// <summary>
        /// Gets the number of not unlocked <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>s actually registered.
        /// </summary>
        public int AchivementNotUnlokedNum { get { return _achievments.FindAll(achv => !achv.Unlocked).Count; } }
        /// <summary>
        /// Gets the <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> at the specific index.
        /// </summary>
        /// <param name="achievement">The name of the <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>.</param>
        /// <returns>The <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> at the specific index.</returns>
        public Achievement this[string achievement] 
        { 
            get { return _achievments.First(first => first.Name == achievement); } 
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Achivements.AchievementManager"/> class.
        /// </summary>
        public AchievementManager()
        {
            _achievments = new List<Achievement>();
        }

        // Methods
        /// <summary>
        /// Adds an <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>.
        /// </summary>
        /// <param name="achievement">The <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> to be added.</param>
        public void Add(Achievement achievement)
        {
            if (!Contains(achievement.Name))
            {
                _achievments.Add(achievement);
                Logger.Log("Achievement {0} has been registered", achievement.Name);
            }
            else
            {
                Logger.Log("Achievement {0} has already been registered", achievement.Name);
            }
        }

        /// <summary>
        /// Removes an <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>.
        /// </summary>
        /// <param name="achievement">The <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> to be removed.</param>
        public void Remove(Achievement achievement)
        {
            _achievments.RemoveAll(match => match.Name == achievement.Name);
            Logger.Log("Achievement {0} has been unregistered", achievement.Name);
        }

        /// <summary>
        /// Determines whether an <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> is registered.
        /// </summary>
        /// <param name="achievement">The <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> to locate.</param>
        public bool Contains(Achievement achievement)
        {
            return _achievments.Contains(achievement);
        }

        /// <summary>
        /// Determines whether an <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> with the specified name is registered.
        /// </summary>
        /// <param name="achievement">The <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> to locate.</param>
        public bool Contains(string achievement)
        {
            return _achievments.Any(any => any.Name == achievement);
        }

        /// <summary>
        /// Unlocks an <see cref="Rewtek.GameLibrary.Achivements.Achievement"/>.
        /// </summary>
        /// <param name="achievement">The name of the <see cref="Rewtek.GameLibrary.Achivements.Achievement"/> to unlock.</param>
        public void Unlock(string achievement)
        {
            var achv = this[achievement];
            if (achv != null)
            {
                achv.Unlocked = true;
                achv.OnUnlock(this, EventArgs.Empty);
            }
        }
    }
}
