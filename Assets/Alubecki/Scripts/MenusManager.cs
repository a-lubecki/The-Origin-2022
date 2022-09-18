using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenusManager : MonoBehaviour {


    [SerializeField] CameraManager cameraManager;
    [SerializeField] PostProcessingManager postProcessingManager;
    [SerializeField] GameObject goMenuMain;
    [SerializeField] GameObject goMenuNewGame;
    [SerializeField] GameObject goMenuSettings;


    void Awake() {
        HideAllMenus();
    }

    void Start() {
        ShowMenuMain();
    }

    public void OnSelectMainMenu() {
        ShowMenuMain();
    }

    public void OnSelectContinue() {
        StartGame();
    }

    public void OnSelectNewGame() {
        ShowNewGameMenu();
    }

    public void OnSelectNewGameDifficulty() {

        Debug.Log("Selected game difficulty " + goMenuNewGame.GetComponent<MenuNewGameBehavior>()?.difficulty);

        StartGame();
    }

    public void OnSelectSettings() {
        ShowMenuSettings();
    }

    public void OnSelectQuitGame() {
        QuitGame();
    }

    void HideAllMenus() {

        goMenuMain.SetActive(false);
        goMenuNewGame.SetActive(false);
        goMenuSettings.SetActive(false);
    }

    void ShowMenuMain() {

        HideAllMenus();

        cameraManager.SelectVCamMainMenu();

        StartCoroutine(ShowMenuAfterDelay(goMenuMain));
    }

    void ShowNewGameMenu() {

        HideAllMenus();

        cameraManager.SelectVCamNewGame();

        StartCoroutine(ShowMenuAfterDelay(goMenuNewGame));
    }

    void ShowMenuSettings() {

        HideAllMenus();

        cameraManager.SelectVCamSettings();

        StartCoroutine(ShowMenuAfterDelay(goMenuSettings));
    }

    void StartGame() {

        HideAllMenus();

        cameraManager.SelectVCamStartGame();
        postProcessingManager.SetWhiteScreen();

        StartCoroutine(DoActionAfterDelay(2f, () => SceneManager.LoadScene(0)));
    }

    void QuitGame() {

        HideAllMenus();

        cameraManager.SelectVCamQuitGame();
        postProcessingManager.SetBlackScreen();

        StartCoroutine(DoActionAfterDelay(2f, () => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }));
    }

    IEnumerator DoActionAfterDelay(float delaySec, Action action) {

        yield return new WaitForSeconds(delaySec);

        action.Invoke();
    }

    IEnumerator ShowMenuAfterDelay(GameObject goMenu) {

        yield return new WaitForSeconds(1);

        goMenu.SetActive(true);
    }

}
