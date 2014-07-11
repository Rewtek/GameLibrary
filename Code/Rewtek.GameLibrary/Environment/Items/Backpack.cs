namespace Rewtek.GameLibrary.Environment.Items
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections;
    using global::System.Collections.Generic;

    #endregion

    /// <summary>
    /// Provides a class for storing items.
    /// </summary>
    public class Backpack : IDisposable, IEnumerable<Item>
    {
        // Variables
        private readonly List<Item> _items;
        private bool _disposed;

        // Properties
        /// <summary>
        /// Gets or sets a value indicating the item count in the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        public int ItemCount { get { return _items.Count; } }
        /// <summary>
        /// Gets or sets a value indicating the capacity of the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        public int Capacity { get { return _items.Capacity; } set { _items.Capacity = value; } }

        // Indexer
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public Item this[int index] { get { return _items[index]; } set { _items[index] = value; } }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> class.
        /// </summary>
        public Backpack()
        {
            _items = new List<Item>();
        }

        /// <summary>
        /// Initializes a new <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> instance.
        /// <param name="capacity">Sets the capacity of the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.</param>
        /// </summary>
        public Backpack(int capacity)
        {
            _items = new List<Item>(capacity);
        }

        // Deconstructor
        ~Backpack()
        {
            Dispose(false);
        }

        // Methods
        #region Public Methods

        /// <summary>
        /// Adds an item to the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Returns an item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>The <see cref="Item"/> of the specified name.</returns>
        public Item GetItem(string name)
        {
            return _items.Find(item => item.Name == name);
        }

        /// <summary>
        /// Removes an item from the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Removes an item with specified name from the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        /// <param name="name">The name of the item to be removed.</param>
        public void RemoveItem(string name)
        {
            _items.Remove(GetItem(name));
        }

        /// <summary>
        /// Determines whether the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> contains an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True, if the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> contains the specified item.</returns>
        public bool HasItem(Item item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Determines whether the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> contains an item with the specified name.
        /// </summary>
        /// <param name="item">The name of the item.</param>
        /// <returns>True, if the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/> contains the specified item.</returns>
        public bool HasItem(string name)
        {
            return _items.Find(item => item.Name == name) != null ? true : false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="Rewtek.GameLibrary.Environment.Items.Backpack"/>.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        #endregion

        #region IEnumerator Implementation

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="System.Collections.Generic.List<T>"/>.
        /// </summary>
        /// <returns>A List<T>.Enumerator for the List<T>.</returns>
        public IEnumerator<Item> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="System.Collections.Generic.List<T>"/>.
        /// </summary>
        /// <returns>A List<T>.Enumerator for the List<T>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
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

            // Dispose managed resources.
            if (disposing)
            {
                _items.Clear();
            }

            // Dispose unmanaged resources.

            _disposed = true;
        }

        #endregion
    }
}
