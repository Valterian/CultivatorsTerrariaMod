﻿using Cultivators.Content.Items.TestItems;
using Cultivators.Content.Resources.Ci;
using Cultivators.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Cultivators.Content.UI.CIBar
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer
    internal class CiBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;
        private Dictionary<ElementsEnum, ElementSettings> ElementsSettings;
        private ElementsEnum SelectedElement;
        private ElementSettings SelectedElementSettings;
        private CiResourcePlayer LocalPlayer;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("Cultivators/Content/UI/CiBar/CiBarResourceFrame")); // Frame of our resource bar
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(10, 0f);
            text.Left.Set(-70, 0f);

            InitElementsUiConfig();

            SelectedElement = ElementsEnum.None;
            SelectedElementSettings = ElementsSettings[SelectedElement];
            gradientA = SelectedElementSettings.CiBarGradientA;
            gradientB = SelectedElementSettings.CiBarGradientB;

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<CiResourcePlayer>();
            // Calculate quotient
            float quotient = (float)modPlayer.exampleResourceCurrent / modPlayer.exampleResourceMax2; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            LocalPlayer = Main.LocalPlayer.GetModPlayer<CiResourcePlayer>();
            SelectedElement = LocalPlayer.SelectedCiElement;
            SelectedElementSettings = ElementsSettings[SelectedElement];
            gradientA = SelectedElementSettings.CiBarGradientA;
            gradientB = SelectedElementSettings.CiBarGradientB;
            // Setting the text per tick to update and show our resource values.
            text.SetText(ExampleResourceUISystem.ExampleResourceText.Format(LocalPlayer.exampleResourceCurrent, LocalPlayer.exampleResourceMax2));
            base.Update(gameTime);
        }

        private void InitElementsUiConfig()
        {
            ElementsSettings = new Dictionary<ElementsEnum, ElementSettings>
            {
                {
                    ElementsEnum.None,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.None").Value,
                        CiBarGradientA = new Color(0, 0, 0), // A dark gray
                        CiBarGradientB = new Color(0, 0, 0), // A light gray
                        TextColor = new Color(0, 0, 0), // A light gray
                    }
                },
                {
                    ElementsEnum.Wind,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.WindLightning").Value,
                        CiBarGradientA = new Color(179, 185, 199), // A dark gray
                        CiBarGradientB = new Color(91, 102, 120), // A light gray
                        TextColor = new Color(91, 102, 120), // A light gray
                    }
                },
                {
                    ElementsEnum.FireLightning,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.Fire").Value,
                        CiBarGradientA = new Color(252, 95, 4), // A dark yellow
                        CiBarGradientB = new Color(255, 255, 204), // A light yellow
                        TextColor = new Color(255, 255, 204), // A light yellow
                    }
                },
                {
                    ElementsEnum.WaterIce,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.WaterIce").Value,
                        CiBarGradientA = new Color(17, 30, 113), // A dark blue
                        CiBarGradientB = new Color(28, 50, 189), // A light blue
                        TextColor = new Color(28, 50, 189), // A light blue
                    }
                },
                {
                    ElementsEnum.EarthNature,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.EarthNature").Value,
                        CiBarGradientA = new Color(56, 87, 27), // A dark green
                        CiBarGradientB = new Color(105, 163, 50), // A light green
                        TextColor = new Color(105, 163, 50), // A light green
                    }
                },
                {
                    ElementsEnum.Death,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.Death").Value,
                        CiBarGradientA = new Color(37, 62, 59), // A dark green
                        CiBarGradientB = new Color(169, 236, 203), // A light green
                        TextColor = new Color(169, 236, 203), // A light green
                    }
                },
                {
                    ElementsEnum.Blood,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.Blood").Value,
                        CiBarGradientA = new Color(55, 23, 23), // A dark red
                        CiBarGradientB = new Color(241, 0, 0), // A light red
                        TextColor = new Color(241, 0, 0), // A light red
                    }
                },
                {
                    ElementsEnum.Darkness,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.Darkness").Value,
                        CiBarGradientA = new Color(98, 24, 184), // A dark purple
                        CiBarGradientB = new Color(230, 165, 233), // A light purple
                        TextColor = new Color(230, 165, 233), // A light purple
                    }
                },
                {
                    ElementsEnum.Light,
                    new ElementSettings
                    {
                        ElementDescription = Language.GetOrRegister($"UI.Elements.Light").Value,
                        CiBarGradientA = new Color(149, 149, 149), // A dark purple
                        CiBarGradientB = new Color(245, 245, 245), // A light purple
                        TextColor = new Color(245, 245, 245), // A light purple
                    }
                }
            };
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class ExampleResourceUISystem : ModSystem
    {
        private UserInterface ExampleResourceBarUserInterface;

        internal CiBar ExampleResourceBar;

        public static LocalizedText ExampleResourceText { get; private set; }

        public override void Load()
        {
            ExampleResourceBar = new();
            ExampleResourceBarUserInterface = new();
            ExampleResourceBarUserInterface.SetState(ExampleResourceBar);

            string category = "UI";
            ExampleResourceText ??= Mod.GetLocalization($"{category}.ExampleResource");
        }

        public override void UpdateUI(GameTime gameTime)
        {
            ExampleResourceBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate
                    {
                        ExampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
