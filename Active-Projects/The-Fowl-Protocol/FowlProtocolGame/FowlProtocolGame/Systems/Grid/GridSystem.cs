using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FowlProtocolGame.Systems.Grid
{
    public class GridSystem
    {
        public static GridSystem Instance { get; private set; }
        
        public const int CellSize = 64;
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        
        private readonly GridCell[,] _cells;
        
        private readonly Texture2D _gridTexture;
        private readonly bool _showGrid;
        
        public GridSystem(int width, int height, GraphicsDevice graphicsDevice, bool showGrid = true)
        {
            GridWidth = width;
            GridHeight = height;
            _cells = new GridCell[width, height];
            _showGrid = showGrid;
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _cells[x, y] = new GridCell
                    {
                        IsWalkable = true,
                        Position = new Vector2(x, y)
                    };
                }
            }
            
            if (_showGrid)
            {
                _gridTexture = new Texture2D(graphicsDevice, 1, 1);
                _gridTexture.SetData(new[] { Color.White });
            }
            
            Instance = this;
        }
        
        public bool IsValidPosition(Vector2 gridPosition)
        {
            int x = (int)gridPosition.X;
            int y = (int)gridPosition.Y;
            
            if (x < 0 || x >= GridWidth || y < 0 || y >= GridHeight)
                return false;
            
            return _cells[x, y].IsWalkable;
        }
        
        public GridCell GetCell(int x, int y)
        {
            if (x >= 0 && x < GridWidth && y >= 0 && y < GridHeight)
                return _cells[x, y];
            
            return null;
        }
        
        public void SetCellWalkable(int x, int y, bool walkable)
        {
            if (x >= 0 && x < GridWidth && y >= 0 && y < GridHeight)
                _cells[x, y].IsWalkable = walkable;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_showGrid || _gridTexture == null) return;
            
            for (int y = 0; y <= GridHeight; y++)
            {
                spriteBatch.Draw(
                    _gridTexture,
                    new Rectangle(0, y * CellSize, GridWidth * CellSize, 1),
                    Color.Gray * 0.5f
                );
            }
            
            for (int x = 0; x <= GridWidth; x++)
            {
                spriteBatch.Draw(
                    _gridTexture,
                    new Rectangle(x * CellSize, 0, 1, GridHeight * CellSize),
                    Color.Gray * 0.5f
                );
            }
            
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (!_cells[x, y].IsWalkable)
                    {
                        spriteBatch.Draw(
                            _gridTexture,
                            new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize),
                            new Color(255, 0, 0, 50)
                        );
                    }
                }
            }
        }
    }
    
    public class GridCell
    {
        public Vector2 Position { get; set; }
        public bool IsWalkable { get; set; }
        public object Contents { get; set; }
    }
}