using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class LookAtCamera : MonoBehaviour
{
    private void Awake()
    {
        var source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1;

        var lookAtConstraint = GetComponent<LookAtConstraint>();
        lookAtConstraint.AddSource(source);
        lookAtConstraint.constraintActive = true;
    }
}
