using System;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Collider _collider;
    public TrailRenderer trail;
    public CharacterComponent Shooter { get; private set; }

    private Action<BulletComponent, CharacterComponent> _hitCallback;

    public void Shoot(CharacterComponent shooter, Vector3 direction , Action<BulletComponent, CharacterComponent> hitCallback)
    {
        Shooter = shooter;

        _hitCallback = hitCallback;

        trail.enabled = true;
        _collider.enabled = true;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * 1000f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterComponent>(out var character))
        {
            if (character.Data.prototype.fraction == Shooter.Data.prototype.fraction)
                return;

            _hitCallback.Invoke(this, character);
        }

        _collider.enabled = false;
        trail.enabled = false;

        ObjectSpawnManager.Despawn(this);
    }
}