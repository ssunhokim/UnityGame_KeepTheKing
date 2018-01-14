using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathManager : MonoBehaviour
{
    public Stack<Vector3> currentPath;
    private Vector3 currentWaypointPosition;
    private float moveTimeTotal;
    private float moveTimeCurrent;

    private void Start()
    {
        //NavigateTo(new Vector3(12.4f,0, -193.6f));
    }

    // Position 계산 메서드
    public Vector3 GetPosition(float moveSpeed)
    {
        Vector3 pos = new Vector3(0,0,0);

        if (currentPath != null && currentPath.Count > 0)
        {
            if (moveTimeCurrent < moveTimeTotal)
            {
                moveTimeCurrent += Time.deltaTime;
                if (moveTimeCurrent > moveTimeTotal)
                    moveTimeCurrent = moveTimeTotal;
                pos = Vector3.Lerp(currentWaypointPosition, currentPath.Peek(), moveTimeCurrent / moveTimeTotal);
            }
            else
            {
                currentWaypointPosition = currentPath.Pop();
                if (currentPath.Count == 0)
                    Stop();
                else
                {
                    moveTimeCurrent = 0.0f;
                    moveTimeTotal = (currentWaypointPosition - currentPath.Peek()).magnitude / moveSpeed;
                }
            }
        }

        return pos;
    }

    public void NavigateTo(Vector3 destination)
    {
        currentPath = new Stack<Vector3>();
        var currentNode = FindClosestWaypoint(transform.position);
        var endNode = FindClosestWaypoint(destination);

        if (currentNode == null || endNode == null || currentNode == endNode)
            return;

        var openList = new SortedList<float, MonsterWayPoint>();
        var closedList = new List<MonsterWayPoint>();

        openList.Add(0, currentNode);
        currentNode.previous = null;
        currentNode.distance = 0.0f;

        while(openList.Count > 0)
        {
            currentNode = openList.Values[0];
            openList.RemoveAt(0);

            var dist = currentNode.distance;
            closedList.Add(currentNode);

            if (currentNode == endNode)
                break;

            foreach (var neighbor in currentNode.neighbors)
            {
                if (closedList.Contains(neighbor) || openList.ContainsValue(neighbor))
                    continue;

                neighbor.previous = currentNode;
                neighbor.distance = dist + (neighbor.transform.position - currentNode.transform.position).magnitude;
                var distanceToTarget = (neighbor.transform.position - endNode.transform.position).magnitude;
                openList.Add(neighbor.distance + distanceToTarget, neighbor);
            }
        }

        if(currentNode == endNode)
        {
            while(currentNode.previous != null)
            {
                currentPath.Push(currentNode.transform.position);
                currentNode = currentNode.previous;
            }

            currentPath.Push(transform.position);
        }
    }

    public void Stop()
    {
        currentPath = null;
        moveTimeCurrent = 0;
        moveTimeTotal = 0;
    }

    private MonsterWayPoint FindClosestWaypoint(Vector3 target)
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;

        foreach(var waypoint in GameObject.FindGameObjectsWithTag("WayPoint"))
        {
            var dist = (waypoint.transform.position - target).magnitude;

            if(dist < closestDist)
            {
                closest = waypoint;
                closestDist = dist;
            }
        }

        if(closest != null)
        {
            return closest.GetComponent<MonsterWayPoint>();
        }

        return null;
    }
}
