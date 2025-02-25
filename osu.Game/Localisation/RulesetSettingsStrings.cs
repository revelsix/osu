// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class RulesetSettingsStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.RulesetSettings";

        /// <summary>
        /// "Rulesets"
        /// </summary>
        public static LocalisableString Rulesets => new TranslatableString(getKey(@"rulesets"), @"Rulesets");

        /// <summary>
        /// "None"
        /// </summary>
        public static LocalisableString BorderNone => new TranslatableString(getKey(@"no_borders"), @"None");

        /// <summary>
        /// "Corners"
        /// </summary>
        public static LocalisableString BorderCorners => new TranslatableString(getKey(@"corner_borders"), @"Corners");

        /// <summary>
        /// "Full"
        /// </summary>
        public static LocalisableString BorderFull => new TranslatableString(getKey(@"full_borders"), @"Full");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
