﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Screens.Edit.Screens.Compose
{
    public class DrawableBeatDivisor : CompositeDrawable
    {
        private static readonly int[] available_divisors = { 1, 2, 3, 4, 6, 8, 12, 16 };

        private readonly BindableBeatDivisor beatDivisor = new BindableBeatDivisor();
        private int currentDivisorIndex;

        public DrawableBeatDivisor(BindableBeatDivisor beatDivisor)
        {
            this.beatDivisor.BindTo(beatDivisor);
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Masking = true;
            CornerRadius = 5;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new TickContainer(beatDivisor, 1, 2, 3, 4, 6, 8, 12, 16)
                            {
                                RelativeSizeAxes = Axes.Both,
                                Padding = new MarginPadding { Horizontal = 5 }
                            }
                        },
                        new Drawable[]
                        {
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = colours.Gray4
                                    },
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Padding = new MarginPadding { Horizontal = 5 },
                                        Child = new GridContainer
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Content = new[]
                                            {
                                                new Drawable[]
                                                {
                                                    new DivisorButton
                                                    {
                                                        Icon = FontAwesome.fa_chevron_left,
                                                        Action = selectPrevious
                                                    },
                                                    new DivisorText(beatDivisor),
                                                    new DivisorButton
                                                    {
                                                        Icon = FontAwesome.fa_chevron_right,
                                                        Action = selectNext
                                                    }
                                                },
                                                new Drawable[]
                                                {
                                                    null,
                                                    new TextFlowContainer(s => s.TextSize = 10)
                                                    {
                                                        Text = "beat snap divisor",
                                                        RelativeSizeAxes = Axes.X,
                                                        TextAnchor = Anchor.TopCentre
                                                    },
                                                },
                                            },
                                            ColumnDimensions = new[]
                                            {
                                                new Dimension(GridSizeMode.Absolute, 20),
                                                new Dimension(),
                                                new Dimension(GridSizeMode.Absolute, 20)
                                            }
                                        }
                                    }
                                }
                            }
                        },
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, 35),
                    }
                }
            };
        }

        private void selectPrevious()
        {
            if (currentDivisorIndex == 0)
                return;
            beatDivisor.Value = available_divisors[--currentDivisorIndex];
        }

        private void selectNext()
        {
            if (currentDivisorIndex == available_divisors.Length - 1)
                return;
            beatDivisor.Value = available_divisors[++currentDivisorIndex];
        }

        private class DivisorText : SpriteText
        {
            private readonly Bindable<int> beatDivisor = new Bindable<int>();

            public DivisorText(BindableBeatDivisor beatDivisor)
            {
                this.beatDivisor.BindTo(beatDivisor);

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                Colour = colours.BlueLighter;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                beatDivisor.ValueChanged += v => updateText();
                updateText();
            }

            private void updateText() => Text = $"1/{beatDivisor.Value}";
        }

        private class DivisorButton : IconButton
        {
            public DivisorButton()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                // Small offset to look a bit better centered along with the divisor text
                Y = 1;

                ButtonSize = new Vector2(20);
                IconScale = new Vector2(0.6f);
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                IconColour = Color4.Black;
                HoverColour = colours.Gray7;
                FlashColour = colours.Gray9;
            }
        }

        private class TickContainer : CompositeDrawable
        {
            private readonly Bindable<int> beatDivisor = new Bindable<int>();

            public new MarginPadding Padding { set => base.Padding = value; }

            private EquilateralTriangle marker;

            private readonly int[] availableDivisors;
            private readonly float tickSpacing;

            public TickContainer(BindableBeatDivisor beatDivisor, params int[] divisors)
            {
                this.beatDivisor.BindTo(beatDivisor);

                availableDivisors = divisors;
                tickSpacing = 1f / (availableDivisors.Length + 1);
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                InternalChild = marker = new EquilateralTriangle
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomCentre,
                    RelativePositionAxes = Axes.X,
                    Height = 7,
                    EdgeSmoothness = new Vector2(1),
                    Colour = colours.Gray4,
                };

                for (int i = 0; i < availableDivisors.Length; i++)
                {
                    AddInternal(new Tick(availableDivisors[i])
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopCentre,
                        RelativePositionAxes = Axes.X,
                        X = getTickPosition(i)
                    });
                }
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                beatDivisor.ValueChanged += v => updatePosition();
                updatePosition();
            }

            private void updatePosition() => marker.MoveToX(getTickPosition(Array.IndexOf(availableDivisors, beatDivisor.Value)), 100, Easing.OutQuint);

            private float getTickPosition(int index) => (index + 1) * tickSpacing;

            private class Tick : Box
            {
                private readonly int divisor;

                public Tick(int divisor)
                {
                    this.divisor = divisor;

                    Size = new Vector2(2, 10);
                }

                [BackgroundDependencyLoader]
                private void load(OsuColour colours)
                {
                    if (divisor >= 16)
                        Colour = colours.Red;
                    else if (divisor >= 8)
                        Colour = colours.Yellow;
                    else
                        Colour = colours.Gray4;
                }
            }
        }
    }
}
