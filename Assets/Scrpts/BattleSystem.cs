using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, PLAYERTURN2, PLAYERTURN3, ENEMYTURN, ENEMYTURN2, ENEMYTURN3, ACTION, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject player2Prefab;
    public GameObject enemy2Prefab;

    public GameObject player3Prefab;
    public GameObject enemy3Prefab;

    public Transform playerPlayerHud;
    public Transform enemyEnemyHud;

    public Transform playerPlayerHud2;
    public Transform enemyEnemyHud2;

    public Transform playerPlayerHud3;
    public Transform enemyEnemyHud3;

    public GameObject HPBar1Prefab;
    public GameObject HPBar2Prefab;
    public GameObject HPBar3Prefab;
    public GameObject HPBar4Prefab;
    public GameObject HPBar5Prefab;
    public GameObject HPBar6Prefab;


    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerPlayerHud);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyEnemyHud);
        enemyUnit = enemyGO.GetComponent<Unit>();

        Instantiate(player2Prefab, playerPlayerHud2);
        //playerUnit = playerGO.GetComponent<Unit>();GameObject playerGO = (<- meant for second player turn = seperate 1st script)

        Instantiate(enemy2Prefab, enemyEnemyHud2);
        //enemyUnit = enemyGO.GetComponent<Unit>();GameObject enemyGO = (<- meant for second enemy turn = seperate 1st script)

        Instantiate(player3Prefab, playerPlayerHud3);
        //playerUnit = playerGO.GetComponent<Unit>();GameObject playerGO = (<- meant for third player turn = seperate 2nd script)

        Instantiate(enemy3Prefab, enemyEnemyHud3);
        //enemyUnit = enemyGO.GetComponent<Unit>();GameObject enemyGO =  (<- meant for third player turn = seperate 2nd script)

        Instantiate(HPBar1Prefab, playerPlayerHud);
        Instantiate(HPBar2Prefab, playerPlayerHud2);
        Instantiate(HPBar3Prefab, playerPlayerHud3);
        Instantiate(HPBar4Prefab, enemyEnemyHud);
        Instantiate(HPBar5Prefab, enemyEnemyHud2);
        Instantiate(HPBar6Prefab, enemyEnemyHud3);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        // Damage the Enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);

        // Check if the enemy is dead 
        if(isDead)
        {
            //End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //Enemy Turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // Change state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        //attacking
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            //won
        } else if (state == BattleState.LOST)
        {
            //lost
        }
    }
    

    void PlayerTurn()
    {
        //choose attack
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

   // IEnumerator PlayerDefend()
   // {
        //playerUnit.Defend(7);

        //playerHUD.SetHP(playerUnit.currentHP);

       // yield return new WaitForSeconds(2f);

       // state = BattleState.ENEMYTURN;
       // StartCoroutine(EnemyTurn());
   // }

   // IEnumerator PlayerMGuard()
  //  {
       // playerUnit.MGuard(7);

      //  playerHUD.SetHP(playerUnit.currentHP);

       // yield return new WaitForSeconds(2f);

      //  state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyTurn());
   // }

    //public void OnAttackButton()
   
        //if (state != BattleState.PLAYERTURN)
           // return;
        //StartCoroutine(PlayerAttack());
   

   // public void OnHealButton()
        //if (state != BattleState.PLAYERTURN)
          //  return;
        //StartCoroutine(PlayerHeal());
   

    


}
