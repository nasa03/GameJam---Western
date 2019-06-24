using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

    int powerUpID;
    float tillDissapear =1.5f;
    Vector2 currPos;
    bool gotTouched;
    public AudioClip pickup;
    public AudioSource audio;
    public Sprite[] skins;
    public SpriteRenderer sprite;
    public GameObject textPickUp;

    // Use this for initialization
    void Start () {
        powerUpID = Random.Range(0, 4);
        transform.position = new Vector2(Random.Range(-12, 12), Random.Range(-7.5f, 7.5f));
        sprite.sprite = skins[powerUpID];
        currPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(currPos.x, currPos.y + Mathf.Sin(Time.time) / 16);
        if(gotTouched)
        {
            tillDissapear -= Time.deltaTime;
            if(tillDissapear < 0)
            {
                Destroy(gameObject);
            }
        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(!gotTouched)
        {
            if (coll.transform.tag == "Player")
            {
                GameObject obj = Instantiate(textPickUp, transform.position, Quaternion.identity) as GameObject;
                obj.transform.SetParent(GameObject.Find("WorldCanvas").transform, true);
                obj.transform.localScale = new Vector3(1, 1, 1);
                switch (powerUpID)
                {
                    case 0:
                        GameObject.Find("Gun").GetComponent<GunScript>().changeReload(-0.3f);
                        obj.GetComponent<Text>().text = "Decreased Reloading Time";
                        Debug.Log("Reload Pickup");
                        break;
                    case 1:
                        GameObject.Find("Gun").GetComponent<GunScript>().changeFireRate(-0.05f);
                        obj.GetComponent<Text>().text = "Increased Firerate";
                        Debug.Log("Firerate Pickup");
                        break;
                    case 2:
                        coll.transform.GetComponent<PlayerScript>().walkingSpeed += 0.2f;
                        obj.GetComponent<Text>().text = "Decreased Reloading Time";
                        Debug.Log("Walk Speed Pickup");
                        break;
                    case 3:
                        GameObject.Find("Gun").GetComponent<GunScript>().changeAmmo(1);
                        obj.GetComponent<Text>().text = "Increased Max Ammo";
                        break;
                    default:
                        {
                            break;
                        }
                }
                gotTouched = true;
                audio.PlayOneShot(pickup);
                sprite.enabled = false;
            }
        }
    }
}
