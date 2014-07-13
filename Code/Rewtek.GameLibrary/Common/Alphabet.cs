#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common\Alphabet.cs" company="RewTek Network">
//      Copyright (c) 2014 Rewtek Network (www.rewtek.net)
//      
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//      furnished to do so, subject to the following conditions:
//      
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//      
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//      SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#endregion

namespace Rewtek.GameLibrary.Common
{
    #region Using directives

    using global::System;

    #endregion

    public static class Alphabet
    {
        /// <summary>
        /// Gets all letters of the alphabet.
        /// </summary>
        public static char[] Letters { get { return new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; } }

        /// <summary>
        /// Returns the index of a letter in the alphabet.
        /// </summary>
        /// <param name="c">The letter.</param>
        /// <returns>The index of the letter.</returns>
        public static int IndexOf(char c)
        {
            return Array.IndexOf<char>(Letters, c);
        }

        /// <summary>
        /// Returns the letter at the index in the alphabet.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The letter at the index.</returns>
        public static char LetterAt(int index)
        {
            return Letters[index];
        }

        /// <summary>
        /// Returns the letter at the index in the alphabet.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The letter at the index.</returns>
        public static char LetterAt(object index)
        {
            try
            {
                return Letters[(int)index];
            }
            catch
            {
                return char.MaxValue;
            }
        }
    }
}
