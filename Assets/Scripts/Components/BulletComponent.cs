using System;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Collider _collider;
    public event Action<BulletComponent, CharacterComponent> OnHit;

    public TrailRenderer trail;

    public CharacterComponent Shooter { get; private set; }

    public void Shoot(CharacterComponent shooter, Vector3 direction)
    {
        Shooter = shooter;

        trail.enabled = true;
        _collider.enabled = true;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * 1000f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterComponent>(out var character))
        {
            if (character != Shooter)
                OnHit?.Invoke(this, character);
            else
                return;
        }

        _collider.enabled = false;
        trail.enabled = false;

        ObjectSpawnManager.Despawn(this);
    }
}