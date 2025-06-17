using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullets;
    public GameObject bulletPos1;
    public GameObject bulletPos2;
    public Joystick joystick;


    public float speed = 4f;
    private float fireTimer = 0f;
    bool isShooting = false;

    Animator anim;
    PlayerController player;
    Collider2D coll;
    AudioSource deathSfx;

    public AudioSource laserSfx;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        coll = GetComponent<Collider2D>();
        deathSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float keyX = Input.GetAxisRaw("Horizontal");
        float keyY = Input.GetAxisRaw("Vertical");

        float joyX = joystick.Horizontal;
        float joyY = joystick.Vertical;

        float x = keyX + joyX;
        float y = keyY + joyY;


        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);


        fireTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space) && fireTimer >= 0.2f || isShooting && fireTimer >= 0.2f)
        {
            GameObject bullet1 = (GameObject)Instantiate(bullets);
            bullet1.transform.position = bulletPos1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(bullets);
            bullet2.transform.position = bulletPos2.transform.position;

            fireTimer = 0f;

            laserSfx.Play();
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet1 = (GameObject)Instantiate(bullets);
        bullet1.transform.position = bulletPos1.transform.position;

        GameObject bullet2 = (GameObject)Instantiate(bullets);
        bullet2.transform.position = bulletPos2.transform.position;

        fireTimer = 0f;

        laserSfx.Play();
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        max.x = max.x + 0.225f;
        min.y = min.y - 0.225f;
        max.y = max.y + 0.225f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            anim.Play("Death");
            player.enabled = false;
            coll.enabled = false;
            FindAnyObjectByType<GameManager>().LoseLives();
            deathSfx.Play();
        }
    }

    public void InRespawn()
    {
        anim.Play("InRespawn");
        player.enabled = true;
    }

    public void Respawn()
    {
        anim.Play("Player");
        coll.enabled = true;
    }
    
    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }
}
