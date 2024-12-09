using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace BerTaDEV
{
    public class EditorUtilities : MonoBehaviour
    {
        public bool developerMode;
        public GameObject devImpact_prefab;
        public LineRenderer rayLine_prefab;
        [Space]
        public Rect devPostFxRect = new Rect(20, 20, 120, 50);
        public Rect postFXactive = new Rect(20, 20, 120, 50);
        public Rect postFXdeActive = new Rect(20, 20, 120, 50);

        public static EditorUtilities instance;

        private void Awake()
        {
            instance = this;
        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.F8))
            {
                developerMode = !developerMode;
                if (developerMode)
                {

                }
                else
                {
                    Camera.main.GetComponent<PostProcessLayer>().enabled = true;
                }
            }

            if (!developerMode) return;


            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                SceneManager.LoadScene(2);
            }
        }

        public void DrawGameRay(Vector3 start, Vector3 point)
        {
            if (!developerMode) return;

            LineRenderer newRay = Instantiate(rayLine_prefab, Vector3.zero, Quaternion.identity);
            newRay.SetPosition(0, start);
            newRay.SetPosition(1, point);
        }
        public void DevImpact(Vector3 pos)
        {
            if (!developerMode) return;
            Instantiate(devImpact_prefab, pos, Quaternion.identity);
        }
        void OnGUI()
        {
            if (developerMode)
            {
                GUILayout.TextArea("Developer Mode");
                if (SpawnManager.instance != null)
                    GUILayout.TextArea(SpawnManager.instance.skeletons.Count.ToString() + " NPC");
                devPostFxRect = GUI.Window(10, devPostFxRect, PostFXWindow, "PostFX");
            }
        }

        void PostFXWindow(int windowID)
        {
            if (GUI.Button(postFXactive, "Active"))
            {
                Camera.main.GetComponent<PostProcessLayer>().enabled = true;
            }
            if (GUI.Button(postFXdeActive, "Deactive"))
            {
                Camera.main.GetComponent<PostProcessLayer>().enabled = false;
            }
        }
    }
}
