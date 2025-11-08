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
}