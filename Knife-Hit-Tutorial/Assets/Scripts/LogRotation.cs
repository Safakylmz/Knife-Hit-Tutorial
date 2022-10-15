using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
#pragma warning disable 0649  //hata verecek, g�rmezden geliyoruz.  bu k�s�m inspectorde g�z�ks�n diye ayr� class �eklinde yaz�l�yor.
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
            motor.maxMotorTorque = 10000;  //torku s�n�rl�yoruz 
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;  //listenin sonuna geldi mi diye kar��la�t�rma yap�yor. sonuna geldiyse 0'dan devam ediyor.

            //d�nmeyi daha basit tek y�n, tek h�z olarak yapabilecekken bir liste �eklinde farkl� �ekillerde d�nd�rmek i�in bu kodu yazd�k.
        }
    }
}
