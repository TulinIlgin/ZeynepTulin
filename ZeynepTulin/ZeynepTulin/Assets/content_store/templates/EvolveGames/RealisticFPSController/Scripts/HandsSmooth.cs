using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveGames
{
    public class HandsSmooth : MonoBehaviour
    {
        public bool ENABLED;
        [Header("HandsSmooth")]
        [SerializeField] CharacterController CharakterC;
        [SerializeField, Range(1, 10)] public float smooth = 4f;
        [Range(0.001f, 1)] public float amount = 0.03f;
        [Range(0.001f, 1)] public float maxAmount = 0.04f;
        [Header("Rotation")]
        [Range(1, 10)] public float RotationSmooth = 4.0f;
        [Range(0.1f, 10)] public float RotationAmount = 1.0f;
        [Range(0.1f, 10)] public float MaxRotationAmount = 5.0f;
        [Range(0.1f, 10)] public float RotationMovementMultipler = 1.0f;

        [Header("CroughRotation")]
        public bool EnabledCroughRotation = false;
        [Range(0.1f, 20)] public float RotationCroughSmooth = 15.0f;
        [Range(5f, 50)] public float RotationCroughMultipler = 18.0f;

        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;

        float CroughRotation;
        Vector3 InstallPosition;
        Quaternion InstallRotation;
        

        private void Start()
        {
            InstallPosition = transform.localPosition;
            InstallRotation = transform.localRotation;
        }
        private void Update()
        {
            if (ENABLED)
            {
                float InputX = -Input.GetAxis("Mouse X");
                float InputY = -Input.GetAxis("Mouse Y");
                float horizontal = -Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                float moveX = Mathf.Clamp(InputX * amount, -maxAmount, maxAmount);
                float moveY = Mathf.Clamp(InputY * amount, -maxAmount, maxAmount);

                Vector3 finalPosition = new Vector3(moveX, moveY + -CharakterC.velocity.y / 60, 0);

                transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + InstallPosition, Time.deltaTime * smooth);



                float TiltX = Mathf.Clamp(InputX * RotationAmount, -MaxRotationAmount, MaxRotationAmount);
                float TiltY = Mathf.Clamp(InputY * RotationSmooth, -MaxRotationAmount, MaxRotationAmount);
                if (EnabledCroughRotation && Input.GetKey(CroughKey)) CroughRotation = Mathf.Lerp(CroughRotation, RotationCroughMultipler, RotationCroughSmooth * Time.deltaTime);
                else CroughRotation = Mathf.Lerp(CroughRotation, 0f, RotationCroughSmooth * Time.deltaTime);

                Vector3 vector = new Vector3(Mathf.Max(vertical * 0.4f, 0) * RotationMovementMultipler, 0, horizontal * RotationMovementMultipler);
                Vector3 finalRotation = new Vector3(-TiltY, 0, TiltX + CroughRotation) + vector;



                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(finalRotation) * InstallRotation, Time.deltaTime * RotationSmooth);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero + InstallPosition, Time.deltaTime * smooth);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Vector3.zero) * InstallRotation, Time.deltaTime * RotationSmooth);
            }
        }
    }
}