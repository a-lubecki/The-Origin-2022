using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuMainBehavior : BaseMenuBehavior {


    public UnityEvent eventOnNewGame;
    public UnityEvent eventOnContinue;
    public UnityEvent eventOnSettings;
    public UnityEvent eventOnQuitGame;


    protected override void InitUI(UIDocument doc) {

        InitButton("ButtonContinue", eventOnContinue);
        InitButton("ButtonNewGame", eventOnNewGame);
        InitButton("ButtonSettings", eventOnSettings);
        //InitButton("ButtonQuitGame", eventOnQuitGame);

        var button = doc.rootVisualElement.Q<VisualElement>("ButtonQuitGame");
        Debug.Log("BUTTON : " + button);
    }

}
