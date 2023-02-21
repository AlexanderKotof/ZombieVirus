using Features.CharactersFeature.Components;
using Features.GamePlayFeature;
using FeatureSystem.Systems;
using System;

public class DealDamageSystem : ISystem
{
    public static event Action<CharacterComponent> OnHeadshot;
    public static event Action<CharacterComponent, float, bool> OnDamageTaken;

    private GamePlayFeature _gamePlayFeature;

    public DealDamageSystem(GamePlayFeature gamePlayFeature)
    {
        _gamePlayFeature = gamePlayFeature;
    }

    public void Destroy()
    {
    }

    public void Initialize()
    {
    }

    public float CalculateAccuracy(CharacterComponent character, CharacterComponent target)
    {
        return UnityEngine.Random.Range(character.GetAccuracy(), 1);
    }

    public bool IsHeadshot(float accuracyChance)
    {
        return accuracyChance >= _gamePlayFeature.accuracyChanceForHeadshotReaquired;
    }

    public void HeadShot(CharacterComponent character, CharacterComponent target)
    {
        var damage = character.GetCharacterDamage();
        var defence = target.GetCharacterArmor();

        damage = damage * _gamePlayFeature.headShotDamageMultiplier - defence;

        OnHeadshot?.Invoke(target);
        DealDamage(target, damage);
    }

    public void Hit(CharacterComponent character, CharacterComponent target)
    {
        var damage = character.GetCharacterDamage();
        var defence = target.GetCharacterArmor();

        damage = damage - defence;
        DealDamage(target, damage);
    }

    public void DealDamage(CharacterComponent target, float damage)
    {
        target.TakeDamage(damage);
        OnDamageTaken?.Invoke(target, damage);
    }
}