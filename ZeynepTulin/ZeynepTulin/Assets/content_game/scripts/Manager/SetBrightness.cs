using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace BerTaDEV
{
    public class SetBrightness : MonoBehaviour
    {
        [Range(-1.0f, 1.0f)]public float value;
        PostProcessVolume volume;
        ColorGrading colorGrading;
        private void Start()
        {
            volume = GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out colorGrading);

            if (SceneManager.GetActiveScene().buildIndex == 0) return;

            if (PlayerPrefs.HasKey("brightness"))
                value = PlayerPrefs.GetFloat("brightness");
            else
                value = 0.0f;
            colorGrading.postExposure.value = value;
        }

        public static SetBrightness instance;

        private void Awake()
        {
            instance = this;
        }

        public void SetValue(float _value)
        {
            value = _value;
            PlayerPrefs.SetFloat("brightness", _value);
            colorGrading.postExposure.value = value;
        }
    }
}
