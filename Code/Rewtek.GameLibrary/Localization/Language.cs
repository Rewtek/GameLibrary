using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;

namespace Rewtek.GameLibrary.Localization
{
    public class Language
    {
        // Properties
        /// <summary>
        /// Gets the native name of the language.
        /// </summary>
        public string NativeName { get; private set; }
        /// <summary>
        /// Gets or sets the name of the culture.
        /// </summary>
        public string CultureName { get; private set; }
        /// <summary>
        /// Gets or sets the strings.
        /// </summary>
        public Dictionary<string, string> Strings { get; set; }

        // Constants
        /// <summary>
        /// Provides the current file version.
        /// </summary>
        public const string FILE_VERSION = "1.0";

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Localization.Language"/> class.
        /// </summary>
        public Language()
        {
            Strings = new Dictionary<string, string>();
        }

        // Methods
        /// <summary>
        /// Loads the <see cref="Rewtek.GameLibrary.Localization.Language"/> from the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A <see cref="Rewtek.GameLibrary.Localization.Language"/>.</returns>
        public static Language Load(string path)
        {
            if (!ResourceSystem.CheckFile(path)) return null;

            Logger.Log("Loading language {0} ...", path);

            try
            {
                var document = XDocument.Load(path);
                var language = new Language();

                if (document.Root.Attribute("Version").Value != FILE_VERSION)
                {
                    MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "File Version Missmatch - " + path);
                    return null;
                }

                language.CultureName = document.Root.Attribute("CultureName").Value;
                language.NativeName = document.Root.Attribute("NativeName").Value;

                foreach (XElement element in document.Root.Descendants())
                {
                    language.Strings.Add(element.Attribute("Name").Value, element.Value);
                }

                return language;
            }
            catch (XmlException ex)
            {
                Logger.Log("Language {0} could not be read. Syntax error on line {1}, column {2}", path, ex.LineNumber, ex.LinePosition);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Xml Syntax Error: {0}\nLine: {1}\nColumn: {2}", path, ex.LineNumber, ex.LinePosition);
                return null;
            }
            catch (Exception)
            {
                Logger.Log("Language {0} could not be read", path);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Language {0} could not be read", path);
                return null;
            }
        }

        /// <summary>
        /// Writes a <see cref="Rewtek.GameLibrary.Localization.Language"/> to the spcified path.
        /// </summary>
        /// <param name="language">The <see cref="Rewtek.GameLibrary.Localization.Language"/>.</param>
        /// <param name="path">The path.</param>
        public static void Write(Language language, string path)
        {
            var rootElement = new XElement("Language",
                new XAttribute("CultureName", language.CultureName),
                new XAttribute("NativeName", language.NativeName),
                new XAttribute("Version", FILE_VERSION));

            foreach (var pair in language.Strings)
            {
                var element = new XElement("String", new XAttribute("Name", pair.Key));
                element.Add(pair.Value);

                rootElement.Add(element);
            }

            var document = new XDocument(rootElement);
            document.Save(path);
        }
    }
}
