namespace Rewtek.GameLibrary.Environment.Missions
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Timers;
    using global::System.Collections.Generic;

    #endregion

    /// <summary>
    /// Provides a class for mission management.
    /// </summary>
    public class Mission
    {
        // Variables
        private readonly List<Objective> _objectives;
        private readonly Timer _timer;
        private bool _disposed;

        // Properties
        /// <summary>
        /// Gets a value indicating the total number of all objectives.
        /// </summary>
        public int ObjectiveNum { get { return _objectives.Count; } }
        /// <summary>
        /// Gets a value indicating the total number of all accomplished objectives.
        /// </summary>
        public int ObjectivesAccomplishedNum { get { return _objectives.FindAll(obj => obj.Accomplished).Count; } }
        /// <summary>
        /// Gets a value indicating the total number of all not accomplished objectives.
        /// </summary>
        public int ObjectivesNotAccomplishedNum { get { return _objectives.FindAll(obj => !obj.Accomplished).Count; } }

        /// <summary>
        /// Gets the mission state.
        /// </summary>
        public MissionState State { get; set; }

        // Events
        public event EventHandler MissionTick;
        public event EventHandler MissionCompleted;
        public event EventHandler<ObjectiveChangedEventArgs> ObjectiveStateChanged;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Missions.Mission"/> class.
        /// </summary>
        public Mission()
        {
            _objectives = new List<Objective>();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += new ElapsedEventHandler(Tick);

            State = MissionState.MISSION_NONE;
        }

        // Deconstructor
        ~Mission()
        {
            Dispose(false);
        }

        // Methods
        public void AddObjective(string name, bool state = false)
        {
            _objectives.Add(new Objective(name, state));
        }

        public bool HasObjective(string name)
        {
            return _objectives.Find(obj => obj.Name == name) == null ? false : true;
        }

        public bool IsObjectiveAccomplished(string name)
        {
            return _objectives.Find(obj => obj.Name == name).Accomplished;
        }

        public void SetObjectiveAccomplished(string name, bool state)
        {
            var objective = _objectives.Find(obj => obj.Name == name);
            if (objective != null)
            {
                objective.Accomplished = state;
                if (ObjectiveStateChanged != null) ObjectiveStateChanged.Invoke(this, new ObjectiveChangedEventArgs(objective));
            }
        }

        public Objective[] GetObjectives()
        {
            return _objectives.ToArray();
        }

        public void RemoveObjective(string name)
        {
            _objectives.Remove(_objectives.Find(obj => obj.Name == name));
        }

        #region Timer Tick

        public void Tick(object sender, ElapsedEventArgs e)
        {
            if (MissionTick != null) MissionTick.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Logger.Log("Mission::Dispose({0})", disposing);

            // Dispose managed resources.
            if (disposing)
            {
                _timer.Stop();
                _timer.Dispose();
                _objectives.Clear();
            }

            // Dispose unmanaged resources.

            _disposed = true;
        }

        #endregion
    }
}
