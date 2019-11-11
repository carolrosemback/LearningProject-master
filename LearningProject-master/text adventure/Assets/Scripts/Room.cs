using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(menuName = "TextAdventure/Room")]                  // creates option to add more rooms in the unity inspector

public class Room : ScriptableObject
{
    [TextArea]                                                      // allows the string to be edited with a height flexible and scrollable text area
    public string roomDescription;                                  // starting room text
    public string roomName;                                         // name of the room
    public Exit[] exits;                                            // number of exits in each room (able to be changed in the editor)
    public InteractableObject[] InteractableObjectsinRoom;

}
