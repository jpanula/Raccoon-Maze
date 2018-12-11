using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "ScriptableObjects/Information/SoundLibrary", order = 1)]
public class SoundLibrary : ScriptableObject
{

    public AudioClip Footsteps;
    public AudioClip Watersteps;
    public AudioClip Death;
    public AudioClip Hit;
    public AudioClip PowerUp;

    public AudioClip Dash;
    public AudioClip Jump;

    public AudioClip FireballCast;
    public AudioClip FireballHit;

    public AudioClip ArcaneMissileCast;
    public AudioClip ArcaneMissileHit;

    public AudioClip WallCrackles;
    public AudioClip WallWeaponHit;

    public AudioClip FireTrap;
    public AudioClip SpikeTrapActivation;
    public AudioClip SpikeTrapHit;
    public AudioClip SpikeTrapUp;

    public AudioClip MenuClick;
    public AudioClip MenuScroll;
    public AudioClip MenuMusic;

    public AudioClip LevelStartMusic;
    public AudioClip FourPlayerMusic;
    public AudioClip ThreePlayerMusic;
    public AudioClip TwoPlayerMusic;

    // Use this for initialization
    /*
    private void Awake()
    {
        InGameMusic = false;
        Wins = new List<int>();
        LevelRotation = new bool[] { false, false, false };
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < Wins.Count; i++)
        {
            Wins[i] = 0;
            LevelRotation = new bool[] { false, false, false };
            InGameMusic = false;
        }
    }
    */
}
