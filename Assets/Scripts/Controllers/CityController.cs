using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Controllers
{
    class CityController
    {
        CityModel cityModel;
        CityView cityView;
        List<BuildingController> buildingControllers = new List<BuildingController>();

        public CityController(CityModel cityModel, CityView cityView)
        {
            this.cityModel = cityModel;
            this.cityView = cityView;
            this.cityModel.AddPower += this.cityView.SetPower;
            
            this.cityView.BuildingON += (bv) =>
            {
                
                Building buildingModel = cityModel.GetBuilding();
                BuildingController buildingController = new BuildingController(bv,  buildingModel);
                buildingControllers.Add(buildingController);
            };

            this.cityView.GetCityManager.EditorChangeOff += () =>
            {
                var buildContr = buildingControllers.Last();
                if (!buildContr.BuildingView.IsPlaced)
                {
                    buildContr.BuildingView.DestroyBuild();
                    this.cityModel.GetBuildings.Remove(buildContr.Buildingmodel);
                    buildingControllers.Remove(buildContr);
                }
                else this.cityModel.AddBuilding(buildContr.Buildingmodel);
            };
        }
    }
}
