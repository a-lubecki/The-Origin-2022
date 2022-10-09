using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuCreditsBehavior : BaseMenuBehavior {


    public float autoScrollSpeed = 1;
    public UnityEvent eventOnBack;

    ScrollView scrollView;
    bool canAutoScroll;


    public override Vector3[] CirclesPositions => new Vector3[] {
        new Vector3(-800, -400, 0),
        new Vector3(800, -400, 0),
        new Vector3(800, 400, 0),
        new Vector3(-800, 400, 0),
    };


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        scrollView = Document.rootVisualElement.Q<ScrollView>();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;

        if (autoScrollSpeed <= 0) {
            throw new ArgumentException("The autoscroll speed must be positive");
        }
    }

    protected override void OnMenuShow() {

        StartCoroutine(AutoScrollAfterDelay());
    }

    protected override void OnMenuPreHide() {

        canAutoScroll = false;
    }

    IEnumerator AutoScrollAfterDelay() {

        yield return new WaitForSeconds(1);

        canAutoScroll = true;
    }

    void Update() {

        if (canAutoScroll) {

            var offset = scrollView.scrollOffset;
            offset.y += Time.deltaTime * autoScrollSpeed;
            scrollView.scrollOffset = offset;
        }
    }

}
