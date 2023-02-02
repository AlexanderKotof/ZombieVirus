using UnityEngine;

public class Weapon : Item
{
    public float Damage;

    public float Range;

    public float ShootingSpeed;

    public float Accuracy;

    public WeaponType type;

    public BulletComponent BulletPrefab;

    public GameObject weaponPrefab;

    public string animationTrigger;

    public float preAnimationLenght;

    public enum WeaponType
    {
        Melee,
        Ranged,
    }
}
