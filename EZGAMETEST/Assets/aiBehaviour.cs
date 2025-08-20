using System;
using System.Collections.Generic;
using UnityEngine;

public class aiBehaviour : MonoBehaviour
{
    private PlayerController botController;
    private PlayerController playerController;

    private List<Action> botMoves;

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

        playerController = playerObj.GetComponent<PlayerController>();

        botMoves = new List<Action>
        {
            botController.Punch,
            botController.DodgeLeft,
            botController.DodgeRight,
            botController.Block
        };

        decideMove();
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

        // Look at the last 10 actions
        int historyLength = Mathf.Min(10, actions.Count);
        for (int i = actions.Count - historyLength; i < actions.Count; i++)
        {
            string act = actions[i];
            for (int j = 0; j < actionCount; j++)
            {
                if (actions[i] == actionNames[j])
                    weights[j]++;
            }
        }

        int maxWeight = historyLength > 0 ? historyLength : 1;
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
