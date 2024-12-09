using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class BombAnimation : MonoBehaviour
    {
        public AudioSource source;
        public WeaponManager weaponManager;
        public void PlaySFX(AudioClip clip)
        {
            source.PlayOneShot(clip, 0.5f);
        }

        public void DeployBomb()
        {
            weaponManager.DeployingBomb();
        }

        public void stopBomb()
        {
            weaponManager.StopBomb();
        }


    }
}
