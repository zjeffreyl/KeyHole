using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum LevelState { PLAYING, RESULT, RESPAWN };
    //State
    public LevelState state = LevelState.PLAYING;
    //Victory particle
    public GameObject victoryParticle;
    public Transform particleLocation;

    public float threshold = 0.05f;

    private bool pressBAllowed = true;
    private bool trigger = false;
    public float orbit_speed;
    public float slide_speed;
    public float halt_speed = 0;
    public float temp_speed;
    private Vector3 v_rotation;
    public Hole hole;
    public float distance;
    // Use this for initialization
    private Vector3 zAxis = new Vector3(0, 0, 1);
    void Start()
    {
        this.transform.position += new Vector3(0, distance, 10);
    }

    public void checkWin()
    {
        float angleDiff = (Mathf.Abs(this.transform.rotation.z) - Mathf.Abs(hole.transform.rotation.z));
        if (Mathf.Abs(angleDiff) < threshold)
        {
            GameObject clone = (GameObject)Instantiate(victoryParticle, particleLocation.transform.position, particleLocation.transform.rotation);
            Destroy(clone.gameObject, 1f);
            
        }
        else
        {
            Debug.Log("Failed");

        }
    }

    public void increaseDifficulty()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(state == LevelState.PLAYING)
        {
            pressBAllowed = true;
        }
        transform.RotateAround(Vector3.zero, zAxis, orbit_speed * Time.deltaTime);
        //Check the angle here if so Instantiate
        if (Input.GetKey(KeyCode.B) && pressBAllowed == true)
        {
            //pressed once and the result will continue
            state = LevelState.RESULT;
            //While in 'PRESSED B' @ anytime cannot access again
            if(state == LevelState.RESULT)
            {
                //lock input
                pressBAllowed = false;
                //now check result
            }
            orbit_speed = 0;
            trigger = true;
            while (trigger == true)
            {
                Debug.Log("Receiving the input in loop");
                float move = slide_speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, hole.transform.position, move);
                //at target position needs to stop here and exit loop
                if (this.transform.position == hole.transform.position)
                {
                    Debug.Log("Key is at the target");
                    checkWin();
                    //exit loop
                    trigger = false;
                }
            }
        }
    }
}