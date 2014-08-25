using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0f));
	}
}
