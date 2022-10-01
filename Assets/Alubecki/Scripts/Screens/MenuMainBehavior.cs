using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuMainBehavior : BaseMenuBehavior {


    public UnityEvent eventOnNewGame;
    public UnityEvent eventOnContinue;
    public UnityEvent eventOnSettings;
    public UnityEvent eventOnQuitGame;

    protected override void InitUI(UIDocument doc) {

        RegisterButton(new FloatingButton(doc, "ButtonContinue"), "CONTINUE", eventOnContinue);
        RegisterButton(new FloatingButton(doc, "ButtonNewGame"), "NEW GAME", eventOnNewGame);
        RegisterButton(new FloatingButton(doc, "ButtonSettings"), "SETTINGS", eventOnSettings).SetHoriontalDirection(false);
        RegisterButton(new FloatingButton(doc, "ButtonCredits"), "CREDITS", eventOnNewGame).SetHoriontalDirection(false);
        RegisterButton(new CustomButton(doc, "ButtonQuitGame"), null, eventOnQuitGame);
    }

}
