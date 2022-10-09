using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuSettingsBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;

    TabsSelector tabsSelector;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        tabsSelector = new TabsSelector(
            doc.rootVisualElement.Q<RadioButtonGroup>(),
            doc.rootVisualElement.Q<VisualElement>("Tabs"),
            doc.rootVisualElement.Q<VisualElement>("RadioButtonGroupSelector")
        );
    }

    protected override void OnMenuShow() {

        //reset selector and tabs for the first call
        tabsSelector.SelectRadioButton(0);
    }

    protected override void OnMenuHide() {

        //reset during hide to avoid seeing selector animation if it was called in OnMenuShow
        //but not for the very first call when the UI is not displayed and dimensions are not calculated
        if (tabsSelector.SelectedTabIndex >= 0) {
            tabsSelector.SelectRadioButton(0);
        }
    }

}
