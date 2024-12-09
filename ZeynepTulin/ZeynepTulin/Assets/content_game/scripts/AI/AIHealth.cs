using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class AIHealth : MonoBehaviour
    {
        public npc NPC;
        public Animator animator;
        public bool canHealth;
        public float health;
        public float maxHealth;
        public ParticleSystem destroy_vfx;
        public ParticleSystem die_vfx;
        public Rigidbody hips;
        public float addforceDie;
        AIMovement aiMove;
        AIAttack aiAttack;
        AISound aiSound;
        Rigidbody[] rigidbodies;
        Vector3 dir;
        SkeletonBOSS boss;
        public enum npc
        {
            skeleton,
            zombie
        }

        private void Start()
        {
            canHealth = true;
            if (GetComponent<SkeletonBOSS>())
                boss = GetComponent<SkeletonBOSS>();
            aiSound = GetComponent<AISound>();
            aiAttack = GetComponent<AIAttack>();
            aiMove = GetComponent<AIMovement>();
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            setragdoll(false);
            aiSound.playSpawnClip();
        }
        private void setragdoll(bool value)
        {
            animator.enabled = !value;
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = !value;
            }
            if (value)
            {
                hips.AddForce(dir * addforceDie, ForceMode.Impulse);
            }
        }
        public void Damage(float damage, Vector3 direction)
        {
            if (!canHealth) return;
            aiSound.playDamageClip();
            if (health <= 0) return;
            dir = direction;
            health -= damage;
            animator.Play("Skeleton@Damage01");
            if (boss)
                boss.CheckHealth(health);
            else
                CheckDie();
        }
        private void CheckDie()
        {
            if (health <= 0)
            {
                SpawnManager.instance.DestroyThis(gameObject);
                die_vfx.Play();
                aiSound.playDieClip();
                GetComponent<CapsuleCollider>().enabled = false;
                aiMove.agent.isStopped = true;
                aiMove.enabled = false;
                aiAttack.canAttack = false;
                aiAttack.isAttack = false;
                setragdoll(true);
                StartCoroutine(DestroyMe());
                ComboManager.instance.Kill();
                if (Random.Range(0, 100) > 70)
                {
                    SlowMoManager.instance.SlowMo(0.5f);
                }
            }
        }
        public void DIE()
        {
            SpawnManager.instance.DestroyThis(gameObject);
            die_vfx.Play();
            aiSound.playDieClip();
            GetComponent<CapsuleCollider>().enabled = false;
            aiMove.agent.isStopped = true;
            aiMove.enabled = false;
            aiAttack.canAttack = false;
            aiAttack.isAttack = false;
            setragdoll(true);
            StartCoroutine(DestroyMe());
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(5);
            Instantiate(destroy_vfx, hips.position, Quaternion.Euler(-90.0f, 0, 0));
            Destroy(gameObject);
        }
    }
}
