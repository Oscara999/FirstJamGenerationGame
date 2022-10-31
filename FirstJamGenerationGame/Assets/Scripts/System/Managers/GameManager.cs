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
        Game();

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
        //playerController.controller.canMove = false;
        //Fail Sound
        SoundManager.Instance.PlayNewSound("Fail");
        //borrar return Point
        playerController.controller.Remove();

        //ActivarTask
        screenTask.ChangeSize(true);
        yield return new WaitUntil(() => !screenTask.start);

        //retornar persona
        playerController.transform.position = currentStartPoint;
        //DesactivarTask
        screenTask.ChangeSize(false);
        playerController.controller.isActive = true;
        playerController.controller.canMove = true;


        yield return null;
    }

    public IEnumerator TakeLetter(int index)
    {
        //Activar Particulas
        playerController.particles[2].SetActive(true);
        //borrar return Point
        playerController.controller.Remove();

        //Desactivar
        playerController.controller.canMove = false;
        //playerController.controller.isActive = false;
        //pausarMusicaPrincipal
        SoundManager.Instance.PauseAllSounds(true);
        //sonar efecto de tomar objeto
        SoundManager.Instance.PlayNewSound("GetItem");
        //Cambiar Current startPoint
        currentStartPoint = playerController.transform.position;
        playerController.particles[2].SetActive(false);
        //sonar musica triste
        SoundManager.Instance.PlayNewSound("Sad");
        //Iniciar Carta


        switch (index)
        {
            case 0:
                dialoguesnotes.StartNewDialogue(1);
                yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
                //parar musica triste
                SoundManager.Instance.EndSound("Sad");
                //despausar musica
                SoundManager.Instance.PauseAllSounds(true);
                //activar controlador 
                playerController.controller.isActive = true;
                //iniciar tiempo
                uiManager.globalTimePanel.SetActive(true);
                break;

            case 1:
                //Cambiar Current startPoint
                //Desactivar
                //sonar efecto de tomar objeto
                //Activar Particulas
                //GirarCamara
                //Cambiar de musica
                //Iniciar Carta
                //activar controlador 
                //iniciar tiempo
                break;

            case 2:
                //Cambiar Current startPoint
                //Desactivar
                //sonar efecto de tomar objeto
                //Activar Particulas
                //GirarCamara
                //Cambiar de musica
                //Iniciar Carta
                //activar controlador 
                //iniciar tiempo
                break;

            case 3:

                break;
        }

        yield return null;
    }

    public void Game()
    {
       
    }

    public IEnumerator StartGame()
    {
        uiManager.mainMenuPanel.SetActive(true);
        yield return new WaitUntil(() => !uiManager.mainMenuPanel.activeInHierarchy);
        dialoguesnotes.StartNewDialogue(0);

        yield return new WaitUntil(() => !dialoguesnotes.inPlaying);
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


}
