using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointComponent : MonoBehaviour
{
    public CharacterData data;

    void Start()
    {
        CharacterSpawnUtils.SpawnCharacter(data, transform.position, transform.rotation);
    }

}
