using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Cancel")) {
			Application.Quit();
		}

		if (Input.GetButtonDown("Submit")) {
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}
	}
}
