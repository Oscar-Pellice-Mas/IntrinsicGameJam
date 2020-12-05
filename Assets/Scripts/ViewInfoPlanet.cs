using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoPlanet : MonoBehaviour
{
    public Planet planeta;

    public Image imatge;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI quantitatPoblacio;
    public TextMeshProUGUI regim;
    public TextMeshProUGUI edat;
    public TextMeshProUGUI energia;
    public TextMeshProUGUI tipus;
    public TextMeshProUGUI recursos;
    public TextMeshProUGUI perillositat;
    public TextMeshProUGUI dineros;
    public TextMeshProUGUI especie;


    // Start is called before the first frame update
    void Start()
    {
        imatge.sprite = planeta.planetSprite;
        nom.text = planeta.Nom;    
        quantitatPoblacio.text = planeta.QuantitatPoblacio.ToString();
        regim.text = planeta.Regim.ToString();
        edat.text = planeta.EdatEspecie.ToString();
        energia.text = planeta.EnergiaConsumida.ToString();
        tipus.text = planeta.tipusPlaneta.ToString();
        recursos.text = planeta.recursosConsumitsPerAny.ToString();
        perillositat.text = planeta.perillositat.ToString();
        dineros.text = planeta.pastaGenerada.ToString();
        especie.text = planeta.especie.ToString();

    }


}
