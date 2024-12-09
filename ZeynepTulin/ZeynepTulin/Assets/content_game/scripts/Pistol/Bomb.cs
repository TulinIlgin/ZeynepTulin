using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class Bomb : MonoBehaviour
    {
        public float ExplodeTime;
        public float ExplodeRadius;
        public float ExplodePower;
        public float ShakeDistance;
        public ShakePreset ExplodeShake;
        public GameObject ExplodeObject;
        public Vector2 Damage;
        Shaker shaker;
        private IEnumerator Start()
        {
            shaker = FindObjectOfType<Shaker>();
            yield return new WaitForSeconds(ExplodeTime);

            Instantiate(ExplodeObject, transform.position + new Vector3(0,0.3f,0), Quaternion.identity);

            if (Vector3.Distance(transform.position, shaker.transform.position) < ShakeDistance)
            {
                shaker.Shake(ExplodeShake);
            }

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplodeRadius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(ExplodePower, explosionPos, ExplodeRadius, 3.0F, ForceMode.Impulse);

                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth)
                    playerHealth.Damage((int)Random.Range(Damage.x, Damage.y));

                AIHealth aiHealth = hit.GetComponent<AIHealth>();
                if (aiHealth)
                    aiHealth.Damage(Random.Range(Damage.x, Damage.y), transform.position);
            }

            Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, ExplodeRadius);
        }
#endif
    }
}
