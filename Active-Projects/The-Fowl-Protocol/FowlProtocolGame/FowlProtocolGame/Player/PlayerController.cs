using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FowlProtocolGame.Systems.Grid;

namespace FowlProtocolGame.Player
{
    public class PlayerController
    {
        private readonly Player _player;
        private KeyboardState _previousKeyboardState;
        
        public PlayerController(Player player)
        {
            _player = player;
            _previousKeyboardState = Keyboard.GetState();
        }
        
        public void HandleInput()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            
            if (!_player.IsMoving)
            {
                if (currentKeyboardState.IsKeyDown(Keys.W))
                {
                    _player.TryMove(Direction.Up, GridSystem.Instance);
                }
                else if (currentKeyboardState.IsKeyDown(Keys.S))
                {
                    _player.TryMove(Direction.Down, GridSystem.Instance);
                }
                else if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    _player.TryMove(Direction.Left, GridSystem.Instance);
                }
                else if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    _player.TryMove(Direction.Right, GridSystem.Instance);
                }
            }
            
            _previousKeyboardState = currentKeyboardState;
        }
    }
}