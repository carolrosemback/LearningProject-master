using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]                                           // to add options in the inspector
public class Exit
{
    public string keyString;                                    // add key string, to receive input commands
    public string exitDescription;                              // text to display when leaving room
    public Room valueRoom;                                      // room to change to when keystring is entered
}
