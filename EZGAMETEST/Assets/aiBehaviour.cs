using System;
using System.Collections.Generic;
using UnityEngine;

public class aiBehaviour : MonoBehaviour
{
    private PlayerController botController;
    [SerializeField] private PlayerController playerController;

    private List<Action> botMoves;

    private float turnTime = 2f; //seconds 

    GameObject playerObj;

    void Awake()
    {
        botController = GetComponent<PlayerController>();
        if(botController == null)
        {
            Debug.LogError("PlayerController component not found on the bot object.");
            return;
        }

        playerObj = GameObject.Find("Player");
        if(playerController == null)
        {
            Debug.LogError("PlayerController component not found on the Player object.");
            return;
        }

        playerController = playerObj.GetComponent<PlayerController>();

        botMoves = new List<Action>
        {
            botController.Punch,
            botController.DodgeLeft,
            botController.DodgeRight,
            botController.Block
        };
    }

    void Start()
    {
        StartCoroutine(AIBehaviourLoop());
        // StartCoroutine(AIBehaviourLoopTesting());
    }


    private System.Collections.IEnumerator AIBehaviourLoopTesting()
    {
        Debug.Log("AI Behaviour Loop started");
        while (true)
        {
            yield return new WaitForSeconds(turnTime);
            if (playerController != null)
            {
                botController.DodgeLeft();
            }
            else
            {
                Debug.LogWarning("no playerController found, cannot decide move.");
            }
        }
    }
    private System.Collections.IEnumerator AIBehaviourLoop()
    {
        Debug.Log("AI Behaviour Loop started");
        while (true)
        {
            yield return new WaitForSeconds(turnTime);
            if (playerController != null)
            {
                decideMove();
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

    private void decideMove()
    {
        int actionNum = getActionNum(playerController.playerActions);
        if (actionNum >= 0 && actionNum < botMoves.Count)
        {
            botMoves[actionNum]?.Invoke();
        }
    }

    private int getActionNum(List<string> actions)
    {
        string[] actionNames = { "Punch", "DodgeLeft", "DodgeRight", "Block" };
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
            weights[i] = maxWeight - weights[i] + 1; 
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
}
