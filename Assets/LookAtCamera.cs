using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class LookAtCamera : MonoBehaviour
{
    private void Awake()
    {
        var lookAtConstraint = GetComponent<LookAtConstraint>();

        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = Camera.main.transform;
        constraintSource.weight = 1;

        lookAtConstraint.AddSource(constraintSource);
        lookAtConstraint.constraintActive = true;
    }
}
