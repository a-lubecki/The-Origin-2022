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


    public virtual Vector3[] CirclesPositions => new Vector3[] {
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero
    };

    public virtual Vector3[] CirclesScales => new Vector3[] {
        Vector3.one,
        Vector3.one,
        Vector3.one,
        Vector3.one
    };


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

        EnableUI();

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

        // Avoid a crash not resolved by Unity when the visuale element is already repainting
        try {
            Document.rootVisualElement.style.display = DisplayStyle.None;
        } catch {
            //no need to flood logs with errors
        }
    }

    void StopOpacityAnimation() {

        isAnimatingShow = false;
        isAnimatingHide = false;

        Body.RemoveFromClassList("opacity-0-animated");
        Body.RemoveFromClassList("opacity-1-animated");
    }

    protected void EnableUI() {

        Body.SetEnabled(true);
    }

    protected void DisableUI() {

        Body.SetEnabled(false);
    }

}
