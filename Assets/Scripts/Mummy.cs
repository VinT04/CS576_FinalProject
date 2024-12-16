using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;
    private Pyramid pyramid;
    private float radius;
    private float speed;
    private Animator animation_controller;

    private GameObject canvas_gameOver;
    private GameObject canvas_win;
    private GameObject initialCanvas;
    void Start()
    {
        radius = 10f;
        speed = 1f;
        pyramid = plane.GetComponent<Pyramid>();
        animation_controller = GetComponent<Animator>();

        // endgame canvas
        canvas_gameOver = GameObject.FindGameObjectsWithTag("GameOver")[0];
        canvas_gameOver.SetActive(false);

        canvas_win = GameObject.FindGameObjectsWithTag("GameWon")[0];
        canvas_win.SetActive(false);
        initialCanvas = GameObject.Find("Canvas") ?? GameObject.Find("Canvas-Temp");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 dir = (player.transform.position - transform.position);
        dir.Normalize();
        bool direct = false;
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == player || hit.collider.gameObject.transform.parent == player.transform)
            {
                // Chase player directly if it is in sight of mummy
                //Debug.Log("Chase directly");
                direct = true;
                dir *= speed * 1.5f * Time.deltaTime;
                dir.y = 0;
                transform.position = transform.position + dir;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        if (!direct)
        {
            //Debug.Log("Chase heuristically");
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
                    if (0 <= newPos.Item1 && newPos.Item1 < pyramid.width && 0 <= newPos.Item2 && newPos.Item2 < pyramid.length && !d.ContainsKey(newPos))
                    {
                        d.Add(newPos, cur.obj);
                        int add = pyramid.map[newPos.Item1, newPos.Item2] == CellType.FLOOR ? 0 : 100;
                        q.Add(newPos, cur.priority + add);
                    }
                }
            }

            // Can find full path saved in dictionary
            (int, int) curCell = playerPosition;
            while (d.ContainsKey(curCell) && d[curCell] != mummyPosition) curCell = d[curCell];

            // curCell will now be second node on path from mummyPosition to playerPosition
            (float, float) targetPoint = pyramid.centers[curCell.Item1, curCell.Item2]; //gridToSpace(curCell.Item1, curCell.Item2);
            if (dist <= radius)
            {
                // If within this radius, trigger audio cue for player and have it at slightly faster speed
                // Need bool variable so audio is only placed once, reset after certain cooldown
                // triggerAudio()
                Vector3 target = new Vector3(targetPoint.Item1, 0, targetPoint.Item2);
                Vector3 movement = target - transform.position;
                movement.y = 0f;
                movement.Normalize();
                movement *= speed * 1.25f * Time.deltaTime;
                transform.position = transform.position + movement;
                transform.rotation = Quaternion.LookRotation(movement);
            }
            else
            {
                // Roam in general direction found to player, but at more passive speed
                Vector3 target = new Vector3(targetPoint.Item1, 0, targetPoint.Item2);
                Vector3 movement = target - transform.position;
                movement.y = 0f;
                movement.Normalize();
                movement *= speed * 0.75f * Time.deltaTime;
                transform.position = transform.position + movement;
                transform.rotation = Quaternion.LookRotation(movement);
            }
            animation_controller.SetInteger("state", 1);
        }

        // Fix y position at 0.75f
        Vector3 curPos = transform.position;
        curPos.y = 0.75f;
        transform.position = curPos;
    }

    public void upgradeDifficult()
    {
        // Cal this each time puzzle is solved by player
        speed += 2;
        radius += 5;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player || collision.gameObject.transform.parent == player.transform)
        {
            // Once mummy hits player, game is over
            Debug.Log("Game Over!");
            Destroy(gameObject);
            
            // Setting the living flag in BlakeMovement script to false to trigger death animation
            player = collision.gameObject;
            player.GetComponent<BlakeMovement>().living = false;

            // Call end screen in UI for user to play game or restart
            canvas_gameOver.SetActive(true);
            initialCanvas.SetActive(false);
        }
    }

    private (int, int) spaceToGrid(float x, float z)
    {
        float xStep = pyramid.bounds.size[0] / (float)pyramid.width;
        float zStep = pyramid.bounds.size[2] / (float)pyramid.length;

        return ((int)(Mathf.Round((x - pyramid.bounds.min[0]) / xStep)), (int)(Mathf.Round((z - pyramid.bounds.min[2]) / zStep)));

    }

    private (float, float) gridToSpace(int i, int j)
    {
        float xStep = pyramid.bounds.size[0] / (float)pyramid.width;
        float zStep = pyramid.bounds.size[2] / (float)pyramid.length;

        return (xStep * i + pyramid.bounds.min[0], zStep * j + pyramid.bounds.min[2]);
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