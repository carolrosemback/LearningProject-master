using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsinRoom = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    List<string> nounsinInventory = new List<string>();
    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public string GetObjectsNotinInventory(Room currentRoom, int i)
    {
        InteractableObject interactableinRoom = currentRoom.InteractableObjectsinRoom[i];
        if(!nounsinInventory.Contains (interactableinRoom.noun))
        {
            nounsinRoom.Add(interactableinRoom.noun);
             return interactableinRoom.description;
        }
        return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsinInventory.Count; i++)
        {
            string noun = nounsinInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
            if (interactableObjectInInventory == null)
                continue;
            for(int j = 0; j <interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];
                if (interaction.actionResponse == null)
                    continue;
                if(!useDictionary.ContainsKey (noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }

            }

        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for(int i =0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
                return usableItemList[i];
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");
        for(int i=0; i < nounsinInventory.Count; i++)
        {
            controller.LogStringWithReturn(nounsinInventory[i]);
        }
    }

    public void ClearCollection()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsinRoom.Clear();
    }

    
    public Dictionary<string, string>Take (string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];

        if(nounsinRoom.Contains(noun))
        {
            nounsinInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsinRoom.Remove(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take");
            return null;
        }
    }


    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsinRoom.Clear();
    }


    public void UseItem(string[] separatedInpuWords)
    {
        string nounToUse = separatedInpuWords[1];
        if (nounsinInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].doActionResponse(controller);
                if (!actionResult)
                    controller.LogStringWithReturn("Hmm, Nothing happens.");

            }
            else
                controller.LogStringWithReturn("You can't use the " + nounToUse);
        }
        else
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use.");
    }
    


}
