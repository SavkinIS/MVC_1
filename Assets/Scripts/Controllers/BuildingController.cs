using Assets.Scripts.Models;

namespace Assets.Scripts.Controllers
{
    class BuildingController
    {
        BuildingView buildingView;
        Building buildingModel;

        public BuildingController(BuildingView buildingView, Building buildingModel)
        {
            this.buildingView = buildingView;
            this.buildingModel = buildingModel;
            this.buildingView.TakePlaceOn += this.buildingModel.Placed;
            this.buildingModel.PlacedChanged += this.buildingView.SetPlaced;
            this.buildingView.SetPower(this.buildingModel.power);
            buildingView.SetScaleу(buildingModel.LengthTile, buildingModel.Height, buildingModel.WidthTile);
        }

        public BuildingView BuildingView => buildingView; 
        public Building Buildingmodel => buildingModel; 

    }
}
