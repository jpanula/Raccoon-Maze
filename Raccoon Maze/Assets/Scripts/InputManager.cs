using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public static class InputManager
{
    public static Dictionary<string, string> keyboardMapping;
    public static Dictionary<string, string> gamepadMapping;
    public static int[] GamepadControl;
    public static bool[] GamepadsConnected;
    public static bool MenuGamepad;
    public static bool StartGame;
    public static int GamepadDisconnected;

    static string[] keyMaps = new string[39]
    {
            "P1Up",
            "P1Down",
            "P1Left",
            "P1Right",
            "P1Melee",
            "P1Ability1",
            "P1Ability2",
            "P1DirLock",

            "P2Up",
            "P2Down",
            "P2Left",
            "P2Right",
            "P2Melee",
            "P2Ability1",
            "P2Ability2",
            "P2DirLock",

            "P3Up",
            "P3Down",
            "P3Left",
            "P3Right",
            "P3Melee",
            "P3Ability1",
            "P3Ability2",
            "P3DirLock",

            "P4Up",
            "P4Down",
            "P4Left",
            "P4Right",
            "P4Melee",
            "P4Ability1",
            "P4Ability2",
            "P4DirLock",

            "Select",
            "Back",
            "Help",
            "Up",
            "Down",
            "Left",
            "Rigth"
};

    static string[] keyboard = new string[39]
    {
            //Player 1
            "W",
            "S",
            "A",
            "D",
            "X",
            "C",
            "V",
            "Space",

            //Player 2
            "UpArrow",
            "DownArrow",
            "LeftArrow",
            "RightArrow",
            "Keypad1",
            "Keypad2",
            "Keypad3",
            "KeypadEnter",

            //Player 3
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",

            //Player 4
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",

            //MenuMovement
            "Alpha3",
            "Alpha3",
            "Alpha3",
            "Alpha3",
            "Alpha3",
            "Alpha3",
            "Alpha3"
    };

    static string[] gamepad = new string[39]
    {
            //Player 1
            "Axis2-",
            "Axis2+",
            "Axis1-",
            "Axis1+",
            "Button0",
            "Button1",
            "Button2",
            "Button5",
        
            //Player 2
            "Axis2-",
            "Axis2+",
            "Axis1-",
            "Axis1+",
            "Button0",
            "Button1",
            "Button2",
            "Button5",


            //Player 3
            "Axis2-",
            "Axis2+",
            "Axis1-",
            "Axis1+",
            "Button0",
            "Button1",
            "Button2",
            "Button5",

            //Player 4
            "Axis2-",
            "Axis2+",
            "Axis1-",
            "Axis1+",
            "Button0",
            "Button1",
            "Button2",
            "Button5",

            //MenuMovement
            "Axis2+",
            "Axis2-",
            "Axis1+",
            "Axis1-",
            "Button0",
            "Button1",
            "Button2"
    };

    static InputManager()
    {
        InitializeDictionaries();
        GamepadControl = new int[4];
        GamepadsConnected = new bool[10];
        IniatilizeGamepads();
        StartGame = false;
        MenuGamepad = false;
        GamepadDisconnected = 0;
    }

    private static void InitializeDictionaries()
    {
        keyboardMapping = new Dictionary<string, string>();
        gamepadMapping = new Dictionary<string, string>();

        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyboardMapping.Add(keyMaps[i], keyboard[i]);
            gamepadMapping.Add(keyMaps[i], gamepad[i]);
        }

        Load();
    }

    public static void IniatilizeGamepads()
    {
        for (int i = 0; i < GamepadControl.Length; i++)
        {
            GamepadControl[i] = -1;
        }

        for (int i = 0; i < GamepadsConnected.Length; i++)
        {
            GamepadsConnected[i] = false;
        }
    }

    public static void SetKeyMap(string keyMap, string key, bool joystickControl)
    {
        if (joystickControl)
        {
            gamepadMapping[keyMap] = key;
            PlayerPrefs.SetString(keyMap + "J", key);
        }
        else
        {
            keyboardMapping[keyMap] = key;
            PlayerPrefs.SetString(keyMap + "K", key);
        }
    }

    public static bool GetKeyDown(string keyMap, bool joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));

        if (joystickControl)
        {
            if (gamepadMapping[keyMap].Length == 6)
            {
                if (gamepadMapping[keyMap].Substring(5) == "+")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) > 0.5f)
                    {
                        return true;
                    }
                }
                else if (gamepadMapping[keyMap].Substring(5) == "-")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) < -0.5f)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + joystickNum + gamepadMapping[keyMap]));
            }
        }
        else
        {
            return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    public static bool GetKeyUp(string keyMap, bool joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));

        if (joystickControl)
        {
            if (gamepadMapping[keyMap].Length == 6)
            {
                if (Mathf.Abs(Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5))) < 0.49f)
                {
                    return true;
                }
            }
            else
            {
                return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + joystickNum + gamepadMapping[keyMap]));
            }

        }
        else
        {
            return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    public static bool GetKey(string keyMap, bool joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));

        if (joystickControl)
        {
            if (gamepadMapping[keyMap].Length == 6)
            {
                if (gamepadMapping[keyMap].Substring(5) == "+")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) > 0.5f)
                    {
                        return true;
                    }
                }
                else if (gamepadMapping[keyMap].Substring(5) == "-")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) < -0.5f)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + joystickNum + gamepadMapping[keyMap]));
            }

        }
        else
        {
            return Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    /*
    public static bool GetKeyDown(string keyMap, bool joystickControl)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));

        if (joystickControl)
        {
            if (gamepadMapping[keyMap].Length == 5)
            {
                if (gamepadMapping[keyMap].Substring(4) == "+")
                {
                    if (Input.GetAxis(gamepadMapping[keyMap].Substring(0, 4)) > 0.5f)
                    {
                        return true;
                    }
                }
                else if (gamepadMapping[keyMap].Substring(4) == "-")
                {
                    if (Input.GetAxis(gamepadMapping[keyMap].Substring(0, 4)) < -0.5f)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), gamepadMapping[keyMap]));
            }
        }
        else
        {
            return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    public static bool GetKeyUp(string keyMap, bool joystickControl)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));
        if (joystickControl)
        {

            if (gamepadMapping[keyMap].Length == 5)
            {
                if (Mathf.Abs(Input.GetAxis(gamepadMapping[keyMap].Substring(0, 4))) < 0.49f)
                {
                    return true;
                }
            }
            else
            {
                return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), gamepadMapping[keyMap]));
            }

        }
        else
        {
            return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    public static bool GetKey(string keyMap, bool joystickControl)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));
        if (joystickControl)
        {
            if (gamepadMapping[keyMap].Length == 5)
            {
                if (gamepadMapping[keyMap].Substring(4) == "+")
                {
                    if (Input.GetAxis(gamepadMapping[keyMap].Substring(0, 4)) > 0.5f)
                    {
                        return true;
                    }
                }
                else if (gamepadMapping[keyMap].Substring(4) == "-")
                {
                    if (Input.GetAxis(gamepadMapping[keyMap].Substring(0, 4)) < -0.5f)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), gamepadMapping[keyMap]));
            }

        }
        else
        {
            return Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }

        return false;
    }

    */

    public static void Load()
    {
        //Debug.Log("moi");

        foreach (KeyValuePair<string, string> entry in gamepadMapping.ToList())
        {
            if (PlayerPrefs.GetString(entry.Key + "J", entry.Value) != "defaultValue")
            {
                gamepadMapping[entry.Key] = PlayerPrefs.GetString(entry.Key + "J", entry.Value);
            }
        }

        foreach (KeyValuePair<string, string> entry in keyboardMapping.ToList())
        {
            if (PlayerPrefs.GetString(entry.Key + "K", entry.Value) != "defaultValue")
            {
                keyboardMapping[entry.Key] = PlayerPrefs.GetString(entry.Key + "K", entry.Value);
            }
        }
    }

    public static void SetGamepadControl(int val, int jNum)
    {
        GamepadControl[jNum] = val;
    }

    private static void SetGamepadsConnected(bool val, int jNum)
    {
        GamepadsConnected[jNum] = val;
    }

    public static void SetStartGame(bool val)
    {
        StartGame = val;
    }

    public static int GetGamepadControl(int jNum)
    {
        return GamepadControl[jNum];
    }

    public static bool GetGamepadsConnected(int jNum)
    {
        return GamepadsConnected[jNum];
    }

    public static bool GetStartGame()
    {
        return StartGame;
    }

    public static void CheckGamepadConnection()
    {
        string[] temp = Input.GetJoystickNames();
        /*
        Debug.Log("1: " + GamepadControl[0]);
        Debug.Log("2: " + GamepadControl[1]);
        /*
        Debug.Log(Input.GetJoystickNames().Length);
        Debug.Log("1: " + Input.GetJoystickNames()[0]);
        Debug.Log("2: " + Input.GetJoystickNames()[1]);
        //Debug.Log("3: " + Input.GetJoystickNames()[2]);
        //Debug.Log("4: " + Input.GetJoystickNames()[3]);
        //*/
        int j = 1;
        for (int i = 0; i < GamepadsConnected.Length; i++)
        {
            //Debug.Log(temp.Length + " " + i);
            if (temp.Length - 1 >= i)
            {
                //Debug.Log(i + " connected?");
                if (string.IsNullOrEmpty(temp[i]))
                {
                    //Debug.Log("yes");
                    SetGamepadsConnected(false, i);
                    GamepadControl[j] = -1;
                    //Debug.Log("ohjain irti " + i);
                }
                else
                {
                    //Debug.Log("no");
                    SetGamepadsConnected(true, i);
                    GamepadControl[j] = i;
                    //Debug.Log("ohjain kiinni " + i);
                }
                j++;

            }


            /*
            if (!GetGamepadsConnected(i))
            {
                /*
                for (int j = 0; j < GamepadControl.Length; j++)
                {
                    //Debug.Log(GamepadControl[j] + " " + i);
                    if (GamepadControl[j] == i + 1)
                    {
                        if(GamepadDisconnected == 0)
                        {
                            GamepadDisconnected = i + 1;
                            Debug.Log("pelaajan ohjain disconnected");
                        }
                    }
                }
                //*/
            /*
            if (GamepadControl[pNum] == i + 1)
            {
                if (GamepadDisconnected == 0)
                {
                    GamepadDisconnected = pNum + 1;
                    GamepadControl[pNum] = -1;
                    Debug.Log("pelaajan ohjain disconnected");
                }
            }
            //*/
            //}
        }
    }

    public static void ResetGamepadControl()
    {
        for (int i = 0; i < GamepadControl.Length; i++)
        {
            SetGamepadControl(-1, i);
        }
    }
}