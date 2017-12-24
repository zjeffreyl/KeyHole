using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    //Victory particle
    public GameObject victoryParticle;
    public Transform particleLocation;

    public float threshold = 0.05f;

    private bool checkOnce = false;
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
	void Start () {
        this.transform.position += new Vector3(0,distance,10);
	}
    
    public void checkWin()
    {
        float angleDiff = (Mathf.Abs(this.transform.rotation.z) - Mathf.Abs(hole.transform.rotation.z));
        if (Mathf.Abs(angleDiff) < threshold)
        {
            Instantiate(victoryParticle, particleLocation.transform.position, particleLocation.transform.rotation);
            //GameObject.Destroy(victoryParticle, 1f);
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
	void Update () {
        transform.RotateAround(Vector3.zero, zAxis , orbit_speed * Time.deltaTime);
        //Check the angle here if so Instantiate
        if (Input.GetKey(KeyCode.B) || trigger)
        {
            orbit_speed = temp_speed;
            orbit_speed = halt_speed;
            trigger = true;
            Debug.Log("Receiving the input");
            float move = slide_speed * Time.deltaTime;
            trigger = false;
            transform.position = Vector3.MoveTowards(transform.position, hole.transform.position, move);
            //check here
            checkOnce = true;
        }
        //at 
        if(this.transform.position == hole.transform.position && checkOnce == true)
        {
            checkOnce = false;
            //no more trigger and running move
            trigger = false;
            Debug.Log("Key is at the target");
            hole.rotation_speed = temp_speed;
            hole.rotation_speed = halt_speed;
            checkWin();
        }
    }
}
