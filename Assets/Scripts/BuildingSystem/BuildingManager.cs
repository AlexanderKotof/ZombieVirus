using BuildingSystem.Prototypes;
using System.Collections.Generic;

namespace BuildingSystem
{
    public class BuildingManager : Singletone<BuildingManager>
    {
        public List<BuildingPrototype> readyForBuild;
        public List<BuildingPrototype> builded;

        public void Initialize()
        {

        }
    }
}
