using System.Collections;
using UnityEngine;

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

        granate.Explode();

        var explosionPosition = granate.transform.position;

        var colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent<CharacterComponent>(out var character))
                continue;

            var damage = Mathf.Clamp(maxDamage / (explosionPosition - character.Position).sqrMagnitude, minDamage, maxDamage);

            character.TakeDamage(damage);
        }

        ObjectSpawnManager.Despawn(grenatePrefab);
    }
}
