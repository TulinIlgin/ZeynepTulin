using bertadev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class WeaponManager : MonoBehaviour
    {
        public Image weapon_Image;
        public Text ammo_text;
        public Text bomb_text;
        [Space]
        public int bomb_count;
        public bool isBombing;
        public float bombSpeed;
        public Animator bomb;
        public GameObject bomb_prefab;
        public Transform bomb_spawn;
        [Space]
        public AudioClip getClip;
        public AudioSource source;
        public Pistol[] weapons;
        [Space]
        public int weapon_index;
        [Space]
        public Pistol active_weapon;
        Camera cameram;

        private void Start()
        {
            cameram = Camera.main;
            active_weapon = weapons[0];
            active_weapon.Show();
            weapon_Image.sprite = active_weapon.weapon_sprite;
        }

        private void Update()
        {
            if (GameManager.instance.Paused) return;
            if (Input.GetKeyDown(KeyCode.Alpha1) && weapon_index != 0 && !isBombing)
            {
                changeWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && weapon_index != 1 && !isBombing)
            {
                changeWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.G) && bomb_count > 0 && !isBombing)
            {
                bomb_count--;
                RefreshUI();
                isBombing = true;
                bomb.gameObject.SetActive(true);
                bomb.Play("ThrowBomb");
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].Hide();
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && active_weapon.ammo_count < active_weapon.max_ammo && active_weapon.pocket_count > 0 && !active_weapon.reloading && !isBombing)
            {
                active_weapon.reloading = true;
                active_weapon.animator.SetBool("reload", true);
            }
        }

        private void changeWeapon(int index)
        {
            active_weapon.Hide();
            active_weapon = weapons[index];
            weapon_index = index;
            active_weapon.Show();
            weapon_Image.sprite = active_weapon.weapon_sprite;
        }
        public void DeployingBomb()
        {
            GameObject newbomb = Instantiate(bomb_prefab, bomb_spawn.position, bomb_spawn.rotation);
            newbomb.GetComponent<Rigidbody>().AddForce(cameram.transform.forward * bombSpeed, ForceMode.Impulse);
        }
        public void StopBomb()
        {
            Debug.Log("bombStop");
            isBombing = false;
            active_weapon = weapons[weapon_index];
            active_weapon.Show();
            bomb.gameObject.SetActive(false);
        }
        public void RefreshUI()
        {
            ammo_text.text = active_weapon.ammo_count.ToString() + "/" + active_weapon.pocket_count.ToString();
            bomb_text.text = bomb_count.ToString();
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (hit.tag == "5.56Box")
            {
                if (hit.gameObject.GetComponent<Lootable>().Looted) return;
                source.PlayOneShot(getClip, 0.5f);
                weapons[0].pocket_count += 45;
                RefreshUI();
                hit.gameObject.GetComponent<Lootable>().Deployed();
            }
            if (hit.tag == "9mmBox")
            {
                if (hit.gameObject.GetComponent<Lootable>().Looted) return;
                source.PlayOneShot(getClip, 0.5f);
                weapons[1].pocket_count += 45;
                RefreshUI();
                hit.gameObject.GetComponent<Lootable>().Deployed();
            }
            if (hit.tag == "bombLoot")
            {
                if (hit.gameObject.GetComponent<Lootable>().Looted) return;
                source.PlayOneShot(getClip, 0.5f);
                bomb_count++;
                RefreshUI();
                hit.gameObject.GetComponent<Lootable>().Deployed();
            }
        }
    }
}
