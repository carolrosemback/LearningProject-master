using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;                                                           // creates inputField variable

    GameController gameController;                                                              // creates GameController variable


    void Awake()
    {
        gameController = GetComponent<GameController>();                                        // assigns controller to GameController script
        inputField.onEndEdit.AddListener(AcceptStringInput);                                // assigns inputField to inputfield
    }

    void AcceptStringInput(string userInput)                                    // function to receive user input
    {
        userInput = userInput.ToLower();                                                    // turns input into lower case
        gameController.LogStringWithReturn(userInput);                                          // calls LogStringWithReturn function in controller, and applies it to userInput

        char[] delimiterCharacters = { ' ' };                                                   // defines a space as the character that separates words
        string[] separatedInputWords = userInput.Split(delimiterCharacters);                    // separates characters in string

        for(int i = 0; i < gameController.inputActions.Length; i++)
        {
            InputAction inputAction = gameController.inputActions[i];
            if (inputAction.keyword == separatedInputWords[0])
            {
                inputAction.RespondToInput(gameController, separatedInputWords);
            }
        }

        InputComplete();                                                                    // calls InputComplete function
    }

    void InputComplete()                                                        // function to print input
    {
        gameController.DisplayLoggedText();                                                     // calls DisplayLoggedText from controller
        inputField.ActivateInputField();                                                    // activates input field
        inputField.text = null;                                                             // removes inputfield text
    }
}
