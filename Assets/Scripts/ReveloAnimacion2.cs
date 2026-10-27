using UnityEngine;

public class RelevoAnimacion2 : MonoBehaviour
{
    public void DispararBolita()
    {
        EnemigoDisparador2 scriptPadre = GetComponentInParent<EnemigoDisparador2>();
        if (scriptPadre != null)
        {
            scriptPadre.DispararBolita();
        }
    }
}