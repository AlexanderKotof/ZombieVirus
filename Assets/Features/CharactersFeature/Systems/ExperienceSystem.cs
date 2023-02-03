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

            foreach (var enemy in _characterSpawnSystem.spawnedEnemies)
            {
                enemy.Died += OnEnemyDied;
            }
        }

        private void OnEnemyDied(CharacterComponent enemy)
        {
            enemy.Died -= OnEnemyDied;
            ExperienceCounter += enemy.Data.prototype.ExperienceReward;
        }

        public void Destroy()
        {

        }
    }
}
