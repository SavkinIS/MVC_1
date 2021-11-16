using Assets.Scripts.Models;

namespace Assets.Scripts.Controllers
{
    class TileController
    {
        TileView tileView;
        TileModel tileModel;

        public TileController(TileView tileView, TileModel tileModel)
        {
            this.tileView = tileView;
            this.tileModel = tileModel;
        }
        public TileView GetTile => tileView;
    }
}
