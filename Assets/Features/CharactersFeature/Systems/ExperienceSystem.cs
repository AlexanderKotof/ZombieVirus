using Features.CharactersFeature.Components;
using FeatureSystem.Systems;

namespace Features.CharactersFeature.Systems
{
    public class ExperienceSystem : ISystem
    {
        private SpawnCharacterSystem _characterSpawnSystem;

        public int ExperienceCounter { get; private set; } = 0;

        public void Initialize()
        {
            _characterSpawnSystem = GameSystems.GetSystem<SpawnCharacterSystem>();

            CharacterComponent.Died += OnEnemyDied;
        }

        private void OnEnemyDied(CharacterComponent enemy)
        {
            if (!_characterSpawnSystem.spawnedEnemies.Contains(enemy))
                return;

            ExperienceCounter += enemy.Prototype.ExperienceReward;
        }

        public void Destroy()
        {
            CharacterComponent.Died -= OnEnemyDied;
        }
    }
}
