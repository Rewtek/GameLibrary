namespace Rewtek.GameLibrary.UI
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Input;
    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Events;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    using Rectangle = Rewtek.GameLibrary.Math.Rectangle;

    public class Widget : IWidgetContainer, IDrawable
    {
        // Variables
        private int _width;
        private int _height;

        private bool _focused;
        private bool _gotFocus;
        private bool _enabled;
        private bool _dirty;
        private bool _visible;

        private bool _containsMouse;
        private bool _mouseDown;
        private bool _mouseUp;

        private Mouse _currentMouse;

        private MouseEventArgs _lastMouse;

        // Properties
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnEnabledChanged();
            }
        }        
        public bool Focusable { get; protected set; }
        public bool Focused
        {
            get { return _focused; }
            set
            {
                if (Focusable)
                {
                    _focused = value;
                    if (_focused)
                    {
                        _gotFocus = true;

                        //Container.SetFocus(this);
                        OnGotFocus();
                    }
                    else
                    {
                        OnLostFocus();
                    }
                }
            }
        }
        public bool Moveable { get; set; }
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                _enabled = _visible;

                OnVisibilityChanged();
            }
        }

        public float Opacity { get; set; }

        public object Tag { get; set; }
        public string Name { get; set; }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _dirty = true;

                OnBoundsChanged();
            }
        }
        public int Height
        {
            get { return this._height; }
            set
            {
                _height = value;
                _dirty = true;

                OnBoundsChanged();
            }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(ScreenPosition.X, ScreenPosition.Y, Width, Height); }
        }
        public Vector2 Position { get; set; }
        public Vector2 ScreenPosition
        {
            get { return Container.ScreenPosition + Position; }
        }
        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
            set
            {
                Width = (int)value.X;
                Height = (int)value.Y;
            }
        }

        public WidgetState State { get; set; }
        public List<Widget> Widgets { get; private set; }
        public IWidgetContainer Container { get; private set; }
        public WidgetContainer RootContainer
        {
            get
            {
                IWidgetContainer container = this.Container;
                while (!(container is WidgetContainer))
                {
                    Widget widget = container as Widget;
                    container = widget.Container;
                }

                return (WidgetContainer)container;
            }
        }
        public GraphicsDevice GraphicsDevice 
        { 
            get { return Container.GraphicsDevice; } 
        }

        // Events
        public event EventHandler Load;
        public event EventHandler Unload;
        public event EventHandler CreateWidget;

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;

        public event EventHandler EnabledChanged;
        public event EventHandler BoundsChanged;
        public event EventHandler VisibilityChanged;

        public event EventHandler Refresh;
        public event EventHandler Paint;

        public event EventHandler<MouseEventArgs> Click;
        public event EventHandler<MouseEventArgs> MouseEnter;
        public event EventHandler<MouseEventArgs> MouseLeave;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;

        // Constructor
        public Widget(IWidgetContainer container)
        {
            _focused = false;
            _enabled = true;

            _currentMouse = Core.Components.Require<Mouse>();
            _lastMouse = new MouseEventArgs(_currentMouse);

            Visible = true;
            Opacity = 1.0f;

            Tag = string.Empty;
            Name = GetType().Name + GetHashCode();

            Position = new Vector2(0, 0);

            Container = container;
            Container.Widgets.Add(this);
            Widgets = new List<Widget>();

            OnLoad();
        }

        // Methods
        #region Widget Implementation

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void ToggleVisibility()
        {
            Visible = !Visible;
        }

        public void CenterScreen()
        {
            Position = new Vector2(
                GraphicsDevice.Viewport.Width / 2 - Width / 2,
                GraphicsDevice.Viewport.Height / 2 - Height / 2);
        }

        public void CenterParent()
        {
            Position = new Vector2(
                Container.Width / 2 - Width / 2,
                Container.Height / 2 - Height / 2);
        }

        #endregion

        #region IWidgetContainer Implementation

        public void SetFocus(Widget widget)
        {
            Container.SetFocus(widget);
        }

        public void Next()
        {
            Container.Next();
        }

        public void Next(IWidgetContainer container)
        {
            Container.Next(container);
        }

        #endregion

        #region IGameHandler Implementation

        public void Update(float elapsed)
        {
            if (Enabled)
            {
                // create widgnet if dirty
                if (_dirty)
                {
                    OnCreateWidget();
                    _dirty = false;
                }

                // get current device info
                #region Update Mouse Events
                _currentMouse = Core.Components.Require<Mouse>();

                var mouseEvent = new MouseEventArgs(_currentMouse);
                var mouseIn = Bounds.Contains(new Vector2(_currentMouse.Position.X, _currentMouse.Position.Y));

                // mouse enter / leave
                if (!_containsMouse && mouseIn)
                {
                    OnMouseEnter(mouseEvent);
                }
                else if (_containsMouse && !mouseIn)
                {
                    OnMouseLeave(mouseEvent);

                    _mouseDown = false;
                    _mouseUp = false;
                }

                _containsMouse = mouseIn;

                if (_containsMouse && _currentMouse.PressedButton == MouseButton.Left)
                {
                    OnMouseDown(mouseEvent);
                    State = WidgetState.Clicked;
                }
                else if (_containsMouse && _currentMouse.PressedButton != MouseButton.Left)
                {
                    if (_lastMouse.PressedButton == MouseButton.Left)
                    {
                        OnClick(mouseEvent);
                        OnMouseUp(mouseEvent);
                    }
                    State = WidgetState.Hover;
                }
                else if (Focused)
                {
                    State = WidgetState.Focused;
                }
                else if (Enabled)
                {
                    State = WidgetState.Normal;
                }
                else
                {
                    State = WidgetState.Disabled;
                }

                if (_mouseDown && _mouseUp)
                {
                    //OnClick(mouseEvent);

                    Focused = true;

                    _mouseDown = false;
                    _mouseUp = false;
                }
                #endregion

                _gotFocus = false;
                _lastMouse = new MouseEventArgs(_currentMouse);

                Widgets.ForEach(widget => widget.Update(elapsed));
            }
            else
            {
                State = WidgetState.Disabled;
            }
            OnUpdate();
        }

        #endregion

        #region IDrawable Implementation

        public void Draw()
        {
            if (Visible)
            {
                OnPaint();
                Widgets.ForEach(widget => widget.Draw());
            }
        }

        #endregion

        #region Event Handler Implementation

        protected virtual void OnLoad()
        {
            Load.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnUnload()
        {
            Unload.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnCreateWidget()
        {
            CreateWidget.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnGotFocus()
        {
            GotFocus.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnLostFocus()
        {
            LostFocus.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnabledChanged()
        {
            EnabledChanged.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnBoundsChanged()
        {
            BoundsChanged.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnVisibilityChanged()
        {
            VisibilityChanged.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdate()
        {
            Refresh.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnPaint()
        {
            Paint.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseEnter(MouseEventArgs e)
        {
            MouseEnter.SafeInvoke(this, e);
        }

        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            MouseLeave.SafeInvoke(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown.SafeInvoke(this, e);
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove.SafeInvoke(this, e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp.SafeInvoke(this, e);
        }

        protected virtual void OnClick(MouseEventArgs e)
        {
            Focused = true;

            Click.SafeInvoke(this, e);
        }

        #endregion
    }
}
