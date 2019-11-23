using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MatchInfo : MonoBehaviour
{
    [Header("What level is this")]
    public int zone;
    public int area;
    private int thisArea;
    //Because in arrays value 1 is declared as 0, so we will have to take 1 out of the number 

    [Header("Mission Rewards")]
    public int medallionsPerGate;
    public int goldPerGate;

    //Choose 2 medallions to give away in this round : 1= Plasma 2= Defense 3 = Health 4 = Turret 5 = Shield
    //Update. Medallions will now be selected as a random number
    [HideInInspector]
    public int medalionID01;
    [HideInInspector]
    public int medalionID02;

    [HideInInspector]
    public int medalionGot01;
    [HideInInspector]
    public int medalionGot02;



    // use the same format for new medals
    [HideInInspector]
    public int gatesToOpen = 4;
    [HideInInspector]
    public static int currentGate = 1;

    public GateCrystAnim GateCrystal01;
    public GateCrystAnim GateCrystal02;
    public GateCrystAnim GateCrystal03;
    public GateCrystAnim GateCrystal04;


    public int difficultyLvl;

    [Header("Chips for this Match")]
    public int chips;
    [HideInInspector]
    public int gold;

    public static bool eventIsOn = true;

    //UI elements

    private GameObject player;

    private Life_Attributes playerLife;
    private bool gameOverScreenCalled = false;

    private bool gameWin = false;

    [Header("UI Elements")]
    public Image lifeBar;
    private Animator lifeBarStatus;

    public Image gadgetDisplay;

    private Animator gadgetDisplayAnim;

    public int gadgetIdMaxValue;

    public static int currentGadgetId;

    //Numbers
    [Header ("Numbers to Text")]
    public TextMeshProUGUI chipsText;
    public TextMeshProUGUI gadgetCostText;

    [Header("Gadget Prices")]
    public int keyCrackerPrice;
    public int turretPrice;
    public int shieldPrice;

    public int currentGadgetCost;


    public bool gameIsPaused = false;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
        playerLife = player.gameObject.GetComponent<Life_Attributes>();

        lifeBarStatus = lifeBar.gameObject.GetComponent<Animator>();
        gadgetDisplayAnim = gadgetDisplay.gameObject.GetComponent<Animator>();



        //Because this will reference an array and arrays start with 0
        thisArea = area - 1;

        medalionID01 = (int)Random.Range(1, 6);

        if (medalionID01 >= 5)
        {
            medalionID01 = 5;
        }
        medalionID02 = (int)Random.Range(1, 5);
        if(medalionID02 == medalionID01)
        {
            if(medalionID01 != 5)
            {
                medalionID02 = 5;
            }
            else
            {
                medalionID02 = medalionID01 + 1;
            }
           

            if (medalionID02 >= 5)
            {
                medalionID02 = 1;
            }
        }

        

        Debug.Log("MedalionID01 = " + medalionID01 + " and MedalionID02 = " + medalionID02);

    }

    // Start is called before the first frame update
    void Start()
    {
        eventIsOn = true;
        currentGadgetId = 0;
        currentGate = 1;


    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = (float)playerLife.hP / (float)playerLife.maxHP;

        if(lifeBar.fillAmount == 1)
        {
            lifeBarStatus.SetInteger("LifeState", 0);
            gameOverScreenCalled = false;
        }
        else if(lifeBar.fillAmount < 1 &&
            lifeBar.fillAmount >= 0.65f)
        {
            lifeBarStatus.SetInteger("LifeState", 1);
        }
        else if (lifeBar.fillAmount < 0.65 &&
            lifeBar.fillAmount >= 0.4)
        {
            lifeBarStatus.SetInteger("LifeState", 2);
        }
        else if(lifeBar.fillAmount < 0.4)
        {
            lifeBarStatus.SetInteger("LifeState", 3);
        }



        if (!playerLife.isAlive &&
        !gameOverScreenCalled)
        {
            //Change this to make an event
            CallGameOver();
            gameOverScreenCalled = true;
            Debug.Log("Game Over Called");
        }

        if (currentGadgetId == 0)
        {
            gadgetDisplayAnim.SetInteger("gadgetID", 0);
            currentGadgetCost = keyCrackerPrice;
        }
        else if(currentGadgetId == 1)
        {
            gadgetDisplayAnim.SetInteger("gadgetID", 1);
            currentGadgetCost = turretPrice;
        }
        else if(currentGadgetId == 2)
        {
            gadgetDisplayAnim.SetInteger("gadgetID", 2);
            currentGadgetCost = shieldPrice;
        }




        chipsText.text = chips.ToString();
        gadgetCostText.text = currentGadgetCost.ToString();

    }

    public void GadgetDispButton()
    {
        if(currentGadgetId < gadgetIdMaxValue)
        {
            Debug.Log("gadget ID = " + currentGadgetId);
            currentGadgetId += 1;
        }
        else if(currentGadgetId == gadgetIdMaxValue)
        {
            currentGadgetId = 0;
        }
        Debug.Log("GadgetBut Pressed");
    }

    public void ChipGet(int chipsReceived)
    {
        int oldChips = chips;
        KeyCracker[] allKeyCrackers = FindObjectsOfType<KeyCracker>();
        foreach (KeyCracker cracker in allKeyCrackers)
        {
            int objectLevel = cracker.gameObject.GetComponent<KeyCracker>().objectLevel;

            chips += (chipsReceived * objectLevel);



            cracker.gameObject.GetComponent<KeyCracker>().TransmitAnimation();

            Debug.Log("You got " + chipsReceived + " chips from a KC level" + objectLevel);

        }

        chips += 8;

        if (chips > 3141)
        {
            chips = 3141;
        }

        Debug.Log("You had " + oldChips + "chips, but now you have " + chips);

        this.gameObject.GetComponent<EnemySpawnSys>().gateProgress += 4;

    }

    public void OpeningaGate()
    {
        Debug.Log("OpenGate was calledout in MatchInfo");
        eventIsOn = true; 
        if(gatesToOpen > 4)
        {
            gatesToOpen = 4;
        }

        gatesToOpen -= 1;

        if (gatesToOpen == 3)
        {
            GateCrystal01.OpenGate();
            medalionGot02 += medallionsPerGate;
        }
        else if (gatesToOpen == 2)
        {
            GateCrystal02.OpenGate();
            medalionGot01 += medallionsPerGate;
        }
        else if(gatesToOpen == 1)
        {
            GateCrystal03.OpenGate();
            medalionGot02 += medallionsPerGate;
        }
        else if(gatesToOpen <= 0)
        {
            GateCrystal04.OpenGate();
            medalionGot01 += medallionsPerGate;
        }


        gold += goldPerGate;

        Debug.Log("Gates to open = " + gatesToOpen);

        if(gatesToOpen <= 0 &&
            playerLife.isAlive)
        {
            DJManager dJManager = this.gameObject.GetComponent<DJManager>();
            StartCoroutine(dJManager.AreaClearClip());
            gold *= 2;
            gameWin = true;
            //Win the game
            this.gameObject.GetComponent<InterMatchEvents>().AreaClearEvent();
            Debug.Log("Gates left is 0 and Player is Alive!");
        }
        else
        {
            //GateEvent
            this.gameObject.GetComponent<InterMatchEvents>().GateComplete();
            currentGate += 1;
            chips += 30;

            DJManager dJManager = this.gameObject.GetComponent<DJManager>();
            StartCoroutine(dJManager.GateCompletePlayJingle());


        }



    }


    void CallGameOver()
    {
        eventIsOn = true;
        DJManager dJManager = this.gameObject.GetComponent<DJManager>();
        StartCoroutine(dJManager.GameOverClip());

        //call a Game over screen and ask the pleayer to watch an ad for double gold
        this.gameObject.GetComponent<InterMatchEvents>().GameOverEvent();
    }


    public void ClosingMatch()
    {
        if(gameOverScreenCalled)
        {
            Player.gold += gold;
            SceneManager.LoadScene(0);
        }
        else
        {
            //medallions to give away in this round : 1= Plasma 2= Defense 3 = Health 4 = Turret 5 = Shield
            switch(medalionID01)
            {
                case 1:
                    Player.plasmaMedallionsInHand += medalionGot01;
                    break;
                case 2:
                    Player.defenseMedallionsInHand += medalionGot01;
                    break;
                case 3:
                    Player.healthMedallionsInHand += medalionGot01;
                    break;
                case 4:
                    Player.turretMedallionsInHand += medalionGot01;
                    break;
                case 5:
                    Player.shieldMedallionsInHand += medalionGot01;
                    break;
            }

            switch (medalionID02)
            {
                case 1:
                    Player.plasmaMedallionsInHand += medalionGot02;
                    break;
                case 2:
                    Player.defenseMedallionsInHand += medalionGot02;
                    break;
                case 3:
                    Player.healthMedallionsInHand += medalionGot02;
                    break;
                case 4:
                    Player.turretMedallionsInHand += medalionGot02;
                    break;
                case 5:
                    Player.shieldMedallionsInHand += medalionGot02;
                    break;
            }

            Player.gold += gold;

            Player.moonstone += 2;

            Debug.Log("Player PlasmaMeds = " + Player.plasmaMedallionsInHand);
            Debug.Log("Player DefMeds = " + Player.defenseMedallionsInHand);
            Debug.Log("Player HealthMeds = " + Player.healthMedallionsInHand);
            Debug.Log("Player TurretMeds = " + Player.turretMedallionsInHand);
            Debug.Log("Player ShieldMeds = " + Player.shieldMedallionsInHand);
            Debug.Log("Player gold = " + Player.gold);
            Debug.Log("Player moonstone = " + Player.moonstone);

            //If you are Adding new areas, this is what you need to modify. Don^t move anything else
            switch (zone)
            {
                case 0:
                    Debug.Log("No area will be unlocked");
                    break;
                case 1:
                    Player.zone1Cleared[thisArea] = true;
                    break;
                case 2:
                    Player.zone2Cleared[thisArea] = true;
                    break;
                case 3:
                    Player.zone3Cleared[thisArea] = true;
                    break;
                case 4:
                    Player.zone4Cleared[thisArea] = true;
                    break;
                case 5:
                    Player.zone5Cleared[thisArea] = true;
                    break;
            }
            SceneManager.LoadScene(0);

        }

    }

    public void AudioClipPlayer(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, this.transform.position);
    }

    public void PauseMenuButton()
    {
        if(!gameIsPaused &&
            !eventIsOn)
        {
            gameIsPaused = true;
            this.gameObject.GetComponent<InterMatchEvents>().PauseMenu();
        }
    }


    public void toMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
