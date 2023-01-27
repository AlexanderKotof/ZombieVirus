using System.Collections;
using UnityEngine;

public class AttackCommand : Command
{
    public CharacterComponent Target => _target;

    private CharacterComponent _target;
    private CharacterComponent _character;

    private Coroutine _coroutine;

    public AttackCommand(CharacterComponent target)
    {
        _target = target;
    }

    public override void BeginExecute(CharacterComponent character)
    {
        _character = character;
        _coroutine = _character.StartCoroutine(AttackCoroutine());
    }

    public override void StopExecute()
    {
        if (_coroutine != null)
            _character.StopCoroutine(_coroutine);
    }

    private IEnumerator AttackCoroutine()
    {
        while (!_target.IsDied)
        {
            var direction = (_target.Position - _character.Position);
            if (direction.sqrMagnitude > _character.AttackRange * _character.AttackRange)
            {
                _character.MoveTo(_target.Position - direction.normalized * _character.AttackRange * 0.9f);
            }
            else if (_character.lastAttackTime + 1 / _character.AttackSpeed < Time.realtimeSinceStartup)
            {
                _character.Stop();
                _character.transform.rotation = Quaternion.LookRotation(direction);

                Attack();
                _character.lastAttackTime = Time.realtimeSinceStartup;
            }

            yield return null;
        }

        Executed?.Invoke(this);
    }

    private void Attack()
    {
        _character.animator.SetTrigger("Attack");

        Debug.Log($"{_character.Prototype.Name} attacks {_target.Prototype.Name}");

        _target.TakeDamage(_character.Damage);
    }
}
