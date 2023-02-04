﻿using BuildingSystem.Prototypes;
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

        public event Action<BuildingPrototype> OnBuilded;

        private Coroutine _buildingCoroutine;

        public void Initialize()
        {
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

            OnBuilded?.Invoke(nowBuilds);

            Coroutines.Stop(_buildingCoroutine);

            nowBuilds = null;
        }
    }
}
