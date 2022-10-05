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

    //Coroutine coroutineVisibility;


    protected abstract void InitUI(UIDocument doc);

    protected virtual void Awake() {

        Document = GetComponent<UIDocument>();
        Body = Document.rootVisualElement.Q<VisualElement>("Body");
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
    }

    public void Hide(bool animated) {

        Document.rootVisualElement.style.display = DisplayStyle.None;
    }

/*
    public void Show(bool animated) {

        if (coroutineVisibility != null) {
            StopCoroutine(coroutineVisibility);
            coroutineVisibility = null;
        }

        gameObject.SetActive(true);
        if (animated && body != null) {
            coroutineVisibility = StartCoroutine(ShowAnimated());
        } else {
            document.enabled = true;
        }
    }

    IEnumerator ShowAnimated() {

        document.enabled = true;

Debug.LogError("TODO");yield return null;
        body.AddToClassList("hidden");

        yield return new WaitForEndOfFrame();

        //body.AddToClassList("visible-animated");
        //body.RemoveFromClassList("hidden");

        //yield return new WaitWhile(() => body.style.opacity.value < 1);

        //body.RemoveFromClassList("visible-animated");
    }

    public void Hide(bool animated) {

        gameObject.SetActive(false);

        if (animated && body != null) {
            coroutineVisibility = StartCoroutine(HideAnimated());
        } else {
            document.enabled = false;
        }
    }

    IEnumerator HideAnimated() {

Debug.LogError("TODO");yield return null;

        body.AddToClassList("hidden-animated");

        yield return new WaitWhile(() => body.style.opacity.value > 0);

        body.RemoveFromClassList("hidden-animated");

        document.enabled = false;
    }*/

    protected void EnableUI() {

        Body?.SetEnabled(true);

        foreach (var button in buttons) {
            button.SetEnabled(true);
        }
    }

    protected void DisableUI() {

        Body?.SetEnabled(false);

        foreach (var button in buttons) {
            button.SetEnabled(false);
        }
    }

}
