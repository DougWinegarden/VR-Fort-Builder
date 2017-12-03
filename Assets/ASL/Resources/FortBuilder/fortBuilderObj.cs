using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fortBuilderObj : MonoBehaviour {

    public GameObject directManipulator;

    public bool selectable;
    public bool selected;

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
}
