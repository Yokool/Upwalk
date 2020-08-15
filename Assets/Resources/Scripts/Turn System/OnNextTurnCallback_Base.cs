using UnityEngine;

public class OnNextTurnCallback_Base : MonoBehaviour
{

    [SerializeField]
    private Turn associatedTurnType;

    public Turn AssociatedTurnType => associatedTurnType;


    private IOnNextTurn_Callback[] nextTurnCallbacks;



    private void OnEnable()
    {
        nextTurnCallbacks = GetComponents<IOnNextTurn_Callback>();
        RegisterItself();
    }

    private void OnDisable()
    {
        UnregisterItself();
    }

    private void RegisterItself()
    {
        foreach(IOnNextTurn_Callback nextTurnCallback in nextTurnCallbacks)
        {
            TurnSystem.INSTANCE.RegisterNextTurnCallback(associatedTurnType, nextTurnCallback);
        }
    }

    private void UnregisterItself()
    {
        foreach (IOnNextTurn_Callback nextTurnCallback in nextTurnCallbacks)
        {
            TurnSystem.INSTANCE.UnregisterNextTurnCallback(associatedTurnType, nextTurnCallback);
        }
    }

}