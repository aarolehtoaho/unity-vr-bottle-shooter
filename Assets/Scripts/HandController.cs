using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public Hand hand;

    private GameObject[] pistols;
    private IXRSelectInteractor interactor;
    private bool isLeftHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand ??= GetComponent<Hand>();
        interactor = GetComponentInChildren<IXRSelectInteractor>();
        isLeftHand = gameObject.name.ToLower().Contains("left");
        pistols = GameObject.FindGameObjectsWithTag("Pistol");
    }

    void OnEnable()
    {
        gripAction?.action?.Enable();
        triggerAction?.action?.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand == null)
            return;

        hand.SetTrigger(GetTriggerValue());
        hand.SetGrip(GetGripValue());

        SetGunTrigger(TriggerPressedDown());
    }

    void OnDisable()
    {
        gripAction?.action?.Disable();
        triggerAction?.action?.Disable();
    }

    private float GetGripValue()
    {
        return gripAction?.action != null ? gripAction.action.ReadValue<float>() : 0f;
    }

    private float GetTriggerValue()
    {
        return triggerAction?.action != null ? triggerAction.action.ReadValue<float>() : 0f;
    }

    private bool TriggerPressedDown()
    {
        return triggerAction?.action != null && triggerAction.action.WasPressedThisFrame();
    }

    private void SetGunTrigger(bool pressed)
    {
        foreach (GameObject pistol in pistols)
        {
            if (isLeftHand)
            {
                pistol.GetComponent<Shoot>().LeftHandTriggerPressed = pressed;
            }
            else
            {
                pistol.GetComponent<Shoot>().RightHandTriggerPressed = pressed;
            }
        }
    }
}
