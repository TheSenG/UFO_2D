using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField]private float typingSpeed = 0.05f;
    [SerializeField]private string fullText;
    [SerializeField] private float waitBeforeFade = 1f;
    [SerializeField] private float fadeSpeed = 1f;

    private TMP_Text textComponent;
    private string currentText = "";
    private AudioSource audioSource;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        //RECORREMOS TODO EL TEXTO
        for (int i =0; i < fullText.Length;i++)
        {
            //AÑADIMOS LA LETRA
            currentText += fullText[i];
            //MOSTRAMOS EL TEXTO
            textComponent.text = currentText;
            //REPRODUCE UN SONIDO
            audioSource.Play();
            //PAUSA ENTRE LETRAS (VELOCIDAD)
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(waitBeforeFade);
        //QUITAMOS EL TEXTO
        StartCoroutine(FadeOutText());
    }
    private IEnumerator FadeOutText()
    {
        //MIENTRAS EL COLOR SEA SUPERIOR
        while (textComponent.alpha > 0f)
        {
            //VAMOS QUIANDO LA TRANSPARENCIA
            textComponent.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        //CUANDO NO SE VEA EL TEXTO SE DESTRUIRA
        Destroy(gameObject);
    }
}
