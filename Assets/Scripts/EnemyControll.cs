using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{

    public GameObject bulletPos;
    public GameObject enemyBullets;
    public Transform targetPos;

    Animator anim;
    EnemyControll enemy;
    Collider2D coll;
    AudioSource deathSfx;

    public float speed = 2f;

    public bool isDead = false;

    void Awake()
    {
        InvokeRepeating(nameof(EnemyShoot), 1f, 3f);
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        enemy = GetComponent<EnemyControll>();
        coll = GetComponent<Collider2D>();
        deathSfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y - 1f)
        {
            Destroy(gameObject);
        }
    }

    void EnemyShoot()
    {
        GameObject bullet = Instantiate(enemyBullets, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(targetPos.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            anim.Play("Enemy_Death");
            coll.enabled = false;
            enemy.enabled = false;
            Destroy(gameObject, 0.5f);
            deathSfx.Play();
        }
    }
    
        public void Die()
    {
        if (isDead) return;

        isDead = true;
    }
}
