using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerCreation : MonoBehaviour
{

    private BasePlayer newPlayer;
    public GameObject informationStore;
    public GameObject classTitleGameObj;
    public GameObject classDescGameObj;
    public GameObject confirmButton;
    private GameInformation informationAdd;

    private StatCalc statCalcScript = new StatCalc();
    private static string classDesc;

    private void Start()
    {
        newPlayer = new BasePlayer();
        newPlayer.PlayerName = "Aila";
        newPlayer.PlayerLevel = 1;

        classDesc = " ";



        
    }

    private void Update()
    {
         
        classDescGameObj.GetComponent<Text>().text = classDesc; 
    }

    public void EscolherDestemida()
    {
        //usamos um new player temporário para depois salvá-lo no gameinformation
        newPlayer.Poder = 15;   
        newPlayer.Imaginacao = 8;
        newPlayer.Resistencia = 13;
        newPlayer.Determinacao = 10;
        newPlayer.Sorte = 4;
        newPlayer.Armadura = 0;
        newPlayer.XPAtual = 0;
        newPlayer.XPNecessario = 300;
        newPlayer.AilaClass = BasePlayer.AilaArchetype.DESTEMIDA;

        GameInformation.Aila = newPlayer;
        //GameInformation.Aila.AilaClass = newPlayer.AilaClass;
        GameInformation.AilaPV = statCalcScript.CalcularPV(GameInformation.Aila.Resistencia);
        GameInformation.AilaPF = statCalcScript.CalcularPF(GameInformation.Aila.Imaginacao);
        GameInformation.AilaPVatual = GameInformation.AilaPV;
        GameInformation.AilaPFatual = GameInformation.AilaPF;

        classTitleGameObj.GetComponent<Text>().text = "Destemida";
        classDesc = "Aila é especialmente corajosa e resistente a adversões. Ela se propões a encontrar de frente com problemas e inimigos (Maior Poder e Resistência)";

        confirmButton.SetActive(true);
    }

    public void EscolherCriativa()
    {
        //usamos um new player temporário para depois salvá-lo no gameinformation
        newPlayer.Poder = 8;
        newPlayer.Imaginacao = 16;
        newPlayer.Resistencia = 6;
        newPlayer.Determinacao = 12;
        newPlayer.Sorte = 6;
        newPlayer.Armadura = 0;
        newPlayer.XPAtual = 0;
        newPlayer.XPNecessario = 300;
        newPlayer.AilaClass = BasePlayer.AilaArchetype.CRIATIVA;

        GameInformation.Aila = newPlayer;
        
        GameInformation.AilaPV = statCalcScript.CalcularPV(GameInformation.Aila.Determinacao);
        GameInformation.AilaPF = statCalcScript.CalcularPF(GameInformation.Aila.Imaginacao);
        GameInformation.AilaPVatual = GameInformation.AilaPV;
        GameInformation.AilaPFatual = GameInformation.AilaPF;

        classTitleGameObj.GetComponent<Text>().text = "Criativa";
        classDesc = "Com o poder imaginativo da juventude, Aila é especialmente criativa, encontrado soluções menos óbvias para seus problemas (Maior Imaginação e Determinação) ";

        confirmButton.SetActive(true);
    }

    public void EscolherAvoada()
    {
        //usamos um new player temporário para depois salvá-lo no gameinformation
        newPlayer.Poder = 9;
        newPlayer.Imaginacao = 9;
        newPlayer.Resistencia = 10;
        newPlayer.Determinacao = 10;
        newPlayer.Sorte = 16;
        newPlayer.Armadura = 0;
        newPlayer.XPAtual = 0;
        newPlayer.XPNecessario = 300;
        newPlayer.AilaClass = BasePlayer.AilaArchetype.AVOADA;

        GameInformation.Aila = newPlayer;
        
        GameInformation.AilaPV = statCalcScript.CalcularPV(GameInformation.Aila.Resistencia);
        GameInformation.AilaPF = statCalcScript.CalcularPF(GameInformation.Aila.Imaginacao);
        GameInformation.AilaPVatual = GameInformation.AilaPV;
        GameInformation.AilaPFatual = GameInformation.AilaPF;

        classTitleGameObj.GetComponent<Text>().text = "Avoada";
        classDesc = "Aila permite que alguns pontos mais banais sejam decididos pelo destino, não se abala demais quando as coisas dão errado 'Não era pra ser' (Maior Sorte e status equilibrados) ";

        confirmButton.SetActive(true);
    }

    public void ConfirmarDefinitivo()
    {

        SaveInformation.SaveAll();

        SceneManager.LoadScene("mapa1");
    }

}
