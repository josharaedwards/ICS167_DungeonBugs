using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public void Init(bool offSet)
    {
        if (offSet)
            _renderer.color = _offsetColor;
        else
            _renderer.color = _baseColor;

    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
