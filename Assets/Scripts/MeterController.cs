using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeterController : MonoBehaviour
{
    [SerializeField] private GameObject bike;
    [SerializeField] private TextMeshProUGUI meterText;
    private Rigidbody rb;

    void Start()
    {
        rb = bike.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // var speed = rb.velocity.magnitude;

        meterText.text = rb.velocity.magnitude.ToString("0.0");
    }
}
