using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Vida;
    public int QuantidadeDeItens;
    public int PosicaoX;
    public int PosicaoY;
    public int PosicaoZ;

    public int speed = 6;

    private Api apiService;
    Player playerCriado;

    async void Start()
    {
        apiService = new Api();

        playerCriado = await apiService.GetJogador("1");
        Vida = playerCriado.Vida;
        QuantidadeDeItens = playerCriado.QuantidadeDeItens;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY) * speed * Time.deltaTime;

        transform.Translate(new Vector3(movement.x, movement.y, 0));

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            QuantidadeDeItens++;
        }
    }
}