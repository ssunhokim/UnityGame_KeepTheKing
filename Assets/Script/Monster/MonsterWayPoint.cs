using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MonsterWayPoint : MonoBehaviour
{
    public float radius = 5.0f;
    public List<MonsterWayPoint> neighbors;

    public MonsterWayPoint previous
    {
        get;
        set;
    }
    public float distance
    {
        get;
        set;
    }

    private void OnDrawGizmos()
    {
        if (neighbors == null)
            return;
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f);

        foreach(var neighbor in neighbors)
        {
            if (neighbor != null)
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
}
