using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Api
{
    private readonly HttpClient httpClient;
    private const string BASE_URL = "https://68f95abcdeff18f212b951ec.mockapi.io";

    public Api()
    {
        httpClient = new HttpClient();
    }

    //public async Task<Player[]> GetTodosJogadores()
    //{
    //    try
    //    {
    //        string url = $"{BASE_URL}/Jogador";
    //        Debug.Log($"GET: {url}");

    //        HttpResponseMessage response = await httpClient.GetAsync(url);
    //        response.EnsureSuccessStatusCode();

    //        string json = await response.Content.ReadAsStringAsync();
    //        Debug.Log($"Resposta recebida: {json.Substring(0, Math.Min(200, json.Length))}...");

    //        // Como JsonUtility não suporta arrays diretamente, vamos usar um wrapper
    //        string wrappedJson = $"{{\"jogadores\":{json}}}";
    //        PlayerArray jogadorArray = JsonUtility.FromJson<PlayerArray>(wrappedJson);

    //        return jogadorArray.jogadores;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError($"Erro ao buscar jogadores: {ex.Message}");
    //        return new Player[0];
    //    }
    //}

    /// <summary>
    /// Busca um jogador específico
    /// </summary>
    public async Task<Player> GetJogador(string id)
    {
        try
        {
            string url = $"{BASE_URL}/player/{id}";
            Debug.Log($"GET: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador recebido: {json}");

            Player jogador = JsonUtility.FromJson<Player>(json);
            return jogador;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogador {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Atualiza dados do jogador
    /// </summary>
    public async Task<Player> AtualizarJogador(string id, Player jogador)
    {
        try
        {
            string url = $"{BASE_URL}/player/{id}";
            Debug.Log($"PUT: {url}");

            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador atualizado: {responseJson}");

            return JsonUtility.FromJson<Player>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar jogador {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Cria novo jogador
    /// </summary>
    public async Task<Player> CriarJogador(Player jogador)
    {
        try
        {
            string url = $"{BASE_URL}/player";
            Debug.Log($"POST: {url}");

            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador criado: {responseJson}");

            return JsonUtility.FromJson<Player>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao criar jogador: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> RemoverItem(string jogadorId, string itemId)
    {
        try
        {
            string url = $"{BASE_URL}/Jogador/{jogadorId}/Itens/{itemId}";
            Debug.Log($"DELETE: {url}");

            HttpResponseMessage response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            Debug.Log("Item removido com sucesso");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao remover item {itemId}: {ex.Message}");
            return false;
        }
    }

    public void Dispose()
    {
        httpClient?.Dispose();
    }
}