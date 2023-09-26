using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] GameObject completedText;
    [SerializeField] Animator fade;
    [SerializeField] private float movSpeed = 5f;
    [SerializeField] private AudioClip itemPickUpSFX, collisionSFX;

    private Rigidbody2D rb2d;
    private int remainingCollectibles;
    private bool canPlay = false;
    private AudioSource audioSource;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //RETORNA LA CANTIDAD DE PICKUPS RESTANTES
        remainingCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        //ACTUALIZA EL TEXTO A LA CANTIDAD DE ITEMS A RECOGER
        UpdateText();
        //ACTIVAMOS AL PLAYER A LOS 7 SEGUNDOS
        StartCoroutine(EnablePlayer(true,7f));
    }
    void FixedUpdate()
    {
        if (canPlay)
        {
            //GUARDAMOS INPUTS DE EJES
            float xMov = Input.GetAxis("Horizontal");
            float yMov = Input.GetAxis("Vertical");

            //CREAMOS VECTOR 2D DE MOVIMIENTO
            Vector2 movement = new Vector2(xMov, yMov);

            //ASSIGANMOS EMPUJE AL RB (PREFERIRIA USAR EL VELOCITY :p)
            rb2d.velocity=(movement * movSpeed);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //SI COLISIONA CON UN PICK UP
        if (collision.gameObject.CompareTag("Collectible"))
        {
            //RESTAMOS A LOS RESTANTES
            remainingCollectibles--;
            //ACTUALIZAMOS TEXTO
            UpdateText();
            //DESTRUIMOS EL ITEM
            Destroy(collision.gameObject);
            //SFX PICKUP
            audioSource.PlayOneShot(itemPickUpSFX,0.75f);
            //SI SE HAN RECOGIDO TODOS LOS COLECCIONABLES
            if(remainingCollectibles <= 0)
            {
                //OSCURECEMOS
                fade.SetTrigger("fade");
                //MOSTRAMOS TEXTO DE VICTORIA
                completedText.SetActive(true);
                //DESACTIVAMOS LOS CONTROLES
                StartCoroutine(EnablePlayer(false));
            }
        }
        else { audioSource.PlayOneShot(collisionSFX,0.75f); }
    }
    void Update()
    {
        //SI PULSA ESC, SALE DEL JUEGO
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit Game");
        }
    }
    void UpdateText()
    {
        //ACTUALIZAMOS ITEMS RESTANTES
        objectiveText.text = "REMAINING: " + remainingCollectibles.ToString();
    }
    private IEnumerator EnablePlayer(bool enabled,float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        canPlay = enabled;
    }
}
