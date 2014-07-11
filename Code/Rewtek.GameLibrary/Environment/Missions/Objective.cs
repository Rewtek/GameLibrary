namespace Rewtek.GameLibrary.Environment.Missions
{
    /// <summary>
    /// Provides a class for mission objectives.
    /// </summary>
    public class Objective
    {
        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Rewtek.GameLibrary.Environment.Missions.Objective"/> has been accomplished.
        /// </summary>
        public bool Accomplished { get; set; }
        /// <summary>
        /// Gets the name of the <see cref="Rewtek.GameLibrary.Environment.Missions.Objective"/>.
        /// </summary>
        public string Name { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Missions.Objective"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Objective(string name)
            : this(name, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Missions.Objective"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        public Objective(string name, bool state)
        {
            Name = name;
            Accomplished = state;
        }

        // Methods
        #region Override Members

        public override string ToString()
        {
            return string.Format("{{ {0}, {1} }}", Name, Accomplished ? "Accomplished" : "Not Accomplished");
        }

        #endregion
    }
}
