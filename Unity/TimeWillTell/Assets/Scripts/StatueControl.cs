using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueControl : MonoBehaviour
{
    [SerializeField] private Transform _leftForeArm;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightForeArm;
    [SerializeField] private Transform _rightHand;

    private Quaternion _correctLeftForeArm;
    private Quaternion _correctLeftHand;
    private Quaternion _correctRightForeArm;
    private Quaternion _correctRightHand;

    // Start is called before the first frame update
    void Start()
    {
        _correctLeftForeArm = new Quaternion (357.86499f,-2.66989588e-07f,230, 0);
        _correctLeftHand = new Quaternion (9.86299801f,90,0.256000727f, 0);
        _correctRightForeArm = new Quaternion (357.902191f,359.878326f,3.3159461f, 0);
        _correctRightHand = new Quaternion (9.86799812f,180,359.884003f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_leftForeArm.rotation == _correctLeftForeArm &&
            _leftHand.rotation == _correctLeftHand &&
            _rightForeArm.rotation == _correctRightForeArm &&
            _rightHand.rotation == _correctRightHand)

        Debug.Log("Yes.");
    }
}
