using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public Hand hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hand == null)
            hand = GetComponent<Hand>();
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
}
