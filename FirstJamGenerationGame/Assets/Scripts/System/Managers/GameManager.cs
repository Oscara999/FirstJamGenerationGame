using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DialogueController dialoguesnotes;
    public Task screenTask;
    public Core.Character.Player playerController;
    public BetweenDeathAndOblivion.UIManager uiManager;
    public TimerController timer;
    bool startGame;
    public float speed;
    public Vector3 currentStartPoint;
    void Start()
    {
        playerController.controller.isActive = false;
        StartCoroutine(StartGame());
        SoundManager.Instance.PlayNewSound("MainBackGround");
    }

    public void Update()
    {
        if (startGame)
        {
            if (Input.GetKeyDown(KeyCode.P) && playerController.controller.isActive)
            {
                uiManager.Pause();
            }
        }
    }

    public IEnumerator RestaureStartPosition()
    {
        playerController.controller.isActive = false;
        playerController.controller.canMove = false;

        //Activar Particulas
        playerController.particles[1].SetActive(true);
        SoundManager.Instance.PlayNewSound("Fail");
        yield return new WaitForSeconds(1f);
        playerController.particles[1].SetActive(false);

        //Fail Sound
        

        //ActivarTask
        screenTask.ChangeSize(true);
        yield return new WaitUntil(() => !screenTask.start);
        screenTask.RestartSize(false);
        //retornar persona
        playerController.transform.position = currentStartPoint;
        //DesactivarTask

        playerController.controller.isActive = true;
        playerController.controller.canMove = true;
        //borrar return Point
        playerController.controller.Remove();
        yield return null;
    }

    public IEnumerator TakeLetter(int index, GameObject letter)
    {
        //Pause Time
        uiManager.globalTimePanel.SetActive(false);
        timer.starTime = false;
        playerController.controller.Remove();

        //pausarMusicaPrincipal
        SoundManager.Instance.PauseAllSounds(true);

        //sonar efecto de tomar objeto
        SoundManager.Instance.PlayNewSound("GetItem");


        //sonar musica triste
        SoundManager.Instance.PlayNewSound("SadBackGround");

        playerController.controller.isActive = false;
        
        //Activar Particulas
        playerController.particles[0].SetActive(true);
        yield return new WaitForSeconds(7f);
        playerController.particles[0].SetActive(false);

        //Cambiar Current startPoint
        currentStartPoint = playerController.transform.position;

        
        //Iniciar Carta
        switch (index)
        {
            case 1:
                dialoguesnotes.StartNewDialogue(1);
                yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
                break;

            case 2:
                dialoguesnotes.StartNewDialogue(2);
                yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
                break;

            case 3:
                dialoguesnotes.StartNewDialogue(3);
                yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
                break;

            case 4:
                dialoguesnotes.StartNewDialogue(4);
                yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
                break;
        }

        SoundManager.Instance.EndSound("SadBackGround");
        //despausar musica
        SoundManager.Instance.PauseAllSounds(false);
        //activar controlador 
        playerController.controller.isActive = true;
        //iniciar tiempo
        uiManager.globalTimePanel.SetActive(true);
        timer.starTime = true;
        //borrar return Point
        letter.SetActive(false);
    }

    public IEnumerator StartGame()
    {
        uiManager.mainMenuPanel.SetActive(true);
        yield return new WaitUntil(() => !uiManager.mainMenuPanel.activeInHierarchy);

        dialoguesnotes.StartNewDialogue(0);
        yield return new WaitUntil(() => !dialoguesnotes.inPlaying);

        uiManager.howToPlayPanel.SetActive(true);
        yield return new WaitUntil(() => !uiManager.howToPlayPanel.activeInHierarchy);

        uiManager.globalTimePanel.SetActive(true);
        timer.starTime = true;
        startGame = true;
        playerController.controller.isActive = true;
        currentStartPoint = playerController.transform.position;
    }

    public void FinishGame(bool state)
    {
        playerController.controller.isActive = false;

        if (state)
        {
            StartCoroutine(Winner());
        }
        else
        {
            StartCoroutine(Failed());
        }
    }

    IEnumerator Winner()
    {
        playerController.controller.isActive = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Winner");
        uiManager.globalTimePanel.SetActive(false);
        timer.starTime = false;
        uiManager.panelWinner.SetActive(true);
        dialoguesnotes.StartNewDialogue(3);
        yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
        ScenesManager.Instance.ReLoadLevel();
    }

    IEnumerator Failed()
    {
        playerController.controller.isActive = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Losser");
        uiManager.panelLosse.SetActive(true);
        uiManager.globalTimePanel.SetActive(false);
        yield return new WaitForSeconds(10f);
    }
}
