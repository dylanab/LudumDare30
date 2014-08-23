using UnityEngine;
using System.Collections;

public class StarDistributor : MonoBehaviour {

	public GameObject starPrefab; // set in the inspector
    private int starCount = 300;
    private float starFieldSphereSize = 30;
    //positon = Vector3(0.0, 0.0, 0.0);
 
    void Start () {
        for (int i=0; i < starCount; ++i) {
            var position = Random.insideUnitSphere * starFieldSphereSize;
            position.z = Random.Range(1f, 20f);

            Instantiate(starPrefab, position, Quaternion.identity);
        }
    }
}
