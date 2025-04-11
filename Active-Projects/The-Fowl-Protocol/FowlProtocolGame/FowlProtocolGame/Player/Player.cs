using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FowlProtocolGame.Systems.Grid;

namespace FowlProtocolGame.Player
{
    public class Player
    {
        public Vector2 GridPosition { get; private set; }
        public Direction FacingDirection { get; private set; }
        public bool IsMoving { get; private set; }
        
        private readonly PlayerController _controller;
        private readonly PlayerRenderer _renderer;
        
        private Vector2 _visualPosition;
        private float _movementProgress;
        private const float MovementSpeed = 5.0f;
        
        public Player(Texture2D sprite)
        {
            GridPosition = new Vector2(0, 0);
            FacingDirection = Direction.Down;
            IsMoving = false;
            
            _visualPosition = new Vector2(0, 0);
            _movementProgress = 0f;
            
            _controller = new PlayerController(this);
            _renderer = new PlayerRenderer(this, sprite);
        }
        
        public void Update(GameTime gameTime)
        {
            _controller.HandleInput();
            
            if (IsMoving)
            {
                _movementProgress += (float)gameTime.ElapsedGameTime.TotalSeconds * MovementSpeed;
                
                if (_movementProgress >= 1.0f)
                {
                    _movementProgress = 0f;
                    _visualPosition = GridPosition;
                    IsMoving = false;
                }
                else
                {
                    Vector2 targetPos = GridPosition;
                    Vector2 startPos = _visualPosition;
                    _visualPosition = Vector2.Lerp(startPos, targetPos, _movementProgress);
                }
            }
            
            _renderer.UpdateAnimation(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            _renderer.Draw(spriteBatch, _visualPosition);
        }
        
        public bool TryMove(Direction direction, GridSystem grid)
        {
            if (IsMoving) return false;
            
            FacingDirection = direction;
            Vector2 targetPosition = GetAdjacentPosition(direction);
            
            if (grid.IsValidPosition(targetPosition))
            {
                GridPosition = targetPosition;
                IsMoving = true;
                return true;
            }
            
            return false;
        }
        
        private Vector2 GetAdjacentPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return new Vector2(GridPosition.X, GridPosition.Y - 1);
                case Direction.Down: return new Vector2(GridPosition.X, GridPosition.Y + 1);
                case Direction.Left: return new Vector2(GridPosition.X - 1, GridPosition.Y);
                case Direction.Right: return new Vector2(GridPosition.X + 1, GridPosition.Y);
                default: return GridPosition;
            }
        }
    }
    
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}