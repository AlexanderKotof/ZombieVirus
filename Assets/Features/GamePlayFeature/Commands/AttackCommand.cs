using Features.CharactersFeature.Components;
using FeatureSystem.Systems;
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
        _character.target = null;

        _character.animator.SetBool("IsAttacking", false);

        if (_coroutine != null)
            _character.StopCoroutine(_coroutine);
    }

    private IEnumerator AttackCoroutine()
    {
        var characterAttackRange = _character.GetAttackRange();
        var characterAttackSpeed = 1 / _character.GetAttackSpeed();

        _character.target = Target;

        while (!_target.IsDied)
        {

            var direction = (_target.Position - _character.Position);
            if (direction.sqrMagnitude > characterAttackRange * characterAttackRange)
            {
                _character.MoveTo(_target.Position - direction.normalized * characterAttackRange * 0.9f);
            }
            else
            {
                var timeSinceStart = TimeUtils.GetTimeSinceStart();
                if (_character.lastAttackTime + characterAttackSpeed < timeSinceStart)
                {
                    _character.Stop();
                    _character.transform.rotation = Quaternion.LookRotation(direction);

                    _character.lastAttackTime = timeSinceStart;

                    Attack();
                }
            }
            yield return null;
        }

        _character.animator.SetBool("IsAttacking", false);

        _character.target = null;

        Executed?.Invoke(this);
    }

    private void Attack()
    {
        _character.animator.SetBool("IsAttacking", true);

        AttackUtils.Attack(_character, _target);
    }
}
