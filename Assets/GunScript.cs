using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {

    public int currentBullets;
    int maxBullets = 6;
    public float fireRate;
    float SfireRate;
    public float reloadspeed;
    float Sreloadspeed;
    public PlayerScript player;
    bool triggerPress = false, reloading, wait;
    Animator anim;
    public LayerMask layer;
    public Transform gunpoint;
    public Text reloadText;
    public GameObject laser;
    public AudioSource audio;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentBullets = maxBullets;
        SfireRate = fireRate;
        Sreloadspeed = reloadspeed;
        reloadText.enabled = false;
	}

    public void changeFireRate(float value)
    {
        SfireRate += value;
    }

    public void changeReload(float value)
    {
        Sreloadspeed += value;
    }

    public void changeAmmo(int value)
    {
        maxBullets += value;
    }
	
	// Update is called once per frame
	void Update () {
        if(Pause.isPaused)
        {
            return;
        }

        Quaternion rotation = Quaternion.LookRotation(player.mousePos - transform.position, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        anim.SetInteger("Side", player.dirUp);

        if(player.lookingLeft)
        {
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
        }

        if(triggerPress)
        {
            fireRate -= Time.deltaTime;
            wait = true;
            if (fireRate < 0)
            {
                fireRate = SfireRate;
                triggerPress = false;
                wait = false;
            }
        }

        if(reloading)
        {
            reloadText.enabled = true;
            reloadspeed -= Time.deltaTime;
            
            if(reloadspeed < 0)
            {
                reloading = false;
                reloadspeed = Sreloadspeed;
                Debug.Log("Reloading Done!");
                currentBullets = maxBullets;
                reloadText.enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!reloading)
            {
                if (!wait)
                {
                    audio.Play();
                    triggerPress = true;
                    currentBullets -= 1;
                    Debug.DrawRay(gunpoint.transform.position, player.mousePos - gunpoint.transform.position , Color.red);
                    RaycastHit2D[] hit = Physics2D.RaycastAll(player.transform.position, player.mousePos - player.transform.position, 500f, layer);
                    if(hit.Length != 0)
                    {
                        for (int i = 0; i < hit.Length; i++)
                        {
                            if (hit[i].transform.tag == "Enemy")
                            {
                                hit[i].transform.GetComponent<EnemyScript>().GetHit(200);
                                Instantiate(Resources.Load("Prefab/HitParticle") as GameObject, hit[i].transform.position, Quaternion.identity);
                                Debug.Log(hit[i].transform.name);
                            }
                        }
                        
                    }
                    if (currentBullets <= 0)
                    {
                        reloading = true;
                        Debug.Log("Reloading");
                    }
                    Instantiate(laser, gunpoint.transform.position, Quaternion.LookRotation(player.mousePos - transform.position, transform.TransformDirection(Vector3.left)));
                }
            }
        }
	}
}
