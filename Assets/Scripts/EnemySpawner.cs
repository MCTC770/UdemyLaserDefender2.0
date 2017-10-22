using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 9f;
    public float height = 10f;
    public float enemySpeed = 5f;
    public float spawnDelay = 0.5f;

    private bool moveRight = true;
    private float minX;
    private float maxX;

    // Use this for initialization
    void Start()
    {
        float playspaceToCameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, playspaceToCameraDistance));
        Vector3 rightmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, playspaceToCameraDistance));
        minX = leftmostPosition.x;
        maxX = rightmostPosition.x;
        SpawnUntilFull();
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
        Invoke("SpawnUntilFull", spawnDelay);
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight == false)
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }
        if (moveRight == true)
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (width / 2);
        float leftEdgeOfFormation = transform.position.x - (width / 2);
        if (leftEdgeOfFormation < minX)
        {
            moveRight = true;
        }
        else if (rightEdgeOfFormation > maxX)
        {
            moveRight = false;
        }

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}