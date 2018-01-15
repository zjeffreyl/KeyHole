using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZoom : MonoBehaviour {

    public Camera cam;
    public int zoom;
    public int normal;
    public float smoothFail;
    public float smoothRetry;
    public Key key;
    public bool retry = false;
    public Color defaultColor;
    public ScoreKeeper scoreKeeper;

    private bool isZoomed = false;

    public void Start()
    {
        defaultColor = cam.backgroundColor;
    }

    private void Update()
    {
        if (isZoomed == true)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * smoothFail);
        }
        if (isZoomed == false)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, normal, Time.deltaTime * smoothRetry);
        }

        // Debug.Log(cam.orthographicSize);
        if (key.state == Key.LevelState.GAMEOVER)
        {
            isZoomed = true;
            retry = true;
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, Color.red, 3f);
        }

        if (Input.GetKeyUp(KeyCode.B) && retry == true)
        {
            scoreKeeper.Reset();
            retry = false;
            isZoomed = false;
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, defaultColor, 3f);
            key.state = Key.LevelState.RESPAWN;
            

        }
    }


}
