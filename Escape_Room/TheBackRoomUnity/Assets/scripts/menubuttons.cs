using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonsofmenu : MonoBehaviour
{
    public float v0;
    public float multv;
    [SerializeField] private string levelsName;
    [SerializeField] private string menusName;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private GameObject modeMenu;

    public GameObject ativabutton;
    int deltaPontos;
    int pontosIniciais;

    void Start()
    {}

    void Update()
    {
        //Sair pelo celular

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                OpenQuitGame();
                return;
            }
        }
    }

    //funcao durante o jogo
    public void Play()
    {
        SceneManager.LoadScene(levelsName);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(menusName);
    }

    //Menu de opcoes
    public void OpenOptions()
    {
        menuInicial.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        menuInicial.SetActive(true);
    }

    //Menu de sair do jogo

    public void OpenQuitGame()
    {
        menuInicial.SetActive(false);
        quitMenu.SetActive(true);

    }

    public void QuitGame()
    {
        Debug.Log("sair do jogo");
        Application.Quit();
    }

    public void CloseQuitGame()
    {
        quitMenu.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale--;
    }
    public void Resume()
    {
        Time.timeScale++;
    }

    //Menu de dificuldade

    public void openModeMenu()
    {
        modeMenu.SetActive(true);
    }

    public void closeModeMenu()
    {
        modeMenu.SetActive(false);
    }
}
