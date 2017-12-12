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
                    WorldManager.Selected = fbComponent;
                }
            }
        }

        public GameObject Select()
        {
            Camera cam = Camera.main;
            Vector3 mousePos = Input.mousePosition;
            Vector3 mouseRay = cam.ScreenToWorldPoint(mousePos);
            RaycastHit hit;
            Physics.Raycast(cam.ScreenPointToRay(mousePos), out hit);

            if (hit.collider != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
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