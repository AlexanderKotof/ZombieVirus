using Features.CharactersFeature.Components;
using FeatureSystem.Systems;
using System;
using System.Collections;
using UnityEngine;

public static class AttackUtils
{
    public static float GetAttackSpeed(this CharacterComponent character)
    {
        var data = character.Data;
        return data.weapon ? data.weapon.ShootingSpeed : data.prototype.attackSpeed;
    }

    public static float GetAttackRange(this CharacterComponent character)
    {
        var data = character.Data;
        return data.weapon ? data.weapon.Range : data.prototype.attackRange;
    }

    public static float GetCharacterDamage(this CharacterComponent character)
    {
        var Data = character.Data;
        return Data.weapon ? Data.weapon.Damage : Data.prototype.damage;
    }

    public static float GetCharacterArmor(this CharacterComponent character)
    {
        var Data = character.Data;
        return Data.armor ? Data.armor.defence : 0;
    }

    public static float GetAccuracy(this CharacterComponent character)
    {
        var Data = character.Data;
        return Data.weapon ? Data.weapon.Accuracy : 0.5f;
    }

    public static void Attack(CharacterComponent character, CharacterComponent target)
    {
        var weapon = character.Data.weapon;

        if(weapon == null)
        {
            character.animator.SetTrigger("Attack");
            character.StartCoroutine(AwaitPreAnimation(0.2f, () => Hit(character, target)));
            return;
        }

        if (weapon.type == Weapon.WeaponType.Melee)
        {
            character.animator.SetTrigger(weapon.animationTrigger);
            character.StartCoroutine(AwaitPreAnimation(weapon.preAnimationLenght, () => Hit(character, target)));
        }
        else
        {
            character.animator.SetTrigger(weapon.animationTrigger);
            character.StartCoroutine(AwaitPreAnimation(weapon.preAnimationLenght, Shoot));

            void Shoot()
            {
                var bullet = ObjectSpawnManager.Spawn(weapon.BulletPrefab);
                bullet.transform.position = character.weaponSpawnPoint.position;

                var direction = (target.Position - bullet.transform.position + Vector3.up).normalized;
                var accuracy = UnityEngine.Random.Range(character.GetAccuracy(), 1);

                direction = direction * accuracy + UnityEngine.Random.onUnitSphere * (1 - accuracy) * 0.2f;

                bullet.Shoot(character, direction.normalized, (bullet, target) => Hit(bullet.Shooter, target));
            }
        }
    }

    private static void Hit(CharacterComponent character, CharacterComponent target)
    {
        var damage = character.GetCharacterDamage();
        var defence = target.GetCharacterArmor();

        damage = damage - defence;
        target.TakeDamage(damage);
    }

    private static IEnumerator AwaitPreAnimation(float wait, Action callback)
    {
        yield return TimeUtils.WaitForTime(wait);
        callback.Invoke();
    }
}
