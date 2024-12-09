using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace BerTaDEV
{
    public class DynamicVignette : MonoBehaviour
    {
        [SerializeField] 
        PostProcessVolume volume;
        Vignette _vignette;
        [Range(-1.0f, 1.0f)] public float mouse_x = 0.0f;
        public Vector2 leftCenter;
        public Vector2 RightCenter;
        [Space]
        public float speed;
        Vector2 awakeCenter;
        private void Start()
        {
            volume.profile.TryGetSettings(out _vignette);
            awakeCenter = new Vector2(0.5f, 0.5f);
        }

        private void Update()
        {
            mouse_x = Input.GetAxisRaw("Mouse X");

            if (mouse_x != 0)
            {
                if (mouse_x < 0)
                {
                    _vignette.center.value = Vector2.Lerp(_vignette.center, leftCenter, speed * Time.deltaTime);
                }
                if (mouse_x > 0)
                {
                    _vignette.center.value = Vector2.Lerp(_vignette.center, RightCenter, speed * Time.deltaTime);
                }
            }
            else
            {
                _vignette.center.value = Vector2.Lerp(_vignette.center, awakeCenter, speed * Time.deltaTime);
            }
        }
    }
}
