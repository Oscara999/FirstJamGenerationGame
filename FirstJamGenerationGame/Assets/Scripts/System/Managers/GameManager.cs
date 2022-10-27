using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DialogueController dialoguesnotes;
    public GameObject gameUI;
    public GameObject panelWinner;
    public GameObject panelLosse;
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
        }
    }

    public void Game()
    {
        Camera.main.transform.Rotate(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y + speed, Camera.main.transform.rotation.z);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        dialoguesnotes.StartNewDialogue(0);
        gameUI.SetActive(true);
    }

    public void FinishGame()
    {
        gameUI.SetActive(true);
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
        panelWinner.SetActive(true);
        yield return new WaitForSeconds(10f);
        //Manage.Instance.isLoad = true;
        //ScenesManager.Instance.LoadLevel("MainMenu");
    }

    IEnumerator Failed()
    {
        panelLosse.SetActive(true);
        yield return new WaitForSeconds(10f);
        //ScenesManager.Instance.isLoad = true;
        //ScenesManager.Instance.LoadLevel("MainMenu");
    }

    IEnumerator StartMatch()
    {
        gameUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        startGame = true;
        //Player.Instance.IsActive = true;
    }
}
