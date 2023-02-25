using Features.CharactersFeature.Prototypes;
using PlayerDataSystem.DataStructures;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Features.CharactersFeature.Components
{
    public class CharacterComponent : MonoBehaviour
    {
        public float StartHealth => Data.maxHealth;
        public float CurrentHealth => Data.currentHealth;

        public Vector3 Position => transform.position;

        public CharacterData Data { get; private set; }
        public CharacterPrototype Prototype { get; private set; }

        public Weapon Weapon { get; private set; }

        public Armor Armor { get; private set; }

        public bool IsDied => CurrentHealth <= 0;

        public event Action<float> HealthChanged;
        public static event Action<CharacterComponent> Died;

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

        public void SetData(CharacterData data, CharacterPrototype prototype)
        {
            Data = data;

            Prototype = prototype;

            agent.speed = prototype.moveSpeed;

            if (data.weaponId != -1)
            {
                if (_spawnedWeapon != null)
                    ObjectSpawnManager.Despawn(_spawnedWeapon);

                Weapon = InventoryUtils.GetItem(data.weaponId) as Weapon;
                _spawnedWeapon = ObjectSpawnManager.Spawn(Weapon.weaponPrefab.transform);

                _spawnedWeapon.SetParent(weaponSpawnPoint);
                _spawnedWeapon.localPosition = Vector3.zero;
                _spawnedWeapon.localRotation = Quaternion.identity;
            }

            if (data.armorId != -1)
            {
                Armor = InventoryUtils.GetItem(data.armorId) as Armor;
            }
        }

        public void TakeDamage(float damage)
        {
            if (IsDied)
                return;

            Data.currentHealth -= damage;
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

            Data.currentHealth = Mathf.Clamp(CurrentHealth + value, 0, StartHealth);
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
}