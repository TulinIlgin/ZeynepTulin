using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BerTaDEV
{
    public class GameManager : MonoBehaviour
    {
        public bool Paused;
        public bool game_end;
        public GameObject pauseCanvas;
        public Text endText;
        [Space]
        public Transform spawnpoint;
        public GameObject PlayerPrefab;
        public GameObject activePlayerPrefab;
        [Space]
        public Animator gameCanvas;
        [Space]
        public GameObject Boss;
        public bool ActiveBOSS;
        [Space]
        public AudioClip bossTimeClip;
        public AudioClip bossDieClip;
        public AudioSource source;
        [Space]
        public int requireScore;


        public static GameManager instance;
        private void Awake()
        {
            instance = this;
            SpawnPlayer();
        }
        private void SpawnPlayer()
        {
            activePlayerPrefab = Instantiate(PlayerPrefab, spawnpoint.position, spawnpoint.rotation);
        }

        private void Start()
        {
            ComboManager.instance.FindVariables();
        }
        public void LoadScene(int index)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1;
            SceneManager.LoadScene(index);
        }

        private void Update()
        {
            if (game_end) return;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Paused = !Paused;
                pauseCanvas.SetActive(Paused);
                Time.timeScale = Paused ? 0 : 1;
                Cursor.visible = Paused;
                Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
        public void ResumeGameButton()
        {
            Paused = false;
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = Paused;
            Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Locked;
        }
        public void CheckScore(int score)
        {
            if (ActiveBOSS) return;
            if (score > requireScore)
            {
                SFX(bossTimeClip, 1);
                ActiveBOSS = true;
                Boss.SetActive(true);
                SpawnManager.instance.spawn = false;
            }
        }
        public void DieBoss()
        {
            SFX(bossDieClip, 1);
            StartCoroutine(EndScreen("MISSION COMPLETED"));
        }
        public void SFX(AudioClip clip, float volume)
        {
            source.PlayOneShot(clip, volume);
        }

        public void DiePlayer()
        {
            StartCoroutine(EndScreen("YOU LOST"));
        }

        IEnumerator EndScreen(string text)
        {
            game_end = true;
            yield return new WaitForSeconds(4);
            Paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameCanvas.Play("End");
            endText.text = text;
        }
    }
}
