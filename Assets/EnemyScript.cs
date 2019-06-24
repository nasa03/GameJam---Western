using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    bool gotHit;
    bool once = true;
    public int score;
    public float speed;
    public SpriteRenderer sprite;
    Rigidbody2D rigid;
    float dieVal = 10;
    float rotationVal;
    public AudioSource audio;
    public Sprite[] skins;
    public AudioClip[] hitsound;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(-(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position) * Random.Range(15,30));
        rotationVal = 1750;
        sprite.sprite = skins[Random.Range(0, 4)];
	}
	
	// Update is called once per frame
	void Update () {
        dieVal -= Time.deltaTime;
        
        if(gotHit)
        {
            rotationVal = -250;
            dieVal = 1;
            gotHit = false;
        }
        sprite.transform.Rotate(Vector3.forward, rotationVal * Time.deltaTime);

        if (!once)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, dieVal);
        }

        if (dieVal < 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!once)
            return;

        GetHit(0);
        if(coll.transform.tag == "Player")
        {
            audio.PlayOneShot(Resources.Load("Sounds/Death") as AudioClip);
            coll.transform.GetComponent<PlayerScript>().died = true;
            Instantiate(Resources.Load("Prefab/BloodParticle") as GameObject, transform.position, Quaternion.identity);
        }
    }

    public void GetHit(float score)
    {
        if (once)
        {
            audio.PlayOneShot(hitsound[Random.Range(0, hitsound.Length)]);
            
            gotHit = true;
            once = false;
            rigid.AddForce((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position) * Random.Range(25, 40));
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().addScore(score);
    }
}
