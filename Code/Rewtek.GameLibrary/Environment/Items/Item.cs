namespace Rewtek.GameLibrary.Environment.Items
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    #endregion

    public class Item
    {
        // Properties
        public string Name { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Environment.Items.Item"/> class.
        /// <param name="name">The item name.</param>
        /// </summary>
        public Item(string name)
        {
            Name = name;
        }

        // Methods

        #region Override Methods

        public override int GetHashCode()
        {
            return base.GetHashCode() + Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
