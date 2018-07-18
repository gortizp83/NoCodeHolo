using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public abstract class InteractibleAction : MonoBehaviour
{
    public abstract void PerformAction();
}

public class Interactible : MonoBehaviour, IInputClickHandler {

    public ZenFulcrum.EmbeddedBrowser.Browser browser;

    [SerializeField]
    private InteractibleAction interactibleAction;

    // Use this for initialization
    private void Start () {

        // if interactible doesn't contain a boxcollider, then add one
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
	}

    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
    {
        //browser.forceClick();
        // Just performing a tagalong action
        if (interactibleAction != null)
        {
            interactibleAction.PerformAction();
        }
    }
}
