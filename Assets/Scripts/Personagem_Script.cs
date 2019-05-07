using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem_Script : MonoBehaviour
{
    //Objetos
    Animator anim;

    //variaveis
    float velAtual;
    public float velMax = 3.0f;

    public float acelInicial = 0.2f;
    public float acel = 0.02f;
    public float desaceleracao = 0.07f;

    public float velRotacao = 130.0f;

    internal int itens = 0;

	void Awake()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update()
    {
        Movimentacao();
        //print(itens);
	}

    private void Movimentacao()
    {
        //rotação
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 rotacao = Vector3.up * h * velRotacao * Time.deltaTime;

        //movimentação

        //Input
        float v = Input.GetAxisRaw("Vertical");
        if (v > 0 && velAtual < velMax)
        {
            velAtual += (velAtual == 0f) ? acelInicial : acel;
        }
        else if (v == 0 && velAtual > 0)
        {
            velAtual -= desaceleracao;
        }

        //Clamp de valores
        velAtual = Mathf.Clamp(velAtual, 0, velMax);

        //transform
        if (velAtual > 0)
        {
            transform.Rotate(rotacao);
        }
        transform.Translate(Vector3.forward * velAtual * Time.deltaTime);

        //set parametros animação
        float valorAnim = Mathf.Clamp(velAtual / velMax, 0, 1);
        anim.SetFloat("Speed", valorAnim);
    }

    //OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        //interação com esferes = destroi ao tocar e soma um item
        if (other.tag == "Item")
        {
            Destroy(other.gameObject);
            itens++;
        }


    }
}
