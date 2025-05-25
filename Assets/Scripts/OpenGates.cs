using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenGates : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gates;

    private void Start()
    {
        P1Controller.Instance.OnPickUpKey += P1Controller_OnPickUpKey;
    }
    private void P1Controller_OnPickUpKey(object sender, EventArgs empty)
    {
        if (Locks.index < 3) return;

        foreach (GameObject gate in _gates)
        {
            Destroy(gate);
        }
        Destroy(this);
    }
}
