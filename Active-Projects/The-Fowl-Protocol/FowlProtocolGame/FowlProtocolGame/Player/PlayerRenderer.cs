using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FowlProtocolGame.Systems.Grid;

namespace FowlProtocolGame.Player
{
    public class PlayerRenderer
    {
        private readonly Player _player;
        private readonly Texture2D _sprite;
        
        private readonly int _frameWidth = 32;
        private readonly int _frameHeight = 32;
        private int _currentFrame = 0;
        private readonly float _animationSpeed = 0.2f;
        private float _animationTimer = 0f;
        
        public PlayerRenderer(Player player, Texture2D sprite)
        {
            _player = player;
            _sprite = sprite;
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Vector2 screenPos = new Vector2(
                position.X * GridSystem.CellSize, 
                position.Y * GridSystem.CellSize
            );
            
            Rectangle sourceRect = GetSourceRectangle();
            
            spriteBatch.Draw(
                _sprite,
                screenPos,
                sourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );
        }
        
        public void UpdateAnimation(GameTime gameTime)
        {
            if (_player.IsMoving)
            {
                _animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                if (_animationTimer >= _animationSpeed)
                {
                    _animationTimer = 0f;
                    _currentFrame = (_currentFrame + 1) % 4;
                }
            }
            else
            {
                _currentFrame = 0;
            }
        }
        
        private Rectangle GetSourceRectangle()
        {
            int row = _player.FacingDirection switch
            {
                Direction.Down => 0,
                Direction.Left => 1,
                Direction.Right => 2,
                Direction.Up => 3,
                _ => 0
            };
            
            return new Rectangle(
                _currentFrame * _frameWidth,
                row * _frameHeight,
                _frameWidth,
                _frameHeight
            );
        }
    }
}