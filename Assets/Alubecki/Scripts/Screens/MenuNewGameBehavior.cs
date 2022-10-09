using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuNewGameBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;
    public UnityEvent eventOnDifficultyChosen;

    [SerializeField] String[] difficultyDescriptions;

    Label labelDifficultyDescription;

    public GameDifficulty SelectedDifficulty { get; private set; }


    protected override void Awake() {
        base.Awake();

        var difficultiesCount = Enum.GetNames(typeof(GameDifficulty)).Length;
        if (difficultyDescriptions.Length != difficultiesCount) {
            throw new ArgumentException("The provided difficultyDescriptions array count is invalid, it must be " + difficultiesCount);
        }
    }

    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        var buttonEasy = RegisterButton(new FloatingButton(doc, "ButtonEasy"), "EASY", eventOnDifficultyChosen, () => SelectedDifficulty = GameDifficulty.EASY);
        buttonEasy.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonEasy"));

        var buttonMedium = RegisterButton(new FloatingButton(doc, "ButtonMedium"), "MEDIUM", eventOnDifficultyChosen, () => SelectedDifficulty = GameDifficulty.MEDIUM);
        buttonMedium.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonMedium"));

        var buttonHard = RegisterButton(new FloatingButton(doc, "ButtonHard"), "HARD", eventOnDifficultyChosen, () => SelectedDifficulty = GameDifficulty.HARD);
        buttonHard.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonHard"));

        labelDifficultyDescription = doc.rootVisualElement.Q<Label>("LabelDifficultyDescription");
        labelDifficultyDescription.style.display = DisplayStyle.None;

        RegisterButtonHoverCallbacks(buttonEasy, GameDifficulty.EASY);
        RegisterButtonHoverCallbacks(buttonMedium, GameDifficulty.MEDIUM);
        RegisterButtonHoverCallbacks(buttonHard, GameDifficulty.HARD);
    }

    void RegisterButtonHoverCallbacks(CustomButton button, GameDifficulty difficulty) {

        button.AddOnHoverListener(
            () => {
                labelDifficultyDescription.style.display = DisplayStyle.Flex;
                labelDifficultyDescription.text = difficultyDescriptions[(int)difficulty];
            },
            () => labelDifficultyDescription.style.display = DisplayStyle.None
        );
    }

}

public enum GameDifficulty {
    EASY = 0,
    MEDIUM,
    HARD
}