using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FowlProtocolGame.Player;
using FowlProtocolGame.Systems.Grid;
using FowlProtocolGame.Systems.HUD;

namespace FowlProtocolGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    // Game systems
    private GridSystem _gridSystem;
    private ARHudSystem _arHudSystem;
    
    // Player
    private Player.Player _player;
    private Texture2D _tempPlayerTexture;
    private SpriteFont _debugFont;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Initialize grid system (10x10 grid)
        _gridSystem = new GridSystem(10, 10, GraphicsDevice, true);
        
        // Create obstacle example
        _gridSystem.SetCellWalkable(3, 4, false);
        _gridSystem.SetCellWalkable(4, 4, false);
        _gridSystem.SetCellWalkable(5, 4, false);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a temporary placeholder texture for the player
        _tempPlayerTexture = new Texture2D(GraphicsDevice, 32, 32);
        Color[] colorData = new Color[32 * 32];
        for (int i = 0; i < colorData.Length; i++)
            colorData[i] = Color.Yellow;
        _tempPlayerTexture.SetData(colorData);
        
        // Initialize player
        _player = new Player.Player(_tempPlayerTexture);
        
        // Load font for HUD
        // TODO: Replace with actual font loading when content pipeline is set up
        /*
        _debugFont = Content.Load<SpriteFont>("DebugFont");
        _arHudSystem = new ARHudSystem(this, _debugFont);
        */
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update player
        _player.Update(gameTime);
        
        // Update HUD if initialized
        if (_arHudSystem != null)
        {
            _arHudSystem.Update(gameTime, _player.GridPosition, _player.FacingDirection);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Draw grid and player
        _spriteBatch.Begin();
        _gridSystem.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);
        _spriteBatch.End();
        
        // Draw HUD if initialized
        if (_arHudSystem != null)
        {
            _arHudSystem.Draw(_spriteBatch);
        }

        base.Draw(gameTime);
    }
}
