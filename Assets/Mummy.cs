using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Mummy : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;
    private Pyramid pyramid;
    private float radius;
    private float speed;
    void Start()
    {
        // Use similar logic to virus from project 5
        // If within radius, then follow player
        // otherwise have some type of random roaming technique?
        radius = 10f;
        speed = 1f;
        pyramid = plane.GetComponent<Pyramid>();
    }

    // Update is called once per frame
    void Update()
    {
        // Todo
        // Two states - wandering or chasing
        // Wandering - choose random point or roam in general direction of player
        // Chasing - Player is target
        // Need to have mummy animations along with these actions
        // Maybe use raycast, if it hits player directly then just make player target?
        // So first check if raycast hits??

        RaycastHit hit;
        Vector3 dir = (player.transform.position - transform.position);
        dir.Normalize();
        bool direct = false;
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity)) {
            
            if (hit.collider.gameObject == player)
            {
                // Chase player directly if it is in sight of mummy
                Debug.Log("Chase directly");
                direct = true;
                dir *= speed * 1.5f * Time.deltaTime;
                dir.y = 0;
                transform.position = transform.position + dir;
            }
        }

        if (!direct)
        {
            Debug.Log("Chase heuristically");
            float dist = Vector3.Magnitude(player.transform.position - transform.position);
            PriorityQueue<(int, int)> q = new PriorityQueue<(int, int)>();

            (int, int) playerPosition = spaceToGrid(player.transform.position.x, player.transform.position.z);
            (int, int) mummyPosition = spaceToGrid(transform.position.x, transform.position.z);
            q.Add(mummyPosition, 0);
            Dictionary<(int, int), (int, int)> d = new Dictionary<(int, int), (int, int)>();

            while (q.Count() > 0)
            {
                PriorityQueueObject<(int, int)> cur = q.Pop();
                if (cur.obj == playerPosition) break;

                List<(int, int)> directions = new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
                for (int i = 0; i < directions.Count; i++)
                {
                    (int, int) newPos = (cur.obj.Item1 + directions[i].Item1, cur.obj.Item2 + directions[i].Item2);
                    if (0 <= newPos.Item1 && newPos.Item1 < pyramid.width && 0 <= newPos.Item2 && newPos.Item2 < pyramid.length)
                    {
                        d.Add(newPos, cur.obj);
                        int add = pyramid.map[newPos.Item1, newPos.Item2] == CellType.FlOOR ? 0 : 100;
                        q.Add(newPos, cur.priority + add);
                    }
                }
            }

            // Can find full path saved in dictionary
            // Now, we know path through maze toward player, so we just make it move toward next node in path?
            (int, int) curCell = playerPosition;
            while (d.ContainsKey(curCell) && d[curCell] != mummyPosition) curCell = d[curCell];

            // curCell will now be second node on path from mummyPosition to playerPosition
            (float, float) targetPoint = gridToSpace(curCell.Item1, curCell.Item2);
            if (dist <= radius)
            {
                // If within this radius, trigger audio cue for player and have it at slightly faster speed?
                // triggerAudio()
                Vector3 target = new Vector3(targetPoint.Item1, 0, targetPoint.Item2);
                Vector3 movement = target - transform.position;
                movement.Normalize();
                movement *= speed * 1.25f * Time.deltaTime;
                transform.position = transform.position + movement;
            }
            else
            {
                // Roam in general direction found to player, but at more passive speed
                Vector3 target = new Vector3(targetPoint.Item1, 0, targetPoint.Item2);
                Vector3 movement = target - transform.position;
                movement.Normalize();
                movement *= speed * 0.75f * Time.deltaTime;
                transform.position = transform.position + movement;

            }
        }        
        
    }

    public void upgradeDifficult()
    {
        speed += 2;
        radius += 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "The Adventurer Blake Variant")
        {
            // Health or game should end?
            Debug.Log("Game Over!");
            Destroy(gameObject);
            // Call endScreen
        }
    }

    private (int, int) spaceToGrid (float x, float z)
    {
        x -= 0.5f;
        z -= 0.5f;
        float xStep = pyramid.bounds.size[0] / (float)pyramid.width;
        float zStep = pyramid.bounds.size[2] / (float)pyramid.length;

        return ( (int) (x / xStep), (int) (z / zStep));

    }

    private (float, float) gridToSpace (int i, int j)
    {
        float xStep = pyramid.bounds.size[0] / (float)pyramid.width;
        float zStep = pyramid.bounds.size[2] / (float)pyramid.length;

        return (xStep * i + 0.5f, zStep * j + 0.5f);
    }
}



public class PriorityQueue<T>
{
    SortedSet<PriorityQueueObject<T>> queue;
    int a = 0;

    public PriorityQueue() { queue = new SortedSet<PriorityQueueObject<T>>(); }

    public void Add(T obj, int priority)
    {
        queue.Add(new PriorityQueueObject<T>(obj, priority, a++));
    }

    public PriorityQueueObject<T> Peak()
    {
        return queue.ElementAt(0);
    }
    public PriorityQueueObject<T> Pop()
    {
        var o = queue.ElementAt(0);
        queue.Remove(o);
        return o;
    }

    public int Count()
    {
        return queue.Count;
    }

}


public class PriorityQueueObject<T> : IComparable<PriorityQueueObject<T>>
{
    public T obj { get; set; }
    public int priority { get; set; }
    public int ex { get; set; }

    public PriorityQueueObject(T obj, int priority, int ex)
    {
        this.obj = obj;
        this.priority = priority;
        this.ex = ex;
    }

    public int CompareTo(PriorityQueueObject<T> other)
    {
        int order = priority.CompareTo(other.priority);
        if (order == 0) return ex.CompareTo(other.ex);
        return order;

    }
}