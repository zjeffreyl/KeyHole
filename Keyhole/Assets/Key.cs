using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum LevelState { PLAYING, PROCESSING, RESPAWN, GAMEOVER };
    //State
    public LevelState state = LevelState.PLAYING;
    //Victory particle
    public GameObject victoryParticle;
    public Transform particleLocation;

    public float threshold = 0.05f;

    private bool pressBAllowed = true;
    [HideInInspector]
    public float current_orbit_speed;
    public float default_orbit_speed = 23.05f;
    public float slide_speed;
    public float halt_speed = 0;
    private Vector3 v_rotation;
    public Hole hole;
    public float distance;
    // Use this for initialization
    private Vector3 zAxis = new Vector3(0, 0, 1);
    void Start()
    {
        this.transform.position += new Vector3(0, distance, 10);
        if (current_orbit_speed == 0f)
        {
            current_orbit_speed = default_orbit_speed;
        }
    }

    public void CheckWin()
    {
        float angleDiff = (Mathf.Abs(this.transform.rotation.z) - Mathf.Abs(hole.transform.rotation.z));
        if (Mathf.Abs(angleDiff) < threshold)
        {
            GameObject clone = (GameObject)Instantiate(victoryParticle, particleLocation.transform.position, particleLocation.transform.rotation);
            Destroy(clone.gameObject, 1f);
            //award point here
            state = LevelState.RESPAWN;

        }
        else
        {
            state = LevelState.GAMEOVER;
        }
    }

    public void Reposition()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, zAxis, current_orbit_speed * Time.deltaTime);

        if (state == LevelState.PLAYING)
        {
            pressBAllowed = true;
            if (Input.GetKey(KeyCode.B) && pressBAllowed == true)
            {
                //While in 'PRESSED B' @ anytime cannot access again
                current_orbit_speed = 0;
                hole.rotation_speed = 0;
                pressBAllowed = false;
                state = LevelState.PROCESSING;
            }
        }

        if(state == LevelState.PROCESSING)
        {
            float move = slide_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(this.transform.position, hole.transform.position, move);
            //When it is at the position
            if (this.transform.position == hole.transform.position)
            {
                CheckWin();
            }
        }

        if(state == LevelState.RESPAWN)
        {

        }

        if (state == LevelState.GAMEOVER)
        {
            Debug.Log("FAILED");
        }


    }
}