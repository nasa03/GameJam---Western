using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

    public GameObject powerup;
    public float timeTillSpawn;
    float storeTime;

	// Use this for initialization
	void Start () {
        storeTime = timeTillSpawn;
	}
	
	// Update is called once per frame
	void Update () {
        timeTillSpawn -= Time.deltaTime;
	    if(timeTillSpawn < 0)
        {
            timeTillSpawn = storeTime;
            Instantiate(powerup, transform.position, Quaternion.identity);
        }
	}
}
