using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private RaycastHit hit = new RaycastHit();
    [SerializeField] private float Distance;
    enum vectorMove
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private vectorMove VectorMove;
    bool CanToReact = true;
    private float SpeedMove = 2.5f;
    float stopedTime;
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "bike":
                    if (hit.distance > Distance)
                        Move();
                    else
                    {
                        stopedTime += Time.deltaTime;
                    }
                    return;

                case "stop":
                    if (hit.distance > Distance)
                        Move();
                    return;

                case "ForwardRight":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        switch (RandomizeDirrectionWay(2))
                        {
                            case 0:
                                break;
                            case 1:
                                StartCoroutine(PointToTurn("right"));
                                break;
                            default: break;
                        }
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                case "ForwardLeft":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        switch (RandomizeDirrectionWay(2))
                        {
                            case 0:
                                break;
                            case 1:
                                StartCoroutine(PointToTurn("left"));
                                break;
                            default: break;
                        }
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                case "LeftRight":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        switch (RandomizeDirrectionWay(2))
                        {
                            case 0:
                                StartCoroutine(PointToTurn("right"));
                                break;
                            case 1:
                                StartCoroutine(PointToTurn("left"));
                                break;
                            default: break;
                        }
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                case "CrossroadsX":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        switch (RandomizeDirrectionWay(3))
                        {
                            case 0:
                                StartCoroutine(PointToTurn("right"));
                                break;
                            case 1:
                                StartCoroutine(PointToTurn("left"));
                                break;
                            case 2:
                                break;
                            default: break;
                        }
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                case "turnRight":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        TurnBike("right");
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                case "turnLeft":
                    if (hit.distance < 0.1f && CanToReact)
                    {
                        TurnBike("left");
                        StartCoroutine(HideForRayCast());
                    }
                    Move();
                    return;

                default: return;
            }
        }
    }

    short RandomizeDirrectionWay(short i)
    {
        return (short)Random.Range(0, i);
    }
    IEnumerator HideForRayCast()
    {
        CanToReact = false;
        yield return new WaitForSeconds(2f);
        CanToReact = true;
    }
    void Move()
    {
        transform.position = transform.position + GetAxis() * SpeedMove * Time.deltaTime;
    }
    void Turn(int Dir)
    {
        transform.Rotate(new Vector3(0, 0, transform.rotation.z + Dir), Space.Self);
    }
    Vector3 GetAxis()
    {
        switch (VectorMove)
        {
            case vectorMove.Up: return Vector3.forward;
            case vectorMove.Right: return Vector3.right;
            case vectorMove.Left: return Vector3.left;
            case vectorMove.Down: return -Vector3.forward;
        }
        return new Vector3();
    }

    IEnumerator PointToTurn(string _turnDir)
    {
        stopedTime = 0;
        if (_turnDir == "right")
        {
            yield return new WaitForSeconds(0.16f + stopedTime);
        }
        else if (_turnDir == "left")
        {
            yield return new WaitForSeconds(0.46f + stopedTime);
        }
        TurnBike(_turnDir);
        yield return null;
    }

    void TurnBike(string turnDir)
    {
        if (turnDir == "left") Turn(-90);
        else if (turnDir == "right") Turn(90);

        switch (VectorMove)
        {
            case vectorMove.Up:
                if (turnDir == "left") VectorMove = vectorMove.Left;
                else if (turnDir == "right") VectorMove = vectorMove.Right;
                return;
            case vectorMove.Right:
                if (turnDir == "left") VectorMove = vectorMove.Up;
                else if (turnDir == "right") VectorMove = vectorMove.Down;
                return;
            case vectorMove.Left:
                if (turnDir == "left") VectorMove = vectorMove.Down;
                else if (turnDir == "right") VectorMove = vectorMove.Up;
                return;
            case vectorMove.Down:
                if (turnDir == "left") VectorMove = vectorMove.Right;
                else if (turnDir == "right") VectorMove = vectorMove.Left;
                return;
        }
    }
}
