using BuildingSystem.Prototypes;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Game Entities/Actions/Add Buildings")]
    public class OpenNewBuildingsAction : ExecuteAction
    {
        public BuildingPrototype[] buildings;

        public override void Execute()
        {
            var manager = BuildingSystem.BuildingManager.Instance;

            manager.AddNewBuildings(buildings);
        }
    }
}
