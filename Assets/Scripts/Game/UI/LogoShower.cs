using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LogoShower : MonoBehaviour
{
    [SerializeField] private GameObject _logoCanvas;
    [SerializeField] private float _delay;
    private void Start()
    {
        _logoCanvas.SetActive(true);
        StartCoroutine(HideLogo(_delay));
    }

    private IEnumerator HideLogo(float delay)
    {
        yield return new WaitForSeconds(delay);
        _logoCanvas.SetActive(false);
    }
}
