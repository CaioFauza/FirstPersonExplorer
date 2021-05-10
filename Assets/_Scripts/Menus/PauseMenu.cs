using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        gm.ChangeState(GameManager.GameState.GAME);
    }

    public void MainMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}
