using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public abstract class BaseMenuBehavior : MonoBehaviour {


    [field: SerializeField] public string ScreenTitle { get; private set; }

    public UIDocument Document { get; private set; }
    public VisualElement Body { get; private set; }

    List<CustomButton> buttons = new List<CustomButton>();

    bool isAnimatingShow;
    bool isAnimatingHide;


    protected abstract void InitUI(UIDocument doc);

    protected virtual void Awake() {

        Document = GetComponent<UIDocument>();
        Body = Document.rootVisualElement.Q<VisualElement>("Body");

        Body.RegisterCallback<TransitionEndEvent>(_ => {

            if (isAnimatingShow) {
                ApplyShow();
            } else if (isAnimatingHide) {
                ApplyHide();
            }
        });
    }

    protected virtual void OnEnable() {

        //reinit UI references every time the game object become active to avoid a bug with the buttons click
        buttons.Clear();
        InitUI(Document);

        EnableUI();
    }

    protected virtual void OnDisable() {

        DisableUI();
    }

    protected virtual void OnMenuPreShow() {
        //override if necessary
    }

    protected virtual void OnMenuShow() {
        //override if necessary
    }

    protected virtual void OnMenuPreHide() {
        //override if necessary
    }

    protected virtual void OnMenuHide() {
        //override if necessary
    }

    protected T RegisterButton<T>(T button, string buttonText, UnityEvent buttonEvent, Action additionalAction = null) where T : CustomButton{

        buttons.Add(button);

        if (!string.IsNullOrEmpty(buttonText)) {
            button.SetText(buttonText);
        }

        button.AddOnClickListener(() => {

            DisableUI();

            additionalAction?.Invoke();
            buttonEvent?.Invoke();
        });

        return button;
    }

    public void Show(bool animated) {

        Document.rootVisualElement.style.display = DisplayStyle.Flex;
        EnableUI();

        OnMenuPreShow();

        if (animated) {
            isAnimatingShow = true;
            Body.AddToClassList("opacity-1-animated");
        } else {
            ApplyShow();
        }
    }

    void ApplyShow() {

        StopOpacityAnimation();
        Document.rootVisualElement.style.display = DisplayStyle.Flex;

        OnMenuShow();
    }

    public void Hide(bool animated) {

        OnMenuPreHide();

        DisableUI();

        if (animated) {
            isAnimatingHide = true;
            Body.AddToClassList("opacity-0-animated");
        } else {
            ApplyHide();
        }
    }

    void ApplyHide() {

        OnMenuHide();

        StopOpacityAnimation();
        Document.rootVisualElement.style.display = DisplayStyle.None;
    }

    void StopOpacityAnimation() {

        isAnimatingShow = false;
        isAnimatingHide = false;

        Body.RemoveFromClassList("opacity-0-animated");
        Body.RemoveFromClassList("opacity-1-animated");
    }

    protected void EnableUI() {

        Body.SetEnabled(true);

        foreach (var button in buttons) {
            button.SetEnabled(true);
        }
    }

    protected void DisableUI() {

        Body.SetEnabled(false);

        foreach (var button in buttons) {
            button.SetEnabled(false);
        }
    }

}
