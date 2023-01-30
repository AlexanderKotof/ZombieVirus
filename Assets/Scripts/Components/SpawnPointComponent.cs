using FeatureSystem.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointComponent : MonoBehaviour
{
    public CharacterPrototype prototype;

    void Start()
    {
        var characterSystem = GameSystems.GetSystem<SpawnCharacterSystem>();
        characterSystem.SpawnEnemy(prototype, transform.position, transform.rotation);
    }

}
