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
        var characterAttackRange = _character.GetAttackRange();
        var characterAttackSpeed = 1 / _character.GetAttackSpeed();

        while (!_target.IsDied)
        {
            var direction = (_target.Position - _character.Position);
            if (direction.sqrMagnitude > characterAttackRange * characterAttackRange)
            {
                _character.MoveTo(_target.Position - direction.normalized * characterAttackRange * 0.9f);
            }
            else if (_character.lastAttackTime + characterAttackSpeed < Time.realtimeSinceStartup)
            {
                _character.Stop();
                _character.transform.rotation = Quaternion.LookRotation(direction);

                Attack();
            }

            yield return null;
        }

        _character.animator.SetBool("IsAttacking", false);

        Executed?.Invoke(this);
    }

    private void Attack()
    {
        _character.lastAttackTime = Time.realtimeSinceStartup;

        _character.animator.SetBool("IsAttacking", true);

        AttackUtils.Attack(_character, _target);
    }
}
