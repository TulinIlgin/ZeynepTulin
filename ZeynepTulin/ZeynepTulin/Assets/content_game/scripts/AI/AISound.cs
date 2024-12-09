using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class AISound : MonoBehaviour
    {
        public AudioSource source;

        [Space]
        public AudioClip[] spawn_clip;
        public AudioClip[] damage_clip;
        public AudioClip[] die_clip;


        public void playSpawnClip()
        {
            AudioClip clip = spawn_clip[Random.Range(0, spawn_clip.Length)];
            source.PlayOneShot(clip);
        }
        public void playDamageClip()
        {
            AudioClip clip = damage_clip[Random.Range(0, damage_clip.Length)];
            source.PlayOneShot(clip);
        }
        public void playDieClip()
        {
            AudioClip clip = die_clip[Random.Range(0, die_clip.Length)];
            source.PlayOneShot(clip);
        }

    }
}
