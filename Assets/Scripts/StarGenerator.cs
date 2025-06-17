using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{

    public GameObject star;
    public int maxStars;

    Color[] starColor =
    {
        new Color (0.5f, 0.5f, 1f),
        new Color (0f, 1f, 1f),
        new Color (1f, 1f, 0f),
        new Color (1f, 0f, 0f),
    };

    // Start is called before the first frame update
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < maxStars; ++i)
        {
            GameObject stars = (GameObject)Instantiate(star);

            stars.GetComponent<SpriteRenderer>().color = starColor[i % starColor.Length];

            stars.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            stars.GetComponent<Star>().speed = +(2.5f * Random.value + 0.5f);

            stars.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
