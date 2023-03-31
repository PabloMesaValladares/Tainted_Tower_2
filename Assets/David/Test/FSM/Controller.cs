using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{

    public class Controller : MonoBehaviour
    {
        public State currentState;//Apuntador al estado actual

        public GameObject player;
        public GameObject enemy;
        [HideInInspector]
        public Rigidbody rb;

        public bool detect;

        public bool ActivateAI { get; set; }
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            ActivateAI = true;//Activa la IA
        }

        // Update is called once per frame
        void Update()
        {//Se ejecutan las acciones del estado actual
            if (!ActivateAI) return; //El parámetro permite que los estados tengan una referencia
            if(currentState != null)
                currentState.UpdateState(this);//controlador, para poder llamar a sus métodos

        }

        public void Transition(State nextState)
        {
            currentState.RestartActions();
            currentState = nextState;
        }

        public void Detecting(bool d)
        {
            detect = d;
        }
    }
}
