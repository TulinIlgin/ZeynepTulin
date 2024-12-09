using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MilkShake;
using EvolveGames;
using BerTaDEV;

namespace bertadev
{
    public class Pistol : MonoBehaviour
    {
        public bool enable;
        public bool auto;
        public bool reloading;
        public WeaponManager weaponManager;
        [Space]
        public Sprite weapon_sprite;
        [Header("VFXs")]
        public ParticleSystem default_impact;
        public ParticleSystem leave_impact;
        public ParticleSystem water_impact;
        public ParticleSystem enemy_impact;
        public ParticleSystem blood_impact;
        [Header("Shooting")]
        public LayerMask shootable;
        float timer;
        public float coolDown;
        public float AddForceValue;
        public Vector2 damage;
        [Header("Pockets")]
        public int ammo_count;
        public int pocket_count;
        public int max_ammo;
        int addable_ammo;
        [Header("Shaking")]
        public Shaker shaker;
        public ShakePreset shake_preset;
        [Header("Visual")]
        public Animator animator;
        public ParticleSystem muzzle;
        [Header("Audio")]
        public AudioClip emptyClip;
        public AudioClip[] shootClips;
        public AudioSource source;
        Ray ray;
        RaycastHit hit;
        Transform cameram;

        private void Start()
        {
            cameram = Camera.main.transform;
        }

        private void Update()
        {
            if (enable)
            {
                if (timer < coolDown && !reloading) { timer += Time.deltaTime; }
                if (!reloading)
                    GetInputs();

                addable_ammo = max_ammo - ammo_count;
                if (addable_ammo > pocket_count) { addable_ammo = pocket_count; }
            }
        }

        public void Show()
        {
            enable = true;
            animator.gameObject.SetActive(true);
            weaponManager.RefreshUI();
        }
        public void Hide()
        {
            animator.Rebind();
            animator.gameObject.SetActive(false);
            reloading = false;
            enable = false;
        }

        private void GetInputs()
        {
            
            if (!auto && Input.GetMouseButtonDown(0) && timer > coolDown)
            {
                timer = 0;
                Shoot();
            }
            if (auto && Input.GetMouseButton(0) && timer > coolDown)
            {
                timer = 0;
                Shoot();
            }
        }

        private void Shoot()
        {
            if (ammo_count <= 0)
            {
                animator.Play("emptyClip");
                source.PlayOneShot(emptyClip);
                return;
            }

            shaker.Shake(shake_preset);
            muzzle.Play();
            AudioClip rand = shootClips[Random.Range(0, shootClips.Length)];
            source.PlayOneShot(rand);
            animator.Play("Shoot");
            ammo_count--;
            weaponManager.RefreshUI();

            //

            ray.origin = cameram.position;
            ray.direction = cameram.forward;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, shootable))
            {
                if (hit.collider)
                {
                    if (hit.rigidbody != null)
                        hit.rigidbody.AddForce(ray.direction * AddForceValue, ForceMode.Impulse);

                    EditorUtilities.instance.DrawGameRay(ray.origin, hit.point);
                    EditorUtilities.instance.DevImpact(hit.point);

                    PlayImpact(hit.collider.tag);
                    if (hit.collider.tag == "HitBox")
                    {
                        hit.collider.GetComponentInParent<AIHealth>().Damage(Random.Range(damage.x, damage.y), ray.direction);
                        ComboManager.instance.Hit();
                    }
                    else
                    {
                        ComboManager.instance.ResetCombo();
                    }
                }
            }
        }

        public void Reloading()
        {
            reloading = false;
            animator.SetBool("reload", false);
            ammo_count = ammo_count + addable_ammo;
            pocket_count = pocket_count - addable_ammo;
            weaponManager.RefreshUI();
        }

        private void PlayImpact(string tag)
        {
            if (tag == "water")
            {
                water_impact.transform.position = hit.point;
                water_impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                water_impact.Play();
            }
            else if (tag == "leave")
            {
                leave_impact.transform.position = hit.point;
                leave_impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                leave_impact.Play();
            }
            else if (tag == "HitBox" && hit.collider.GetComponentInParent<AIHealth>().NPC == AIHealth.npc.skeleton)
            {
                enemy_impact.transform.position = hit.point;
                enemy_impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                enemy_impact.Play();
            }
            else if (tag == "HitBox" && hit.collider.GetComponentInParent<AIHealth>().NPC == AIHealth.npc.zombie)
            {
                blood_impact.transform.position = hit.point;
                blood_impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                blood_impact.Play();
            }
            else
            {
                default_impact.transform.position = hit.point;
                default_impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                default_impact.Play();
            }
        }
    }
}
