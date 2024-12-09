using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class SlowMoManager : MonoBehaviour
    {
        public float slowMotionTimescale;

        private float startTimescale;
        private float startFixedDeltaTime;

        public static SlowMoManager instance;
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            startTimescale = Time.timeScale;
            startFixedDeltaTime = Time.fixedDeltaTime;
        }

        private void StartSlowMotion()
        {
            Time.timeScale = slowMotionTimescale;
            Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
        }

        private void StopSlowMotion()
        {
            Time.timeScale = startTimescale;
            Time.fixedDeltaTime = startFixedDeltaTime;
        }

        public void SlowMo(float time)
        {
            StartCoroutine(slowmotion(time));
        }
        IEnumerator slowmotion(float time)
        {
            StartSlowMotion();
            yield return new WaitForSeconds(time);
            StopSlowMotion();
            StopCoroutine(slowmotion(0));
        }
    }
}
