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
        var granate = Instantiate(grenatePrefab, caster.Position + Vector3.up, Quaternion.identity);

        granate.Throw((target.Position - caster.Position + Vector3.up* verticalMultiplier).normalized * throwingSpeed);

        yield return new WaitForSeconds(detonationTime);

        granate.Explode();

        var explosionPosition = granate.transform.position;

        var colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach(var collider in colliders)
        {
            if (!collider.TryGetComponent<CharacterComponent>(out var character))
                continue;

            var damage = Mathf.Clamp(maxDamage / (explosionPosition - character.Position).sqrMagnitude, minDamage, maxDamage);

            character.TakeDamage(damage);
        }

        Destroy(granate.gameObject, 1f);
    }

    public override IEnumerator Cast(CharacterComponent caster, Vector3 point)
    {
        var granate = Instantiate(grenatePrefab, caster.Position + Vector3.up, Quaternion.identity);

        granate.Throw((point - caster.Position + Vector3.up * verticalMultiplier).normalized * throwingSpeed);

        yield return new WaitForSeconds(detonationTime);

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

        Destroy(granate.gameObject, 1f);
    }
}
