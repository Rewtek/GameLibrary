
namespace Rewtek.GameLibrary.Rendering.Geometry
{
    using global::System;
    using global::System.Drawing;

    using Microsoft.DirectX;
    using Microsoft.DirectX.Direct3D;

    public class GemoetryRenderer
    {
        // Variables
        private readonly GraphicsDevice _device;

        // Constructor
        public GemoetryRenderer(GraphicsDevice graphicsDevice)
        {
            _device = graphicsDevice;
        }

        // Methods
        public void DrawRectangle(float x, float y, float width, float height)
        {
            DrawRectangle(x, y, width, height, Color.Black);
        }

        public void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            _device.Device.VertexFormat = CustomVertex.TransformedColored.Format;
            color = Color.Red;
            var vertex = new CustomVertex.TransformedColored[4];

            vertex[0].Position = new Vector4(x, y, 0f, 0f);
            vertex[0].Color = color.ToArgb();
            vertex[1].Position = new Vector4(x + width, y, 0f, 0f);
            vertex[1].Color = color.ToArgb();
            vertex[2].Position = new Vector4(x, y + height, 0f, 0f);
            vertex[2].Color = color.ToArgb();
            vertex[3].Position = new Vector4(x + width, y + height, 0f, 0f);
            vertex[3].Color = color.ToArgb();

            _device.Device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vertex);
        }
    }
}
