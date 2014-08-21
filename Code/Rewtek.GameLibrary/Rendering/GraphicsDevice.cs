namespace Rewtek.GameLibrary.Rendering
{
    using global::System;
    using global::System.Drawing;
    using global::System.Windows.Forms;

    using Microsoft.DirectX;
    using Microsoft.DirectX.Direct3D;

    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Game.Handlers;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.Rendering.Surfaces;

    using Font = Rewtek.GameLibrary.Rendering.Fonts.Font;

    public class GraphicsDevice : Component, IGameHandler, IRenderHandler
    {
        // Variables
        private Device _device;

        // Properties
        public bool Initialized { get; set; }

        public Device Device { get { return _device; } }
        public WindowSurface Window { get; private set; }
        public IntPtr WindowHandle { get { return Window.Handle; } }

        // Constants
        public const string ERR_D3D_INITIALIZE = "D3D Graphics Device could not be initialized.\n";

        // Constructor
        public GraphicsDevice()
        {

        }

        // Methods
        public void Initialize(WindowSurface window)
        {
            if (Initialized) return;
            
            Window = window;

            try
            {
                var presentParameters = new PresentParameters();
                presentParameters.Windowed = true;
                presentParameters.SwapEffect = SwapEffect.Discard;
                presentParameters.BackBufferCount = 1;
                presentParameters.BackBufferWidth = window.Width;
                presentParameters.BackBufferHeight = window.Height;
                presentParameters.BackBufferFormat = Format.X8R8G8B8;
                presentParameters.DeviceWindowHandle = window.Handle;

                _device = new Device(Manager.Adapters.Default.Adapter,
                    DeviceType.Hardware,
                    Window.Handle,
                    CreateFlags.HardwareVertexProcessing,
                    presentParameters);
                _device.SetRenderState(RenderStates.ZEnable, false);
                _device.SetRenderState(RenderStates.Lighting, false);
                _device.VertexFormat = CustomVertex.TransformedColored.Format;

                _device.DeviceReset += new EventHandler(OnCreateDevice);

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

        public void OnCreateDevice(object sender, EventArgs e)
        {
            Device dev = (Device)sender;

            // Now create the vertex buffer
            vertexBuffer = new VertexBuffer(
                typeof(CustomVertex.TransformedColored), 3, dev, 0,
                CustomVertex.TransformedColored.Format, Pool.Default);
            vertexBuffer.Created +=
                new System.EventHandler(this.OnCreateVertexBuffer);
            this.OnCreateVertexBuffer(vb, null);
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

        VertexBuffer vb = null;
        VertexBuffer vertexBuffer;
        public void Render()
        {
            if (_device == null || Window == null || Window.Handle == null) return;

            try
            {
                _device.Clear(ClearFlags.Target, Color.CornflowerBlue, 0.0f, 0);
                _device.BeginScene();
                
                // New for Tutorial 2
                //_device.VertexFormat = CustomVertex.TransformedColored.Format;

                var vertex = new CustomVertex.TransformedColored[4];
                var x = 100;
                var y = 100;
                var width = 200;
                var height = 100;
                var color = Color.Gray.ToArgb();

                vertex[0].Position = new Vector4(x, y, 0f, 0f);
                vertex[0].Color = color;
                vertex[1].Position = new Vector4(x + width, y, 0f, 0f);
                vertex[1].Color = color;
                vertex[2].Position = new Vector4(x, y + height, 0f, 0f);
                vertex[2].Color = color;
                vertex[3].Position = new Vector4(x + width, y + height, 0f, 0f);
                vertex[3].Color = color;

                _device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vertex);

                //Core.Components.Require<SceneManager>().Render();

                Font.Default.Draw("Test Sequence (테스트) ᕙ(-.-)ᕗ", 10, 10);
                Font.Default.Draw("Frame: " + Core.Components.Require<GameLoop>().FrameIndex, 10, 30);
                Font.Default.Draw("Frame-Rate: " + Core.Components.Require<GameLoop>().FramesPerSecond, 10, 50);

                _device.EndScene();
                _device.Present();
            }
            catch (Exception ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, "Render", ex.Message);
            }

            Application.DoEvents();
        }

        public void Shutdown()
        {
            _device.Dispose();
        }
    }
}