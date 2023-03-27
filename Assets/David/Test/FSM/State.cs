using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Carnation/State")]
    public class State : ScriptableObject
    {
        public Action[] actions; //En un state se ejecutan varias acciones

        public Transition[] transitions; //Desde un estado se pueden pasar a otros estados a traves de transiciones

        public void UpdateState(Controller controller)//Se ejecuta desde el controller
        {
            DoActions(controller); //Ejecutamos las acciones
            CheckTransitions(controller); //Comprobamos las transiciones
        }

        private void DoActions(Controller controller)
        {
            for(int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        public void RestartActions()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].RestartVariables();
            }
        }
        private void CheckTransitions(Controller controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decision = transitions[i].desicion.Decide(controller);
                if(decision)
                {
                    transitions[i].desicion.RestartVariables();
                    controller.Transition(transitions[i].trueState);
                    return;
                }
            }
        }
    }
}
