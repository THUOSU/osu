﻿//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transformations;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Platform;

namespace osu.Game.Overlays
{
    public class Options : OverlayContainer
    {
        internal const float SideMargins = 10;
        private const float width = 400;
        private FlowContainer optionsContainer;
        private BasicStorage storage;

        protected override void Load(BaseGame game)
        {
            base.Load(game);

            storage = game.Host.Storage;

            Depth = float.MaxValue;
            RelativeSizeAxes = Axes.Y;
            Size = new Vector2(width, 1);
            Position = new Vector2(-width, 0);

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(51, 51, 51, 255)
                },
                // TODO: Links on the side to jump to a section
                new ScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollDraggerOnLeft = true,
                    Children = new[]
                    {
                        optionsContainer = new FlowContainer
                        {
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Direction = FlowDirection.VerticalOnly,
                            Children = new[]
                            {
                                new SpriteText
                                {
                                    Text = "settings",
                                    TextSize = 40,
                                    Margin = new MarginPadding { Left = SideMargins, Top = 30 },
                                },
                                new SpriteText
                                {
                                    Colour = new Color4(235, 117, 139, 255),
                                    Text = "Change the way osu! behaves",
                                    TextSize = 18,
                                    Margin = new MarginPadding { Left = SideMargins, Bottom = 30 },
                                }
                            }
                        }
                    }
                }
            };
            addGeneral();
        }

        private void addGeneral()
        {
            optionsContainer.Add(new OptionsSection
            {
                Header = "General",
                Children = new[]
                {
                    new OptionsSubsection
                    {
                        Header = "Sign In",
                        Children = new[]
                        {
                            new SpriteText { Text = "TODO" }
                        }
                    },
                    new OptionsSubsection
                    {
                        Header = "Language",
                        Children = new Drawable[]
                        {
                            new SpriteText { Text = "TODO: Dropdown" },
                            new BasicCheckBox
                            {
                                Children = new[] { new SpriteText { Text = "Prefer metadata in original language" } }
                            },
                            new BasicCheckBox
                            {
                                Children = new[] { new SpriteText { Text = "Use alternative font for chat display" } }
                            },
                        }
                    },
                    new OptionsSubsection
                    {
                        Header = "Updates",
                        Children = new Drawable[]
                        {
                            new SpriteText { Text = "TODO: Dropdown" },
                            new SpriteText { Text = "Your osu! is up to date" }, // TODO: map this to reality
                            new Button
                            {
                                AutoSizeAxes = Axes.Y,
                                RelativeSizeAxes = Axes.X,
                                Colour = new Color4(14, 132, 165, 255),
                                Text = "Open osu! folder",
                                Action = storage.OpenInNativeExplorer,
                            }
                        }
                    }
                }
            });
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Escape:
                    if (State == Visibility.Hidden) return false;

                    State = Visibility.Hidden;
                    return true;
            }
            return base.OnKeyDown(state, args);
        }

        protected override void PopIn()
        {
            MoveToX(0, 300, EasingTypes.Out);
        }

        protected override void PopOut()
        {
            MoveToX(-width, 300, EasingTypes.Out);
        }
    }

    class OptionsSection : Container
    {
        private SpriteText header;
        private FlowContainer content;
        protected override Container Content => content;
        
        public string Header
        {
            get { return header.Text; }
            set { header.Text = value; }
        }
        
        public OptionsSection()
        {
            const int headerSize = 30, headerMargin = 25;
            const int borderSize = 2;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            AddInternal(new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(3, 3, 3, 255),
                    RelativeSizeAxes = Axes.X,
                    Height = borderSize,
                },
                new Container
                {
                    Padding = new MarginPadding
                    {
                        Top = 10 + borderSize,
                        Left = Options.SideMargins,
                        Right = Options.SideMargins,
                    },
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new[]
                    {
                        header = new SpriteText
                        {
                            TextSize = headerSize,
                            Colour = new Color4(247, 198, 35, 255),
                        },
                        content = new FlowContainer
                        {
                            Margin = new MarginPadding { Top = headerSize + headerMargin },
                            Direction = FlowDirection.VerticalOnly,
                            Spacing = new Vector2(0, 25),
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                        },
                    }
                },
            });
        }
    }

    class OptionsSubsection : Container
    {
        private SpriteText header;
        private Container content;
        protected override Container Content => content;
        
        public string Header
        {
            get { return header.Text; }
            set { header.Text = value.ToUpper(); }
        }
    
        public OptionsSubsection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            AddInternal(new Drawable[]
            {
                content = new FlowContainer
                {
                    Direction = FlowDirection.VerticalOnly,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new[]
                    {
                        header = new SpriteText
                        {
                            TextSize = 25,
                            // TODO: Bold
                        }
                    }
                },
            });
        }
    }
}
