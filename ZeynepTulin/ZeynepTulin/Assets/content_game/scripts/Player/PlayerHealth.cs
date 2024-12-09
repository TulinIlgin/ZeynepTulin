using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class PlayerHealth : MonoBehaviour
    {
        public float Health;
        public float MaxHealth;
        float addablevolume;
        [Space]
        public float HealCoolDown;
        public float HealTimer;
        public float HealSpeed;
        [Space]
        public Image heal_bar;
        public Text heal_text;
        [Space]
        public GameObject deathCamera;
        [Space]
        public AudioClip[] damageClips;
        public AudioSource source;
        [Space]
        public ParticleSystem particleSystem;
        PostProcessVolume lowhp_volume;

        bool damaged;
        bool heal;
        public void Damage(int damage)
        {
            if (Health <= 0) return;
            HealTimer = -5;
            particleSystem.Play();
            addablevolume += damage;
            Health -= damage;
            heal = false;
            RefreshUI();
            if (Health <= 0)
            {
                Die();
            }
        }
        private void Start()
        {
            lowhp_volume = GameObject.Find("LowHPVolume_DontChangeThisName!").GetComponent<PostProcessVolume>();
        }
        private void Update()
        {
            if (HealTimer >= HealCoolDown)
            {
                Health = Mathf.Lerp(Health, MaxHealth, HealSpeed * Time.deltaTime);
                addablevolume -= Mathf.Lerp(addablevolume, 0.0f, HealSpeed * Time.deltaTime);
                RefreshUI();
            }
            else
            {
                HealTimer += Time.deltaTime;
            }
            lowhp_volume.weight = addablevolume / MaxHealth;

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.K))
            {
                Damage(10);
            }
#endif
        }
        private void RefreshUI()
        {
            heal_bar.fillAmount = Health / MaxHealth;
            heal_text.text = "HP : " + (int)Health;
        }

        private void Die()
        {
            deathCamera.SetActive(true);
            gameObject.SetActive(false);
            deathCamera.transform.SetParent(null);
            SpawnManager.instance.spawn = false;
            GameManager.instance.DiePlayer();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Sword" && !damaged)
            {
                damaged = true;
                Damage(Random.Range(15,25));

                AudioClip rand = damageClips[Random.Range(0, damageClips.Length)];
                source.PlayOneShot(rand);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Sword")
            {
                damaged = false;
            }
        }
    }
}
