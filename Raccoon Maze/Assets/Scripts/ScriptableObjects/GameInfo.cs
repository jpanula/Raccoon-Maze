using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "ScriptableObjects/Information/GameInfo", order = 1)]
public class GameInfo : ScriptableObject {

    public bool[] LevelRotation;
    public int PlayerAmount;
    public List<int> Wins;
    public int WinGoal;


    // Use this for initialization
    private void Awake()
    {
        Wins = new List<int>();
        LevelRotation = new bool[] {false, false, false};
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < Wins.Count; i++)
        {
            Wins[i] = 0;
        }
    }
}
