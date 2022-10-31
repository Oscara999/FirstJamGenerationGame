using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BetweenDeathAndOblivion
{
    public class UIManager : MonoBehaviour
    {
        public GameObject panelWinner;
        public GameObject panelLosse;

        private TimerController timer = new TimerController();
        


        [SerializeField] public GameObject mainMenuPanel, pausedMenuPanel, gameOverPanel, globalTimePanel, howToPlayPanel, sheetCounterPanel, TimeGame, DialogueController;



        public bool isPause;

        public void Start()
        {
            isPause = false;
        }

        /// <summary>
        /// Pausa la ejecución de la aplicación
        /// </summary>
        public void Pause()
        {
            isPause = !isPause;
            SoundManager.Instance.PauseAllSounds(isPause);
            pausedMenuPanel.SetActive(isPause);

            if (isPause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        
        /// <summary>
        /// Método que permite salir del juego.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}