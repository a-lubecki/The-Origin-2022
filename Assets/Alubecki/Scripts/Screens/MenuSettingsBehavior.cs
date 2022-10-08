using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuSettingsBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;

    TabsSelector tabsSelector;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        tabsSelector = new TabsSelector(
            doc.rootVisualElement.Q<RadioButtonGroup>(),
            doc.rootVisualElement.Q<VisualElement>("Tabs")
        );
    }

}
