using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public class BaseMenuBehavior : MonoBehaviour {


    UIDocument document;
    List<Button> buttons = new List<Button>();


    protected virtual void Awake() {

        document = GetComponent<UIDocument>();
    }

    protected virtual void Start() {

        EnableAllButtons();
    }

    protected virtual void OnDisable() {

        DisableAllButtons();
    }

    protected Button InitButton(string buttonName, UnityEvent buttonEvent, Action additionalAction = null) {

        var button = document.rootVisualElement.Q<Button>(buttonName);
        if (button == null) {
            throw new ArgumentException("The button " + buttonName + " was not declard in the UIDocument");
        }

        buttons.Add(button);

        button.clicked += () => {

            DisableAllButtons();

            additionalAction?.Invoke();
            buttonEvent?.Invoke();
        };

        return button;
    }

    void EnableAllButtons() {

        foreach (var button in buttons) {
            button.SetEnabled(true);
        }
    }

    void DisableAllButtons() {

        foreach (var button in buttons) {
            button.SetEnabled(false);
        }
    }

}
