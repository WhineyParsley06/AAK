using UnityEngine;

public class Horse_Script : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int turnStep = 0; // 0→1→2→3→0 (gira en cada esquina)

    void Update()
    {
        // Siempre avanza hacia su cabeza
        transform.position += transform.right * moveSpeed * Time.deltaTime;

        // Giro en cuadrado según posición y etapa
        switch (turnStep)
        {
            case 0: // Gira en Z = 7
                if (transform.position.z >= 8f)
                {
                    transform.Rotate(0, -90f, 0);
                    turnStep = 1;
                }
                break;

            case 1: // Gira en X = -7
                if (transform.position.x <= -8f)
                {
                    transform.Rotate(0, -90f, 0);
                    turnStep = 2;
                }
                break;

            case 2: // Gira en Z = -7
                if (transform.position.z <= -8f)
                {
                    transform.Rotate(0, -90f, 0);
                    turnStep = 3;
                }
                break;

            case 3: // Gira en X = 7
                if (transform.position.x >= 7f)
                {
                    transform.Rotate(0, -90f, 0);
                    turnStep = 0; // Reinicia ciclo
                }
                break;
        }
    }
}
