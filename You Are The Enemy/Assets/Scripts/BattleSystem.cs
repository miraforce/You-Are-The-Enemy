using UnityEngine;
using TMPro;
using System.Collections;

public enum BattleState {start, playerTurn, enemyTurn, won, lost};
public class BattleSystem : MonoBehaviour
{
    //initialize variables
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = " A wild " + enemyUnit.unitName + " appeared...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.playerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose your action";
    }

    IEnumerator PlayerAttack()
    {
        //damage the enemy
        bool isDead = enemyUnit.takeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "teh attack is successful";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //end the battle
            state = BattleState.won;
            EndBattle();
        }
        else
        {
            //enemy turn
            state = BattleState.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
        //check if the enemy is dead
        //change state based on what happened
    }

    IEnumerator PlayerHeal ()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "u heal";

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.takeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.lost;
            EndBattle();
        }
        else
        {
            state = BattleState.playerTurn;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.won)
        {
            dialogueText.text = "u won";
        }
        else if (state == BattleState.lost)
        {
            dialogueText.text = "u lost";
        }
    }

    //Button scripts
    public void OnAttackButton()
    {
        Debug.Log("Attacking...");
        if (state != BattleState.playerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        Debug.Log("Healing...");
        if (state != BattleState.playerTurn)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
}
