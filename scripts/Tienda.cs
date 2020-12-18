using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Global
{
    public static int count = 40;
    public static bool gorilacomprado = false;
    public static bool tiburoncomprado = false;
    public static bool osocomprado = false;
    public static bool venadocomprado = false;

    

}

public class Tienda : MonoBehaviour
{
    public Text changingText;

    public GameObject gorila;
    public GameObject codigo_gorila;

    public GameObject tiburon;
    public GameObject codigo_tiburon;

    public GameObject oso;
    public GameObject codigo_oso;

    public GameObject venado;
    public GameObject codigo_venado;

    // Start is called before the first frame update
    void Start()
    {
        changingText.text = (Global.count).ToString();
        desactivargorila();
        codigogorila();
        desactivaroso();
        codigooso();
        desactivartiburon();
        codigotiburon();
        desactivarvenado();
        codigovenado();
    }

    // Update is called once per frame
    void Update()
    {
        desactivargorila();
        codigogorila();
        desactivaroso();
        codigooso();
        desactivartiburon();
        codigotiburon();
        desactivarvenado();
        codigovenado();
    }

    public void TextChange(string comp)
    {
        int temp = UInt16.Parse(changingText.text);
        if (temp - 15 >= 0)
        {
            UnityEngine.Debug.Log("pouedo comprar");
            if(comp=="tiburon" && !Global.tiburoncomprado)
            {
                changingText.text = (-15 + temp).ToString();
                Global.count -= 15;
                Global.tiburoncomprado = true;
            }
            else if(comp == "gorila" && !Global.gorilacomprado)
            {
                changingText.text = (-15 + temp).ToString();
                Global.count -= 15;
                Global.gorilacomprado = true;
            }
            else if (comp == "oso" && !Global.osocomprado)
            {
                changingText.text = (-15 + temp).ToString();
                Global.count -= 15;
                Global.osocomprado = true;
            }
            else if (comp == "venado" && !Global.venadocomprado)
            {
                changingText.text = (-15 + temp).ToString();
                Global.count -= 15;
                Global.venadocomprado = true;
            }
        }
    }
    public void desactivargorila()
    {
        if (Global.gorilacomprado)
            gorila.SetActive(false);
    }
    public void codigogorila()
    {
        if (Global.gorilacomprado)
            codigo_gorila.SetActive(true);
    }

    public void desactivartiburon()
    {
        if (Global.tiburoncomprado)
            tiburon.SetActive(false);
    }
    public void codigotiburon()
    {
        if (Global.tiburoncomprado)
            codigo_tiburon.SetActive(true);
    }

    public void desactivaroso()
    {
        if (Global.osocomprado)
            oso.SetActive(false);
    }
    public void codigooso()
    {
        if (Global.osocomprado)
            codigo_oso.SetActive(true);
    }

    public void desactivarvenado()
    {
        if (Global.venadocomprado)
            venado.SetActive(false);
    }
    public void codigovenado()
    {
        if (Global.venadocomprado)
            codigo_venado.SetActive(true);
    }
}
