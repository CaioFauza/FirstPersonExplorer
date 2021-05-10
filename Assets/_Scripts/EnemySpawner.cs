using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameManager gm;
    public GameObject zombie;

    float x, y=20f, z;

    int zombieCounter = 0;
    int maxZombies = 100;
    void Start()
    {
        gm = GameManager.GetInstance();
    }
    void Update()
    {
        if(gm.gameState == GameManager.GameState.END || gm.gameState == GameManager.GameState.MENU) { zombieCounter = 0; }
        if(gm.gameState == GameManager.GameState.GAME) Spawn();
    }

    void Spawn()
    {
         while(zombieCounter < maxZombies)
         {
            x = Random.Range(-30, 160);
            z = Random.Range(80, 180);
            Instantiate(zombie, new Vector3(x, y, z), Quaternion.identity);
            zombieCounter++;
         }
    }
}
