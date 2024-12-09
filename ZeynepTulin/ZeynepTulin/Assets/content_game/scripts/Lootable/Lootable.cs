using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class Lootable : MonoBehaviour
    {
        public bool Looted;
        public float spawnTime;
        public Vector2 X;
        public Vector2 Z; 
        [Space]
        public GameObject f_object;
        SphereCollider sphereCollider;
        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            Spawn();
        }

        public void Deployed()
        {
            Looted = true;
            f_object.SetActive(false);
            sphereCollider.enabled = false;
            StopCoroutine(spawnforWait());
            StartCoroutine(spawnforWait());
        }

        IEnumerator spawnforWait()
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }

        public void Spawn()
        {
            Looted = false;
            sphereCollider.enabled = true;
            f_object.SetActive(true);
            Vector3 position = new Vector3(Random.Range(X.x, X.y), 3.0f, Random.Range(Z.x, Z.y));
            transform.position = position;
        }
    }
}
