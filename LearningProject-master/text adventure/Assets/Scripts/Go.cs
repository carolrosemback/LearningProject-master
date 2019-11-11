using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]

public class Go : InputAction
{
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)
    {
        gameController.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);                                         //
    }
}
