using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestStateMachine : MonoBehaviour
{
    public IGestState initialState;
    public IGestState currentState;
    protected List<IGestState> transitions;

    public void ProcessGestureStateMachine(){
        currentState.ProcessGestures(this);
    }

    public void transition(IGestState transitionState){
        currentState = transitionState;
    }
}
