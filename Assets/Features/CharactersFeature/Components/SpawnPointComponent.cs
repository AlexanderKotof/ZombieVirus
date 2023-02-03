using Features.CharactersFeature.Prototypes;
using Features.CharactersFeature.Systems;
using FeatureSystem.Systems;
using UnityEngine;

namespace Features.CharactersFeature.Components
{
    public class SpawnPointComponent : MonoBehaviour
    {
        public CharacterPrototype prototype;

        void Start()
        {
            var characterSystem = GameSystems.GetSystem<SpawnCharacterSystem>();
            characterSystem.SpawnEnemy(prototype, transform.position, transform.rotation);
        }

    }
}