using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject[] WayPoints;
    public bool isReady = false;
    private Stack<Vector3> stack = new Stack<Vector3>();
    private TraverseWaypoint travWP;
    private bool randWalk = true;

    void Start()
    {
        travWP = GetComponent<TraverseWaypoint>();
    }
    // Find shortest path and put it in stack
    // return stack of vector3
    public void getShortestPath(int end)
    {
        // remove everything from the stack
        stack.Clear();

        // end is the location i want to end up at


        // distance array
        int[] dist = new int[WayPoints.Length];
        int[] to = new int[WayPoints.Length];

        for(int l = 0; l < dist.Length; l++)
        {
            dist[l] = int.MaxValue;

        }

        // Set the starting distance to 0
        int start = findClosestWayPoint();
        dist[start] = 0;


        // Initalize our queue
        Queue<int> queue = new Queue<int>();

        // Add the starting point to the queue
        queue.Enqueue(start);

        int loc, e;

        while(queue.Count != 0)
        {
            loc = queue.Dequeue();

            //--CHECK RIGHT--//
            
            // Check if one to the right is a valid location
            if(loc % (int)Mathf.Sqrt(WayPoints.Length) != (int)Mathf.Sqrt(WayPoints.Length) - 1 && WayPoints[loc].GetComponent<Waypoint>().Traversable)
            {
                e = setRight(loc);
                if (e <= (int)Mathf.Sqrt(WayPoints.Length))
                {
                    if (dist[e] > dist[loc] + 1)
                    {
                        // We have found a new shortest path
                        queue.Enqueue(e);
                        dist[e] = dist[loc] + 1;
                        to[e] = loc;
                    }
                }
            }

            //--CHECK LEFT--//
            if (loc % (int)Mathf.Sqrt(WayPoints.Length) != 0 && WayPoints[loc].GetComponent<Waypoint>().Traversable)
            {
                e = setLeft(loc);
                // Check if one to the left is a valid location
                if (e >= 0)
                {
                    if (dist[e] > dist[loc] + 1)
                    {
                        // We have found a new shortest path
                        queue.Enqueue(e);
                        dist[e] = dist[loc] + 1;
                        to[e] = loc;
                    }
                }
            }
            
       
            //--CHECK DOWN--//
            e = setDown(loc);
            if(e < WayPoints.Length && WayPoints[loc].GetComponent<Waypoint>().Traversable)
            {
                if (dist[e] > dist[loc] + 1)
                {
                    // We have found a new shortest path
                    queue.Enqueue(e);
                    dist[e] = dist[loc] + 1;
                    to[e] = loc;
                }
            }


            //--CHECK UP--//
            e = setUp(loc);
            if (e >= 0 && WayPoints[loc].GetComponent<Waypoint>().Traversable)
            {
                if (dist[e] > dist[loc] + 1)
                {
                    // We have found a new shortest path
                    queue.Enqueue(e);
                    dist[e] = dist[loc] + 1;
                    to[e] = loc;
                }
            }

            if(loc == end)
            {
                break;
            }
        }

        int t = end;


        while(t != start)
        {
            
            stack.Push(WayPoints[t].transform.position);
            t = to[t];
        }

        stack.Push(WayPoints[start].transform.position);

        print(stack.ToString());
        isReady = true;
        randWalk = true;
    }

    private int setUp(int l)
    {
        return l - (int)Mathf.Sqrt(WayPoints.Length);
    }
    private int setDown(int l)
    {
        return l + (int)Mathf.Sqrt(WayPoints.Length);
    }
    private int setLeft(int l)
    {
        return l - 1;
    }
    private int setRight(int l)
    {
        return l + 1;
    }

    private void goToWayPoint()
    {
        // Call Move.cs
        // Move.traverse(stack.pop())
        travWP.traverse(stack.Pop());
    }

    // Returns the location of the closest waypoint
    // in the fakewaypoint array
    private int findClosestWayPoint()
    {
        Vector3 horse = this.transform.position;
        // Hold the closest point
        int val = 0;

        // Loop through waypoints
        for(int i = 0; i < WayPoints.Length; i++)
        {
            if(distance(WayPoints[val].transform.position, horse) >= distance(WayPoints[i].transform.position, horse)) 
                {
                val = i;
            }
        }
        
        // return the closest waypoint
        return val;
    }

    // Takes in two vector3 objects and returns the distance
    // between the x and z values of the two points
    private double distance(Vector3 loc1, Vector3 loc2)
    {
        return Mathf.Sqrt(Mathf.Pow((loc1.x - loc2.x), 2f) + Mathf.Pow((loc1.z - loc2.z), 2f));
    }
	
	// Update is called once per frame
	void Update () {
		if(isReady)
        {
            goToWayPoint();
        }

        if(stack.Count == 0 && randWalk)
        {
            randWalk = false;
            int rand = Random.Range(0, 81);
            while(!WayPoints[rand].GetComponent<Waypoint>().Traversable)
            {
                rand = Random.Range(0, 81);
            }
            print(rand);
            getShortestPath(rand);

        }
	}
}
