// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class MouseSettingsStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.MouseSettings";

        /// <summary>
        /// "Mouse"
        /// </summary>
        public static LocalisableString Mouse => new TranslatableString(getKey(@"mouse"), @"Mouse");

        /// <summary>
        /// "not applicable in full screen mode"
        /// </summary>
        public static LocalisableString NotApplicableFullscreen => new TranslatableString(getKey(@"not_applicable_full_screen"), @"not applicable in full screen mode");

        /// <summary>
        /// "High precision mouse"
        /// </summary>
        public static LocalisableString HighPrecisionMouse => new TranslatableString(getKey(@"high_precision_mouse"), @"High precision mouse");

        /// <summary>
        /// "attempts to bypass any operating system mouse acceleration. on windows, this is equivalent to what used to be known as &quot;raw input&quot;."
        /// </summary>
        public static LocalisableString HighPrecisionMouseTooltip => new TranslatableString(getKey(@"high_precision_mouse_tooltip"), @"attempts to bypass any operating system mouse acceleration. on windows, this is equivalent to what used to be known as ""raw input"".");

        /// <summary>
        /// "Confine mouse cursor to window"
        /// </summary>
        public static LocalisableString ConfineMouseMode => new TranslatableString(getKey(@"confine_mouse_mode"), @"Confine mouse cursor to window");

        /// <summary>
        /// "Disable mouse wheel adjusting volume during gameplay"
        /// </summary>
        public static LocalisableString DisableMouseWheelVolumeAdjust => new TranslatableString(getKey(@"disable_mouse_wheel_volume_adjust"), @"Disable mouse wheel adjusting volume during gameplay");

        /// <summary>
        /// "volume can still be adjusted using the mouse wheel by holding "alt""
        /// </summary>
        public static LocalisableString DisableMouseWheelVolumeAdjustTooltip => new TranslatableString(getKey(@"disable_mouse_wheel_volume_adjust_tooltip"), @"volume can still be adjusted using the mouse wheel by holding ""alt""");

        /// <summary>
        /// "Disable mouse buttons during gameplay"
        /// </summary>
        public static LocalisableString DisableMouseButtons => new TranslatableString(getKey(@"disable_mouse_buttons"), @"Disable mouse buttons during gameplay");

        /// <summary>
        /// "enable high precision mouse to adjust sensitivity"
        /// </summary>
        public static LocalisableString EnableHighPrecisionForSensitivityAdjust => new TranslatableString(getKey(@"enable_high_precision_for_sensitivity_adjust"), @"enable high precision mouse to adjust sensitivity");

        /// <summary>
        /// "Cursor sensitivity"
        /// </summary>
        public static LocalisableString CursorSensitivity => new TranslatableString(getKey(@"cursor_sensitivity"), @"Cursor sensitivity");

        /// <summary>
        /// "this setting has known issues on your platform. if you encounter problems, it is recommended to adjust sensitivity externally and keep this disabled for now."
        /// </summary>
        public static LocalisableString HighPrecisionPlatformWarning => new TranslatableString(getKey(@"high_precision_platform_warning"), @"this setting has known issues on your platform. if you encounter problems, it is recommended to adjust sensitivity externally and keep this disabled for now.");

        /// <summary>
        /// "Always"
        /// </summary>
        public static LocalisableString AlwaysConfine => new TranslatableString(getKey(@"always_confine"), @"Always");

        /// <summary>
        /// "During Gameplay"
        /// </summary>
        public static LocalisableString ConfineDuringGameplay => new TranslatableString(getKey(@"confine_during_gameplay"), @"During Gameplay");

        /// <summary>
        /// "Never"
        /// </summary>
        public static LocalisableString NeverConfine => new TranslatableString(getKey(@"never_confine"), @"Never");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
