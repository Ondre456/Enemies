using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Repainter : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor = Color.red;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}
