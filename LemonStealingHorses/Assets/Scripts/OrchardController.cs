using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchardController : MonoBehaviour {

    public TreeController[] Orchard;
    public float growthrate;


    private int randTree;
    private int randLemon;
    private float nextGrow;

    void Update()
    {
        if (Time.time > nextGrow)
        {
            nextGrow = Time.time + growthrate;
            Grower();
        }
    }

    void Grower()
    {
        randTree = Random.Range(0, 16);
        randLemon = Random.Range(0, 4);

        Debug.Log("Growing Tree " + randTree + " with lemon " + randLemon);

        Orchard[randTree].grow(randLemon);
    }
}
