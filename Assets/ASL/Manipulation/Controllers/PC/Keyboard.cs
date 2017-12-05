using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;

namespace ASL.Manipulation.Controllers.PC
{
    public class Keyboard : MonoBehaviour
    {
        private MoveBehavior _moveBehavior;
        private ObjectInteractionManager objManager;

        private void Awake()
        {
            objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
            _moveBehavior = objManager.RegisterBehavior<MoveBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKey(KeyCode.Q))
            {
                MoveBehavior.Up();
            }

            if (Input.GetKey(KeyCode.E))
            {
                MoveBehavior.Down();
            }

            if (Input.GetKey(KeyCode.S))
            {
                MoveBehavior.Back();
            }
            if(Input.GetKey(KeyCode.W))
            {
                MoveBehavior.Forward();
            }
            if(Input.GetKey(KeyCode.A))
            {
                MoveBehavior.Left();
            }
            if(Input.GetKey(KeyCode.D))
            {
                MoveBehavior.Right();
            }


            if (Input.GetKey(KeyCode.H))
            {
                MoveBehavior.RotateClockwise();
            }
            if (Input.GetKey(KeyCode.K))
            {
                MoveBehavior.RotateCounterClockwise();
            }

            if (Input.GetKey(KeyCode.I))
            {
                MoveBehavior.RotateRight();
            }
            if (Input.GetKey(KeyCode.Y))
            {
                MoveBehavior.RotateLeft();
            }

            if (Input.GetKey(KeyCode.U))
            {
                MoveBehavior.RotateForward();
            }
            if (Input.GetKey(KeyCode.J))
            {
                MoveBehavior.RotateBackward();
            }



            if (Input.GetKey(KeyCode.R))
            {
                MoveBehavior.ScaleUp();
            }
            if (Input.GetKey(KeyCode.F))
            {
                MoveBehavior.ScaleDown();
            }

    */


            if (Input.GetKey(KeyCode.Backspace))
            {
                MoveBehavior.Delete();
            }

        }

        public MoveBehavior MoveBehavior
        {
            get
            {
                return _moveBehavior;
            }
            set
            {
                if(value != null)
                {
                    _moveBehavior = value;
                }
            }
        }
    }
}