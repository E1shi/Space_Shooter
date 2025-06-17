using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    public GameObject[] Planets;

    Queue<GameObject> avaiblePlanets = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        avaiblePlanets.Enqueue(Planets[0]);
        avaiblePlanets.Enqueue(Planets[1]);
        avaiblePlanets.Enqueue(Planets[2]);

        InvokeRepeating(nameof(MovePlanetDown), 0f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MovePlanetDown()
    {

        EnqueuePlanets();

        if (avaiblePlanets.Count == 0)
        {
            return;
        }

        GameObject aplanet = avaiblePlanets.Dequeue();
        aplanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach (GameObject aplanet in Planets)
        {
            if ((aplanet.transform.position.y < 0) && (!aplanet.GetComponent<Planet>().isMoving))
            {
                aplanet.GetComponent<Planet>().ResetPosition();

                avaiblePlanets.Enqueue(aplanet);
            }
        }
    }
}
