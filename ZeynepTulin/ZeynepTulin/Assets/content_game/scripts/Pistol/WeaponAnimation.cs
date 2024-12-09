using bertadev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class WeaponAnimation : MonoBehaviour
    {
        public AudioSource source;
        [Space]
        public Pistol weapon;


        public void PlaySFX(AudioClip clip)
        {
            source.PlayOneShot(clip, 0.5f);
        }
        public void StopReload()
        {
            weapon.Reloading();
        }
    }
}
