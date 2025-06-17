using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBullet : MonoBehaviour
{
    float bulletSpeed = 7f;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        //Mau pake cara pertama atau kedua sama aja

        //Vector2 position = transform.position; 
        //position = new Vector2(position.x, position.y + bulletSpeed * Time.deltaTime);
        //transform.position = position;

        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;


        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyControll enemy = collision.GetComponent<EnemyControll>();

            if (enemy != null && !enemy.isDead)
            {
                enemy.Die(); // Musuh yang handle mati dan destroy dirinya
                gameManager.GetComponent<GameManager>().score += 100;
            }

            Destroy(gameObject); // hancurkan peluru
        }
    }
}
