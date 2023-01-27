using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointComponent : MonoBehaviour
{
    public CharacterPrototype prototype;

    void Start()
    {
        prototype.SpawnCharacter(transform.position, transform.rotation);
    }

}
