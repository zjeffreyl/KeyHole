using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    public float rotation_speed;
    private Vector3 v_rotation;
	// Use this for initialization
	void Start () {
      
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(v_rotation);
    }

    void FixedUpdate()
    {
        v_rotation = new Vector3(0, 0, -rotation_speed);
    }
  
}
