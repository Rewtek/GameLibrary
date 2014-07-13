namespace Rewtek.GameLibrary.Environment.Items
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Components;

    public class ItemManager : IComponent
    {
        // Variables
        private readonly List<Item> _items;

        // Properties
        public int ItemCount { get { return _items.Count; } }

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        public ItemManager()
        {
            _items = new List<Item>();
            //_items.Add(new Item(ItemType.Resource, "WATER"));
            //_items.Add(new Item(ItemType.Resource, "WOOD"));
            //_items.Add(new Item(ItemType.Weapon, "AXE"));
        }

        // Methods
        public void Load()
        {

        }

        public void Save(string fileName)
        {
            var rootElement = new XElement("ItemList", new XAttribute("Version", FILE_VERSION));

            foreach (var item in _items)
            {
                var element = new XElement("Item", new XAttribute("Type", item.Type));
                var attributes = new XElement("Attributes");
                var basic = new XElement("Basic");

                basic.Add(new XElement("Name", new XAttribute("Value", item.Name)));
                basic.Add(new XElement("Code", new XAttribute("Value", string.Format("{0}{1:00}", item.Letter, _items.IndexOf(item)))));
                basic.Add(new XElement("Active", new XAttribute("Value", item.Active)));
                attributes.Add(basic);

                var shop = new XElement("Shop");
                shop.Add(new XElement("Buyable", new XAttribute("Value", item.Buyable)));
                shop.Add(new XElement("Label", new XAttribute("Value", item.Label)));
                shop.Add(new XElement("ReqLevel", new XAttribute("Value", item.ReqLevel)));
                attributes.Add(shop);

                var custom = new XElement("Custom");
                if (item.Type == ItemType.Weapon)
                {
                    custom.Add(new XElement("Power", new XAttribute("Value", item.Power)));
                }
                attributes.Add(custom);

                element.Add(attributes);
                rootElement.Add(element);
            }

            var document = new XDocument(rootElement);
            document.Save(fileName);

            Logger.Log(LogLevel.Debug, "ItemList.xml has been created.");
        }

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
