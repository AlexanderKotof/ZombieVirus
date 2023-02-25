using Features.CharactersFeature.Components;
using Features.GamePlayFeature.Utils;
using Features.SkillsFeature.Components;
using FeatureSystem.Systems;
using System.Collections;
using UnityEngine;

namespace Features.SkillsFeature.Prototypes
{
    [CreateAssetMenu(menuName = "Prototypes/Skill")]
    public class ThrowGrenadeSkill : SkillPrototype
    {
        public GrenateComponent grenatePrefab;

        public float throwingSpeed;

        public float detonationTime;

        public float explosionRadius;

        public float maxDamage;

        public float minDamage;

        public float verticalMultiplier;

        public override IEnumerator Cast(CharacterComponent caster, CharacterComponent target)
        {
            return Cast(caster, target.Position);
        }

        public override IEnumerator Cast(CharacterComponent caster, Vector3 point)
        {
            var granate = ObjectSpawnManager.Spawn(grenatePrefab);
            granate.transform.position = caster.Position + Vector3.up;

            granate.Throw((point - caster.Position + Vector3.up * verticalMultiplier).normalized * throwingSpeed);

            yield return TimeUtils.WaitForTime(detonationTime);

            var explosionPosition = granate.transform.position;

            var dealDamageSystem = GameSystems.GetSystem<DealDamageSystem>();

            var colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<CharacterComponent>(out var character))
                    continue;

                if (character.Prototype.fraction == caster.Prototype.fraction)
                    continue;

                var damage = Mathf.Clamp(maxDamage / (explosionPosition - character.Position).sqrMagnitude, minDamage, maxDamage);

                dealDamageSystem.DealDamage(character, damage);
            }

            granate.Explode();

            yield return TimeUtils.WaitForTime(granate.particles.main.duration);

            ObjectSpawnManager.Despawn(granate);
        }
    }
}