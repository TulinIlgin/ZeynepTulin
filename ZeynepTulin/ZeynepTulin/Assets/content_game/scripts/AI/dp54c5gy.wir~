using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

namespace BerTaDEV
{
    public class AIMovement : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Animator animator;
        [Space]
        public bool Follow;
        public Vector2 randomSpeed;
        public float agent_speed;
        Transform player;
        bool wasmove;
        private void Start()
        {
            wasmove = Follow;
            player = GameObject.FindWithTag("Player").transform;
            agent_speed = Random.Range(randomSpeed.x, randomSpeed.y);
            agent.speed = agent_speed;
            Invoke(nameof(stand), 1.25f);
        }
        private void Update()
        {
            if (GameManager.instance.game_end) return;
            if (Follow && player.gameObject.activeInHierarchy)
            {
                if (agent.isStopped) { agent.isStopped = false; }
                agent.SetDestination(player.position);
            }
            else if (!agent.isStopped) { agent.isStopped = true; }

            if (agent.enabled)
            {
                agent_speed = agent.velocity.magnitude;
                animator.SetFloat("speed", agent_speed);
            }
        }
        public void Stop()
        {
            wasmove = Follow;
            Follow = false;
        }
        public void GO()
        {
            Follow = wasmove;
        }
        private void stand()
        {
            Follow = true;
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }
}
