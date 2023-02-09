using Features.CharactersFeature.Components;
using Features.CharactersFeature.Systems;
using FeatureSystem.Systems;
using PlayerDataSystem;
using PlayerDataSystem.DataStructures;
using System;
using UnityEngine;

namespace Features.GamePlayFeature.Data
{
    [Serializable]
    public class PlayerTeam
    {
        public PlayerComponent[] Characters { get; set; }

        public CharacterData[] charactersData;

    }
}