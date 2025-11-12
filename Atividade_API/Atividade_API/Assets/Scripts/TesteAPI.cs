using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class TesteAPI : MonoBehaviour
{
    private Api apiService;
    [SerializeField] private PlayerController playerController;

    async void Start()
    {
        apiService = new Api();

        Debug.Log("=== TESTE DA API ===");

        //Adicionar Jogadores
        Player novoPlayer1 = new Player();
        novoPlayer1.QuantidadeDeItens = playerController.QuantidadeDeItens;
        novoPlayer1.Vida = playerController.Vida;
        novoPlayer1.PosicaoX = playerController.PosicaoX;
        novoPlayer1.PosicaoY = playerController.PosicaoY;
        novoPlayer1.PosicaoZ = playerController.PosicaoZ;
        //adicionar jogador na API
        Player criadoJogador1 = await apiService.CriarJogador(novoPlayer1);
        Debug.Log($"(Vida: {criadoJogador1.Vida}, Quantidade de Itens: {criadoJogador1.QuantidadeDeItens})");

        Debug.Log("=== FIM DOS TESTES ===");
    }

    //async Task MostrarTodosJogadores()
    //{
    //    Player[] jogadores = await apiService.GetTodosJogadores();
    //    Debug.Log($"Total de jogadores: {jogadores.Length}");
    //    foreach (var jogador in jogadores)
    //    {
    //        Debug.Log($"(Vida: {jogador.Vida}, Quantidade de Itens: {jogador.QuantidadeDeItens})");
    //    }
    //}


    void OnDestroy()
    {
        apiService?.Dispose();
    }
}