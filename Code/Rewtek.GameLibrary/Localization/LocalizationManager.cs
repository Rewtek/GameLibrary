namespace Rewtek.GameLibrary.Localization
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Common;
    using Rewtek.GameLibrary.Components;

    public class LocalizationManager : Component
    {
        // Properties
        /// <summary>
        /// Gets the current <see cref="Rewtek.GameLibrary.Localization.Language"/>.
        /// </summary>
        public Language CurrentLanguage { get; private set; }
        /// <summary>
        /// Gets a <see cref="System.Collections.Generic.List"/> containing all <see cref="Rewtek.GameLibrary.Localization.Language"/>s.
        /// </summary>
        public List<Language> Languages { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Localization.LocalizationManager"/> class.
        /// </summary>
        public LocalizationManager()
        {
            Languages = new List<Language>();
        }

        // Methods
        /// <summary>
        /// Changes the current <see cref="Rewtek.GameLibrary.Localization.Language"/>.
        /// </summary>
        /// <param name="language">The <see cref="Rewtek.GameLibrary.Localization.Language"/>.</param>
        public void ChangeLanguage(string language)
        {
            Language currentLanguage = Languages.FirstOrDefault(f => f.CultureName == language);
            if (currentLanguage == null)
            {
                Logger.Log("Language {0} does not exist. Could not change language.", language);
                return;
            }

            CurrentLanguage = currentLanguage;
            Logger.Log("Language changed to {0}", language);
        }

        /// <summary>
        /// Returns the localized value for the specified key.
        /// </summary>
        /// <param name="key">The identifier key.</param>
        public string Get(string key)
        {
            if (CurrentLanguage == null || !CurrentLanguage.Strings.ContainsKey(key))
            {
                return "Not found Message";
            }

            return CurrentLanguage.Strings[key];
        }

        /// <summary>
        /// Returns the localized format for the specified key and appends format parameters.
        /// </summary>
        /// <param name="key">The identifier key.</param>
        /// <param name="parameters">The parameters.</param>
        public string Get(string key, params object[] parameters)
        {
            return string.Format(Get(key), parameters);
        }

        /// <summary>
        /// Adds the directory to the root directories and loads all languages inside it.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadLanguages(string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                Logger.Log("Language directory is not defined");
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Language directory is not defined");
                return;
            }

            if (!ResourceSystem.ExistsDirectory(directory))
            {
                Logger.Log("Language directory {0} does not exist", directory);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Language directory {0} does not exist", directory);
                return;
            }

            Logger.Log("Loading languages from {0}", directory);

            foreach (var language in System.IO.Directory.GetFiles(directory))
            {
                Languages.Add(Language.Load(language));
            }
        }
    }
}
