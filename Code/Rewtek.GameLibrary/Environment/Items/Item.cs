namespace Rewtek.GameLibrary.Environment.Items
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Common;

    public class Item
    {
        // Variables

        // Properties
        public ItemType Type { get; set; }
        public char Letter { get; private set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }

        public ItemLabel Label { get; set; }
        public bool Buyable { get; set; }
        public int ReqLevel { get; set; }

        public int Power { get; set; }

        // Constructor
        public Item(ItemType type)
            : this(type, "Item")
        {
        }

        public Item(ItemType type, string name)
        {
            Type = type;

            Letter = Alphabet.LetterAt(Type);
            Name = string.Format("{0}_{1}", Letter, name);
            Code = string.Format("{0}00", Letter);

            Logger.Log(LogLevel.Debug, "New item ({0}-{1}) created", Code, Name);
        }
    }
}
