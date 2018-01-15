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
    public float max_orbit_speed = 30f;
    public float min_orbit_speed = 20f;
    public float max_distance = 6f;
    public float min_distance = 3.5f;
    public float slide_speed;
    public SpriteRenderer rend;
    public float halt_speed = 0;
    private Vector3 v_rotation;
    public Hole hole;
    public float distance;
    //sprites
    public Sprite bigKey;
    public Sprite smallKey;

    // Use this for initialization
    private Vector3 zAxis = new Vector3(0, 0, 1);

    public ScoreKeeper scoreKeeper;
    public int pointsAwarded = 1;

    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        var ps = victoryParticle.GetComponent<ParticleSystem>().colorOverLifetime;
        ps.color = new ParticleSystem.MinMaxGradient(Color.white);

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
            //change to bigKey
            rend.sprite = bigKey;
            GameObject clone = (GameObject)Instantiate(victoryParticle, particleLocation.transform.position, particleLocation.transform.rotation);
            Destroy(clone.gameObject, 1f);
            //award point here
            state = LevelState.RESPAWN;
            scoreKeeper.ScoreCounter(pointsAwarded);

        }
        else
        {
            //stay small key and lose
            state = LevelState.GAMEOVER;
        }
    }
    // Changes the Orbit Speed, Distance, Color of the Key
    public void Reposition()
    {
        current_orbit_speed = Random.Range(min_orbit_speed, max_orbit_speed);
        while(current_orbit_speed - hole.rotation_speed < 3f)
        {
            current_orbit_speed = Random.Range(min_orbit_speed, max_orbit_speed);
        }
        distance = Random.Range(min_distance, max_distance);
        this.transform.position = new Vector3(0, distance , 10);
        this.transform.SetPositionAndRotation(new Vector3(0, distance, 10), new Quaternion(0, 0, 0, 0));
        float reverse = Random.Range(0, 500);
        if (reverse > 249) current_orbit_speed *= -1;
        rend.color = new Color(Random.Range(.0f, 1f), Random.Range(.0f, 1f), Random.Range(.0f, 1f));
        var ps = victoryParticle.GetComponent<ParticleSystem>().colorOverLifetime;
        ps.color = new ParticleSystem.MinMaxGradient(rend.color);
        transform.RotateAround(Vector3.zero, zAxis, Random.Range(0,360));
        hole.Reposition();
        //Debug.Log("Distance: " + distance + " Speed: " + current_orbit_speed + " Color: " + rend.color);
        state = LevelState.PLAYING;

    } 

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, zAxis, current_orbit_speed * Time.deltaTime);

        if (state == LevelState.PLAYING)
        {
            pressBAllowed = true;

            bool pressed = false;
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    pressed = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                pressed = true;
            }
            if (pressed && pressBAllowed == true)
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
            rend.sprite = smallKey;
            Reposition();
        }

        if (state == LevelState.GAMEOVER)
        {
            //Reset when you lose
        }


    }
}