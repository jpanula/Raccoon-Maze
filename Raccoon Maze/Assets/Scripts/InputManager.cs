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
    public static Dictionary<string, string> ps4Mapping;
    public static Dictionary<string, string> xboxOneMapping;
    public static int[] GamepadControl;
    public static bool[] GamepadsConnected;
    public static bool MenuGamepad;
    public static bool StartGame;
    public static int GamepadDisconnected;

    static string[] keyMaps = new string[39]
    {
            "P1MoveHorizontal",
            "P1MoveVertical",
            "P1DirHorizontal",
            "P1DirVertical",
            "P1Melee",
            "P1Ability1",
            "P1Ability2",
            "P1DirLock",

            "P2MoveHorizontal",
            "P2MoveVertical",
            "P2DirHorizontal",
            "P2DirVertical",
            "P2Melee",
            "P2Ability1",
            "P2Ability2",
            "P2DirLock",

            "P3MoveHorizontal",
            "P3MoveVertical",
            "P3DirHorizontal",
            "P3DirVertical",
            "P3Melee",
            "P3Ability1",
            "P3Ability2",
            "P3DirLock",

            "P4MoveHorizontal",
            "P4MoveVertical",
            "P4DirHorizontal",
            "P4DirVertical",
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
    static string[] ps4 = new string[39]
    {
            //Player 1
            "Axis1",
            "Axis2",
            "Axis3",
            "Axis6",
            "Button5",
            "Button7",
            "Button6",
            "Button4",
        
            //Player 2
            "Axis1",
            "Axis2",
            "Axis3",
            "Axis6",
            "Button5",
            "Button7",
            "Button6",
            "Button4",


            //Player 3
            "Axis1",
            "Axis2",
            "Axis3",
            "Axis6",
            "Button5",
            "Button7",
            "Button6",
            "Button4",

            //Player 4
            "Axis1",
            "Axis2",
            "Axis3",
            "Axis6",
            "Button5",
            "Button7",
            "Button6",
            "Button4",

            //MenuMovement
            "Axis1",
            "Axis2",
            "Axis3",
            "Axis4",
            "Button0",
            "Button1",
            "Button2"
    };

    static string[] xboxOne = new string[39]
    {
            //Player 1
            "Axis1",
            "Axis2",
            "Axis4",
            "Axis5",
            "Button5",
            "Axis10",
            "Axis9",
            "Button4",
        
            //Player 2
            "Axis1",
            "Axis2",
            "Axis4",
            "Axis5",
            "Button5",
            "Axis10",
            "Axis9",
            "Button4",


            //Player 3
            "Axis1",
            "Axis2",
            "Axis4",
            "Axis5",
            "Button5",
            "Axis10",
            "Axis9",
            "Button4",

            //Player 4
            "Axis1",
            "Axis2",
            "Axis4",
            "Axis5",
            "Button5",
            "Axis10",
            "Axis9",
            "Button4",

            //MenuMovement
            "Axis1",
            "Axis2",
            "Axis4",
            "Axis5",
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
        //keyboardMapping = new Dictionary<string, string>();
        ps4Mapping = new Dictionary<string, string>();
        xboxOneMapping = new Dictionary<string, string>();

        for (int i = 0; i < keyMaps.Length; ++i)
        {
            //keyboardMapping.Add(keyMaps[i], keyboard[i]);
            ps4Mapping.Add(keyMaps[i], ps4[i]);
            xboxOneMapping.Add(keyMaps[i], xboxOne[i]);
        }

        //Load();
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

    /*
    public static void SetKeyMap(string keyMap, string key, int joystickControl)
    {
        if (joystickControl > 0)
        {
            if(joystickControl == 1)
            {
                ps4Mapping[keyMap] = key;
            }
            if (joystickControl == 2)
            {
                xboxOneMapping[keyMap] = key;
            }
            
            //PlayerPrefs.SetString(keyMap + "J", key);
        }
        else
        {
            keyboardMapping[keyMap] = key;
            //PlayerPrefs.SetString(keyMap + "K", key);
        }
    }
    */
    public static bool GetKeyDown(string keyMap, int joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));
        Dictionary<string, string> gamepadMapping = new Dictionary<string, string>();

        
        if (joystickControl > 0)
        {
            if (joystickControl == 1)
            {
                gamepadMapping = ps4Mapping;
            }
            else if (joystickControl == 2)
            {
                gamepadMapping = xboxOneMapping;
            }
            //Debug.Log("Joystick" + joystickNum + gamepadMapping[keyMap]);
            if (gamepadMapping[keyMap].Length == 5 || gamepadMapping[keyMap].Length == 6)
            {
                //if (gamepadMapping[keyMap].Substring(5) == "+")
                //{
                //Debug.Log(Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap]));
                if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap]) > 0.1f)
                    {
                        //Debug.Log("trigger");
                        return true;
                    }
                //}
                /*
                else if (gamepadMapping[keyMap].Substring(5) == "-")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 4)) < -0.5f)
                    {
                        return true;
                    }
                }
                */
            }
            else
            {
                return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + joystickNum + gamepadMapping[keyMap]));
            }
        }
        /*
        else
        {
            return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }
        */

        return false;
    }

    public static bool GetKeyUp(string keyMap, int joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));

        Dictionary<string, string> gamepadMapping = new Dictionary<string, string>();


        if (joystickControl > 0)
        {
            if (joystickControl == 1)
            {
                gamepadMapping = ps4Mapping;
            }
            else if (joystickControl == 2)
            {
                gamepadMapping = xboxOneMapping;
            }
            if (gamepadMapping[keyMap].Length == 6)
            {
                if (Mathf.Abs(Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 4))) < 0.49f)
                {
                    return true;
                }
            }
            else
            {
                return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + joystickNum + gamepadMapping[keyMap]));
            }

        }
        /*
        else
        {
            return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }
        */

        return false;
    }


    public static float GetAxis(string keyMap, int joystickControl, string joystickNum)
    {
        Dictionary<string, string> gamepadMapping = new Dictionary<string, string>();

        
        if (joystickControl > 0)
        {
            if (joystickControl == 1)
            {
                //Debug.Log("PS4");
                gamepadMapping = ps4Mapping;
            }
            else if (joystickControl == 2)
            {
                //Debug.Log("XBox");
                gamepadMapping = xboxOneMapping;
            }
            //Debug.Log(keyMap);
            if (gamepadMapping[keyMap].Length == 5)
            {
                return Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5));
            }
        }
        return 0;
    }
    public static bool GetKey(string keyMap, int joystickControl, string joystickNum)
    {
        //Debug.Log(keyMap.Length);
        //Debug.Log(keyMap.Substring(4));
        Dictionary<string, string> gamepadMapping = new Dictionary<string, string>();


        if (joystickControl > 0)
        {
            if (joystickControl == 1)
            {
                gamepadMapping = ps4Mapping;
            }
            else if (joystickControl == 2)
            {
                gamepadMapping = xboxOneMapping;
            }
            if (gamepadMapping[keyMap].Length == 6)
            {
                if (gamepadMapping[keyMap].Substring(5) == "+")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) > 0.1f)
                    {
                        return true;
                    }
                }
                else if (gamepadMapping[keyMap].Substring(5) == "-")
                {
                    if (Input.GetAxis("Joystick" + joystickNum + gamepadMapping[keyMap].Substring(0, 5)) < -0.1f)
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
        /*
        else
        {
            return Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), keyboardMapping[keyMap]));
        }
        */

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

        /*
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
    */
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
        int j = 0;
        for (int i = 0; i < GamepadsConnected.Length; i++)
        {
            //Debug.Log(temp.Length + " " + i);
            
            if (temp.Length - 1 >= i)
            {
                Debug.Log("controller " + i  + " " + temp[i]);
                //Debug.Log(temp[i] + " ---- " + j);
                //Debug.Log(i + " connected?");
                if (string.IsNullOrEmpty(temp[i]))
                {
                    //Debug.Log("yes");
                    //SetGamepadsConnected(false, i);
                    //Debug.Log(j);
                    //Debug.Log("ohjain irti " + i);
                }
                else
                {
                    //Debug.Log("no");
                    //SetGamepadsConnected(true, i);
                    GamepadControl[j] = i;
                    j++;
                    //Debug.Log(j);
                    //Debug.Log("ohjain kiinni " + i);
                }
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
        Debug.Log("P1 " + GamepadControl[0]);
        Debug.Log("P2 " + GamepadControl[1]);
        Debug.Log("P3 " + GamepadControl[2]);
        Debug.Log("P4 " + GamepadControl[3]);
    }

    public static void ResetGamepadControl()
    {
        for (int i = 0; i < GamepadControl.Length; i++)
        {
            SetGamepadControl(-1, i);
        }
    }
}