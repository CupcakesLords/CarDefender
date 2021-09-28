using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private float spawnSpeed;
    private float spawnSpeedTimer;

    private bool paused;

    public GameObject slot;

    public ObjectPool EnemyPool;
    public GameObject[] Enemy;

    public ObjectPool EffectPool;

    private int level;
    private int numberOfEnemy; private int enemyLeft;
    private int baseEnemy;

    public SaveDataObject data;

    private int SaveData(int show)
    {
        data.level = level;
        return 0;
    }

    private void Awake()
    {
        GameEventSystem.eventSystem.onGameLost += onGameLost;
        GameEventSystem.eventSystem.onGameSave += SaveData;
        level = data.level;
    }

    void Start()
    {
        paused = false;

        spawnSpeed = 1.5f;
        spawnSpeedTimer = 5f;

        EnemyPool.Init();
        EffectPool.Init();

        //level = 1; 
        //level = data.level;
        numberOfEnemy = 10; baseEnemy = 10;
        enemyLeft = 10;
        DisplayLevel(level, numberOfEnemy);
    }

    void DisplayLevel(int lv, int num)
    {
        GameEventSystem.eventSystem.LevelUp(lv); //set data for enemies

        GameUI.UISystem.UITextLevel.GetComponent<LevelScreen>().SetLevel(lv);
        GameUI.UISystem.UITextLevel.SetActive(true);
        Pause(true); slot.GetComponent<Slots>().Pause(true);
        StartCoroutine(TimerCountDown(2.5f));
    }

    int onGameLost(int number)
    {
        numberOfEnemy = baseEnemy + 2 * level;
        enemyLeft = baseEnemy + 2 * level;
        //start a coroutine to wait a few seconds vefore popping up new level text
        Pause(true); slot.GetComponent<Slots>().Pause(true);
        StartCoroutine(LoseTimerCountDown(2.5f, level, numberOfEnemy));
        return 0;
    }

    public void NextLevel() //just checking if there are enemies left to advance to next level
    {
        enemyLeft = enemyLeft - 1; //Debug.Log("Enemy destroyed! Enemies left: " + enemyLeft);
        if (numberOfEnemy <= 0 && enemyLeft <= 0)
        {
            level = level + 1;
            numberOfEnemy = baseEnemy + 2 * level;
            enemyLeft = baseEnemy + 2 * level;
            DisplayLevel(level, numberOfEnemy);
        }
    }

    void Update()
    {
        if (paused)
            return;
        if (spawnSpeedTimer > 0)
        {
            spawnSpeedTimer -= Time.deltaTime;
        }
        else
        {
            if (numberOfEnemy > 0)
            {
                numberOfEnemy = numberOfEnemy - 1;
                Spawn();
            }
            spawnSpeedTimer = spawnSpeed;
        }
    }

    int LevelToEnemyMaxSpawn()
    {
        if (level > 25)
            return Enemy.Length;
        else if (level > 11 && level <= 25)
            return 6;
        else
            return 3;
    }

    void Spawn()
    {
        System.Random rand = new System.Random();
        int i;
        i = rand.Next(0, LevelToEnemyMaxSpawn());
        
        float j = (float)rand.NextDouble();
        Vector3 pos = new Vector3((j - 0.5f) * 3, 7f, 0);
     
        string tag = Enemy[i].GetComponent<SpriteRenderer>().sprite.name;
        GameObject enemyFromPool = EnemyPool.GetFromQueue(tag);
        if (enemyFromPool == null)
        {
            GameObject enemyObject = Instantiate(Enemy[i], pos, Quaternion.identity);
            HealthPopUp.AssignHealthTextToCar(enemyObject);
            EnemyHealth eh = enemyObject.GetComponent<EnemyHealth>();
            eh.Reset();
            Enemy e = enemyObject.GetComponent<Enemy>();
            e.SetHolder(gameObject);
            e.Fall();
        }
        else
        {
            enemyFromPool.SetActive(true);
            enemyFromPool.transform.position = pos;
            HealthPopUp.AssignHealthTextToCar(enemyFromPool);
            EnemyHealth eh = enemyFromPool.GetComponent<EnemyHealth>();
            eh.Reset();
            Enemy e = enemyFromPool.GetComponent<Enemy>();
            e.Fall();
        }
    }

    float Timer = 0;

    private IEnumerator TimerCountDown(float time)
    {
        Timer = time;
        while (Timer > 0)
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        Timer = 0;
        GameUI.UISystem.UITextLevel.SetActive(false);
        Pause(false); slot.GetComponent<Slots>().Pause(false);
    }

    public void Pause(bool pause)
    {
        paused = pause;
    }

    float Timer1 = 0;

    private IEnumerator LoseTimerCountDown(float time, int level, int numberOfEnemy)
    {
        GameUI.UISystem.UITextLose.SetActive(true);
        Timer1 = time;
        while (Timer1 > 0)
        {
            Timer1 -= Time.deltaTime;
            yield return null;
        }
        Timer1 = 0;
        GameUI.UISystem.UITextLose.SetActive(false);
        //Pause(false); slot.GetComponent<Slots>().Pause(false);
        DisplayLevel(level, numberOfEnemy);
    }

    //public int getLevel()
    //{
    //    return level;
    //}
}
