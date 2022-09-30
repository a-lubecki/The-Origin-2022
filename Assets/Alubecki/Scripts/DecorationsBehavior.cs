using UnityEngine.Events;
using UnityEngine.UIElements;


public class DecorationsBehavior : BaseMenuBehavior {


    VisualElement veTopBackground;
    Label labelScreenTitle;


    protected override void InitUI(UIDocument doc) {

        veTopBackground = doc.rootVisualElement.Q<VisualElement>("TopBackground");
        labelScreenTitle = doc.rootVisualElement.Q<Label>("ScreenTitle");

//        var veLines = doc.rootVisualElement.Q<VisualElement>("Lines");
//        veLines.AddToClassList("hidden");
    }

    public void UpdateScreenTitle(string title) {

        if (string.IsNullOrEmpty(title)) {
            veTopBackground.style.display = DisplayStyle.None;
            return;
        }

        veTopBackground.style.display = DisplayStyle.Flex;

        labelScreenTitle.text = title;
    }

}
