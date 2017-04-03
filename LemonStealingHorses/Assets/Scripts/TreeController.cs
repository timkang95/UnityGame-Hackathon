using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

    public GameObject[] Lemons;
    public Waypoint loc;
    public GameObject allWaypoints;
    public bool[] isGrown;
    private int lemonCount = 0;
    public float growTime;
    public GameObject godRayz;

    public void grow(int spot)
    {
        if (!isGrown[spot])
        {
            Transform tempWP;
            int tempX = 0;
            int tempZ = 0;

            StartCoroutine(LerpGrow(spot, new Vector3(0, 0, 0), new Vector3(0.75f, 0.75f, 0.75f)));
            isGrown[spot] = true;
            lemonCount++;

            switch (spot)
            {
                case 0:
                    tempX = loc.X - 1;
                    tempZ = loc.Z;
                    break;
                case 1:
                    tempX = loc.X;
                    tempZ = loc.Z - 1;
                    break;
                case 2:
                    tempX = loc.X + 1;
                    tempZ = loc.Z;
                    break;
                case 3:
                    tempX = loc.X;
                    tempZ = loc.Z + 1;
                    break;
            }
            tempWP = allWaypoints.transform.Find("Row " + tempZ + "/Waypoint " + tempX);

            GameObject rayz = Instantiate(godRayz, tempWP);
            rayz.transform.position = new Vector3(tempWP.position.x, (tempWP.position.y - 0.9f), tempWP.position.z);
            //cube.transform.position = tempWP.position;
        }
    }

    public void pick(int spot)
    {
        if (isGrown[spot])
        {
            isGrown[spot] = false;
            lemonCount--;
            StartCoroutine(LerpGrow(spot, new Vector3(0.75f, 0.75f, 0.75f), new Vector3(0, 0, 0)));
        }
    }

    IEnumerator LerpGrow(int spot, Vector3 Start, Vector3 End)
    {
        float progress = 0;

        while (progress <= 1)
        {
            Lemons[spot].transform.localScale = Vector3.Lerp(Start, End, progress);
            progress += Time.deltaTime * growTime;
            yield return null;
        }
        Lemons[spot].transform.localScale = End;
    }
}
