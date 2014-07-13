#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Components\ComponentManager.cs" company="RewTek Network">
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
// <note>
//      Some parts of this code were taken from XemioNetwork/GameLibrary (xemio.net).
// </note>
// --------------------------------------------------------------------------------------------------------------------
#endregion

namespace Rewtek.GameLibrary.Components
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    #endregion

    public class ComponentManager : IDisposable
    {
        // Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        protected List<IComponent> Components { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Components"/> class.
        /// </summary>
        public ComponentManager()
        {
            Logger.Log(Messages.COMPONENT_INITIALIZING, GetType().Name);

            Components = new List<IComponent>();
        }

        // Destructor
        ~ComponentManager()
        {
            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);
        }

        // Methods
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Install(IComponent component)
        {
            if (Components.Find(match => match.GetType().Name == component.GetType().Name) != null)
            {
                Logger.Log("Component {0} has already been installed", component.GetType().Name);
            }
            else
            {
                Components.Add(component);

                Logger.Log("Component {0} has been installed", component.GetType().Name);
            }
        }

        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        public T Get<T>() where T : IComponent
        {
            var component = Components.Find(comp => comp.GetType() == typeof(T));
            if (component != null)
            {
                return (T)component;
            }

            Logger.Log(LogLevel.Debug, "Component {0} does not exist inside the component registry.", typeof(T).Name);

            return default(T);
        }

        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            Components.Remove(component);

            Logger.Log(LogLevel.Debug, "Component {0} has been removed", component.GetType().Name);
        }

        /// <summary>
        /// Removes the specified component abstraction.
        /// </summary>
        public void Remove<T>() where T : IComponent
        {
            Remove(Get<T>());
        }

        /// <summary>
        /// Requires the specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        public T Require<T>() where T : IComponent
        {
            var component = Get<T>();
            if (object.Equals(component, null))
            {
                throw new MissingComponentException(typeof(T));
            }

            return component;
        }

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Components.ForEach(component => component.Dispose());
        }

        #endregion
    }
}
