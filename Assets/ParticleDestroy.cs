using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

    ParticleSystem particle;
	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!particle.IsAlive())
        {
            Destroy(this.gameObject);
        }
	}
}
