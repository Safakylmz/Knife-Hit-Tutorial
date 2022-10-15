using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
#pragma warning disable 0649  //hata verecek, görmezden geliyoruz.  bu kýsým inspectorde gözüksün diye ayrý class þeklinde yazýlýyor.
        public float Speed;
        public float Duration;
#pragma warning restore 0649
    }

    [SerializeField]
    private RotationElement[] rotationPattern;

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine("PlayRotationPattern");
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotationIndex].Speed;
            motor.maxMotorTorque = 10000;  //torku sýnýrlýyoruz 
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;  //listenin sonuna geldi mi diye karþýlaþtýrma yapýyor. sonuna geldiyse 0'dan devam ediyor.

            //dönmeyi daha basit tek yön, tek hýz olarak yapabilecekken bir liste þeklinde farklý þekillerde döndürmek için bu kodu yazdýk.
        }
    }
}
