using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    float rotation, moveDir;
	// Use this for initialization
	void Start () {
        rotation = Random.Range(0.4f, 0.8f);
        moveDir = Random.Range(0.4f, 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
        rotation -= Time.deltaTime;
        moveDir -= Time.deltaTime;

        transform.Translate(moveDir, 0, 0);
	}
}
