using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float spawnTime = 2f;
    [SerializeField] GameObject enemyPrefab;
    int numEnemies = 0;
    int moveEnemie = 0;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= spawnTime)
        {
            timer = 0f;
            moveEnemie += 1;
            if (numEnemies < 3)
            {
                // Spawn Enemy
                Vector2 circleposition = Random.insideUnitCircle.normalized*Random.Range(5, 20);
                Instantiate(enemyPrefab, new Vector3(circleposition.x,1,circleposition.y),Quaternion.identity).name = $"Enemy {++numEnemies}";
            }
            else
            {   
                if (moveEnemie < 3)
                {
                    GameObject myCubeObject = GameObject.Find($"Enemy {moveEnemie}");
                    Vector2 circleposition = Random.insideUnitCircle.normalized*Random.Range(5, 20);
                    myCubeObject.transform.Translate(new Vector3(circleposition.x,1,circleposition.y));
                }
                else
                {
                    moveEnemie = 0;
                }
            }
        }    
    }
}
