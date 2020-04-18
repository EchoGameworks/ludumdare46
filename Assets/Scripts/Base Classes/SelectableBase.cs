using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableBase : MonoBehaviour
{
    public bool IsPlayerMovable = true;    

    [Header("UI")]
    public UIStatus uiStatus;
    public UIAttackArea uiAttackArea;
    //public 
    private LayerMask SelectedLayer;
    private LayerMask SelectableLayer;

    protected virtual void Start()
    {
        
        SelectableLayer = LayerMask.NameToLayer("Selectable");
        SelectedLayer = LayerMask.NameToLayer("Selected");
    }

    public void Select()
    {
        //print("Selected - " + gameObject.name);
        if(uiStatus != null) uiStatus.ToggleUI(true);
        if (uiAttackArea != null) uiAttackArea.ToggleUI(true);
    }

    public void Deselect()
    {
        if (uiStatus != null) uiStatus.ToggleUI(false);
        if (uiAttackArea != null) uiAttackArea.ToggleUI(false);
        //print("Deselected - " + gameObject.name);

    }

}
