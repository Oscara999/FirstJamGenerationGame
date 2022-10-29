using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BetweenDeathAndOblivion;

public class GameManager : Singleton<GameManager>
{
    public DialogueController dialoguesnotes;
    
    public BetweenDeathAndOblivion.UIManager uiManager;
    public TimerController timer;
    bool startGame;
    public float speed;

    void Start()
    {
        StartCoroutine(StartGame());
        SoundManager.Instance.PlayNewSound("MainBackGround");
    }

    public void Update()
    {
        Game();

        if (startGame)
        {
            // si el jugador preciosa ESC y el juego no esta en pausa, pausa el juego. si el juego esta pausado
            // y el jugador preciosa ESC, lo pausa.

            if (Input.GetKeyDown(KeyCode.P))
            {
                uiManager.Pause();
            }
        }
    }

    public void Game()
    {
       
    }

    public IEnumerator StartGame()
    {
        
        uiManager.mainMenuPanel.SetActive(true);
        yield return new WaitUntil(() => !uiManager.mainMenuPanel.activeInHierarchy);
        uiManager.globalTimePanel.SetActive(true);
        timer.starTime = true;
        yield return new WaitForSeconds(2f);
        dialoguesnotes.StartNewDialogue(0);
        yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
        startGame = true;

    }

    public void FinishGame()
    {
       
        //Player.Instance.IsActive = false;

        //if ()
        //{
        //    StartCoroutine(Winner());
        //}
        //else
        //{
        //    StartCoroutine(failed());
        //}
    }

    IEnumerator Winner()
    {
        uiManager.panelWinner.SetActive(true);
        yield return new WaitForSeconds(10f);
        //Manage.Instance.isLoad = true;
        //ScenesManager.Instance.LoadLevel("MainMenu");
    }

    IEnumerator Failed()
    {
        uiManager.gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        //ScenesManager.Instance.isLoad = true;
        //ScenesManager.Instance.LoadLevel("MainMenu");
    }

    IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(2f);
        startGame = true;
        //Player.Instance.IsActive = true;
    }
}
