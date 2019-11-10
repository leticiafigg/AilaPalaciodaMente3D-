using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUICursor : MonoBehaviour
{
    public GameObject Cursor;

    public GameObject sliderInimPvObj;
    private float pvInimMax;
    private float pvInimMin;


    public GameObject sliderInimStunObj;
    private float stunInimMax;
    private float stunInimMin;

    private static int destaqueIndex;

    public  static  GameObject   inimDestacado;
    public  static List<Inimigo> inimListados;
    public  static GameObject[]  inimsFabricados;

    // Start is called before the first frame update
    void Start()
    {
        destaqueIndex = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (destaqueIndex < inimsFabricados.Length && destaqueIndex >= 0)
        {
            inimDestacado = inimsFabricados[destaqueIndex];
        }
        else
        {
            destaqueIndex = 0;
        }

        //atualiza os valores máximos e atuais
        sliderInimPvObj.GetComponent<Slider>().minValue = 0;
        sliderInimPvObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().pvTotal;

        sliderInimStunObj.GetComponent<Slider>().minValue = 0;
        sliderInimStunObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().stunTotal;


        //atualiza a posição da barra do slider de acordo com a vida total (Calculado automaticamente pelo Slider)
        sliderInimPvObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().pvAtual;
        sliderInimStunObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().stunAtual;

        Cursor.transform.SetParent(inimDestacado.transform, false);



    }

    public static void SetCursorEnemies(GameObject[] inims)
    {
        inimListados = BattleHandler.inimStatsList;  // armazenando os inimigos na lista passada para referência futura 
        inimsFabricados = inims;
        inimDestacado = inimsFabricados[destaqueIndex];
    }

    public Inimigo SelecionarInimigo(KeyCode tecla)
    {

        switch(tecla)
        {
            case (KeyCode.A):
                destaqueIndex--;
                break;
            case (KeyCode.D):
                destaqueIndex++;
                break;
            case (KeyCode.None):

                break;
        }
        
        return inimDestacado.GetComponent<Inimigo>();

    }
    
        






}
