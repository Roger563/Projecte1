using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public enum Direction { None, Left, Right, Up, Down }

    [System.Serializable]
    public class Obj
    {
        public string name;

        [Header("Pool of objects")]
        public List<GameObject> pool;

        [Header("Movement")]
        public Direction direction;
        public float speed;

        [Header("Spawn")]
        public Transform respawn;

        [Header("Times")]
        public float ogOriginal;
        public float ogCoyet0n;

        [HideInInspector]
        public float Coyet0n;
        [HideInInspector]
        public int index = 0;
    }

    public List<Obj> objs;

    void Awake()
    {
        foreach (Obj o in objs)
        {
            o.index = o.pool.Count - 1;
            o.Coyet0n = o.ogOriginal;

            foreach(GameObject g in o.pool)
            {
                if (g.GetComponent<Rigidbody2D>() == null)
                    g.AddComponent<Rigidbody2D>();

                Rigidbody2D rb = g.GetComponent<Rigidbody2D>();

                rb.bodyType = RigidbodyType2D.Kinematic;
                //rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                switch (o.direction)
                {
                    case Direction.None:
                        g.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        break;
                    case Direction.Left:
                        g.GetComponent<Rigidbody2D>().velocity = new Vector2(o.speed, 0);
                        break;
                    case Direction.Right:
                        g.GetComponent<Rigidbody2D>().velocity = new Vector2(-o.speed, 0);
                        break;
                    case Direction.Up:
                        g.GetComponent<Rigidbody2D>().velocity = new Vector2(0, o.speed);
                        break;
                    case Direction.Down:
                        g.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -o.speed);
                        break;
                }
            }
        }
    }

    void Update()
    {
        foreach (Obj o in objs)
        {
            if (o.Coyet0n > 0)
            {
                o.Coyet0n -= Time.deltaTime;
            }
            else
            {
                Spawn(o);
                o.Coyet0n = o.ogCoyet0n;
            }
        }
        
    }

    void Spawn(Obj obj)
    {
        Debug.Log("wha");
        obj.pool[obj.index].transform.position = obj.respawn.transform.position;
        obj.index--;
        if (obj.index == -1)
            obj.index = obj.pool.Count - 1;
        Debug.Log(obj.index);
    }
}
