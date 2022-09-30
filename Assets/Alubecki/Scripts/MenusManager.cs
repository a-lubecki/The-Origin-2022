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

        HideAllMenus(false);

        decorations.Show(true);
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

        Debug.Log("Selected game difficulty " + menuNewGame.difficulty);

        StartGame();
    }

    public void OnSelectSettings() {
        ShowMenuSettings();
    }

    public void OnSelectQuitGame() {
        QuitGame();
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

    void ShowNewGameMenu() {

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

        HideAllMenus(true);

        cameraManager.SelectVCamStartGame();
        postProcessingManager.SetWhiteScreen();

        StartCoroutine(DoActionAfterDelay(2f, () => SceneManager.LoadScene(0)));
    }

    void QuitGame() {

        HideAllMenus(true);

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

    IEnumerator ShowMenuAfterDelay(BaseMenuBehavior menu) {

        yield return new WaitForSeconds(1);

        menu.Show(true);
        decorations.UpdateScreenTitle(menu.ScreenTitle);
    }

}
