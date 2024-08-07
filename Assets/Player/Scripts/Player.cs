 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour
{
    public delegate void MyEventHandler();

    public static event MyEventHandler CorruptionEvent;

    public static event MyEventHandler LosingHealthShieldEvent;


    private PlayerController playerController;

    [SerializeField] PlayerHealthBar healthBar;

  
    public int maxCorupption = 100;
    public int currentCorupption = 0;

    public int numberOfHealthShield = 3;
    public int maxHealth = 100;
    public int currentHealth = 0;
    public bool isPlayerDead = false;
    private Material material;

    private SmartEnemyAgent agent;

    private int essenceCounter = 0;


    private void OnEnable()
    {
      
        LosingHealthShieldEvent += OnHealthShieldLost;
    }

    private void OnDisable()
    {
        LosingHealthShieldEvent -= OnHealthShieldLost;
    }
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerController = GetComponent<PlayerController>();
        agent = FindAnyObjectByType<SmartEnemyAgent>();  
    }

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void OnGainCorruption(int value)
    {

        if (currentCorupption < maxCorupption)
        {
            currentCorupption += value;
            Debug.Log(currentCorupption);

            if (currentCorupption > maxCorupption)
            {
                currentCorupption = maxCorupption;
            }

            if(currentCorupption == maxCorupption)
            {
                ChangeShader();
            }
        }

        CorruptionEvent?.Invoke();
    }

    //private IEnumerator FlashOnHit()
    //{
    //    material.SetFloat("_Flash", 1);
    //    yield return new WaitForSeconds(0.5f);
    //    material.SetFloat("_Flash", 0);
    //}
    public void OnTakingDamage(int value)
    {
        
        //StartCoroutine(FlashOnHit());   
        if (currentHealth > 0)
        {
            
            playerController.rb.velocity += playerController.PlayerMovementManager.currentDirection * -5;
            currentHealth -= value;
            
            if (currentHealth <=0)
            {
                LosingHealthShieldEvent?.Invoke();

            }
        }
       
    }

    private void OnHealthShieldLost()
    {
        numberOfHealthShield--;
        if (numberOfHealthShield <= 0)
        {
            playerController.ChangeState(PlayerStateEnum.Death);
            //PlayerDeathEvent?.Invoke();
            //agent.SetReward(3f);
            //agent.EndEpisode();
        }
        currentHealth = maxHealth;
    }

    //public void OnKnockBack(Vector2 launchVector,float enemyXpos)
    //{
    //    if(this.transform.position.x - enemyXpos < 0)
    //    {
    //        playerController.rb.velocity += new Vector2(- launchVector.x, launchVector.y);
    //    }
    //    else
    //    {
    //        playerController.rb.velocity += new Vector2(launchVector.x, launchVector.y);
    //    }
        
    //}

    private void ChangeShader()
    {
        material.SetFloat("_isCorrupt", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnGainCorruption(5);

        }

    }

    public void OnEssencePickUp()
    {
        essenceCounter++;
        Debug.Log(essenceCounter);
    }

  
}
