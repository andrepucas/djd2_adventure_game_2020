using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyLights : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _lamp;
    [SerializeField] private MeshRenderer[] _redBooks;
    [SerializeField] private MeshRenderer[] _blueBooks;

    [SerializeField] private Material _lampOriginal;
    [SerializeField] private Material _redBooksOriginal;
    [SerializeField] private Material _blueBooksOriginal;

    [SerializeField] private Material _lampEmission;
    [SerializeField] private Material _redBooksEmission;
    [SerializeField] private Material _blueBooksEmission;

    private Light _light;

    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (_light.enabled)
            EmissionOn();
        
        else
            EmissionOff();
    }

    private void EmissionOn()
    {
        foreach (MeshRenderer lamp in _lamp)
        {
            lamp.material = _lampEmission;
        }

        foreach (MeshRenderer redBook in _redBooks)
        {
            redBook.material = _redBooksEmission;
        }

        foreach (MeshRenderer blueBook in _blueBooks)
        {
            blueBook.material = _blueBooksEmission;
        }
    }

    private void EmissionOff()
    {
        foreach (MeshRenderer lamp in _lamp)
        {
            lamp.material = _lampOriginal;
        }

        foreach (MeshRenderer redBook in _redBooks)
        {
            redBook.material = _redBooksOriginal;
        }

        foreach (MeshRenderer blueBook in _blueBooks)
        {
            blueBook.material = _blueBooksOriginal;
        }
    }
}