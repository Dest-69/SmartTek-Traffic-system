using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadsController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float Interval;
    [SerializeField] private typeCrossroadsController TypeCrossroadsController;

    [Header("Resources")]
    [SerializeField] private Collider[] Stop;
    [SerializeField] private Renderer[] LightYellow;
    [SerializeField] private Renderer[] LightGreen;
    [SerializeField] private Renderer[] LightRad;
    [SerializeField] private Material[] MaterialLightOn;
    [SerializeField] private Material[] MaterialLightOff;
    private int temp;
    enum typeCrossroadsController
    {
        OneSide,
        TwoSides,
        AllToAll
    }
    void Start()
    {
        switch (TypeCrossroadsController)
        {
            case typeCrossroadsController.OneSide:
                temp = Random.Range(0, Stop.Length);
                StartCoroutine(MainCtrl());
                return;
            case typeCrossroadsController.TwoSides:
                // code
                return;
            case typeCrossroadsController.AllToAll:
                // code
                return;

            default: return;
        }
    }
    #region OneSide Crossroads Controller
    IEnumerator MainCtrl()
    {
        while (true)
        {
            StartCoroutine(LightsOn(temp));
            yield return new WaitForSeconds(Interval);
            LightsOff(temp);
            if (temp < Stop.Length - 1) temp++;
            else temp = 0;
        }
    }
    void LightsOff(int _temp)
    {
        Stop[_temp].enabled = true;
        LightRad[_temp].material = MaterialLightOn[2];
        LightYellow[_temp].material = MaterialLightOff[1];
        LightGreen[_temp].material = MaterialLightOff[0];
    }
    IEnumerator LightsOn(int _temp)
    {
        Stop[_temp].enabled = false;
        LightRad[_temp].material = MaterialLightOff[2];
        LightYellow[_temp].material = MaterialLightOn[1];
        yield return new WaitForSeconds(1);
        LightYellow[_temp].material = MaterialLightOff[1];
        LightGreen[_temp].material = MaterialLightOn[0];
    }
    #endregion

    #region TwoSides Crossroads Controller

    #endregion

    #region AllToAll Crossroads Controller

    #endregion
}
