using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class DestroyOnTime : MonoBehaviour
    {
        public float time;
        void Start()
        {
            Destroy(gameObject, time);
        }
    }
}
