using UnityEngine;
using System.Collections;
using TMPro;
public class WaveSpawnerScript : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public WaveScript[] waves;

    public float waveTimer = 5;   //na koliko sekundi spawnuje wave
    private float countdown = 2;  // za odbrojavanje do sledeceg wave-a
    private int waveIndex = 0;   //redni broj wave-a
    public Transform spawnPoint;  //lokacija gde ce se spawnovati wave
    public TextMeshProUGUI waveCountdownText;

    private void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0) //kada je countdown 0, spawn-uje novi wave
        {
            StartCoroutine(SpawnWave());
            countdown = waveTimer;  //resetuje countdown na vreme izmedju wave-ova
            return;
        }

        countdown-=Time.deltaTime; //smanjuje countdown za 1 svaku sekundu

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); //da countdown ne ode ispod 0

        waveCountdownText.text = Mathf.Round(countdown).ToString(); //Mathf.Round da izbaci decimale
        
        //waveCountdownText.text = string.Format("{0:00.00}", countdown); za decimale
    }

    IEnumerator SpawnWave() //IEnumerator se koristi kako bi timing ove funkcije radio nezavisno od ostalog 
    {                       //Ova funkcija omogucava da se u svakom novom wave-u, enemy-ji tog wave-a spawnuju na 0.5 sekundi
        PlayerStatsScript.Rounds++;

        WaveScript wave = waves[waveIndex];

        foreach (EnemySpawnEntry entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                SpawnEnemy(entry.enemyPrefab);
                yield return new WaitForSeconds(1f / wave.rate); //pomocu ovoga ce se SpawnEnemy() pozvati na svakih 0.5 sekundi
            }

        }

        waveIndex++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("Level Won!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); //spawnuje enemy-ja na odgovarajucu poziciju i rotaciju
        enemiesAlive++;
    }


}
