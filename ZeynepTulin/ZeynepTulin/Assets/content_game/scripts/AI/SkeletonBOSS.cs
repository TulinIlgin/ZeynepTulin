using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class SkeletonBOSS : MonoBehaviour
    {
        public Animator animator;
        public ParticleSystem HealParticle;
        public Image healthBar;
        public GameObject BossCanvas;
        [Space]
        AIAttack attacker;
        AIHealth health;
        AIMovement mover;
        AISound sounder;
        int singleWait;

        private void Start()
        {
            singleWait = 4;
            attacker = GetComponent<AIAttack>();
            health = GetComponent<AIHealth>();
            mover = GetComponent<AIMovement>();
            sounder = GetComponent<AISound>();
            animator.SetBool("boss", true);
        }

        public void CheckHealth(float _health)
        {
            healthBar.fillAmount = _health / health.maxHealth;
            if(_health <= 4000 && _health > 3000 && singleWait == 4) 
            {
                singleWait = 3;
                Debug.Log("4000 HEALTH");
                StopCoroutine(WAIT(0));
                StartCoroutine(WAIT(5));
            }
            if (_health <= 3000 && _health > 2000 && singleWait == 3)
            {
                singleWait = 2;
                Debug.Log("3000 HEALTH");
                StopCoroutine(WAIT(0));
                StartCoroutine(WAIT(6));
            }
            if (_health <= 2000 && _health > 1000 && singleWait == 2)
            {
                SpawnManager.instance.spawn = true;
                singleWait = 1;
                Debug.Log("2000 HEALTH");
                StopCoroutine(WAIT(0));
                StartCoroutine(WAIT(7));
            }
            if (_health <= 1000 && _health > 0 && singleWait == 1)
            {
                singleWait = 0;
                Debug.Log("1000 HEALTH");
                StopCoroutine(WAIT(0));
                StartCoroutine(WAIT(10));
            }
            if (_health <= 0)
            {
                Debug.Log("DIED BOSS");
                DIE();
            }
        }

        public void SFX(AudioClip clip)
        {
            sounder.source.PlayOneShot(clip);
        }

        private void DIE()
        {
            BossCanvas.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            mover.agent.isStopped = true;
            mover.enabled = false;
            attacker.canAttack = false;
            attacker.isAttack = false;
            SpawnManager.instance.spawn = false;
            SpawnManager.instance.DieAll();
            animator.SetBool("die", true);
            GameManager.instance.DieBoss();
            SlowMoManager.instance.SlowMo(2f);
        }
        private IEnumerator WAIT(int skeletonCount)
        {
            animator.SetBool("wait", true);
            attacker.canAttack = false;
            mover.Stop();
            health.canHealth = false;
            HealParticle.Play();

            yield return new WaitForSeconds(1);
            SpawnManager.instance.MultiSpawn(skeletonCount);
            yield return new WaitForSeconds(5);

            mover.GO();
            attacker.canAttack = true;
            animator.SetBool("wait", false);
            health.canHealth = true;
            HealParticle.Stop();
        }
    }
}
