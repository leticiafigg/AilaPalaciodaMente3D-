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
    
    //private  static List<GameObject>  inimsFabricados;

    // Start is called before the first frame update
    void Start()
    {
        destaqueIndex = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (destaqueIndex < BattleHandler.inimObjList.Count && destaqueIndex >= 0)
        {
            if(BattleHandler.inimObjList[destaqueIndex] != null)
            inimDestacado = BattleHandler.inimObjList[destaqueIndex];
        }
        else
        {
            destaqueIndex = 0;
        }

        if(inimDestacado != null)
        {
            //atualiza os valores máximos e atuais
            sliderInimPvObj.GetComponent<Slider>().minValue = 0;
            sliderInimPvObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().pvTotal;

            sliderInimStunObj.GetComponent<Slider>().minValue = 0;
            sliderInimStunObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().StunTotal;


            //atualiza a posição da barra do slider de acordo com a vida total (Calculado automaticamente pelo Slider)
            sliderInimPvObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().pvAtual;
            sliderInimStunObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().StunAtual;

            Cursor.transform.position = inimDestacado.transform.position;
        }
    }

    public static void SetCursorEnemies()
    {
        //inimsFabricados = inims;
        inimDestacado = BattleHandler.inimObjList[destaqueIndex];
    }

    public void SelecionarInimigo(KeyCode tecla)
    {

        switch(tecla)
        {
            case (KeyCode.A):
                destaqueIndex = destaqueIndex - 1;
                break;
            case (KeyCode.D):
                destaqueIndex = destaqueIndex + 1;
                break;
        }
        
        //return inimDestacado.GetComponent<Inimigo>();

    }
    
    public Inimigo RetornarAlvo()
    {
        return inimDestacado.GetComponent<Inimigo>();
    }






}
