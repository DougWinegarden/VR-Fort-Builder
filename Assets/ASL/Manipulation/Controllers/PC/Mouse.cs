using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;

namespace ASL.Manipulation.Controllers.PC
{
    public class Mouse : MonoBehaviour
    {
        private TransformControl transControl;

        private ObjectInteractionManager objManager;

        private fortBuilderObj previousObj;

        public void Awake()
        {
            objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();

            transControl = GameObject.Find("MainCanvas").GetComponent<TransformControl>();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject selectedObject = Select();
                fortBuilderObj fbComponent = selectedObject == null ? null : selectedObject.GetComponent<fortBuilderObj>();
                if (fbComponent != null && fbComponent.selectable)
                {
                    objManager.RequestOwnership(selectedObject, PhotonNetwork.player.ID);
                    WorldManager.Selected = fbComponent;

                    // select is for changing the material to highlight the selected object
                    // this select function is in fortBuilderObj, while the other select 
                    // is in this class.
                    fbComponent.Select();

                    if (fbComponent != previousObj)
                    {

                        if(previousObj != null)
                        {
                            previousObj.Deselect();
                            previousObj = fbComponent;
                        }
                        
                    }
                }
                //else
                //{
                //    if (previousObj != null)
                //    {
                //        previousObj.Deselect();
                //        previousObj = comp;
                //    }
                //}

               
            }

                /*
                if (Input.GetMouseButtonDown(1))
                {
                    //string prefabName = "Sphere";
                    string prefabName = "FortBuilderCube";
                    Vector3 position = new Vector3(0, 0, 2);
                    Quaternion rotation = Quaternion.identity;
                    objManager.Instantiate(prefabName, position, rotation);
                }
                */
            }

        public GameObject Select()
        {
            Camera cam = GameObject.FindObjectOfType<Camera>();
            Vector3 mousePos = Input.mousePosition;
            Vector3 mouseRay = cam.ScreenToWorldPoint(mousePos);
            RaycastHit hit;
            Physics.Raycast(cam.ScreenPointToRay(mousePos), out hit);

            if (hit.collider != null)
            {
                return hit.collider.gameObject;
            }
            else
            {
                GameObject camera = GameObject.Find("Main Camera");
                if(camera != null)
                {
                    return camera;
                }
                else
                {
                    Debug.LogError("Cannot find camera object. Selecting null object.");
                    return null;
                }
            }
        }
    }
}