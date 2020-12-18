using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecVen : MonoBehaviour
{
    public GameObject VentanaTigre;
    public GameObject VentanaElefante;
    public GameObject VentanaRhino;
    public GameObject VentanaPulpo;
    public GameObject VentanaOso;

    Animation AVTigre;
    Animation AVRhino;
    Animation AVElefante;
    Animation AVPulpo;
    Animation AVOso;

    bool VisibleTigre = false;
    bool VisibleElefante = false;
    bool VisibleRhino = false;
    bool VisiblePulpo = false;
    bool VisibleOso = false;

    // Start is called before the first frame update
    void Start()
    {
        AVElefante = VentanaElefante.GetComponent<Animation>();
        AVTigre = VentanaTigre.GetComponent<Animation>();
        AVRhino = VentanaRhino.GetComponent<Animation>();
        AVPulpo = VentanaPulpo.GetComponent<Animation>();
        AVOso = VentanaOso.GetComponent<Animation>();
    }

    public void VentanaTigrePulsada()
    {
        if (VisibleTigre == false)
        {
            AVTigre["tiger"].speed = 1;
            AVTigre.Play();
            VisibleTigre = true; 
        }
    }
    public void VentanaPulpoPulsada()
    {
        if (VisiblePulpo == false)
        {
            AVPulpo["pulpo"].speed = 1;
            AVPulpo.Play();
            VisiblePulpo = true;
        }
    }
    public void VentanaElefantePulsada()
    {
        if (VisibleElefante == false)
        {
            AVElefante["elephant"].speed = 1;
            AVElefante.Play();
            VisibleElefante = true;
        }
    }
    public void VentanaRhinoPulsada()
    {
        if (VisibleRhino == false)
        {
            AVRhino["rhino"].speed = 1;
            AVRhino.Play();
            VisibleRhino = true;
        }
    }
    public void VentanaOsoPulsada()
    {
        if (VisibleOso == false)
        {
            AVOso["oso"].speed = 1;
            AVOso.Play();
            VisibleOso = true;
        }
    }

    public void VentanaNoPulsada()
    {
        string NombreBoton = EventSystem.current.currentSelectedGameObject.name;
        if(NombreBoton!="tiger" & VisibleTigre==true)
        {
            AVTigre["tiger"].speed = -1;
            AVTigre["tiger"].time = AVTigre["tiger"].length;
            AVTigre.Play();
            VisibleTigre = false;
        }
        if (NombreBoton != "rhino" & VisibleRhino == true)
        {
            AVRhino["rhino"].speed = -1;
            AVRhino["rhino"].time = AVRhino["rhino"].length;
            AVRhino.Play();
            VisibleRhino = false;
        }
        if (NombreBoton != "elephant" & VisibleElefante == true)
        {
            AVElefante["elephant"].speed = -1;
            AVElefante["elephant"].time = AVElefante["elephant"].length;
            AVElefante.Play();
            VisibleElefante = false;
        }
        if (NombreBoton != "pulpo" & VisiblePulpo == true)
        {
            AVPulpo["pulpo"].speed = -1;
            AVPulpo["pulpo"].time = AVPulpo["pulpo"].length;
            AVPulpo.Play();
            VisiblePulpo= false;
        }
        if (NombreBoton != "oso" & VisibleOso == true)
        {
            AVOso["oso"].speed = -1;
            AVOso["oso"].time = AVOso["oso"].length;
            AVOso.Play();
            VisibleOso = false;
        }
    }
}
