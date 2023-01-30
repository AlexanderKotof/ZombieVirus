using System;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public event Action<BulletComponent, CharacterComponent> OnHit;

    public CharacterComponent Shooter { get; private set; }

    public void Shoot(CharacterComponent shooter, CharacterComponent target)
    {
        Shooter = shooter;
        var direction = (target.Position - transform.position + Vector3.up).normalized;
        rigidbody.AddForce(direction * 1000f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterComponent>(out var character) && character != Shooter)
        {
            OnHit?.Invoke(this, character);
            Destroy(gameObject);
        }
    }
}