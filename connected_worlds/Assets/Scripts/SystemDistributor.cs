using UnityEngine;
using System.Collections;

public class SystemDistributor : MonoBehaviour {

	public GameObject SystemPrefab; // set in the inspector
    private int systemCount = 70;
    private float mapSize = 40;
    public Mesh mesh;
    public Material sysMat0;
    public Material sysMat1;
    public Material sysMat2;
    //public Material starMat3;
    //public Material starMat4;
    //positon = Vector3(0.0, 0.0, 0.0);
 
    void Start() {

        string[] starArray1 = {"Alpha", "Beta", "Eridani", "Sirius", "Orion", "Cygnus", "Persii", "Crux", "Epsilon", "Ares", "Proxima", "Caprica", "Fomalhaut", "Canis",
                                  "Perseus", "Corona", "Hydrus", "Virgo", "Hercules", "Taurus", "Gemini", "Ursa", "Pegasus", "Cory", "Kolob", "Cobol", "Tau"};

        string[] starArray2 = {"Centauri", "Eri", "Tau", "Omega", "Minor", "Major", "Borealis", "Leo", "Aquarii", "Ares", "Pegasi", "Dylan", "Tauri", "Prime", "Scorpii",
                                  "Pi", "Gamma", "Mu", "Theta", "Delta", "Xi", "Lyrae", "Bootis", "Orionis", "Ceti"};


        for (int i = 0; i < systemCount; ++i)
        {
            var position = Random.insideUnitSphere * mapSize;

            position.z = 0f;

            GameObject System = Instantiate(SystemPrefab, position, Quaternion.identity) as GameObject;
            Vector3 scale = System.transform.localScale;
            float newScale = Random.Range(1.0f, 0.5f);
            scale.x = newScale;
            scale.y = newScale;
            scale.z = newScale;
            System.transform.localScale = scale;

            float r = Random.Range(0f, 1f);

            if (r > .95f){
                System.renderer.material = sysMat2;
            } else if (r > .9f) {
                System.renderer.material = sysMat1;
            } else if (r > .85f) {
                System.renderer.material = sysMat0;
            }

            string randomName = "";

            int roll = Random.Range(0, starArray1.Length);

            randomName += starArray1[roll] + " ";

            roll = Random.Range(0, starArray2.Length);

            randomName += starArray2[roll] + " ";

            roll = Random.Range(0, 3);

            if (roll == 2)
            {
                randomName += " " + Random.Range(1, 99);
            }

            SystemController control = System.GetComponent<SystemController>();
            control.name = randomName; 

        }
    }
}
