using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaduraEngine.GameContent.MapManagement
{
    public class TileMapManager
    {
        private Texture2D _tileSet;
        private int _tileSetTilesWidth;
        private int _tileWidth;
        private int _tileHeight;
        
        public TileMapManager(Texture2D tileSet, int tileSetTilesWidth, int tileWidth, int tileHeight)
        {
            _tileSet = tileSet;
            _tileSetTilesWidth = tileSetTilesWidth;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }
    }
}