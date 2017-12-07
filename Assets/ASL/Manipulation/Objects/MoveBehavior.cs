using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASL.Manipulation.Objects
{
    public class MoveBehavior : MonoBehaviour
    {
        private ObjectInteractionManager objManager;
        public GameObject focusObject;
        private float moveScale = 0.10f;
        private float rotateScale = 5.0f;

        private float scaleScale = 1.05f;

        public virtual void Awake()
        {
            objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
            objManager.FocusObjectChangedEvent += SetObject;
        }

        private void SetObject(ObjectSelectedEventArgs e)
        {
            focusObject = e.FocusObject;
        }

        /*
        public virtual void Up()
        {
            if (focusObject != null)
            {
                focusObject.transform.Translate(Vector3.up * MoveScale);
            }
        }
        */

        public virtual void Up()
        {
            if(focusObject != null)
            {
                focusObject.transform.Translate(Vector3.up * MoveScale, Space.World);
            }
        }

        public virtual void Down()
        {
            if (focusObject != null)
            {
                if(focusObject.transform.position.y >= 0.5)
                {
                    focusObject.transform.Translate(Vector3.down * MoveScale, Space.World);
                }
                
            }
        }

        public virtual void Forward()
        {
            if (focusObject != null)
            {
                if (focusObject.transform.position.z <= 4)
                {
                    focusObject.transform.Translate(Vector3.forward * MoveScale, Space.World);
                }
            }
        }

        public virtual void Back()
        {
            if (focusObject != null)
            {
                if (focusObject.transform.position.z >= -4)
                {
                    focusObject.transform.Translate(-Vector3.forward * MoveScale, Space.World);
                }
            }
        }

        public virtual void Left()
        {
            if (focusObject != null)
            {
                if (focusObject.transform.position.x >= -4)
                {
                    focusObject.transform.Translate(Vector3.left * MoveScale, Space.World);
                }
            }
        }

        public virtual void Right()
        {
            if (focusObject != null)
            {
                if (focusObject.transform.position.x <= 4)
                {
                    focusObject.transform.Translate(Vector3.right * MoveScale, Space.World);
                }
            }

        }








        public virtual void RotateClockwise()
        {
            if(focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.up, RotateScale, Space.World);
            }
        }

        public virtual void RotateCounterClockwise()
        {
            if(focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.up, RotateScale * -1, Space.World);
            }
        }

        public virtual void RotateRight()
        {
            if (focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.forward, RotateScale * -1, Space.World);
            }
        }

        public virtual void RotateLeft()
        {
            if (focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.forward, RotateScale, Space.World);
            }
        }


        public virtual void RotateForward()
        {
            if (focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.right, RotateScale, Space.World);
            }
        }
        public virtual void RotateBackward()
        {
            if (focusObject != null)
            {
                focusObject.transform.Rotate(Vector3.right, RotateScale * -1, Space.World);
            }
        }


        public virtual void ScaleUp()
        {
            if (focusObject != null)
            {
                if(focusObject.transform.localScale.magnitude <= 4)
                {
                    focusObject.transform.localScale *= scaleScale;
                }
                
            }
        }
        public virtual void ScaleDown()
        {
            if (focusObject != null)
            {
                if (focusObject.transform.localScale.magnitude >= 0.5)
                {
                    focusObject.transform.localScale /= scaleScale;
                }
                
            }
        }


        public virtual void Delete()
        {
            if (focusObject != null)
            {
                //focusObject.destroy();
                Destroy(focusObject);
            }
        }

        public virtual void Copy()
        {
            if (focusObject != null)
            {
                objManager.Instantiate(focusObject.name, focusObject.transform.position, focusObject.transform.rotation);

                // set focus on new object
                //GameObject selectedObject = Mouse.Select();
                //fortBuilderObj fbComponent = selectedObject == null ? null : selectedObject.GetComponent<fortBuilderObj>();
                //if (fbComponent != null && fbComponent.selectable)
                //{
                //    objManager.RequestOwnership(selectedObject, PhotonNetwork.player.ID);
                //    WorldManager.Selected = fbComponent;

                //    // select is for changing the material to highlight the selected object
                //    // this select function is in fortBuilderObj, while the other select 
                //    // is in this class.
                //    fbComponent.Select();

                //    if (fbComponent != previousObj)
                //    {

                //        if (previousObj != null)
                //        {
                //            previousObj.Deselect();
                //            previousObj = fbComponent;
                //        }

                //    }
                //}
            }
        }



        public virtual void Drag(Vector3 deltaPosition)
        {
            if(focusObject != null)
            {
                focusObject.transform.Translate(deltaPosition);
            }
        }

        #region Properties
        public float MoveScale
        {
            get
            {
                return moveScale;
            }
            set
            {
                if (value > 0.0f)
                {
                    moveScale = value;
                }
            }
        }

        public float RotateScale
        {
            get
            {
                return rotateScale;
            }
            set
            {
                if(value > 0.0f)
                {
                    rotateScale = value;
                }
            }
        }
#endregion
    }
}