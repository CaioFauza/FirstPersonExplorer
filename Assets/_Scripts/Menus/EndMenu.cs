using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    GameManager gm;
    Text endGameText;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        Cursor.visible = true;

        endGameText = GameObject.Find("UI_EndText").GetComponent<Text>();
        endGameText.text = gm.lifes > 0 ? "You win!" : "You lose!";
    }
    public void MainMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}
