using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnSys : MonoBehaviour
{
    //Remember the Boolean EventisOn in the class matchinfo that controls the movement of every character;

    [HideInInspector]
    public int gateProgress;

    public int gateMaxValue;

    private float timeBtwTheyCome = 3;
    public float startTimeBtwTheyCome;

    [Header("Enemies in this Level")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject bigGuy;

    //First enemy is the weakest and third enemy is the Strongest

    [Header("Where enemies appear")]
    public SpawnPosition spawnPosition1;
    public SpawnPosition spawnPosition2;
    public SpawnPosition spawnPosition3;

    private MatchInfo matchInfo;

    public GameObject smallSpawnerPrefab;
    public GameObject bigSpawnerPrefab;

    public Image hackingBar;
    [Header("BarCheckPoints")]
    public int gateWaveCounter = 0;

    //"checkpointPassed" opens the gate to the routines while "aRoutineHasStarted" prevents other routines from starting
    [HideInInspector]
    public bool aRoutineHasStarted = false;
    [HideInInspector]
    public bool checkpointPassed = false;

    [Header("Behavior type")]
    public int factor;
    public bool isThereBigGuy;

    [HideInInspector]
    public bool openGateCalled = false;

    private void Awake()
    {
        matchInfo = this.gameObject.GetComponent<MatchInfo>();

    }

    private void Update()
    {
        hackingBar.fillAmount = (float)gateProgress / (float)gateMaxValue;
        if(!MatchInfo.eventIsOn)
        {
            if (timeBtwTheyCome <= 0 &&
            gateProgress < gateMaxValue)
            {


                if (checkpointPassed)
                {
                    EnemyTag[] areEnemiesAround = GameObject.FindObjectsOfType<EnemyTag>();
                    if (areEnemiesAround.Length == 0)
                    {
                        
                        Debug.Log("The number of enemies is " + areEnemiesAround);


                        if (gateWaveCounter >= 4)
                        {
                            gateWaveCounter = 0;
                        }
                        else
                        {
                            gateWaveCounter += 1;
                            Debug.Log("Gate counter should have changed. It is = " + gateWaveCounter);
                        }

                        checkpointPassed = false;
                        aRoutineHasStarted = false;
                        
                    }

                    
                }
                else if(!checkpointPassed &&
                    !aRoutineHasStarted)
                {
                    if (gateWaveCounter == 0)
                    {
                        SummonCheckpoint01();
                        Debug.Log("Will Summon Checkpoint01");
                    }
                    else if(gateWaveCounter == 1)
                    {
                        SummonCheckpoint02();
                        Debug.Log("Will Summon Checkpoint02");
                    }
                    else if(gateWaveCounter == 2)
                    {
                        SummonCheckpoint03();
                        Debug.Log("Will Summon Checkpoint03");
                    }
                    else if(gateWaveCounter == 3)
                    {
                        SummonCheckpoint04();
                        Debug.Log("Will Summon Checkpoint04");
                    }
                    else if(gateWaveCounter == 4)
                    {
                        FinalSummon();
                        Debug.Log("Will Summon Checkpoint05");
                    }

                    Debug.Log("checkpoint is false");

                }

                //Do magic

                timeBtwTheyCome = startTimeBtwTheyCome;

                Debug.Log("TimeBtwTheyCome has restarted");

            }
            else if (gateProgress >= gateMaxValue &&
            !openGateCalled)
            {
                EnemyTag[] areEnemiesAround = GameObject.FindObjectsOfType<EnemyTag>();
                if (areEnemiesAround.Length == 0)
                {
                    gateProgress = gateMaxValue;
                    matchInfo.OpeningaGate();
                    timeBtwTheyCome = 7;

                    openGateCalled = true;
                }
                else
                {
                    Debug.Log("Attempting to Open this gate but enemies around : " + areEnemiesAround[0].name);
                }
                

                timeBtwTheyCome = startTimeBtwTheyCome;



            }
            else if(gateProgress >= gateMaxValue)
            {
                Debug.Log("openGateCalled : " + openGateCalled);
            }
            else
            {
                timeBtwTheyCome -= Time.deltaTime;

            }


        }
        else if(MatchInfo.eventIsOn)
        {
            StopAllCoroutines();
        }


        
    }

    //Limit to writte code in the spaces bellow
    void SummonCheckpoint01()
    {
        if(factor == 1)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor1Gate01Check01");
                Debug.Log("Hello from SummonCheckpoint01");
            }
            else if(MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor1Gate02Check01");
                Debug.Log("Hello from the Gate 2");
            }

            else if(MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor1Gate04Check01");
            }
            
        }
        else if (factor == 2)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor2Gate01Check01");
                Debug.Log("Hello from SummonCheckpoint01 Factor 2");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor2Gate02Check01");
                Debug.Log("Hello from the Gate 2");
            }

            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor2Gate04Check01");
            }

        }
        else if (factor == 3)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor3Gate01Check01");
                Debug.Log("Hello from SummonCheckpoint01 Factor 3");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor3Gate02Check01");
                Debug.Log("Hello from the Gate 2");
            }

            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor3Gate04Check01");
            }

        }
    }

    void SummonCheckpoint02()
    {
        if (factor == 1)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor1Gate01Check02");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor1Gate02Check02");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor1Gate04Check02");
            }

        }
        else if (factor == 2)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor2Gate01Check02");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor2Gate02Check02");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor2Gate04Check02");
            }

        }
        else if (factor == 3)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor3Gate01Check02");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor3Gate02Check02");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor3Gate04Check02");
            }

        }
    }

    void SummonCheckpoint03()
    {
        if (factor == 1)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor1Gate01Check03");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor1Gate02Check03");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor1Gate04Check03");
            }

        }
        else if (factor == 2)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor2Gate01Check03");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor2Gate02Check03");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor2Gate04Check03");
            }

        }
        else if (factor == 3)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor3Gate01Check03");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor3Gate02Check03");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor3Gate04Check03");
            }

        }
    }

    void SummonCheckpoint04()
    {
        if (factor == 1)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor1Gate01Check04");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor1Gate02Check04");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor1Gate04Check04");
            }

        }
        else if (factor == 2)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor2Gate01Check04");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor2Gate02Check04");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor2Gate04Check04");
            }

        }
        else if (factor == 3)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor3Gate01Check04");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor3Gate02Check04");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor3Gate04Check04");
            }

        }
    }

    void FinalSummon()
    {
        if (factor == 1)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor1Gate01Check05");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor1Gate02Check05");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor1Gate04Check05");
            }

        }
        else if (factor == 2)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor2Gate01Check05");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor2Gate02Check05");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor2Gate04Check05");
            }

        }
        else if (factor == 3)
        {
            if (MatchInfo.currentGate == 1)
            {
                StartCoroutine("Factor3Gate01Check05");
            }
            else if (MatchInfo.currentGate == 2 ||
                MatchInfo.currentGate == 3)
            {
                StartCoroutine("Factor3Gate02Check05");
            }
            else if (MatchInfo.currentGate > 3)
            {
                StartCoroutine("Factor3Gate04Check05");
            }

        }
    }

    //ResetWave


    void Spawn1Gate1()
    {
        spawnPosition1.PositionPicker();

        if(spawnPosition1.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition1.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy1;

        }

    }

    void Spawn1Gate2()
    {
        spawnPosition2.PositionPicker();

        if (spawnPosition2.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition2.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy1;

        }

    }
        void Spawn1Gate3()
    {
        spawnPosition3.PositionPicker();

        if (spawnPosition3.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition3.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy1;

        }

    }

    void Spawn2Gate1()
    {
        spawnPosition1.PositionPicker();

        if (spawnPosition1.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition1.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy2;

        }

    }

    void Spawn2Gate2()
    {
        spawnPosition2.PositionPicker();

        if (spawnPosition2.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition2.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy2;

        }

    }
    void Spawn2Gate3()
    {
        spawnPosition3.PositionPicker();

        if (spawnPosition3.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition3.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy2;

        }

    }


    void Spawn3Gate1()
    {
        spawnPosition1.PositionPicker();

        if (spawnPosition1.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition1.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy3;

        }

    }

    void Spawn3Gate2()
    {
        spawnPosition2.PositionPicker();

        if (spawnPosition2.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition2.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy3;

        }

    }
    void Spawn3Gate3()
    {
        spawnPosition3.PositionPicker();

        if (spawnPosition3.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(smallSpawnerPrefab, spawnPosition3.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = enemy3;

        }

    }

    void SpawnBigGate1()
    {
        spawnPosition1.PositionPicker();

        if (spawnPosition1.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(bigSpawnerPrefab, spawnPosition1.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = bigGuy;

        }

    }

    void SpawnBigGate2()
    {
        spawnPosition2.PositionPicker();

        if (spawnPosition2.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(bigSpawnerPrefab, spawnPosition2.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = bigGuy;

        }

    }
    void SpawnBigGate3()
    {
        spawnPosition3.PositionPicker();

        if (spawnPosition3.weGotPosition)
        {
            GameObject spawnedEnemy = Instantiate(bigSpawnerPrefab, spawnPosition3.assignedPosition.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemySpawnEffect>().enemyToPlace = bigGuy;

        }

    }


    //Factor 1 Gate1
    IEnumerator Factor1Gate01Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate01Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(5);
        Spawn2Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate01Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate1();
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate01Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor1Gate01Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 1 Gate2 and Gate 3

    IEnumerator Factor1Gate02Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate02Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate02Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate02Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor1Gate02Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        Spawn1Gate1();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 1 Gate4 

    IEnumerator Factor1Gate04Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn3Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate04Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        if(isThereBigGuy)
        {
            SpawnBigGate2();
        }
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate04Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        Spawn3Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor1Gate04Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor1Gate04Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        Spawn1Gate1();
        if (isThereBigGuy)
        {
            SpawnBigGate1();
        }
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn3Gate1();
        checkpointPassed = true;
    }




    //Factor 2 Gate1
    IEnumerator Factor2Gate01Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate01Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        Spawn2Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate01Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate01Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        checkpointPassed = true;
    }
    IEnumerator Factor2Gate01Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 2 Gate2 and Gate 3

    IEnumerator Factor2Gate02Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate02Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        Spawn1Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate02Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate02Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor2Gate02Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn1Gate1();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 2 Gate4 

    IEnumerator Factor2Gate04Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        Spawn3Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn3Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate04Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn3Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn2Gate2();
        Spawn2Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn2Gate2();
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        Spawn2Gate3();
        Spawn1Gate1();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate04Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        if (isThereBigGuy)
        {
            SpawnBigGate3();
        }
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        Spawn3Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor2Gate04Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor2Gate04Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        if (isThereBigGuy)
        {
            SpawnBigGate1();
        }
        Spawn1Gate1();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn3Gate1();
        checkpointPassed = true;
    }





    //Factor 3 Gate1
    IEnumerator Factor3Gate01Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        Spawn2Gate2();
        yield return new WaitForSeconds(4);
        Spawn2Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate01Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(5);
        Spawn2Gate2();
        yield return new WaitForSeconds(2);
        Spawn1Gate2();
        yield return new WaitForSeconds(2);
        Spawn2Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate01Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate1();
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn1Gate1();
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate01Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate1();
        Spawn1Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn3Gate2();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor3Gate01Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        yield return new WaitForSeconds(0.8f);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 3 Gate2 and Gate 3

    IEnumerator Factor3Gate02Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn2Gate1();
        yield return new WaitForSeconds(2);
        Spawn1Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate02Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn3Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate02Check03()
    {
        aRoutineHasStarted = true;
        Spawn3Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        Spawn2Gate1();
        yield return new WaitForSeconds(3);
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate02Check04()
    {
        aRoutineHasStarted = true;
        Spawn3Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn3Gate3();
        yield return new WaitForSeconds(7);
        Spawn3Gate3();
        Spawn3Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor3Gate02Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn2Gate1();
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn1Gate1();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }


    //Factor 3 Gate4 

    IEnumerator Factor3Gate04Check01()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        yield return new WaitForSeconds(4);
        Spawn1Gate2();
        if (isThereBigGuy)
        {
            SpawnBigGate3();
        }
        yield return new WaitForSeconds(5);
        Spawn2Gate1();
        yield return new WaitForSeconds(8);
        Spawn1Gate3();
        Spawn3Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate04Check02()
    {
        aRoutineHasStarted = true;
        Spawn2Gate1();
        yield return new WaitForSeconds(5);
        Spawn1Gate2();
        yield return new WaitForSeconds(5);
        Spawn2Gate3();
        yield return new WaitForSeconds(8);
        if (isThereBigGuy)
        {
            SpawnBigGate2();
        }
        Spawn1Gate2();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate04Check03()
    {
        aRoutineHasStarted = true;
        Spawn1Gate1();
        Spawn1Gate2();
        yield return new WaitForSeconds(1);
        Spawn1Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate2();
        Spawn2Gate1();
        if (isThereBigGuy)
        {
            SpawnBigGate1();
        }
        yield return new WaitForSeconds(3);
        Spawn3Gate3();
        checkpointPassed = true;
    }

    IEnumerator Factor3Gate04Check04()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        yield return new WaitForSeconds(0.8f);
        Spawn2Gate3();
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        checkpointPassed = true;
    }
    IEnumerator Factor3Gate04Check05()
    {
        aRoutineHasStarted = true;
        Spawn2Gate3();
        Spawn1Gate1();
        if (isThereBigGuy)
        {
            SpawnBigGate1();
        }
        yield return new WaitForSeconds(3);
        Spawn1Gate3();
        Spawn1Gate2();
        yield return new WaitForSeconds(3);
        Spawn3Gate1();
        checkpointPassed = true;
    }


}
