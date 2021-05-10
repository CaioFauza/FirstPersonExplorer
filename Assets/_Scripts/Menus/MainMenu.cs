using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameManager gm;
    public GameObject cat;
    private void OnEnable()
    {
        gm = GameManager.GetInstance();
        try
        {
            GameObject cat = GameObject.Find("Cat");
            Destroy(cat);
        }
        catch { }
        Instantiate(cat, new Vector3(63.74f, 13.75f, 185.57f), Quaternion.identity);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
    }
    public void StartGame()
    {
        gm.ChangeState(GameManager.GameState.INTRO_MENU);
    }

    public void VolumeSettings()
    {
        gm.ChangeState(GameManager.GameState.VOLUME_MENU);
    }

    public void AboutMenu()
    {
        gm.ChangeState(GameManager.GameState.ABOUT_MENU);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
