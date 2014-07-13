namespace Rewtek.GameLibrary.Environment.Missions
{
    public class ObjectiveChangedEventArgs : System.EventArgs
    {
        // Properties
        public Objective Objective { get; private set; }
        public bool Accomplished { get { return Objective.Accomplished; } }
        public string Name { get { return Objective.Name; } }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Missions.ObjectiveChangedEventArgs"/> class.
        /// </summary>
        /// <param name="objective">The objective.</param>
        public ObjectiveChangedEventArgs(Objective objective)
        {
            Objective = objective;
        }
    }
}
