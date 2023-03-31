using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerInstance;

    public GameObject enemyPrefab;
    public List<Transform> enemyPosList = new List<Transform>();

    public GameObject boostPrefab;
    public List<Transform> boostPosList = new List<Transform>();

    private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> boostList = new List<GameObject>();
    
    public bool respawnCounting = false;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBackgroundSound("background");
        
        GameLoop();
    }
    
    async void GameLoop()
    {
        await RoundStarting();
        await RoundPlaying();
        await RoundEnding();

        if (IsPlayerDead())
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            GameLoop();
        }
    }
    
    private async UniTask RoundStarting()
    {
        // Return all the remain bullet on the game
        PoolManager.Instance.bullet.OnReturnAll();

        respawnCounting = false;
        ClearAllOldEnemy();
        ClearAllOldBoost();

        SpawnEnemy();
        SpawnBoost();
        
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        
    }

    private async UniTask RoundPlaying()
    {
        while (!IsPlayerDead() && EnemyRemain() != 0)
        {
            if (EnemyRemain() < 3 && respawnCounting == false)
            {
                respawnCounting = true;
                SpawnEnemy();
            }
            
            await UniTask.Yield();
        }
    }

    private async UniTask RoundEnding()
    {
        Debug.Log("You are in ending round");
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
    }
    
    private bool IsPlayerDead()
    {
        return this.playerInstance.GetComponent<Player>().isDead;
    }

    private int EnemyRemain()
    {
        int count = 0;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null || enemyList[i].gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    void ClearAllOldEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetActive(false);
            //Destroy(enemyList[i]);
        }
        
        enemyList.Clear();
    }

    void ClearAllOldBoost()
    {
        for (int i = 0; i < boostList.Count; i++)
        {
            Destroy(boostList[i]);
        }
        
        boostList.Clear();
    }

    void SpawnEnemy()
    {
        bool isHaveBoss = false;
        
        for (int i = 0; i < enemyPosList.Count; i++)
        {
            // 1-3: có enemy tại vị trí i, ngựợc lại không có
            int value = Random.Range(1, 5);
            if (value <= 3)
            {
                GameObject enemy = Instantiate(enemyPrefab, enemyPosList[i].position, Quaternion.identity);
                
                // 1: enemy này là boss, ngược lại là normal
                if (isHaveBoss == false)
                {
                    value = Random.Range(1, 5);
                    if (value == 1)
                    {
                        enemy.GetComponentInChildren<Enemy>().isBoss = true;
                        isHaveBoss = true;
                    }
                }

                enemyList.Add(enemy);
            }
        }
    }

    void SpawnBoost()
    {
        for (int i = 0; i < boostPosList.Count; i++)
        {
            GameObject boost = Instantiate(boostPrefab, boostPosList[i].position, Quaternion.identity);

            int value = Random.Range(1, 5);
            boost.GetComponentInChildren<Boost>().boostValue = value * 5;
            
            boostList.Add(boost);
        }
    }
}
