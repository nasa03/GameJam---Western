using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pickupText : MonoBehaviour {

    Vector2 origin;
    float timeTillDie = 1.5f;
    float increase = 0;
    Text text;


	// Use this for initialization
	void Start () {
        origin = transform.position;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        increase += Time.deltaTime / 45;
        timeTillDie -= Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + increase);
        text.color = new Color(text.color.r, text.color.g, text.color.b, timeTillDie);
        if(timeTillDie < 0)
        {
            Destroy(gameObject);
        }
	
	}
}
