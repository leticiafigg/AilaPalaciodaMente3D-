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

    private static GameObject inimDestacado;
    public  static List<Inimigo> inimListados;
    public  static GameObject[] inimsFabricados;
    //public static GameObject[] inimigosCriados;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //atualiza os valores máximos e atuais

        sliderInimPvObj.GetComponent<Slider>().minValue = 0;
        sliderInimPvObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().pvTotal;

        sliderInimStunObj.GetComponent<Slider>().minValue = 0;
        sliderInimStunObj.GetComponent<Slider>().maxValue = inimDestacado.GetComponent<Inimigo>().stunTotal;


        //atualiza a posição da barra do slider de acordo com a vida total (Calculado automaticamente pelo Slider)
        sliderInimPvObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().pvAtual;
        sliderInimStunObj.GetComponent<Slider>().value = inimDestacado.GetComponent<Inimigo>().stunAtual;

        Cursor.transform.SetParent(inimDestacado.transform);

    }

    public static void SetCursorEnemies(GameObject[] inims)
    {
        inimListados = BattleHandler.inimStatsList;  // armazenando os inimigos na lista passada para referência futura 
        inimsFabricados = inims;
        inimDestacado = inimsFabricados[0];
    }
    
        






}
