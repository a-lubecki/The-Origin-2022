using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TabsSelector {


    RadioButtonGroup radioButtonGroup;
    List<RadioButton> radioButtons;
    List<VisualElement> tabs;
    VisualElement selector;


    public int SelectedTabIndex => radioButtonGroup.value;


    public TabsSelector(RadioButtonGroup radioButtonGroup, VisualElement tabsContainer, VisualElement selector = null) {

        this.radioButtonGroup = radioButtonGroup;

        //find all radio buttons of group to get their positions to move selector square
        var rbParent = radioButtonGroup.Q<RadioButton>().parent;
        radioButtons = new List<RadioButton>();

        foreach(var elem in rbParent.Children()) {
            if (elem is RadioButton) {
                radioButtons.Add(elem as RadioButton);
            }
        }

        tabs = new List<VisualElement>(tabsContainer.Children());
        this.selector = selector;

        radioButtonGroup.RegisterCallback<ChangeEvent<int>>(e => SelectTab(e.newValue));
    }

    public void SelectRadioButton(int index) {

        radioButtonGroup.value = 0;
    }

    void SelectTab(int tabIndex) {

        for (int i = 0; i < tabs.Count; i++) {
            tabs[i].style.display = (i == tabIndex) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        if (selector != null) {

            var yPos = 0f;

            if (tabIndex >= 0 && tabIndex < radioButtons.Count) {
                yPos = radioButtons[tabIndex].localBound.yMin;
            }

            selector.style.top = new StyleLength(new Length(yPos));
        }
    }

}
