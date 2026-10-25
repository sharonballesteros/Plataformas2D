using UnityEngine;

public class RelevoAnimacion : MonoBehaviour
{
    // Esta funcion atrapa el evento del Animator en "eneee" y se lo pasa a "Ene2"
    public void DispararBolita()
    {
        EnemigoDisparador scriptPadre = GetComponentInParent<EnemigoDisparador>();
        if (scriptPadre != null)
        {
            scriptPadre.DispararBolita();
        }
    }
}