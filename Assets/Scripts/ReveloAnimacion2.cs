using UnityEngine;

public class RelevoAnimacion2 : MonoBehaviour
{
    // Esta función atrapa el evento del Animator en "ene2.1" y se lo pasa al padre "Ene3"
    public void DispararBolita()
    {
        EnemigoDisparador2 scriptPadre = GetComponentInParent<EnemigoDisparador2>();
        if (scriptPadre != null)
        {
            scriptPadre.DispararBolita();
        }
    }
}