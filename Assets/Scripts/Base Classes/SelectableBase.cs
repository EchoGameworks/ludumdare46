using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableBase : MonoBehaviour
{
    public bool IsPlayerMovable = true;
    NavMeshAgent nma;

    public MeshRenderer meshRenderer;
    [HideInInspector]
    public Material mainMaterial;

    private LayerMask SelectedLayer;
    private LayerMask SelectableLayer;

    protected virtual void Start()
    {
        //meshRenderer.material = new Material(meshRenderer.material);
        //mainMaterial = meshRenderer.material;
        
        SelectableLayer = LayerMask.NameToLayer("Selectable");
        SelectedLayer = LayerMask.NameToLayer("Selected");

        nma = GetComponent<NavMeshAgent>();
    }

    public void Select()
    {
        print("Selected - " + gameObject.name);
        //meshRenderer.gameObject.layer = SelectedLayer;
       // mainMaterial.SetFloat("_IsHighlighted", 1f);
    }

    public void Deselect()
    {
        print("Deselected - " + gameObject.name);
        //meshRenderer.gameObject.layer = SelectableLayer;
        //mainMaterial.SetFloat("_IsHighlighted", 0f);
    }

    public void Move(Vector3 location)
    {
        if (!IsPlayerMovable) return;
        print("moving - " + location);
        nma.SetDestination(location);
    }

}
