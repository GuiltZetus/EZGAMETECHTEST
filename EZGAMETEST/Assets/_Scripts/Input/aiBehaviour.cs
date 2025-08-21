using System;
using System.Collections.Generic;
using UnityEngine;

public class aiBehaviour : MonoBehaviour
{
    private PlayerController botController;
    [SerializeField] private PlayerController playerController;

    private List<Action> botAttackMoves, botDefendMoves;

    private float turnTime = 2f; //seconds 

    GameObject playerObj;
    string[] attackActionNames = { "DodgeLeft", "DodgeRight", "Block"};
    string[] defendActionNames = {"Punch", "LeftHook", "RightHook"};


    private float aiResponsiveness = 1 , DiffcultyMultiplier =1f;
    private float aiBaseDamage = 10, aiBaseHealth = 100;



    void Awake()
    {
        botController = GetComponent<PlayerController>();
        if (botController == null)
        {
            Debug.LogError("PlayerController component not found on the bot object.");
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the Player object.");
            return;
        }

        DiffcultyMultiplier = GameManager.difficulty;
        setDifficultyMultiplier(DiffcultyMultiplier);

        botAttackMoves = new List<Action>
        {
            botController.Punch,
            botController.leftHook,
            botController.rightHook
        };

        botDefendMoves = new List<Action>
        {
            botController.DodgeLeft,
            botController.DodgeRight,
            // botController.Block
        };
    }


    void Start()
    {
        StartCoroutine(AIBehaviourLoop());
    }


    // private System.Collections.IEnumerator AIBehaviourLoopTesting()
    // {
    //     Debug.Log("AI Behaviour Loop started");
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(turnTime);
    //         if (playerController != null)
    //         {
    //             botController.DodgeRight();
    //         }
    //         else
    //         {
    //             Debug.LogWarning("no playerController found, cannot decide move.");
    //         }
    //     }
    // }

    private System.Collections.IEnumerator AIBehaviourLoop()
    {
        Debug.Log("AI Behaviour Loop started");
        while (true)
        {
            yield return new WaitForSeconds(turnTime);
            if (playerController != null)
            {
                decideAttackMove();
            }
            else if (playerController == null)
            {
                Debug.LogWarning("no playerController found, cannot decide move.");
            }
            else if (playerController.playerActions.Count == 0)
            {
                Debug.LogWarning("No player actions to base AI decision on.");
            }
        }
    }

    public void decideAttackMove()
    {
        int actionNum = getActionNum(playerController.playerActions, attackActionNames);
        if (actionNum >= 0 && actionNum < botAttackMoves.Count)
        {
            botAttackMoves[actionNum]?.Invoke();
        }
    }

    public void decideDodge()
    {
        if (!playerController.characterAnimator.GetBool("isDodging") && !playerController.characterAnimator.GetBool("isPunching"))
        {
            int actionNum = getActionNum(playerController.playerActions, defendActionNames);
            if (actionNum >= 0 && actionNum < botDefendMoves.Count)
            {
                botDefendMoves[actionNum]?.Invoke();
            }
        }
    }

     private int getActionNum(List<string> actions, string[] actionNames)
    {
        int actionCount = actionNames.Length;
        int[] weights = new int[actionCount];

        int playerActionsHistoryLength = Mathf.Min(10, actions.Count);

        for (int i = actions.Count - playerActionsHistoryLength; i < actions.Count; i++)
        {
            string act = actions[i];
            for (int j = 0; j < actionCount; j++)
            {
                if (actions[i] == actionNames[j])
                    weights[j]++;
            }
        }

        int maxWeight = playerActionsHistoryLength > 0 ? playerActionsHistoryLength : 1;
        for (int i = 0; i < actionCount; i++)
        {
            int rawWeight = maxWeight - weights[i] + 1;
            weights[i] = (int)Mathf.Pow(rawWeight, aiResponsiveness);
        }

        int totalWeight = 0;
        for (int i = 0; i < actionCount; i++)
            totalWeight += weights[i];

        int rand = UnityEngine.Random.Range(0, totalWeight);
        int sum = 0;
        for (int i = 0; i < actionCount; i++)
        {
            sum += weights[i];
            if (rand < sum)
                return i;
        }

        return UnityEngine.Random.Range(0, actionCount);
    }

    public void setDifficultyMultiplier(float aiGeneralDiffcultyMultiplier)
    {
        botController.playerHealth = aiBaseHealth * aiGeneralDiffcultyMultiplier;
        botController.playerDamage = aiBaseDamage * aiGeneralDiffcultyMultiplier;
    }
}
