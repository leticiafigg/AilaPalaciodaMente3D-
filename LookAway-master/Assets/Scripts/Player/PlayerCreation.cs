using System;
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

    public GameObject classStartStats;
    


    public GameObject confirmButton;
    private GameInformation informationAdd;
    private List<BaseAction> actionsToAdd;
    private StatCalc statCalcScript = new StatCalc();
   
    private static string classDesc;

    private void Start()
    {
        newPlayer = new BasePlayer();
        newPlayer.PlayerName = "Aila";
        newPlayer.PlayerLevel = 1;
        classDesc = " ";

        actionsToAdd = new List<BaseAction>(); // estão sendo adicionados manualmente, então por ora devem ser carregados manualmente, também

        actionsToAdd.Add(new AttackAction());
        actionsToAdd.Add(new Shove());
        actionsToAdd.Add(new Pat());
        actionsToAdd.Add(new ToqueChocante());

        GameInformation.AcoesAprendidas = actionsToAdd;

    }

    private void FixedUpdate()
    {
         
        
                                                    
        
    }

    public void EscolherDestemida()
    {
        //usamos um new player temporário para depois salvá-lo no gameinformation
        newPlayer.Poder = 18;   
        newPlayer.Imaginacao = 9;
        newPlayer.Resistencia = 15;
        newPlayer.Determinacao = 10;
        newPlayer.Sorte = 10;
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

        AtualizarHUDInfo();
    }

    public void EscolherCriativa()
    {
        //usamos um new player temporário para depois salvá-lo no gameinformation
        newPlayer.Poder = 9;
        newPlayer.Imaginacao = 18;
        newPlayer.Resistencia = 10;
        newPlayer.Determinacao = 14;
        newPlayer.Sorte = 11;
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

        AtualizarHUDInfo();

        
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
        classDesc = "Aila permite que alguns pontos mais banais sejam decididos pelo destino, não se abala demais quando as coisas dão errado (Maior Sorte e status equilibrados) ";

        AtualizarHUDInfo();

        
    }

    public void ConfirmarDefinitivo(string primeiracena)
    {

        SaveInformation.SaveAll();

        SceneManager.LoadScene(primeiracena);
    }

    private void AtualizarHUDInfo()
    {
        classDescGameObj.GetComponent<Text>().text = classDesc;

        classStartStats.GetComponent<Text>().text =
            " Poder: " + GameInformation.Aila.Poder + " " +
            " " +
            " Imaginacao: " + GameInformation.Aila.Imaginacao + " " +
            " " +
            " Resistência: " + GameInformation.Aila.Resistencia + " " +
            " " +
            " Determinação: " + GameInformation.Aila.Determinacao + " " +
            " " +
            " Sorte: " + GameInformation.Aila.Sorte;

        confirmButton.SetActive(true);
        classStartStats.SetActive(true);

    }
}
