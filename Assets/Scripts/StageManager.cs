using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject SelectedUnit;
    public Camera cam;
    public LayerMask SelectableLayer;

    void Start()
    {

    }

    void Update()
    {
        HandleSelect();
    }

    void HandleSelect()
    {
        //Left Click
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Selectable Objects
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, SelectableLayer))
            {
                //print(hit.transform.name);
                SetSelectedUnit(hit.transform.gameObject);
                return;
            }

            //Ground Select
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //print("Ground Select");
                DeselectUnit();
            }
        }
    }

    public void Move(Vector3 location)
    {
        if (SelectedUnit == null) return;
        CharacterBase sb = SelectedUnit.GetComponent<CharacterBase>();
        if (sb != null)
        {

            sb.Move(location);
        }
        else
        {
            Debug.LogWarning("No Selectable Base Available on Deselect");
        }
    }

    public void SetSelectedUnit(GameObject go)
    {
        SelectableBase sb = go.GetComponent<SelectableBase>();
        if (sb != null)
        {
            if (SelectedUnit != null) SelectedUnit.GetComponent<SelectableBase>().Deselect(true);
            sb.Select();
        }
        else
        {
            Debug.LogWarning("No Selectable Base Available on Select", go);
        }
        SelectedUnit = go;
    }

    public void DeselectUnit()
    {
        if (SelectedUnit == null) return;
        SelectableBase sb = SelectedUnit.GetComponent<SelectableBase>();
        if (sb != null)
        {
            sb.Deselect();
        }
        else
        {
            Debug.LogWarning("No Selectable Base Available on Deselect");
        }
        SelectedUnit = null;
    }
}
