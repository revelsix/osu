// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Game.Tournament.Components;
using osu.Game.Tournament.Models;
using osuTK;
using osuTK.Input;

namespace osu.Game.Tournament.Screens.Gameplay.Components
{
    public class MatchHeader : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.X;
            Height = 95;
            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(5),
                    Children = new Drawable[]
                    {
                        new DrawableTournamentTitleText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Scale = new Vector2(1.2f)
                        },
                        new RoundDisplay
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Scale = new Vector2(0.4f)
                        },
                    }
                },

                new TeamScoreDisplay(TeamColour.Red)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                },
                new TeamScoreDisplay(TeamColour.Blue)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                },
            };
        }
    }

    public class TeamScoreDisplay : CompositeDrawable
    {
        private readonly TeamColour teamColour;

        private readonly Bindable<TournamentMatch> currentMatch = new Bindable<TournamentMatch>();
        private readonly Bindable<TournamentTeam> currentTeam = new Bindable<TournamentTeam>();
        private readonly Bindable<int?> currentTeamScore = new Bindable<int?>();

        public TeamScoreDisplay(TeamColour teamColour)
        {
            this.teamColour = teamColour;

            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load(LadderInfo ladder)
        {
            currentMatch.BindValueChanged(matchChanged);
            currentMatch.BindTo(ladder.CurrentMatch);
        }

        private void matchChanged(ValueChangedEvent<TournamentMatch> match)
        {
            currentTeamScore.UnbindBindings();
            currentTeamScore.BindTo(teamColour == TeamColour.Red ? match.NewValue.Team1Score : match.NewValue.Team2Score);

            currentTeam.UnbindBindings();
            currentTeam.BindTo(teamColour == TeamColour.Red ? match.NewValue.Team1 : match.NewValue.Team2);

            // team may change to same team, which means score is not in a good state.
            // thus we handle this manually.
            teamChanged(currentTeam.Value);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            switch (e.Button)
            {
                case MouseButton.Left:
                    if (currentTeamScore.Value < currentMatch.Value.PointsToWin)
                        currentTeamScore.Value++;
                    return true;

                case MouseButton.Right:
                    if (currentTeamScore.Value > 0)
                        currentTeamScore.Value--;
                    return true;
            }

            return base.OnMouseDown(e);
        }

        private void teamChanged(TournamentTeam team)
        {
            InternalChildren = new Drawable[]
            {
                new TeamDisplay(team, teamColour, currentTeamScore, currentMatch.Value.PointsToWin),
            };
        }
    }
}
