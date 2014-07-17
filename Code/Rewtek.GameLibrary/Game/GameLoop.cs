namespace Rewtek.GameLibrary.Game
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Diagnostics;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Game.Handlers;
    using Rewtek.GameLibrary.Components;

    public class GameLoop : Component
    {
        // Variables
        private readonly List<IGameHandler> _handlers;
        private readonly Thread _thread;
        private Stopwatch _gameTime;
        private double _renderTime;
        private double _tickTime;
        private int _fpsCount;
        private double _lastFpsUpdate;

        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rewtek.GameLibrary.Game.GameLoop"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public long FrameIndex { get; private set; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        public int FramesPerSecond { get; private set; }

        /// <summary>
        /// Gets the frame time (render time + tick time).
        /// </summary>
        public double FrameTime
        {
            get { return this._tickTime + this._renderTime; }
        }
        /// <summary>
        /// Gets the tick time.
        /// </summary>
        public double TickTime
        {
            get { return this._tickTime; }
        }
        /// <summary>
        /// Gets the render time.
        /// </summary>
        public double RenderTime
        {
            get { return this._renderTime; }
        }

        /// <summary>
        /// Gets the tick handlers.
        /// </summary>
        public IEnumerable<ITickHandler> TickHandlers
        {
            get
            {
                return this._handlers.OfType<ITickHandler>().OrderBy(handler =>
                {
                    var sortable = handler as ISortableTickHandler;
                    if (sortable != null)
                    {
                        return sortable.TickIndex;
                    }

                    return 0;
                });
            }
        }
        /// <summary>
        /// Gets the render handlers.
        /// </summary>
        public IEnumerable<IRenderHandler> RenderHandlers
        {
            get
            {
                return this._handlers.OfType<IRenderHandler>().OrderBy(handler =>
                {
                    var sortable = handler as ISortableRenderHandler;
                    if (sortable != null)
                    {
                        return sortable.RenderIndex;
                    }

                    return 0;
                });
            }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Game.GameLoop"/> class.
        /// </summary>
        public GameLoop()
        {
            _handlers = new List<IGameHandler>();
            _thread = new Thread(InternalLoop)
            {
                IsBackground = true,
                Name = "<Hatwell::GameLoop>"
            };
        }

        // Methods
        /// <summary>
        /// Runs the game loop and creates a parallel thread.
        /// </summary>
        public void Start()
        {
            if (Active) return;

            Active = true;

            _thread.Start();

            Logger.Log("GameLoop has been started");
        }

        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            if (!Active) return;

            Active = false;

            _thread.Abort();

            Logger.Log("GameLoop has been stopped");
        }

        /// <summary>
        /// Subscribes and adds the handler to the gameloop.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Subscribe(IGameHandler handler)
        {
            Logger.Log("Subscribed {0} to the game loop", handler.GetType().Name);
            _handlers.Add(handler);
        }

        /// <summary>
        /// Unsubscribes the gameloop and removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Unsubscribe(IGameHandler handler)
        {
            Logger.Log("Removed {0} from game loop", handler.GetType().Name);
            _handlers.Remove(handler);
        }

        /// <summary>
        /// A function representing the internal game loop logic.
        /// </summary>
        private void InternalLoop()
        {
            try
            {
                _gameTime = Stopwatch.StartNew();

                while (true)
                {
                    //We use the SpinWait to wait for the next requested tick.
                    //It's not totally correct, but we save a LOT of CPU power.
                    //To compensate the incorrectness we introduced our SpinWaitTolerance constant.
                    //SpinWait.SpinUntil(IsTickRequested);

                    //CheckRenderRequest();
                    UpdateFramesPerSecond();

                    //HandleRenderRequest();
                    //HandleUnprocessedTicks();

                    OnTick(0);
                    OnRender(0);
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, "GameLoop::InternalLoop", "Unexpected exception in game loop: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified time has elapsed.
        /// </summary>
        /// <param name="pointInTime">The point in time.</param>
        /// <param name="elapsed">The elapsed.</param>
        private bool IsElapsed(double pointInTime, double elapsed)
        {
            return _gameTime.Elapsed.TotalMilliseconds - pointInTime >= elapsed;
        }

        /// <summary>
        /// Updates the timing properties for FramesPerSecond.
        /// </summary>
        private void UpdateFramesPerSecond()
        {
            if (IsElapsed(_lastFpsUpdate, 1000.0))
            {
                FramesPerSecond = _fpsCount;

                _lastFpsUpdate = _gameTime.Elapsed.TotalMilliseconds;
                _fpsCount = 0;
            }
            FramesPerSecond = (int)FrameIndex;
        }

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnTick(float elapsed)
        {
            Stopwatch tickWatch = Stopwatch.StartNew();

            //Increment the frame index, since a frame has passed. A
            //frame can only pass inside a tick call, since render isn't called
            //as frequent as tick
            FrameIndex++;

            foreach (ITickHandler gameHandler in TickHandlers)
            {
                gameHandler.Tick(elapsed);
            }

            tickWatch.Stop();
            _tickTime = tickWatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnRender(float elapsed)
        {
            Stopwatch renderWatch = Stopwatch.StartNew();

            foreach (IRenderHandler gameHandler in RenderHandlers)
            {
                gameHandler.Render();
            }

            renderWatch.Stop();
            _renderTime = renderWatch.Elapsed.TotalMilliseconds;
        }
    }
}
