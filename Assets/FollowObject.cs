using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    Vector3 savedTrans;
    public GameObject target;

	// Use this for initialization
	void Start () {
        savedTrans = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + savedTrans;
	}
}
