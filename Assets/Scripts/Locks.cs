using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locks : MonoBehaviour
{
    [SerializeField] private List<Image> _lockImages;
    private int index;
    private void Start()
    {
        P1Controller.Instance.OnPickUpKey += P1Controller_OnPickUpKey;
    }
    private void P1Controller_OnPickUpKey(object sender, EventArgs empty)
    {
        if (index >= _lockImages.Count) return;
        _lockImages[index].enabled = false;
        index--;
    }
}
