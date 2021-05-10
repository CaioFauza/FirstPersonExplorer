using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public int lifes;
    public bool catStatus;

    public float sfxVolume = 0.5f, backgroundMusicVolume = 0.5f;
    private static GameManager _instance;

    public enum GameState { MENU, VOLUME_MENU, ABOUT_MENU, INTRO_MENU, GAME, PAUSE, END };

    public GameState gameState { get; private set; }

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }

    private GameManager()
    {
        lifes = 1;
        catStatus = false;
    }

    public void ChangeState(GameState nextState)
    {
        if (nextState == GameState.GAME && gameState != GameState.PAUSE) Reset();
        gameState = nextState;
        changeStateDelegate();
    }

    private void Reset()
    {
        lifes = 1;
        catStatus = false;
    }

}


