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
    public UIOverlay uiOverlay;

    //public 
    private LayerMask SelectedLayer;
    private LayerMask SelectableLayer;

    protected virtual void Start()
    {
        uiOverlay = GameObject.FindGameObjectWithTag("UI").GetComponent<UIOverlay>();
        SelectableLayer = LayerMask.NameToLayer("Selectable");
        SelectedLayer = LayerMask.NameToLayer("Selected");
    }

    public virtual void Select()
    {
        //print("Selected - " + gameObject.name);
        if(uiStatus != null) uiStatus.ToggleUI(true);
        if (uiAttackArea != null) uiAttackArea.ToggleUI(true);
        AudioManager.instance.PlaySound(AudioManager.SoundEffects.Select, true);
    }

    public void Deselect(bool skipToolTip = false)
    {
        if (uiStatus != null) uiStatus.ToggleUI(false);
        if (uiAttackArea != null) uiAttackArea.ToggleUI(false);
        if (uiOverlay != null && !skipToolTip) uiOverlay.HideToolTip();
        //print("Deselected - " + gameObject.name);

    }

}
