using UnityEngine;

public class GrenateComponent : MonoBehaviour
{
    public Rigidbody _rigidbody;

    public ParticleSystem particles;

    public void Throw(Vector3 force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(force);
    }

    public void Explode()
    {
        particles.Play();
    }
}
