using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM//Con esto estamos diciendo que este script pertenece al namespace FSM
{

    public abstract class Action : ScriptableObject
    {
        public abstract void Act(Controller controller);

        public abstract void RestartVariables();
    }

}
