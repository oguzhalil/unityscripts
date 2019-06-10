using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EboxGames;
using System.Reflection;
using System;

public class UIManager : Singleton<UIManager>
{
    public Canvas canvasMainmenu;

    Dictionary<Type, IUIElement> dictionary;

    public UIElement homePage;

    [SerializeField] private UIElement[] uiElements;

    IUIElement current;

    protected override void Awake()
    {
        base.Awake();

        dictionary = new Dictionary<Type, IUIElement>();

        foreach (var uiElement in uiElements)
        {
            dictionary.Add(uiElement.DerivedType(), uiElement as IUIElement);
        }

        Show(homePage);

        //var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        //var layers = new UIElement[fields.Length];

        //for (int i = 0; i < fields.Length; i++)
        //{
        //    FieldInfo field = fields[i];

        //    object instance = field.GetValue(this);

        //    if (instance is UIElement)
        //    {
        //        dictionary.Add(typeof(UIElement), instance as IUIElement);
        //    }
        //    else
        //        Debug.LogError("Type casting error.");
        //}
    }

    public void CanvasVisible(bool value)
    {
        canvasMainmenu.gameObject.SetActive(value);
    }

    public void Hide(UIElement uiElement)
    {
        // Hide currently showed ui element
        current.Hide();

        // Show desired ui element
        uiElement.Hide();

        // Assign newly showed ui element to current
        current = uiElement;
    }


    public void Show(UIElement uiElement)
    {
        // Hide currently showed ui element
        if (current != null)
            current.Hide();

        // Show desired ui element
        uiElement.Show();

        // Assign newly showed ui element to current
        current = uiElement;
    }

    public void Show<T>() where T : UIElement
    {
        // Hide currently showed ui element
        if (current != null)
            current.Hide();

        IUIElement element = GetUIElement(typeof(T));

        // Show desired ui element
        element.Show();

        // Assign newly showed ui element to current
        current = element;
    }

    public void Hide<T>() where T : UIElement
    {
        IUIElement element = GetUIElement(typeof(T));

        element.Hide();
    }

    IUIElement GetUIElement(Type type)
    {
        if (dictionary.ContainsKey(type))
        {
            return dictionary[type];
        }

        return null;
    }
}
