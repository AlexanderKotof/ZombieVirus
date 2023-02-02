using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour
{
    public float StartHealth { get; set; }
    public float CurrentHealth { get; set; }

    public Vector3 Position => transform.position;

    public CharacterData Data { get; private set; }
    public bool IsDied => CurrentHealth <= 0;

    public event Action<float> HealthChanged;
    public event Action<CharacterComponent> Died;

    public float lastAttackTime;

    public Animator animator;
    public NavMeshAgent agent;
    public new Collider collider;

    public Transform weaponSpawnPoint;

    private Transform _spawnedWeapon;

    public CharacterComponent target { get; set; }

    public Command CurrentCommand { get; set; }

    public void MoveTo(Vector3 destination)
    {
        if (!agent.enabled)
            return;

        agent.isStopped = false;
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        if (!agent.enabled)
            return;

        agent.isStopped = true;
    }

    public void SetData(CharacterData data)
    {
        Data = data;

        StartHealth = CurrentHealth = data.prototype.health;

        agent.speed = data.prototype.moveSpeed;

        if (data.weapon)
        {
            _spawnedWeapon = ObjectSpawnManager.Spawn(data.weapon.weaponPrefab.transform);

            _spawnedWeapon.SetParent(weaponSpawnPoint);
            _spawnedWeapon.position = Vector3.zero;
            _spawnedWeapon.rotation = Quaternion.identity;
        }
    }

    public void TakeDamage(float damage)
    {
        if (IsDied)
            return;

        CurrentHealth -= damage;
        HealthChanged?.Invoke(CurrentHealth);

        if (IsDied)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        if (IsDied)
            return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, StartHealth);
        HealthChanged?.Invoke(CurrentHealth);
    }

    private void Die()
    {
        Died?.Invoke(this);
        animator.SetTrigger("Die");

        collider.enabled = false;
        agent.enabled = false;

        Stop();

        if (CurrentCommand != null)
        {
            CurrentCommand.StopExecute();
        }
    }

    private void Update()
    {
        if (IsDied)
            return;

        bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
    }

}
