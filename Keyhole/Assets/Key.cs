using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public float orbit_speed; 
    private Vector3 v_rotation;
    public Hole hole;
    public float distance;
    // Use this for initialization
    private Vector3 zAxis = new Vector3(0, 0, 1);
	void Start () {
        this.transform.position += new Vector3(0,distance,10);
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, zAxis , orbit_speed * Time.deltaTime);
	}
}
