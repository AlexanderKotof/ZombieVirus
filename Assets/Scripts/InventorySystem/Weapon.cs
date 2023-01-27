public class Weapon : Item
{
    public float Damage;

    public float Range;

    public float Speed;

    public WeaponType type;

    public BulletComponent BulletPrefab;

    public enum WeaponType
    {
        Melee,
        Ranged,
    }
}
