using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager> 
{
    /// <summary>
    /// Lista de prefabs Utiles de la Scena
    /// </summary>
    public List<GameObject> systemPrefabs;
    /// <summary>
    /// Pantalla Loading
    /// </summary>
    GameObject loadingPanel;
    /// <summary>
    /// Varirable que almacena el nombre de la scen principal a carga.
    /// </summary>
    [SerializeField]
    string mainLevel;
    public GameObject pausePanel;
  

    void Awake()
    {
        base.Awake();
        EditPrefabsGame();
    }

    /// <summary>
    /// Método que permite editar los prefabs necesarios para la correcta ejecución del juego
    /// </summary>
    void EditPrefabsGame()
    {
        SoundManager.Instance.CreateSoundsLevel(MusicLevel.GAME);

        for (int i = 0; i < systemPrefabs.Count; i++)
        {
            if (systemPrefabs[i].name.Equals("LoadingPanel"))
            {
                systemPrefabs[i].SetActive(false);
            }
            else
            {
                systemPrefabs[i].SetActive(true);
            }
        }
        
        loadingPanel = systemPrefabs[3].gameObject;
    }

    /// <summary>
    /// Método que permite cargar una escena de manera asincrona
    /// </summary>
    /// <param name="levelName">Nombre de la escena que se desea cargar.</param>
    public void ReLoadLevel()
    {
        StartCoroutine(LoadingScreen(5f)); 
    }
    /// <summary>
    /// Método que permite cambiar la calidad de graficas.
    /// </summary>
    /// <param name="qualityIndex">Numero de calidad grafica segun ployect settings.</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Método generado por si existe algun error.
    /// </summary>
    public void LoadErrorScene()
    {
        SceneManager.LoadScene("Error");
    }

    /// <summary>
    /// Coroutine para la pantalla de carga
    /// </summary>
    /// <returns>Proceso de carga</returns>
    /// <param name="ao">Asyncronous Operation object</param>
    IEnumerator LoadingScreen(float time)
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.DeleteSoundsLevel();
        SceneManager.LoadScene("Demo");
    }
}
