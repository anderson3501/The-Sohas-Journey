using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement instance;

    public event EventHandler MuerteJugador;
    public GameObject BulletPrefab;
    public GameObject GruntPrefab;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private float LastShoot;
    private float vidasPersonaje;
    private float sliderVidas = 100;
    private float enemiesGenerated = 20;
    private float leftEnemies = 20;

    public Slider barra;
    private bool velocidadDuplicada = false;
    private float LastGenerate;

    private float semilla;
    private const long a = 1103515245;
    private const long c = 12345;
    private const long m = 2147483648;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>(); 
        Animator = GetComponent<Animator>();
        LastGenerate = Time.time;
        //Grunt_Script.OnEnemyEliminated += HandleEnemyEliminated;
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-6.5f, 6.5f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(6.5f, 6.5f, 1.0f);
        Animator.SetBool("Running", Horizontal != 0.0f);

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.5f)
        {
            Shoot();
            LastShoot = Time.time;
        }
        GenerateEnemies();
    }

    private void Shoot()
    {
        // Obtén la dirección del disparo
        Vector3 direction = (transform.localScale.x > 1.0f) ? Vector2.right : Vector2.left;

        // Instancia el prefab de bala
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.5f, Quaternion.identity);

        // Accede al componente BulletController de la bala instanciada
        BulletController bulletController = bullet.GetComponent<BulletController>();

        // Configura la dirección de la bala
        bulletController.SetDirection(direction);
    }

    private void FixedUpdate()
    {
        if (velocidadDuplicada)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * Speed * 2, Rigidbody2D.velocity.y);
        }
        else
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        }
    }

    public void Hit()
    {
        sliderVidas -= 20;
        barra.value = sliderVidas;
        if (sliderVidas <= 0)
        {
            Debug.Log("Jugador muere");
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth()
    {
        if (sliderVidas < 100)
        {
            sliderVidas += 20;
            if (sliderVidas > 100)
            {
                sliderVidas = 100;
            }
            barra.value = sliderVidas;
        }
    }

    public void DuplicateSpeed()
    {
        StartCoroutine(DuplicateSpeedCoroutine(2.0f));
    }

    private IEnumerator DuplicateSpeedCoroutine(float duration)
    {
        Speed *= 1.3f;
        velocidadDuplicada = true;

        yield return new WaitForSeconds(duration);

        Speed /= 1.3f;
        velocidadDuplicada = false;
    }

    private void GenerateEnemies()
    {
        int num = Next(0, 100);

        if (enemiesGenerated > 0 && Time.time > LastGenerate + 3.0f)
        {
            Vector3[] spawnPoints = {
                new Vector3(1.015f, -2.598f, 0),
                new Vector3(9.05f, -2.598f, 0),
                new Vector3(12.664f, -2.598f, 0),
                new Vector3(17.161f, -2.598f, 0),
                new Vector3(24.765f, -2.598f, 0)
            };

            Vector3 spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]; // Especificar UnityEngine.Random
            Instantiate(GruntPrefab, spawnPoint, Quaternion.identity);
            enemiesGenerated -= 1;
            LastGenerate = Time.time;
        }
        else if (enemiesGenerated == 0 && leftEnemies == 0 && Time.time > LastGenerate + 5.0f)
        {
            // Acabar juego por matar enemigos
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Debug.Log("Juego terminado");
        }
    }

    private void HandleEnemyEliminated()
    {
        leftEnemies -= 1;
        Debug.Log("Enemigos restantes: " + leftEnemies);
        // Lógica para manejar la notificación de eliminación del enemigo
        Debug.Log("¡Un enemigo fue eliminado!");
    }

    public int Next(int min, int max)
    {
        semilla = (a * semilla + c) % m;
        int num2 = (int)((max - min) * ((double)semilla / m)) + min;
        semilla = semilla * Time.time;
        return num2;
    }
}