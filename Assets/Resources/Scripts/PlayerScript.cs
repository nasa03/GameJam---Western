using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerScript : MonoBehaviour {

    public float walkingSpeed;
    public float score;
    public int dirUp, dirLeft;
    public Vector3 mousePos;
    bool isMoving;
    public bool lookingLeft;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    VignetteAndChromaticAberration vACA;
    float vACATimer;
    public Sprite deadSprite;
    public Text scoreText;
    public bool died;
    public GameObject gun, restartButton;
    

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        vACA = Camera.main.GetComponent<VignetteAndChromaticAberration>();
        vACATimer = 0;
	}

    public void addScore(float _score)
    {
        score += _score;
        scoreText.text = "Score: " + score;
    }
	
	// Update is called once per frame
	void Update () {
        if (Pause.isPaused)
        {
            return;
        }

        if (died)
        {
            if (vACATimer <= 0.3f)
            {
                vACA.intensity = vACATimer;
                vACATimer += Time.deltaTime / 8;
            }
            //this.enabled = false;
            rigid.isKinematic = true;
            gun.SetActive(false);
            anim.enabled = false;
            restartButton.SetActive(true) ;
            GetComponent<BoxCollider2D>().enabled = false;
            sprite.sprite = deadSprite;
            return;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");


        rigid.velocity = new Vector2(walkingSpeed * moveX * Time.deltaTime * 60, walkingSpeed * moveY * Time.deltaTime * 60);
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));

        Vector3 direction = (transform.position - mousePos).normalized;
        
        if(direction.x > 0)
        {
            dirLeft = 1;
        }
        if(direction.x < 0)
        {
            dirLeft = 2;
        }

        if(direction.y > 0.2)
        {
            dirUp = 1;
        }
        else if(direction.y < -0.2)
        {
            dirUp = 2;
        }
        else
        {
            dirUp = 0;
        }
        anim.SetInteger("DirUp", dirUp);
        anim.SetInteger("DirLeft", dirLeft);
        anim.SetBool("isMoving", isMoving);
        if(dirUp == 0 && dirLeft == 1)
        {
            sprite.transform.localScale = new Vector2(-1,1);
            lookingLeft = true;
        }
        else
        {
            sprite.transform.localScale = new Vector2(1, 1);
            lookingLeft = false;
        }

        if (rigid.velocity.x + (rigid.velocity.y * 2) > 0.1 || rigid.velocity.x + (rigid.velocity.y * 2) < -0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (transform.position.y <= -7.5f)
            transform.position = new Vector3(transform.position.x, - 7.5f, transform.position.z);
        else if (transform.position.y >= 7.5f)
            transform.position = new Vector3(transform.position.x, 7.5f, transform.position.z);

        if (transform.position.x <= -12.7f)
            transform.position = new Vector3(-12.7f, transform.position.y, transform.position.z);
        else if (transform.position.x >= 12.7f)
            transform.position = new Vector3(12.7f, transform.position.y, transform.position.z);
    }
    //Debug.Log(direction.normalized);
    //Debug.Log(dirUp + " "+ dirLeft + " Norm: " + direction);
}

