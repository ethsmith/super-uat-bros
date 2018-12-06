using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {

    // Pawn connected to the controller
    public Pawn pawn;

    // Move function that must be implemented
    public abstract void Move();
}
