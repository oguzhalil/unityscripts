using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UIElement : MonoBehaviour, IUIElement
{
    public static UIManager UI { get { return UIManager.Instance; } }

    [SerializeField] private UnityEvent onShow;
    [SerializeField] private UnityEvent onHide;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary> OnClick back button on android. </summary>
    public virtual void OnClickEscape() { }

    public abstract Type DerivedType();
}


public interface IUIElement
{
    void Show();
    void Hide();


    /// <summary> OnClick back button on android. </summary>
    //void OnClickEscape();
}