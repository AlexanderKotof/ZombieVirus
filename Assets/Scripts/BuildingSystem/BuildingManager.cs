using BuildingSystem.Prototypes;
using BuildingSystem.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingManager : Singletone<BuildingManager>
    {
        public List<BuildingPrototype> readyForBuild;
        public List<BuildingPrototype> builded;

        public BuildingPrototype nowBuilds;
        public DateTime startedAt;

        public event Action BuildingsUpdated;

        private Coroutine _buildingCoroutine;

        public void Initialize()
        {
            /*
            var buildingsData = PlayerDataSystem.PlayerDataManager.Data.buildingsData;

            builded = new List<BuildingPrototype>(buildingsData.buildedIds.Length);
            foreach (var id in buildingsData.buildedIds)
            {
                builded.Add(BuildingUtils.GetItem(id));
            }

            readyForBuild = new List<BuildingPrototype>(buildingsData.readyToBuildIds.Length);
            foreach (var id in buildingsData.readyToBuildIds)
            {
                readyForBuild.Add(BuildingUtils.GetItem(id));
            }

            if (buildingsData.buldingProgressId != -1)
            {
                nowBuilds = BuildingUtils.GetItem(buildingsData.buldingProgressId);
                startedAt = buildingsData.startedAt;

                _buildingCoroutine = Coroutines.Run(BuildingCoroutine());
            }
            */
        }

        public bool IsBuilds(BuildingPrototype building, out int timeLeftSec)
        {
            if (nowBuilds == building)
            {
                timeLeftSec = nowBuilds.buildingTimeSec - Mathf.FloorToInt((float)DateTime.Now.Subtract(startedAt).TotalSeconds);
                return true;
            }

            timeLeftSec = 0;
            return false;
        }

        public bool CanBuild(BuildingPrototype building)
        {
            if (nowBuilds != null)
                return false;

            if (!readyForBuild.Contains(building))
                return false;
            
            foreach(var resource in building.requiredResources)
            {
                if (!PlayerInventoryManager.Instance.PlayerInventory.HasItem(resource.Item, resource.Count))
                    return false;
            }

            return true;
        }

        public void Build(BuildingPrototype building)
        {
            if (!CanBuild(building))
                return;

            nowBuilds = building;
            startedAt = DateTime.Now;

            _buildingCoroutine = Coroutines.Run(BuildingCoroutine());

            BuildingsUpdated?.Invoke();
        }

        public void AddNewBuildings(BuildingPrototype[] buildings)
        {
            readyForBuild.AddRange(buildings);
            BuildingsUpdated?.Invoke();
        }

        private IEnumerator BuildingCoroutine()
        {
            while(nowBuilds != null)
            {
                yield return null;

                if (DateTime.Now.Subtract(startedAt).TotalSeconds >= nowBuilds.buildingTimeSec)
                {
                    Builded();
                }
            }
        }

        private void Builded()
        {
            readyForBuild.Remove(nowBuilds);
            builded.Add(nowBuilds);

            foreach (var action in nowBuilds.OnBuildedActions)
            {
                action.Execute();
            }

            Coroutines.Stop(_buildingCoroutine);

            nowBuilds = null;

            BuildingsUpdated?.Invoke();
        }
    }
}
