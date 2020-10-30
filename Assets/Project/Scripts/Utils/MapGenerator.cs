using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map generation")]
    public int numberOfIntersections;
    public int numberOfSpawners;
    public int intersectionDistance;

    [Header("Prefabs")]
    public GameObject intersection;
    public GameObject longHallway;
    public GameObject shortHallway;
    public GameObject deadEnd;
    public GameObject enemySpawner;
    public GameObject startingRoom;

    [Header("Requirements")]
    public GameObject player;
    public GameController controller;

    private int currentSpawners = 0;
    private int currentIntersections = 0;
    private int distance = 0;

    enum RoomType { Starting, Middle, Intersection};

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y-1, player.transform.position.z);
        GameObject temp = Instantiate(startingRoom , position, player.transform.rotation);
        temp.transform.parent = controller.transform;

        position = new Vector3(temp.transform.GetChild(0).position.x, temp.transform.GetChild(0).position.y, temp.transform.GetChild(0).position.z);

        AddRoom(RoomType.Starting, temp.transform.GetChild(0));
    }

    void AddRoom(RoomType lastRoom, Transform snapPoint)
    {
        if (lastRoom == RoomType.Starting)
        {
            if (Random.Range(1, 101) <= 50)
            {
                GameObject temp = Instantiate(shortHallway, snapPoint.position, snapPoint.rotation);
                temp.transform.parent = controller.transform;
                distance++;
                AddRoom(RoomType.Middle, temp.transform.GetChild(0));
            }
            else
            {
                GameObject temp = Instantiate(longHallway, snapPoint.position, snapPoint.rotation);
                temp.transform.parent = controller.transform;
                distance += 2;
                AddRoom(RoomType.Middle, temp.transform.GetChild(0));
            }
        }

        else if (lastRoom == RoomType.Middle)
        {
            int r = Random.Range(1, 101);
            if (r <= 33)
            {

                r = Random.Range(1, 101);
                if (r <= 50)
                {
                    GameObject temp = Instantiate(shortHallway, snapPoint.position, snapPoint.rotation);
                    temp.transform.parent = controller.transform;
                    distance++;
                    AddRoom(RoomType.Middle, temp.transform.GetChild(0));
                }
                else
                {
                    GameObject temp = Instantiate(longHallway, snapPoint.position, snapPoint.rotation);
                    temp.transform.parent = controller.transform;
                    distance += 2;
                    AddRoom(RoomType.Middle, temp.transform.GetChild(0));
                }
            }

            else if (r > 33 && r <= 66)
            {
                if (currentIntersections == numberOfIntersections)
                {
                    if (currentSpawners < numberOfSpawners)
                    {
                        GameObject temp = Instantiate(enemySpawner, snapPoint.position, snapPoint.rotation);
                        temp.transform.parent = controller.transform;
                        currentSpawners++;
                    }
                    else
                    {
                        GameObject temp = Instantiate(deadEnd, snapPoint.position, snapPoint.rotation);
                        temp.transform.parent = controller.transform;
                    }
                }
                else
                {
                    AddRoom(RoomType.Middle, snapPoint);
                }
            }

            else if (r > 66 && r <= 100)
            {
                if (currentIntersections < numberOfIntersections && !(distance < intersectionDistance))
                {
                    GameObject temp = Instantiate(intersection, snapPoint.position, snapPoint.rotation);
                    temp.transform.parent = controller.transform;
                    distance = 0;
                    currentIntersections++;
                    for (int i = 0; i < 3; i++)
                    {
                        AddRoom(RoomType.Intersection, temp.transform.GetChild(i));
                    }
                }
                else
                {
                    AddRoom(RoomType.Middle, snapPoint);
                }
            }
        }

        else if(lastRoom == RoomType.Intersection)
        {
            int r = Random.Range(1, 101);
            if (r <= 50)
            {
                GameObject temp = Instantiate(shortHallway, snapPoint.position, snapPoint.rotation);
                temp.transform.parent = controller.transform;
                AddRoom(RoomType.Middle, temp.transform.GetChild(0));
            }
            else
            {
                GameObject temp = Instantiate(longHallway, snapPoint.position, snapPoint.rotation);
                temp.transform.parent = controller.transform;
                AddRoom(RoomType.Middle, temp.transform.GetChild(0));
            }
        }
    }

    public bool CheckNextSpace()
    {



        return true;
    }
}
