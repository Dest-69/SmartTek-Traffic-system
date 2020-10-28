using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private RaycastHit hit = new RaycastHit();
    [SerializeField] private float Distance;
    [SerializeField] private float SpeedMove;
    enum vectorMove
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private vectorMove VectorMove;
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "bike":
                    if (hit.distance > Distance)
                        Move();
                    return;

                case "stop":
                    if (hit.distance > Distance)
                        Move();
                    return;

                case "CrossroadsRight":

                    if (hit.distance < 0.05f)
                    {
                        bool isBikeTurn;
                        isBikeTurn = (Random.value < 0.5);
                        if (isBikeTurn)
                        {
                            Turn(90);
                            TurnBike("right");
                            StartCoroutine(HideForRayCast(hit.collider));
                        }
                        else Move();
                    }
                    Move();
                    return;
                case "CrossroadsLeft":

                    if (hit.distance < 0.05f)
                    {
                        bool isBikeTurn;
                        isBikeTurn = (Random.value < 0.5);
                        if (isBikeTurn)
                        {
                            Turn(-90);
                            TurnBike("left");
                            StartCoroutine(HideForRayCast(hit.collider));
                        }
                        else Move();
                    }
                    Move();
                    return;

                case "turnRight":
                    if (hit.distance < 0.05f)
                    {
                        Turn(90);
                        TurnBike("right");
                        StartCoroutine(HideForRayCast(hit.collider));
                    }
                    else Move();
                    return;

                case "turnLeft":
                    if (hit.distance < 0.05f)
                    {
                        Turn(-90);
                        TurnBike("left");
                        StartCoroutine(HideForRayCast(hit.collider));
                    }
                    else Move();
                    return;

                default: return;
            }
        }
        Debug.DrawRay(transform.position, transform.right, Color.green);
    }
    IEnumerator HideForRayCast(Collider me)
    {
        me.enabled = false;
        yield return new WaitForSeconds(1);
        me.enabled = true;
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

    void TurnBike(string turnDir)
    {
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
