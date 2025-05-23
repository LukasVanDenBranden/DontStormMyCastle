using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUi : MonoBehaviour
{
    [SerializeField] List<Image> _keys;
    [SerializeField] Sprite _fullkeySprite;
    private int _index = 0;
    private void Start()
    {
        P1Controller.Instance.OnPickUpKey += P1Controller_OnPickUpKey;
    }
    private void P1Controller_OnPickUpKey(object sender, EventArgs empty)
    {
        if (_index >= _keys.Count) return;
        _keys[_index].sprite = _fullkeySprite;
        _index++;
    }
}
