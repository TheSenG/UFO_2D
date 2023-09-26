using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float movSpeed = 0.05f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            //VECTOR DE DESPLACAMIENTO
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, target.position, movSpeed);

            //ACTIALIZAR LA POSICION DE LA CAMARA
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}