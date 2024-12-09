using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject skeleton_prefab;
        [Space]
        public Vector2 X;
        public Vector2 Z;
        [Space]
        public bool spawn;
        public float Delay;
        public int runtimeMaxSkeleton;
        float timer;
        public static SpawnManager instance;
        public List<GameObject> skeletons;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            if (GameManager.instance.game_end) return;
            if (spawn)
            {
                if (skeletons.Count < runtimeMaxSkeleton)
                    timer += Time.deltaTime;
                else if (timer != 0)
                    timer = 0;

                if(timer > Delay && skeletons.Count < runtimeMaxSkeleton)
                {
                    timer = 0;
                    Spawn();
                    if (Random.Range(0, 100) > 50 && skeletons.Count < runtimeMaxSkeleton)
                    {
                        Spawn();
                    }
                }
            }
            else if (timer != 0) { timer = 0; }
        }
        public void MultiSpawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn();
            }
        }
        public void Spawn()
        {
            Vector3 position = new Vector3(Random.Range(X.x, X.y), 1, Random.Range(Z.x, Z.y));
            GameObject newSkeleton = Instantiate(skeleton_prefab, position, Quaternion.identity);
            skeletons.Add(newSkeleton);
        }
        public void DieAll()
        {
            for (int i = 0; i < skeletons.Count; i++)
            {
                skeletons[i].GetComponent<AIHealth>().DIE();
            }
        }
        public void DestroyThis(GameObject me)
        {
            skeletons.Remove(me);
        }
    }

}
