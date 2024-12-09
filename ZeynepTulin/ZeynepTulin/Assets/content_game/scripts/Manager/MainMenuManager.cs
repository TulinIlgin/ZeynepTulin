using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class MainMenuManager : MonoBehaviour
    {
        public Slider progressBar;
        [Space]
        public int quality_index;
        public Dropdown quality_dropdown;
        [Space]
        public float sensitivity;
        public Slider sensitivity_slider;
        [Space]
        public float brightness;
        public Slider brightness_slider;
        [Space]
        public float volvalue;
        public AudioMixer mixer;
        public Slider soundslider;

        private void Start()
        {
            quality_index = PlayerPrefs.GetInt("Quality");
            quality_dropdown.value = quality_index;

            if (PlayerPrefs.HasKey("sensitivity"))
            {
                sensitivity = PlayerPrefs.GetFloat("sensitivity");
                sensitivity_slider.value = sensitivity;
            }
            else
            {
                sensitivity = 2.0f;
                sensitivity_slider.value = sensitivity;
                PlayerPrefs.SetFloat("sensitivity", 2.0f);
            }

            if (PlayerPrefs.HasKey("brightness"))
            {
                brightness = PlayerPrefs.GetFloat("brightness");
                brightness_slider.value = brightness;
                SetBrightness.instance.SetValue(brightness);
            }
            else
            {
                brightness = 0.0f;
                brightness_slider.value = brightness;
                PlayerPrefs.SetFloat("brightness", 0.0f);
                SetBrightness.instance.SetValue(brightness);
            }

            if (PlayerPrefs.HasKey("sfxValue"))
            {
                volvalue = PlayerPrefs.GetFloat("sfxValue");
                soundslider.value = volvalue;
                mixer.SetFloat("sfx", Mathf.Log10(volvalue) * 20);
            }
            else
            {
                soundslider.value = 1;
                mixer.SetFloat("sfx", Mathf.Log10(volvalue) * 20);
            }

        }
        public void LoadScene(int sceneId)
        {
            StartCoroutine(startLoading(sceneId));
        }

        IEnumerator startLoading(int sceneId)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneId);
            while(!async.isDone)
            {
                progressBar.value = async.progress;
                yield return null;
            }
        }
        public void SetBrightnessB(float value)
        {
            brightness = value;
            SetBrightness.instance.SetValue(value);
        }
        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
            quality_index = index;
            PlayerPrefs.SetInt("Quality", index);
        }

        public void SetSensitivity(float value)
        {
            sensitivity = value;
            PlayerPrefs.SetFloat("sensitivity", sensitivity);
        }

        public void SetSFX(float slidervalue)
        {
            volvalue = slidervalue;
            mixer.SetFloat("sfx", Mathf.Log10(slidervalue) * 20);
            PlayerPrefs.SetFloat("sfxValue", slidervalue);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}
