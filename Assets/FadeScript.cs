using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

    SpriteRenderer sprite;
    float val = 1;
	// Use this for initialization
	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        val -= Time.deltaTime * 3;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, val);
        if (val < 0)
            Destroy(this.gameObject);
	}
}
