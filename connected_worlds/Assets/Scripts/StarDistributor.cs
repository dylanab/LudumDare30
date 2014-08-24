using UnityEngine;
using System.Collections;

public class StarDistributor : MonoBehaviour {

	public GameObject starPrefab; // set in the inspector
    private int starCount = 15000 * 2;
    private float starFieldSphereSize = 500;
    public Mesh mesh;
    public Material starMat0;
    public Material starMat1;
    public Material starMat2;
    public Material starMat3;
    public Material starMat4;
    //positon = Vector3(0.0, 0.0, 0.0);
 
    void Start() {
        for (int i=0; i < starCount; ++i) {
            var position = Random.insideUnitSphere * starFieldSphereSize;
            position.z = Random.Range(1f, 10f);

            //Graphics.DrawMesh(mesh, position, Quaternion.identity, material, 0);
            int randomInt = Random.Range(0, 100);
            if (randomInt % 8 == 0)
            {
                starPrefab.renderer.material = starMat3;
            }
            else if (randomInt % 20 == 0)
            {
                starPrefab.renderer.material = starMat4;
            }
            else if (randomInt % 40 == 0)
            {
                starPrefab.renderer.material = starMat2;
            }
            else
            {
                starPrefab.renderer.material = starMat1;
            }

            Instantiate(starPrefab, position, Quaternion.identity);
        }
    }
}
