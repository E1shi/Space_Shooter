using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed = 3f;
    Vector3 direction;

    public void SetDirection(Vector3 targetPosition)
    {

        direction = (targetPosition - transform.position).normalized;

    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;
        if (pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
