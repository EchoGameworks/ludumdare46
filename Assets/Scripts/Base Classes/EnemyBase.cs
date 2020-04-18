using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : SelectableBase
{
    private float visibility = 0f;

    public List<GameObject> CanSeeMeObjects;

    protected override void Start()
    {
        base.Start();
        CanSeeMeObjects = new List<GameObject>();
        visibility = 0f;
    }

    void Update()
    {
        CheckVisibility();
    }

    private void CheckVisibility()
    {
        if (CanSeeMeObjects.Count > 0)
        {
            visibility += 0.1f;
        }
        else
        {
            visibility -= 0.1f;
        }
        visibility = Mathf.Clamp(visibility, 0f, 1f);
        mainMaterial.SetFloat("_Visibility", visibility);
        if (visibility > 0f)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        else
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    public void RemoveCanSeeMeObject(GameObject go)
    {
        CanSeeMeObjects.Remove(go);
    }

    public void AddCanSeeMeObject(GameObject go)
    {
        //IsVisible = true;
        CanSeeMeObjects.Add(go);
    }
}
