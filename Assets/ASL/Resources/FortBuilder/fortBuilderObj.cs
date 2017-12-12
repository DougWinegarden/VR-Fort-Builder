using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class fortBuilderObj : MonoBehaviour {

    public GameObject destroyCube;
    //public GameObject directManipulator;

    public bool selectable;
    public bool selected;
    public bool check = false;

    private Material objMaterial;
    public Material selectMaterial;

    private int? requestedPhotonView = null;

    void Start()
    {
        objMaterial = gameObject.GetComponent<Renderer>().material;
        
    }

    void Update()
    {
        if (this.transform.position.x >= 12 || this.transform.position.x <= -12
            || this.transform.position.z >= 12 || this.transform.position.z <= -12 ||
                this.transform.position.y <= -3)
        {
            

            if(SceneManager.GetActiveScene().name == "Vive")
            {
                GameObject rightController = GameObject.Find("Controller (right)");
                if(gameObject == rightController.GetComponent<ViveController>().currentObj)
                {
                    rightController.GetComponent<ViveController>().currentObj = null;
                    rightController.GetComponent<ViveController>().isObjSelected = false;
                }
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void Select()
    {
        if (gameObject.GetPhotonView().ownerId == PhotonNetwork.player.ID)
        {
            setAsSelected();
        }
        else
        {
            PhotonView photonView = gameObject.GetPhotonView();
            requestedPhotonView = photonView.viewID;
            if (photonView != null)
            {
                if (PhotonNetwork.playerList.Contains(photonView.owner))
                {
                    photonView.RPC("GrabNotSelected", PhotonTargets.Others);
                }
                else
                {
                    Debug.Log("Player owning object not in room. Requesting ownership");
                    photonView.RequestOwnership();
                }
            }

        }
    }

    private void setAsSelected()
    {
        selected = true;
        WorldManager.objManager.OnObjectSelected(gameObject, PhotonNetwork.player.ID);
        objMaterial = gameObject.GetComponent<Renderer>().material;
        gameObject.GetComponent<Renderer>().material = selectMaterial;
    }

    public void OnOwnershipTransfered(object[] viewAndPlayers)
    {
        PhotonView view = viewAndPlayers[0] as PhotonView;

        PhotonPlayer newOwner = viewAndPlayers[1] as PhotonPlayer;

        PhotonPlayer oldOwner = viewAndPlayers[2] as PhotonPlayer;

        if(requestedPhotonView != null && requestedPhotonView == view.viewID && newOwner.ID == PhotonNetwork.player.ID)
        {
            requestedPhotonView = null;
            setAsSelected();
        }

        Debug.Log("OnOwnershipTransfered for PhotonView" + view.ToString() + " from " + oldOwner + " to " + newOwner);
    }

    public void Deselect()
    {
        selected = false;
        gameObject.GetComponent<Renderer>().material = objMaterial;
    }

    [PunRPC]
    void GrabNotSelected(PhotonMessageInfo info)
    {
        if (selected) return;

        var ownableObject = gameObject.GetComponent<UWBNetworkingPackage.OwnableObject>();
        if(ownableObject != null )
        {
            ownableObject.RelinquishOwnership(info.sender.ID);
        }
    }


    /*

    public Vector3 _previousPosition;

    private GameObject _manipulator;
    private Color primaryColor;
    

    public bool IsTransformDirty
    {
        get
        {
            if (this.gameObject.transform.hasChanged == true && _previousPosition != this.gameObject.transform.localPosition)
            {
                _previousPosition = this.gameObject.transform.localPosition;
                return true;
            }
            return false;
        }
        set
        {
            this.gameObject.transform.hasChanged = value;
        }
    }

    //public bool IsSelectable = true;

    //public BaseMesh ParentMesh;

    // Use this for initialization
    void Start()
    {
        primaryColor = GetComponent<MeshRenderer>().material.color;
        _previousPosition = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_manipulator != null)
        {
            this.transform.localPosition = _manipulator.transform.localPosition;
        }
    }

    public void SetPreviousPosition()
    {
        _previousPosition = this.transform.localPosition;
    }

    private void OnMouseDown()
    {
        
    }

    public void DestoryManipulator()
    {
        if (_manipulator == null) return;
        Destroy(_manipulator);
        _manipulator = null;
    }

    public bool HasManipulator()
    {
        return _manipulator != null;
    }

    public void Select()
    {
        selected = true;
        if (_manipulator == null && selectable)
        {
            //_manipulator = Instantiate(Resources.Load("DirectManipulator", typeof(GameObject))) as GameObject;
            _manipulator = Instantiate(directManipulator) as GameObject;
            _manipulator.transform.localPosition = this.transform.localPosition;
            //ParentMesh.ObservableController = this.gameObject;
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
    }

    public void Deselect()
    {
        selected = false;
        DestoryManipulator();
        GetComponent<MeshRenderer>().material.color = primaryColor;
    }

    */
}
