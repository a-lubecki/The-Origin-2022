using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuCreditsBehavior : BaseMenuBehavior {


    public float autoScrollSpeed = 1;
    public UnityEvent eventOnBack;

    ScrollView scrollView;
    bool canAutoScroll;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        scrollView = Document.rootVisualElement.Q<ScrollView>();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;

        if (autoScrollSpeed <= 0) {
            throw new ArgumentException("The autoscroll speed must be positive");
        }
    }

    protected override void OnMenuShow() {
        canAutoScroll = true;
    }

    protected override void OnMenuHide() {
        canAutoScroll = false;
    }

    void Update() {

        if (canAutoScroll) {

            var offset = scrollView.scrollOffset;
            offset.y += Time.deltaTime * autoScrollSpeed;
            scrollView.scrollOffset = offset;
        }
    }

}
