// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable enable

using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Timing;
using osu.Game.Beatmaps.ControlPoints;

namespace osu.Game.Graphics.Containers
{
    /// <summary>
    /// Provides various data sources which allow for synchronising visuals to a known beat.
    /// Primarily intended for use with <see cref="BeatSyncedContainer"/>.
    /// </summary>
    [Cached(typeof(IBeatSyncProvider))]
    public interface IBeatSyncProvider
    {
        ControlPointInfo? ControlPoints { get; }

        IClock? Clock { get; }

        ChannelAmplitudes? Amplitudes { get; }
    }
}
