using UnityEngine;

public class OnNextTurnCallback_Base : MonoBehaviour
{

    [SerializeField]
    private Turn associatedTurnType;

    private IOnNextTurn_Callback nextTurnCallback;

    public void OnNextTurn()
    {
        nextTurnCallback.OnNextTurn();
    }

    private void OnEnable()
    {
        nextTurnCallback = GetComponent<IOnNextTurn_Callback>();
        RegisterItself();
    }

    private void OnDisable()
    {
        UnregisterItself();
    }

    private void RegisterItself()
    {
        TurnSystem.INSTANCE.RegisterNextTurnCallback(associatedTurnType, nextTurnCallback);
    }

    private void UnregisterItself()
    {
        TurnSystem.INSTANCE.UnregisterNextTurnCallback(associatedTurnType, nextTurnCallback);
    }

}