using NoCodeHL.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitTextToWidth : MonoBehaviour {
    public bool clipText;
    public TextMesh textMesh;
    public MeshRenderer meshRenderer;
    public float maxWidth;

    private float _width;
    private float _height;
    private TextSize _textSizeCalculator;
    // Use this for initialization
    void Start () {
        _textSizeCalculator = new TextSize(textMesh, meshRenderer);
        textMesh.text = _textSizeCalculator.GetClippedText(textMesh.text, maxWidth);
    }
}
