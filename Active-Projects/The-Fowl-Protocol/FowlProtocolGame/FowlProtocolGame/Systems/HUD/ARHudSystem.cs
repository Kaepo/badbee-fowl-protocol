using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FowlProtocolGame.Player;

namespace FowlProtocolGame.Systems.HUD
{
    public class ARHudSystem
    {
        public static ARHudSystem Instance { get; private set; }
        
        private readonly List<HudElement> _hudElements;
        private readonly SpriteFont _font;
        private readonly Vector2 _screenDimensions;
        private readonly bool _isEnabled;
        
        public ARHudSystem(Game game, SpriteFont font)
        {
            _hudElements = new List<HudElement>();
            _font = font;
            _screenDimensions = new Vector2(
                game.GraphicsDevice.Viewport.Width,
                game.GraphicsDevice.Viewport.Height
            );
            _isEnabled = true;
            
            InitializeDefaultHudElements();
            
            Instance = this;
        }
        
        private void InitializeDefaultHudElements()
        {
            _hudElements.Add(new HudElement
            {
                Id = "statusBar",
                Position = new Vector2(10, 10),
                Size = new Vector2(_screenDimensions.X - 20, 30),
                BackgroundColor = new Color(0, 0, 0, 128),
                BorderColor = Color.White,
                Text = "Fowl Protocol AR System v1.0",
                TextColor = Color.White,
                IsVisible = true
            });
            
            _hudElements.Add(new HudElement
            {
                Id = "compass",
                Position = new Vector2(_screenDimensions.X - 60, 50),
                Size = new Vector2(50, 50),
                BackgroundColor = new Color(0, 0, 0, 128),
                BorderColor = Color.White,
                Text = "N",
                TextColor = Color.White,
                IsVisible = true
            });
            
            _hudElements.Add(new HudElement
            {
                Id = "coordinates",
                Position = new Vector2(10, _screenDimensions.Y - 40),
                Size = new Vector2(120, 30),
                BackgroundColor = new Color(0, 0, 0, 128),
                BorderColor = Color.White,
                Text = "X: 0 Y: 0",
                TextColor = Color.White,
                IsVisible = true
            });
        }
        
        public void Update(GameTime gameTime, Vector2 playerPosition, Direction playerDirection)
        {
            if (!_isEnabled) return;
            
            HudElement coordinatesElement = _hudElements.Find(e => e.Id == "coordinates");
            if (coordinatesElement != null)
            {
                coordinatesElement.Text = $"X: {(int)playerPosition.X} Y: {(int)playerPosition.Y}";
            }
            
            HudElement compassElement = _hudElements.Find(e => e.Id == "compass");
            if (compassElement != null)
            {
                compassElement.Text = playerDirection.ToString()[0].ToString();
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isEnabled) return;
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            foreach (HudElement element in _hudElements)
            {
                if (!element.IsVisible) continue;
                
                Texture2D pixel = GetPixelTexture(spriteBatch.GraphicsDevice);
                
                spriteBatch.Draw(
                    pixel,
                    new Rectangle((int)element.Position.X, (int)element.Position.Y, (int)element.Size.X, (int)element.Size.Y),
                    element.BackgroundColor
                );
                
                DrawBorder(spriteBatch, element);
                
                if (_font != null && !string.IsNullOrEmpty(element.Text))
                {
                    Vector2 textSize = _font.MeasureString(element.Text);
                    Vector2 textPosition = new Vector2(
                        element.Position.X + (element.Size.X - textSize.X) / 2,
                        element.Position.Y + (element.Size.Y - textSize.Y) / 2
                    );
                    
                    spriteBatch.DrawString(_font, element.Text, textPosition, element.TextColor);
                }
            }
            
            spriteBatch.End();
        }
        
        private void DrawBorder(SpriteBatch spriteBatch, HudElement element)
        {
            Texture2D pixel = GetPixelTexture(spriteBatch.GraphicsDevice);
            int borderWidth = 1;
            
            spriteBatch.Draw(
                pixel,
                new Rectangle((int)element.Position.X, (int)element.Position.Y, (int)element.Size.X, borderWidth),
                element.BorderColor
            );
            
            spriteBatch.Draw(
                pixel,
                new Rectangle((int)element.Position.X, (int)(element.Position.Y + element.Size.Y - borderWidth), (int)element.Size.X, borderWidth),
                element.BorderColor
            );
            
            spriteBatch.Draw(
                pixel,
                new Rectangle((int)element.Position.X, (int)element.Position.Y, borderWidth, (int)element.Size.Y),
                element.BorderColor
            );
            
            spriteBatch.Draw(
                pixel,
                new Rectangle((int)(element.Position.X + element.Size.X - borderWidth), (int)element.Position.Y, borderWidth, (int)element.Size.Y),
                element.BorderColor
            );
        }
        
        private Texture2D GetPixelTexture(GraphicsDevice graphicsDevice)
        {
            Texture2D pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            return pixel;
        }
        
        public HudElement GetElement(string id)
        {
            return _hudElements.Find(e => e.Id == id);
        }
        
        public void AddElement(HudElement element)
        {
            _hudElements.Add(element);
        }
        
        public void RemoveElement(string id)
        {
            _hudElements.RemoveAll(e => e.Id == id);
        }
    }
    
    public class HudElement
    {
        public string Id { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color BackgroundColor { get; set; }
        public Color BorderColor { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public bool IsVisible { get; set; }
    }
}