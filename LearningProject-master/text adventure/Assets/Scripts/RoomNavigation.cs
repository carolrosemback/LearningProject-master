using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;                                                                                    // holds current room (initially start room), and its script

    Dictionary<string, Room> exitRoomsDictionary = new Dictionary<string, Room>();

    private GameController gameController;                                                                      // creates game_controller variable

    void Awake()
    {
        gameController = GetComponent<GameController>();                                                        // gets GameController script
    }

    public void UnpackExitsInRoom()                                                                 // function to build room list on inspector
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)                                                      /* gets lenght of the exits array, from the Exits script*/
        {
            exitRoomsDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);

            gameController.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);             /*adds to the interactionDescriptionsInRoom list new 
                                                                                                                interactions, one for each exit on the room*/
        }
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitRoomsDictionary.ContainsKey(directionNoun))
        {

            currentRoom = exitRoomsDictionary[directionNoun];
            gameController.LogStringWithReturn("You head off to the " + directionNoun);
            gameController.DisplayRoomText();

        }
        else
        {
            gameController.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        exitRoomsDictionary.Clear();
    }
}
