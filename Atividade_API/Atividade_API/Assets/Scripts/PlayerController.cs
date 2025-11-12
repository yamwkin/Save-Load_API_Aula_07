using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Vida;
    public int QuantidadeDeItens;
    public float PosicaoX;
    public float PosicaoY;
    public float PosicaoZ;

    public int speed = 6;
    [SerializeField] private GameObject textUpdate;

    private Api apiService;
    Player criadoJogador1;

    public TextMeshProUGUI vidatxt;
    public TextMeshProUGUI itenstxt;
    public TextMeshProUGUI posx;
    public TextMeshProUGUI posy;
    public TextMeshProUGUI posz;
    public GameObject painel;

    async void Start()
    {
        apiService = new Api();

        criadoJogador1 = await apiService.GetJogador("1");
        Vida = criadoJogador1.Vida;
        QuantidadeDeItens = criadoJogador1.QuantidadeDeItens;

        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        PosicaoX = gameObject.transform.position.x;
        PosicaoY = gameObject.transform.position.y;
        PosicaoZ = gameObject.transform.position.z;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            painel.SetActive(!painel.activeSelf);
        }

        vidatxt.text = "Vida: " + criadoJogador1.Vida.ToString();
        itenstxt.text = "Itens: " + criadoJogador1.QuantidadeDeItens.ToString();
        posx.text = "Posição X: " + criadoJogador1.PosicaoX.ToString();
        posy.text = "Posição Y: " + criadoJogador1.PosicaoY.ToString();
        posz.text = "Posição Z: " + criadoJogador1.PosicaoZ.ToString();
    }

    public void botao()
    {
        Vida = 100;
        QuantidadeDeItens = 0;
        criadoJogador1.QuantidadeDeItens = 0;
        criadoJogador1.Vida = 100;
        criadoJogador1.PosicaoX = 0;
        criadoJogador1.PosicaoY = 0;
        criadoJogador1.PosicaoZ = 0;

        UpdatePlayerData();
    }

    public void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY) * speed * Time.deltaTime;

        transform.Translate(new Vector3(movement.x, movement.y, 0));

    }


    public async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            QuantidadeDeItens++;
            criadoJogador1.QuantidadeDeItens = QuantidadeDeItens;
            UpdatePlayerData();
            textUpdate.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Dano"))
        {
            Vida--;
            textUpdate.SetActive(true);
            criadoJogador1.Vida = Vida;
            UpdatePlayerData();
            Destroy(collision.gameObject);
        }
    }

    public async void UpdatePlayerData()
    {
        Player atualizadoJogador1 = await apiService.AtualizarJogador(criadoJogador1.id, criadoJogador1);
    }

    System.Collections.IEnumerator UpdatePosition()
    {
        while (true)
        {
            criadoJogador1.PosicaoX = transform.position.x;
            criadoJogador1.PosicaoY = transform.position.y;
            criadoJogador1.PosicaoZ = transform.position.z;

            UpdatePlayerData();

            yield return new WaitForSeconds(1f);
        }
    }
}