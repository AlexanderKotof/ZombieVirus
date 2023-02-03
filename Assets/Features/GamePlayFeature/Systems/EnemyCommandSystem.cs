using Features.CharactersFeature.Components;
using Features.CharactersFeature.Systems;
using FeatureSystem.Systems;

namespace Features.GamePlayFeature.Systems
{
    public class EnemyCommandSystem : ISystem, ISystemUpdate
    {
        private SpawnCharacterSystem _characterSystem;

        public void Initialize()
        {
            _characterSystem = GameSystems.GetSystem<SpawnCharacterSystem>();
        }

        public void Destroy()
        {

        }

        private float detectionRange = 10f;

        public void Update()
        {
            for (int i = 0; i < _characterSystem.spawnedEnemies.Count; i++)
            {
                CharacterComponent enemy = _characterSystem.spawnedEnemies[i];

                if (enemy.CurrentCommand != null)
                    continue;

                foreach (var player in _characterSystem.spawnedCharacters)
                {
                    if ((enemy.Position - player.Position).sqrMagnitude > detectionRange * detectionRange)
                        continue;

                    var command = new AttackCommand(player);
                    enemy.ExecuteCommand(command);
                }

            }
        }
    }
}