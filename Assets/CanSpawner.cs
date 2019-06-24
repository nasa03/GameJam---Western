using UnityEngine;
using System.Collections;

public class CanSpawner : MonoBehaviour {

    float countTimer;
    public float minSpawn, maxSpawn;
    public GameObject enemy;
	// Use this for initialization
	void Start () {
        countTimer = Random.Range(minSpawn, maxSpawn);
	}
	
	// Update is called once per frame
	void Update () {
        countTimer -= Time.deltaTime;
        if(countTimer < 0)
        {
            countTimer = Random.Range(minSpawn, maxSpawn);
            int ranVal = Random.Range(0, 3);
            switch(ranVal)
            {
                case 0:
                    {
                        Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10)), Quaternion.identity);
                        break;
                    }
                case 1:
                    {
                        Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10)), Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), 0, 10)), Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(0, Random.Range(0, Screen.height), 10)), Quaternion.identity);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            
        }
	}
}
