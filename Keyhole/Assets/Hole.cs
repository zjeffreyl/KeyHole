using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    public float rotation_speed;
    public float max_rotation_speed = 3.0f;
    public float min_rotation_speed = 1.0f;
    public float default_rotation_speed = 1.86f;
    private Vector3 v_rotation;
    private SpriteRenderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(v_rotation);
    }
    public void Reposition()
    {

        rotation_speed = Random.Range(min_rotation_speed, max_rotation_speed);
        float reverse = Random.Range(0, 500);
        if (reverse > 249) rotation_speed *= -1; 
        //Debug.Log("Hole Speed: " + rotation_speed);
    }
    void FixedUpdate()
    {
        v_rotation = new Vector3(0, 0, -rotation_speed);
    }
  
}
