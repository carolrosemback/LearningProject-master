using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text displayText;                                                                                        // define displayText text variable
    public InputAction[] inputActions;                                                                              // define input action variable

    [HideInInspector] public RoomNavigation roomNavigation;                                                         // creates roomNavigation variable
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();                       // creates room interactions' descriptions' list
    [HideInInspector] public InteractableItems interactableItems;

    List<string> actionLog = new List<string>();                                                                    // creates action log string

    void Awake()
    {
        interactableItems = GetComponent<InteractableItems>();
        roomNavigation = GetComponent<RoomNavigation>();                                                            // gets RoomNavigation script
    }

    void Start()
    {
        DisplayRoomText();                                                                                          // calls function to display room text
        DisplayLoggedText();                                                                                        // calls function to display text on the log
    }



    public void DisplayLoggedText()                                                                     // function to display the text in the log
    {
        ClearForNewRoom();                                                                                          // call clearing function

        UnpackRoom();                                                                                               // unpack new room

        string logAsText = string.Join("\n", actionLog.ToArray());                                              /* defines logListAsText, which is a new string that holds
                                                                                                                       the action log, with each element on a new line*/
        displayText.text = logAsText;                                                                           //changes display text to logListAsText
    }

    public void DisplayRoomText()                                                                       // function to display room text
    {
        ClearForNewRoom();

        UnpackRoom();                                                                                               // calls function to build room list

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());          /* makes string from the array of all interactions'
                                                                                                                        descriptions, each on a new line*/

        string combinedText = roomNavigation.currentRoom.roomDescription + "\n" + joinedInteractionDescriptions;    /* combines room description with joined interaction
                                                                                                                        descriptions*/
        LogStringWithReturn(combinedText);                                                                          // calls function to add combined text to log
    }


    public void LogStringWithReturn(string stringToAdd)                                                 // function to add strings to log
    {

        actionLog.Add(stringToAdd + "\n");                                                                          // adds string to log on a new line
    }


    void UnpackRoom()                                                                                   // function to build room list on editor
    {
        roomNavigation.UnpackExitsInRoom();                                                                         // gets function on roomNavigation to build room list
        PrepareObjectsToTakeorExamine(roomNavigation.currentRoom);
    }


    void PrepareObjectsToTakeorExamine(Room currentRoom)
    {
        for(int i = 0; i <currentRoom.InteractableObjectsinRoom.Length; i++)
        {
            string descriptionNotinInventory = interactableItems.GetObjectsNotinInventory(currentRoom, i);
            if(descriptionNotinInventory != null)
                interactionDescriptionsInRoom.Add(descriptionNotinInventory);

            InteractableObject interactableInRoom = currentRoom.InteractableObjectsinRoom[i];

            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if (interaction.inputAction.keyword == "examine")
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                if (interaction.inputAction.keyword == "take")
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
            }          
        }
    }
    
    public string TestVerbDictionaryWithNoun(Dictionary<string, string>verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
            return verbDictionary[noun];

        return "You can't " + verb + " " + noun; 
    }

    void ClearForNewRoom()                                                                              // function to clear lists when changing rooms
    {
        interactableItems.ClearCollection();
        interactionDescriptionsInRoom.Clear();                                                                      // clear interactionDescriptions list
        roomNavigation.ClearExits();                                                                                // clear exit room dictionary in room navigation
    }

}
