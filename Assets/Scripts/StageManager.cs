using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject SelectedUnit;
    public Camera cam;
    public LayerMask SelectableLayer;
    public CameraControl cameraControl; 
    public bool FirstStart = true;
    public UIOverlay uiOverlay;
    public Transform CharacterHolder;

    void Start()
    {
        FirstStart = true;
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


        if (Input.GetKey(KeyCode.Tab))
        {
            if (SelectedUnit != null)
            {
                HeroBase hb = SelectedUnit.GetComponent<HeroBase>();
                if(hb != null)
                {
                    hb.nma.isStopped = true;
                }
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

    public void SelectGenesis()
    {
        if (!FirstStart) return;
        LeanTween.delayedCall(0.5f, SelectGenesisFinal);
    }

    public void SelectGenesisFinal()
    {
        GameObject[] GOs = GameObject.FindGameObjectsWithTag("Caravan");
        foreach (GameObject go in GOs)
        {
            HeroBase hb = go.GetComponent<HeroBase>();
            if (hb.HeroType == HeroBase.HeroTypes.Tree)
            {
                SetSelectedUnit(go);
                cameraControl.followTransform = go.transform;
                //LeanTween.move(cam.gameObject, new Vector3(go.transform.position.x, cam.transform.position.y, go.transform.position.z), 0.5f).setEaseInOutQuad();
                FirstStart = false;
            }
        }
    }

    public void SetSelectedUnit(GameObject go)
    {
        SelectableBase sb = go.GetComponent<SelectableBase>();
        if (sb != null)
        {
            if (go == SelectedUnit) return;
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

    public void Defeat()
    {
        foreach(Transform t in CharacterHolder)
        {
            CharacterBase cb = t.GetComponent<CharacterBase>();
            if(cb != null)
            {
                cb.StatusCurrent = CharacterBase.CharacterStatus.Idle;
            }
            
        }
        SelectGenesisFinal();
        LeanTween.delayedCall(1.5f, () => uiOverlay.ShowGameOver());
    }
}
