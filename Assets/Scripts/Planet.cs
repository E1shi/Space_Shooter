using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float speed;
    public bool isMoving;

    Vector2 min;
    Vector2 max;

    void Awake()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;
        
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < min.y)
        {
            isMoving = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);   
    }
}
