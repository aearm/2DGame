﻿using UnityEngine;
using System.Collections;

public class ControllerState2D
{
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeff { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingBelow { get; set; }
    public bool IsMovingDownSlope { get; set; }
    public bool IsMovingUpSlope { get; set; }
    public bool IsGrounded { get { return IsCollidingBelow; } }
    public float SlopeAngle { get; set; }
    public bool HasCollision { get { return IsCollidingRight || IsCollidingLeff || IsCollidingAbove || IsCollidingBelow; } }

    public void Rest()
    {
        IsMovingUpSlope = IsMovingDownSlope = IsCollidingLeff = IsCollidingRight = IsCollidingAbove = IsCollidingBelow = false;
        SlopeAngle = 0;
    }
    public override string ToString()
    {
        return string.Format("(controller: r:{0} l:{1} a:{2} b:{3} down-slope: {4} up-slope: {5} angle: {6})", IsCollidingRight, IsCollidingLeff, IsCollidingAbove, IsCollidingBelow, IsMovingDownSlope, IsMovingUpSlope, SlopeAngle);
    }
}