using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void BeginGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
