using System.Collections;
using UnityEngine;

public abstract class SkillPrototype : ScriptableObject, IHasId
{
    public int Id;
    int IHasId.Id => Id;

    public string skillName;

    public Sprite icon;

    public int requiredLevel;

    public float cooldown;

    public SkillPrototype nextLevelPrototype;

    public abstract IEnumerator Cast(CharacterComponent caster, CharacterComponent target);
    public abstract IEnumerator Cast(CharacterComponent caster, Vector3 target);
}
