using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] InputReader input;
    [SerializeField] Transform targetY;
    [SerializeField] Transform targetX;

    [SerializeField]
    Controller_Physics player;
    [SerializeField]
    Transform crossHair;
    [SerializeField]
    CinemachineVirtualCamera climbCamera;

    [Header("Settings")]
    [SerializeField, Range(0.5f, 20f)] float SpeedMulitiplier = 1f;

    bool isUnLockPressed = false;
    bool cameraMovementLock = false;

    float newRotationY;
    float newRotationX;


    private void OnEnable()
    {
        transform.parent = null;

        input.Look += OnLook;
        input.EnableMouseControlCamera += OnEnableMouseControlCamera;
        input.DisableMouseControlCamera += OnDisableMouseControlCamera;

        isUnLockPressed = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        input.Look -= OnLook;
        input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
        input.DisableMouseControlCamera -= OnDisableMouseControlCamera;

    }

    private void LateUpdate()
    {

        if (input.mouseMovement.magnitude == 0) 
        {
            newRotationY = targetX.eulerAngles.y;
            newRotationX = targetX.eulerAngles.x;
            return;
        }

        climbCamera.gameObject.SetActive(player.OnClimb);
        crossHair.gameObject.SetActive(!player.OnClimb);
        //Vector2 cameraMovement = input.mouseMovement;

        if (player.OnClimb)
        {
            Vector3 currAngle = -player.GetClimbNormal();
            currAngle.y = 0;
            currAngle.Normalize();
            Quaternion rotation = Quaternion.LookRotation(currAngle);
            float anchor = rotation.eulerAngles.y;

            if (anchor - 89 < 0)
            {
                if (newRotationY > 180)
                {
                    newRotationY = Mathf.Clamp(newRotationY, 360 + (anchor - 89), newRotationY);
                }
                else
                {
                    newRotationY = Mathf.Clamp(newRotationY, newRotationY, (anchor + 89));
                }
            }
            else if (anchor + 89 > 360)
            {
                if (newRotationY < 180)
                {
                    newRotationY = Mathf.Clamp(newRotationY, newRotationY, anchor + 89 - 360);
                }
                else
                {
                    newRotationY = Mathf.Clamp(newRotationY, (anchor - 89), newRotationY);
                }
            }
            else
            {
                newRotationY = Mathf.Clamp(newRotationY, (anchor - 89), (anchor + 89));
            }
            targetY.transform.eulerAngles = new Vector3(0, newRotationY, 0);
        }
        else
        {
            targetY.transform.eulerAngles = new Vector3(0, newRotationY, 0);
        }

        //y축 변경
        //x축 변경
        targetX.rotation = Quaternion.Euler(newRotationX, newRotationY, targetX.eulerAngles.z);
    }

    void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
    {
        if (cameraMovementLock) return;
        if (isUnLockPressed) return;
        newRotationY = targetX.eulerAngles.y + cameraMovement.x * SpeedMulitiplier * Time.deltaTime;
        newRotationX = targetX.eulerAngles.x - cameraMovement.y * SpeedMulitiplier * Time.deltaTime;
        newRotationX = Mathf.Clamp(newRotationX > 180 ? newRotationX - 360 : newRotationX, -89, 89);
    }


    void OnEnableMouseControlCamera()
    {
        isUnLockPressed = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnDisableMouseControlCamera()
    {
        isUnLockPressed = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(DisableMouseForFrame());
    }

    IEnumerator DisableMouseForFrame()
    {
        cameraMovementLock = true;
        yield return new WaitForEndOfFrame();
        cameraMovementLock = false;
    }

}
