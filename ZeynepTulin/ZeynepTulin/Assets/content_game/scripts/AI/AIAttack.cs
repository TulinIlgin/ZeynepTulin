using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace BerTaDEV
{
    public class AIAttack : MonoBehaviour
    {
        public bool canAttack;
        [Space]
        public AudioClip[] attackClips;
        public AudioSource attackSource;
        public Animator animator;
        public bool isAttack;
        public float MinDistance;
        float distance;
        Transform player;


        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        private void Update()
        {
            if (GameManager.instance.game_end)
            {
                canAttack = false;
                isAttack = false;
                animator.SetBool("attack", false);
            }
            else
            {
                if (!canAttack) return;
                if (player.gameObject.activeInHierarchy)
                {
                    distance = Vector3.Distance(transform.position, player.position);
                    if (distance < MinDistance)
                    {
                        isAttack = true;
                        animator.SetBool("attack", true);
                    }
                    else if (isAttack)
                    {
                        isAttack = false;
                        animator.SetBool("attack", false);
                    }
                }
                else if (isAttack)
                {
                    isAttack = false;
                    animator.SetBool("attack", false);
                }
            }
        }
        public void PlayAttackSFX()
        {
            AudioClip rand = attackClips[Random.Range(0, attackClips.Length)];
            attackSource.PlayOneShot(rand);
        }
    }
}
