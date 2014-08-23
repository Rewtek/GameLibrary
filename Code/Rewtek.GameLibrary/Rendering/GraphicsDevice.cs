namespace Rewtek.GameLibrary.Rendering
{
    #region Using directives

    using global::System;
    using global::System.Drawing;
    using global::System.Threading;
    using global::System.Windows.Forms;

    using Microsoft.DirectX;
    using Microsoft.DirectX.Direct3D;

    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Game.Scenes;
    using Rewtek.GameLibrary.Game.Handlers;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.Rendering.Geometry;
    using Rewtek.GameLibrary.Rendering.Surfaces;

    using Font = Rewtek.GameLibrary.Rendering.Fonts.Font;

    #endregion

    public class GraphicsDevice : Component, IGameHandler, IRenderHandler
    {
        // Variables
        private Device _device;
        private PresentParameters _presentParameters;
        private VertexBuffer vb = null;
        private VertexBuffer vertexBuffer;

        // Properties
        public bool Initialized { get; set; }

        public Device Device { get { return _device; } }
        public GemoetryRenderer Gemoetry { get; private set; }
        public WindowSurface Window { get; private set; }
        public IntPtr WindowHandle 
        { 
            get { return Window.Handle; } 
        }
        public Viewport Viewport 
        {
            get
            {
                return new Viewport(Device.Viewport);
            }
        }

        // Constants
        public const string ERR_D3D_INITIALIZE = "D3D Graphics Device could not be initialized.\n";

        // Constructor
        public GraphicsDevice()
        {

        }

        // Methods
        #region Public Methods

        public void Initialize(WindowSurface window)
        {
            if (Initialized) return;
            
            Window = window;

            try
            {
                _presentParameters = new PresentParameters();
                _presentParameters.Windowed = true;
                _presentParameters.SwapEffect = SwapEffect.Discard;
                _presentParameters.BackBufferCount = 1;
                _presentParameters.BackBufferWidth = window.Width;
                _presentParameters.BackBufferHeight = window.Height;
                _presentParameters.BackBufferFormat = Format.X8R8G8B8;
                _presentParameters.DeviceWindowHandle = window.Handle;

                _device = new Device(Manager.Adapters.Default.Adapter,
                    DeviceType.Hardware,
                    Window.Handle,
                    CreateFlags.HardwareVertexProcessing,
                    _presentParameters);

                //_device.RenderState.AlphaBlendEnable = true;
                //_device.RenderState.IndexedVertexBlendEnable = true;
                //_device.RenderState.AlphaSourceBlend = Blend.SourceAlpha;
                //_device.RenderState.SourceBlend = Blend.SourceAlpha;
                //_device.RenderState.AlphaFunction = Compare.Always;
                //_device.RenderState.DestinationBlend = Blend.SourceAlpha;
                //_device.RenderState.AlphaFunction = BlendFunction.Add;

                _device.RenderState.AlphaBlendEnable = true;
                _device.RenderState.AlphaTestEnable = true;
                _device.RenderState.SourceBlend = Blend.SourceAlpha;
                _device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
                _device.RenderState.BlendOperation = BlendOperation.Add;
                _device.RenderState.AlphaSourceBlend = Blend.Zero;
                _device.RenderState.AlphaDestinationBlend = Blend.Zero;
                _device.RenderState.AlphaBlendOperation = BlendOperation.Add;

                _device.RenderState.AlphaFunction = Compare.Always;
                _device.RenderState.VertexBlend = VertexBlend.ZeroWeights;

                _device.SetRenderState(RenderStates.ZEnable, false);
                _device.SetRenderState(RenderStates.Lighting, false);
                _device.VertexFormat = CustomVertex.TransformedColored.Format;

                _device.DeviceLost += new EventHandler(OnDeviceLost);
                _device.DeviceReset += new EventHandler(OnResetDevice);
                
                Gemoetry = new GemoetryRenderer(this);

                Core.Components.Require<GameLoop>().Subscribe(this);
                
                Initialized = true;
            }
            catch (Direct3DXException ex)
            {
                MessageBox.Show(string.Format("{0}{1} (0x{2:X})", ERR_D3D_INITIALIZE, ex.ErrorString, ex.ErrorCode),
                    "GraphicsDevice::Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectXException ex)
            {
                MessageBox.Show(string.Format("{0}{1} (0x{2:X})", ERR_D3D_INITIALIZE, ex.ErrorString, ex.ErrorCode),
                    "GraphicsDevice::Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ERR_D3D_INITIALIZE + ex.Message,
                    "GraphicsDevice::Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Render()
        {
            if (_device == null || Window == null || Window.Handle == null) return;

            try
            {
                _device.Clear(ClearFlags.Target, Color.CornflowerBlue, 0.0f, 0);
                _device.BeginScene();

                Core.Components.Require<SceneManager>().Render();

                _device.EndScene();
                _device.Present();
            }
            catch (DirectXException ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "D3D Render Error: {0} (0x{1:X})", ex.ErrorString, ex.ErrorCode);
                MsgBox.Show(MsgBoxIcon.Error, "", ex.StackTrace);
            }
            catch (ThreadAbortException)
            {
                // DO NOTHING HERE
            }
            catch (Exception ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "D3D Render Error: {0}", ex.Message);
            }

            Application.DoEvents();
        }

        public void Shutdown()
        {
            if (_device.Disposed) return;

            Core.Components.Require<GameLoop>().Unsubscribe(this);
            Core.Components.Require<GameLoop>().Stop();

            _device.Dispose();
        }

        #endregion

        #region Event Handler

        public void OnDeviceLost(object sender, EventArgs e)
        {
            Font.Default.Dispose();

            while (Device.CheckCooperativeLevel())
            {
                Thread.Sleep(50);
            }

            try
            {
                Device.Reset(_presentParameters);
            }
            catch
            {
                
            }
        }

        public void OnResetDevice(object sender, EventArgs e)
        {
            // reset font
            Font.CreateDefaultFont();

            // Now create the vertex buffer
            //vertexBuffer = new VertexBuffer(
            //    typeof(CustomVertex.TransformedColored), 3, dev, 0,
            //    CustomVertex.TransformedColored.Format, Pool.Default);
            //vertexBuffer.Created +=
            //    new System.EventHandler(this.OnCreateVertexBuffer);
            //this.OnCreateVertexBuffer(vb, null);

            // register our event handlers for lost devices
            _device.DeviceLost += new EventHandler(OnDeviceLost);
            _device.DeviceReset += new EventHandler(OnResetDevice);
        }

        public void OnCreateVertexBuffer(object sender, EventArgs e)
        {
            VertexBuffer vb = (VertexBuffer)sender;
            GraphicsStream stm = vb.Lock(0, 0, 0);
            CustomVertex.TransformedColored[] verts =
                new CustomVertex.TransformedColored[3];

            verts[0].X = 150; verts[0].Y = 50; verts[0].Z = 0.5f; verts[0].Rhw = 1;
            verts[0].Color = System.Drawing.Color.Aqua.ToArgb();

            verts[1].X = 250; verts[1].Y = 250; verts[1].Z = 0.5f; verts[1].Rhw = 1;
            verts[1].Color = System.Drawing.Color.Brown.ToArgb();

            verts[2].X = 50; verts[2].Y = 250; verts[2].Z = 0.5f; verts[2].Rhw = 1;
            verts[2].Color = System.Drawing.Color.LightPink.ToArgb();

            stm.Write(verts);

            vb.Unlock();
        }

        #endregion
    }
}