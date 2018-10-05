using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "ScriptableObjects/Information/GameInfo", order = 1)]
public class GameInfo : ScriptableObject {

    public int PlayerAmount;
    public int[] Wins;
    public int WinGoal;


    // Use this for initialization
    private void Awake()
    {
        Wins = new int[PlayerAmount];
        for (int i = 0; i < Wins.Length; i++)
        {
            Wins[i] = 0;
        }
    }

    public void Reset()
    {
        for (int i = 0; i < Wins.Length; i++)
        {
            Wins[i] = 0;
        }
    }
}
