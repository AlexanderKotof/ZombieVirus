﻿using System;
using System.Collections;
using UnityEngine;

public static class AttackUtils
{
    public static float GetAttackSpeed(this CharacterComponent character)
    {
        var data = character.Data;
        return data.weapon ? data.weapon.Speed : data.prototype.attackSpeed;
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
                var bullet = GameObject.Instantiate(weapon.BulletPrefab, character.weaponSpawnPoint.position, Quaternion.identity);
                bullet.Shoot(character, target);
                bullet.OnHit += OnBulletHit;
            }
        }
    }

    private static void OnBulletHit(BulletComponent bullet, CharacterComponent target)
    {
        bullet.OnHit -= OnBulletHit;
        Hit(bullet.Shooter, target);
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
        yield return new WaitForSeconds(wait);
        callback.Invoke();
    }
}