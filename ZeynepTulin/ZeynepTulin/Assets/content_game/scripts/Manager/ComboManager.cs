using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class ComboManager : MonoBehaviour
    {
        public int combo;
        public int kill;
        public int score;
        public Text scoreText_i;
        public Text comboText_i;
        public Text killText_i;
        public static ComboManager instance;
        public CFXR_ParticleText hit_textB;
        public ParticleSystem hit_textPS;
        [Space]
        public CFXR_ParticleText kill_textB;
        public ParticleSystem kill_textPS;
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.H))
            {
                Hit();
            }
#endif
        }
        public void FindVariables()
        {
            scoreText_i = GameObject.Find("ScoreText_i").GetComponent<Text>();
            comboText_i = GameObject.Find("ComboText_i").GetComponent<Text>();
            killText_i = GameObject.Find("KillText_i").GetComponent<Text>();

            hit_textB = GameObject.Find("hit_text_i").GetComponent<CFXR_ParticleText>();
            hit_textPS = GameObject.Find("hit_text_i").GetComponent<ParticleSystem>();

            kill_textB = GameObject.Find("kill_text_i").GetComponent<CFXR_ParticleText>();
            kill_textPS = GameObject.Find("kill_text_i").GetComponent<ParticleSystem>();
        }
        public void Hit()
        {
            combo++;
            comboText_i.text = "COMBO : x" + combo;
            hit_textPS.Stop();
            string comboText = "x" + combo + " HIT";
            hit_textB.rotation = Random.Range(-15, 15);
            hit_textB.UpdateText(comboText);
            hit_textPS.Play();
            
        }
        public void Kill()
        {
            kill++;

            if (combo > 0)
                score += 2 * combo;
            else
                score += 2;

            GameManager.instance.CheckScore(score);
            scoreText_i.text = "SCORE : " + score.ToString();
            killText_i.text = "KILL : " + kill.ToString();
            kill_textPS.Stop();
            string comboText = "x" + kill + " KILL!";
            kill_textB.rotation = Random.Range(-15, 15);
            kill_textB.UpdateText(comboText);
            kill_textPS.Play();
        }
        public void ResetCombo()
        {
            combo = 0;
            comboText_i.text = "COMBO : x" + combo;
        }
    }
}
