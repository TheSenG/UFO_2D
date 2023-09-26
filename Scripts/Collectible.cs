using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]private float minWidth, maxWidth;
    [SerializeField] private float minHeight, maxHeight;
    [SerializeField] private float minForce, maxForce;
    [SerializeField] private float minRotationSpeed , maxRotationSpeed;

    private float rotationSpeed;
    private float startForce;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //AL EMPEZAR LE DAMOS UNA FORMA DISTINTA A CADA UNO
        Vector3 randomScale = new Vector3(Random.Range(minWidth,maxWidth), Random.Range(minHeight,maxHeight),0);

        transform.localScale = randomScale;

        //ASIGNAMOS UNA VELOCIDAD ALEATORIA ENTRE LOS VALORES ESTABLECIDOS
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        //ASIGNAMOS UNA FUERZA DE EMPUJE INICIAL
        startForce = Random.Range(minForce, maxForce);

        //CREAMOS UN VECTOR DE DIRECCION ALEATORIA
        Vector2 randomDirection = Random.insideUnitSphere;
        rb2d.AddForce(randomDirection * startForce);
    }
    void Update()
    {
        //ROTAR CONSTANTEMENTE
        transform.Rotate((Vector3.forward*rotationSpeed)*Time.deltaTime,Space.World);
    }
}
