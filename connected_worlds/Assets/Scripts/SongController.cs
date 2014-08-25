using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
}
