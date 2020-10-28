using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Image RecordImage;
    [SerializeField] private GameObject CameraRotateObj;
    [SerializeField] private float CameraRotateField;
    [SerializeField] private float CameraSpeedRotation;
    void Start()
    {
        StartCoroutine(REC());
    }
    void FixedUpdate()
    {
        if (CameraRotateObj.transform.rotation.y > CameraRotateField || CameraRotateObj.transform.rotation.y < -CameraRotateField) CameraSpeedRotation *= -1;
        CameraRotateObj.transform.Rotate(new Vector3(0, CameraRotateField, 0), CameraSpeedRotation);
    }

    IEnumerator REC()
    {
        while (true)
        {
            RecordImage.enabled = true;
            yield return new WaitForSeconds(2f);
            RecordImage.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
