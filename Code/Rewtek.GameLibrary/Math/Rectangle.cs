﻿#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Math\Rectangle.cs" company="Xemio Network">
//      Copyright (c) 2013 Xemio Network (xemio.net)
//      
//      Permission is hereby granted, free of charge, to any person obtaining a copy of
//      this software and associated documentation files (the "Software"), to deal in
//      the Software without restriction, including without limitation the rights to
//      use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//      the Software, and to permit persons to whom the Software is furnished to do so,
//      subject to the following conditions:
//      
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//      
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//      FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//      COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//      IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Empty
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion

namespace Rewtek.GameLibrary.Math
{
    using System;

    public struct Rectangle
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rectangle(float x, float y, float width, float height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public Rectangle(Vector2 a, Vector2 b)
        {
            this._x = a.X;
            this._y = a.Y;
            this._width = MathHelper.Abs(a.X - b.X);
            this._height = MathHelper.Abs(a.Y - b.Y);
        }
        #endregion

        #region Fields
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        #endregion

        #region Coordinates and Size
        /// <summary>
        /// Gets or sets the X-coordinate.
        /// </summary>
        public float X
        {
            get { return this._x; }
            set { this._x = value; }
        }
        /// <summary>
        /// Gets or sets the Y-coordinate.
        /// </summary>
        public float Y
        {
            get { return this._y; }
            set { this._y = value; }
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public float Width
        {
            get { return this._width; }
            set { this._width = value; }
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public float Height
        {
            get { return this._height; }
            set { this._height = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the left.
        /// </summary>
        public float Left
        {
            get { return this.X; }
        }
        /// <summary>
        /// Gets the right.
        /// </summary>
        public float Right
        {
            get { return this.X + this.Width; }
        }
        /// <summary>
        /// Gets the top.
        /// </summary>
        public float Top
        {
            get { return this.Y; }
        }
        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public float Bottom
        {
            get { return this.Y + this.Height; }
        }
        /// <summary>
        /// Gets the location.
        /// </summary>
        public Vector2 Location
        {
            get { return new Vector2(this.X, this.Y); }
        }
        /// <summary>
        /// Gets the center.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f); }
        }
        /// <summary>
        /// Gets the edges.
        /// </summary>
        public Vector2[] Corners
        {
            get
            {
                return new[]
                {
                    new Vector2(this.Left, this.Top),
                    new Vector2(this.Right, this.Top),
                    new Vector2(this.Right, this.Bottom),
                    new Vector2(this.Left, this.Bottom)
                };
            }
        }
        #endregion

        #region Released Rectangle
        private static Rectangle _empty = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// Gets an empty rectangle instance.
        /// </summary>
        public static Rectangle Empty
        {
            get { return Rectangle._empty; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Expands the rectangle by pushing all 4 directions by the according axis-orientated x and y values.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Rectangle Expand(float x, float y)
        {
            this._x -= x;
            this._y -= y;
            this._width += x * 2;
            this._height += y * 2;

            return this;
        }
        /// <summary>
        /// Determines whether this instance contains the specified rectangle.
        /// </summary>
        /// <param name="value">The value.</param>
        public bool Contains(Rectangle value)
        {
            return 
                value.X >= this.X &&
                value.Y >= this.Y &&
                value.X + value.Width <= this.Right &&
                value.Y + this.Height <= this.Bottom;
        }
        /// <summary>
        /// Determines whether this instance contains the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public bool Contains(Vector2 vector)
        {
            return vector.X > this.X &&
                   vector.X < this.Right &&
                   vector.Y > this.Y &&
                   vector.Y < this.Bottom;
        }
        /// <summary>
        /// Intersectses the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public bool Intersects(Rectangle rectangle)
        {
            return 
                !(this.Left > rectangle.Right ||
                this.Right < rectangle.Left ||
                this.Top > rectangle.Bottom ||
                this.Bottom < rectangle.Top);
        }
        /// <summary>
        /// Intersects the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public Rectangle Intersect(Rectangle rectangle)
        {
            if (!this.Intersects(rectangle))
            {
                return Rectangle.Empty;
            }

            float[] horizontal = { this.Left, this.Right, rectangle.Left, rectangle.Right };
            float[] vertical = { this.Bottom, this.Top, rectangle.Bottom, rectangle.Top };

            Array.Sort(horizontal);
            Array.Sort(vertical);

            float left = horizontal[1];
            float bottom = vertical[1];
            float right = horizontal[2];
            float top = vertical[2];

            return new Rectangle(left, top, right - left, bottom - top);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator +(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X += vector.X;
            rectangle.Y += vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator -(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X -= vector.X;
            rectangle.Y -= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator *(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X *= vector.X;
            rectangle.Y *= vector.Y;
            rectangle.Width *= vector.X;
            rectangle.Height *= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="scale">The scale.</param>
        public static Rectangle operator *(Rectangle rectangle, float scale)
        {
            rectangle.X *= scale;
            rectangle.Y *= scale;
            rectangle.Width *= scale;
            rectangle.Height *= scale;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator /(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X /= vector.X;
            rectangle.Y /= vector.Y;
            rectangle.Width /= vector.X;
            rectangle.Height /= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="scale">The scale.</param>
        public static Rectangle operator /(Rectangle rectangle, float scale)
        {
            rectangle.X /= scale;
            rectangle.Y /= scale;
            rectangle.Width /= scale;
            rectangle.Height /= scale;

            return rectangle;
        }
        #endregion
    }
}