using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera mainCamera;

    float shakePower = 0.13f;
    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void DoShake()
    {
        if(shakePower > 0)
        {
            Vector3 camPos = mainCamera.transform.position;
            float xMove = Random.value * shakePower * 2 - shakePower;
            //Get another random
            float yMove = Random.value * shakePower * 2 - shakePower;
            camPos.x += xMove;
            camPos.y += yMove;

            mainCamera.transform.position = camPos;
        }
    }

    public void StopShake()
    {
        CancelInvoke("DoShake");
        mainCamera.transform.position = new Vector3(0,0, -10);
    }
}
