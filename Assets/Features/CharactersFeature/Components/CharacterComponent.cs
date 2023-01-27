using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour
{
    public float StartHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }
    public float AttackSpeed { get; private set; }

    public Vector3 Position => transform.position;

    public CharacterPrototype Prototype { get; private set; }
    public bool IsDied => CurrentHealth <= 0;

    public event Action<float> HealthChanged;
    public event Action<CharacterComponent> Died;

    public float lastAttackTime;

    public Animator animator;
    public NavMeshAgent agent;

    public Command CurrentCommand { get; private set; }

    public void ExecuteCommand(Command command)
    {
        if (CurrentCommand != null)
        {
            CurrentCommand.Executed -= CommandExecuted;
            CurrentCommand.StopExecute();
        }

        CurrentCommand = command;
        command.BeginExecute(this);

        command.Executed += CommandExecuted;
    }

    private void CommandExecuted(Command command)
    {
        Debug.Log($"{command.GetType().Name} executed");

        CurrentCommand.Executed -= CommandExecuted;
        CurrentCommand = null;
    }

    public void MoveTo(Vector3 destination)
    {
        agent.isStopped = false;
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public void SetPrototype(CharacterPrototype prototype)
    {
        Prototype = prototype;

        CurrentHealth = StartHealth = prototype.health;
        Damage = prototype.damage;

        agent.speed = prototype.moveSpeed;
        AttackRange = prototype.attackRange;
        AttackSpeed = prototype.attackSpeed;
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

    private void Die()
    {
        Died?.Invoke(this);
        animator.SetTrigger("Die");

        Stop();

        if (CurrentCommand != null)
        {
            CurrentCommand.Executed -= CommandExecuted;
            CurrentCommand.StopExecute();
        }
    }

    private void Update()
    {
        if (IsDied)
            return;

        bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }
}
