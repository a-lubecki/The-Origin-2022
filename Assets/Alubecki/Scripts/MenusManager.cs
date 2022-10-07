using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenusManager : MonoBehaviour {


    [SerializeField] CameraManager cameraManager;
    [SerializeField] PostProcessingManager postProcessingManager;
    [SerializeField] DecorationsBehavior decorations;
    [SerializeField] MenuMainBehavior menuMain;
    [SerializeField] MenuNewGameBehavior menuNewGame;
    [SerializeField] MenuSettingsBehavior menuSettings;


    void Start() {

        postProcessingManager.SetBlackScreen(false);

        HideAllMenus(false);
        decorations.Hide(false);

        StartCoroutine(ShowMenuAfterDelay(decorations));
        ShowMenuMain();

        StartCoroutine(DoActionAfterDelay(0.1f, () => postProcessingManager.SetDefaultScreen(true)));
    }

    public void OnSelectMainMenu() {

        ShowMenuMain();
    }

    public void OnSelectContinue() {
        StartGame();
    }

    public void OnSelectNewGame() {
        ShowMenuNewGame();
    }

    public void OnSelectNewGameDifficulty() {

        Debug.Log("Selected game difficulty " + menuNewGame.difficulty);

        StartGame();
    }

    public void OnSelectSettings() {
        ShowMenuSettings();
    }

    public void OnSelectQuitGame() {
        QuitGame();
    }

    public void OnSelectBuyDLC() {
        //open custom URL of a real game
        Application.OpenURL("https://store.steampowered.com/");
    }

    void HideAllMenus(bool animated) {

        decorations.UpdateScreenTitle(null);

        menuMain.Hide(animated);
        menuNewGame.Hide(animated);
        menuSettings.Hide(animated);
    }

    void ShowMenuMain() {

        HideAllMenus(true);

        cameraManager.SelectVCamMainMenu();

        StartCoroutine(ShowMenuAfterDelay(menuMain));
    }

    void ShowMenuNewGame() {

        HideAllMenus(true);

        cameraManager.SelectVCamNewGame();

        StartCoroutine(ShowMenuAfterDelay(menuNewGame));
    }

    void ShowMenuSettings() {

        HideAllMenus(true);

        cameraManager.SelectVCamSettings();

        StartCoroutine(ShowMenuAfterDelay(menuSettings));
    }

    void StartGame() {

        decorations.Hide(true);
        HideAllMenus(true);

        cameraManager.SelectVCamStartGame();
        postProcessingManager.SetWhiteScreen(true);

        StartCoroutine(DoActionAfterDelay(2f, () => SceneManager.LoadScene(0)));
    }

    void QuitGame() {

        decorations.Hide(true);
        HideAllMenus(true);

        cameraManager.SelectVCamQuitGame();
        postProcessingManager.SetBlackScreen(true);

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

    IEnumerator ShowMenuAfterDelay(BaseMenuBehavior menu) {

        yield return new WaitForSeconds(1);

        menu.Show(true);
        decorations.UpdateScreenTitle(menu.ScreenTitle);
    }

}
