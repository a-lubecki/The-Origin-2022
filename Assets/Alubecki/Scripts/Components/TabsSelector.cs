using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TabsSelector {


    List<VisualElement> tabs;


    public TabsSelector(RadioButtonGroup radioButtonGroup, VisualElement tabsContainer) {

        tabs = new List<VisualElement>(tabsContainer.Children());

        radioButtonGroup.RegisterCallback<ChangeEvent<int>>(e => SelectTab(e.newValue));
    }

    void SelectTab(int tabIndex) {

        for (int i = 0; i < tabs.Count; i++) {
            tabs[i].style.display = (i == tabIndex) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

}
