using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarcomponente : MonoBehaviour
{
    public void activate(GameObject component)
    {
        if (!component.activeSelf)
        {
            component.SetActive(true);
        }
    }

    public void inactivate(GameObject component)
    {
        if (component.activeSelf)
        {
            component.SetActive(false);
        }
    }
}
