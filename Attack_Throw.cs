using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Throw : MonoBehaviour
{
    public float throwForce;
    public GameObject thingToThrow;
    public GameObject throwPosition;

    public GameObject targetedEnemy;

    public Transform enemyDetector1;
    public Transform enemyDetector2;

    public LayerMask intruders;

    public Movement movement;

    //movement max and min
    private float maxDistance = 22;
    private float oldMaxDistance = 22;

    private float minDistance = 17;
    private float oldMinDistance = 17;

    //for adjustments
    private float oldThrowForce;

    private LayerMask whatIsWall;

    //life controllers
    public Life_Attributes life;

    private bool isAlive = true;

    private bool deadAnimationPlayed = false;

    // Wanderdice System
    private bool canRollDice = true;

    private float wanderDice;

    private float timeBtwDice;
    private float startTimeBtwDice = 6;


    // in order to set lapses between attacks 
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int attackPower;

    //Animator calls
    //private bool isWalking; this will be set in the animator

    public Animator animator;

    private bool canWalk = true;


    public GameObject levelConsole;

    public int chipsToGive;

    private void Awake()
    {
        levelConsole = GameObject.FindObjectOfType<MatchInfo>().gameObject;

        Debug.Log("dificulty level is " + levelConsole.GetComponent<MatchInfo>().difficultyLvl);

        
    }

    private void Start()
    {
        movement = this.gameObject.GetComponent<Movement>();
        life = this.gameObject.GetComponent<Life_Attributes>();

        animator = gameObject.GetComponent<Animator>();


        // Values bellow will change accordingly to the difficulty set in MatchInfo

        int hpBoost = levelConsole.GetComponent<MatchInfo>().difficultyLvl * 40;
        int attackBoost = levelConsole.GetComponent<MatchInfo>().difficultyLvl * 5;

        Life_Attributes myLifeAttributes = this.gameObject.GetComponent<Life_Attributes>();

        myLifeAttributes.maxHP += hpBoost;

        myLifeAttributes.hP += hpBoost;
        attackPower += attackBoost;

        oldThrowForce = throwForce;

        whatIsWall = this.gameObject.GetComponent<Movement>().whatIsWall;

    }


    private void FixedUpdate()
    {
        //Remember to set isAlive to false from the Life_Attributes Class
        if(isAlive &&
            !MatchInfo.eventIsOn)
        {
            if (targetedEnemy != null)
            {
                if (this.transform.position.x < targetedEnemy.transform.position.x)
                {
                    if (this.transform.position.x < targetedEnemy.transform.position.x - maxDistance)
                    {
                        if (movement.moveRight)
                        {
                            if(canWalk)
                            {
                                movement.Walker();
                                animator.SetBool("isWalking", true);

                                RaycastHit2D wallDetect = Physics2D.Raycast(enemyDetector2.position, Vector2.right, 2, whatIsWall);

                                if (wallDetect.collider != null)
                                {
                                    if (minDistance >= 0)
                                    {
                                        maxDistance -= 5;
                                        minDistance -= 5;
                                        throwForce -= 0.8f;
                                    }

                                }
                            }
                           
                        }
                        else
                        {
                            movement.Flip();
                        }

                    }
                    else if (this.transform.position.x >= targetedEnemy.transform.position.x - maxDistance &&
                        this.transform.position.x <= targetedEnemy.transform.position.x - minDistance)
                    {
                        if (movement.moveRight)
                        {
                            animator.SetBool("isWalking", false);
                            //Attack
                            Attacker();


                        }
                        else if (!movement.moveRight)
                        {
                            movement.Flip();
                        }

                    }
                    else if (this.transform.position.x > targetedEnemy.transform.position.x - minDistance)
                    {
                        if (!movement.moveRight)
                        {
                            if(canWalk)
                            {
                                movement.Walker();
                                animator.SetBool("isWalking", true);

                                RaycastHit2D wallDetect = Physics2D.Raycast(enemyDetector2.position, Vector2.right, 2, whatIsWall);

                                if (wallDetect.collider != null)
                                {
                                    if (minDistance >= 0)
                                    {
                                        maxDistance -= 5;
                                        minDistance -= 5;
                                        throwForce -= 0.8f;
                                    }

                                }
                            }
                            
                        }
                        else if (movement.moveRight)
                        {
                            movement.Flip();
                        }
                    }

                }

                else if (this.transform.position.x > targetedEnemy.transform.position.x)
                {
                    if (this.transform.position.x > targetedEnemy.transform.position.x + maxDistance)
                    {
                        if (!movement.moveRight)
                        {
                            if(canWalk)
                            {
                                movement.Walker();
                                animator.SetBool("isWalking", true);

                                RaycastHit2D wallDetect = Physics2D.Raycast(enemyDetector2.position, Vector2.right, 2, whatIsWall);

                                if (wallDetect.collider != null)
                                {
                                    if (minDistance >= 0)
                                    {
                                        maxDistance -= 5;
                                        minDistance -= 5;
                                        throwForce -= 0.8f;
                                    }

                                }
                            }
                            
                        }
                        else if (movement.moveRight)
                        {
                            movement.Flip();
                        }

                    }
                    else if (this.transform.position.x <= targetedEnemy.transform.position.x + maxDistance &&
                        this.transform.position.x >= targetedEnemy.transform.position.x + minDistance)
                    {
                        if (!movement.moveRight)
                        {
                            animator.SetBool("isWalking", false);
                            //Attack
                            Attacker();

                        }
                        else if (movement.moveRight)
                        {
                            movement.Flip();
                        }

                    }
                    else if (this.transform.position.x < targetedEnemy.transform.position.x + minDistance)
                    {
                        if (movement.moveRight)
                        {
                            if(canWalk)
                            {
                                movement.Walker();
                                animator.SetBool("isWalking", true);

                                RaycastHit2D wallDetect = Physics2D.Raycast(enemyDetector2.position, Vector2.right, 2, whatIsWall);

                                if (wallDetect.collider != null)
                                {
                                    if (minDistance >= 0)
                                    {
                                        maxDistance -= 5;
                                        minDistance -= 5;
                                        throwForce -= 0.8f;
                                    }

                                }
                            }
                            
                        }
                        else if (!movement.moveRight)
                        {
                            movement.Flip();
                        }
                    }
                }


            }
            else if (targetedEnemy == null)
            {
                WanderAround();
            }
        }
        else if(MatchInfo.eventIsOn)
        {
            animator.SetBool("isWalking", false);
        }
        
        
    }

    private void Update()
    {
        EnemyDetection();
        isAlive = life.isAlive;

        if (isAlive &&
            !MatchInfo.eventIsOn)
        {
            if (targetedEnemy != null)
            {
                if (this.transform.position.y < targetedEnemy.transform.position.y - 6 ||
                    this.transform.position.y > targetedEnemy.transform.position.y + 6)
                {
                    targetedEnemy = null;
                }

                if (targetedEnemy.gameObject.GetComponent<Life_Attributes>() != null &&
                    targetedEnemy.gameObject.GetComponent<Life_Attributes>().hP <= 0)
                {
                    targetedEnemy = null;
                }
            }
        }
        else if (!isAlive &&
            !deadAnimationPlayed)
        {
            animator.SetTrigger("Dead");
            levelConsole.GetComponent<MatchInfo>().ChipGet(chipsToGive);

            this.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            deadAnimationPlayed = true;
        }

    }



    void Attacker()
    {
        if (timeBtwAttack <= 0)
        {
            //Attack
            animator.SetTrigger("Attack");

            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }

    void ThrowingSomething()
    {
        GameObject thrownThing = Instantiate(thingToThrow, throwPosition.transform.position, Quaternion.identity);
        
        
        
        if(thrownThing.gameObject.GetComponent<Granade>() != null)
        {
            thrownThing.gameObject.GetComponent<Granade>().owner = this.gameObject;
            thrownThing.gameObject.GetComponent<Granade>().attackPower = attackPower;
            thrownThing.gameObject.GetComponent<Granade>().moveRight = this.gameObject.GetComponent<Movement>().moveRight;
            thrownThing.gameObject.GetComponent<Granade>().thrownSpeed = throwForce;
        }
        else if(thrownThing.gameObject.GetComponent<RazorBlade>() != null)
        {
            thrownThing.gameObject.GetComponent<RazorBlade>().owner = this.gameObject;
            thrownThing.gameObject.GetComponent<RazorBlade>().attackPower = attackPower;
            thrownThing.gameObject.GetComponent<RazorBlade>().moveRight = this.gameObject.GetComponent<Movement>().moveRight;
            thrownThing.gameObject.GetComponent<RazorBlade>().thrownSpeed = throwForce;
        }
    }


 
    void EnemyDetection()
    {
        if(targetedEnemy == null)
        {
            throwForce = oldThrowForce;
            maxDistance = oldMaxDistance;
            minDistance = oldMinDistance;

            RaycastHit2D isThisEnemy = Physics2D.Raycast(enemyDetector1.position, Vector2.down, 10, intruders);

            if (isThisEnemy.collider != null)
            {
                targetedEnemy = isThisEnemy.collider.gameObject;
                
            }
            else if(isThisEnemy.collider == null)
            {

                if (movement.moveRight)
                {
                    RaycastHit2D isThisEnemy2 = Physics2D.Raycast(enemyDetector2.position, Vector2.right, 20, intruders);

                    if (isThisEnemy2.collider != null)
                    {
                        Debug.Log("Second Ray found something");
                        targetedEnemy = isThisEnemy2.collider.gameObject;
                    }
                }
                else if(!movement.moveRight)
                {
                    RaycastHit2D isThisEnemy2 = Physics2D.Raycast(enemyDetector2.position, Vector2.left, 20, intruders);

                    if (isThisEnemy2.collider != null)
                    {
                        Debug.Log("Second Ray found something");
                        targetedEnemy = isThisEnemy2.collider.gameObject;
                    }
                }
                

            }
        }
        
    }


    void WanderAround()
    {
        if (wanderDice <= 1.6)
        {
            if(canWalk)
            {
                movement.Walker();
                animator.SetBool("isWalking", true);
            }
            
        }
        else if (wanderDice <= 2.1)
        {
            animator.SetBool("isWalking", false);
        }
        else if (wanderDice > 2.1)
        {
            movement.Flip();
            wanderDice = 1.5f;
        }



        if(timeBtwDice <= 0)
        {
            wanderDice = Random.Range(0, 2.5f);
            timeBtwDice = startTimeBtwDice;
        }
        else
        {
            timeBtwDice -= Time.deltaTime;
        }

    }

    public void BackToIdle()
    {
        animator.SetTrigger("canGoBackToIdle");
    }


    public void DeadBye()
    {
        Destroy(this.gameObject);
    }

    public void AnimCannotWalk()
    {
        //this functions will fix the problem of enemies moving when they're not supposed to

        canWalk = false;
    }

    public void AnimCanWalk()
    {
        //this functions will fix the problem of enemies moving when they're not supposed to

        canWalk = true;
    }


    /*
    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Bopongi closestEnemy = null;
        Bopongi[] allEnemies = GameObject.FindObjectsOfType<Bopongi>();

        foreach (Bopongi currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;

                targetedEnemy = currentEnemy.transform.position;
            }
        }

        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }

    void EnemyAttackOrMove()
    {
        if (targetedEnemy.x - this.transform.position.x > 1 ||
            targetedEnemy.x - this.transform.position.x < -1)
        {
            Flip();
            Movement();
            if (targetedEnemy.x > this.transform.position.x)
            {
                moveRight = true;
            }
            else
            {
                moveRight = false;
            }

        }
        else
        {
            Flip();
            Attacking();

            if (targetedEnemy.x > this.transform.position.x)
            {
                moveRight = true;
            }
            else
            {
                moveRight = false;
            }
        }

    }

      
     */

}
