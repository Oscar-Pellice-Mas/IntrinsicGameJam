using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundInfo : MonoBehaviour
{
    //La poblacio abans i ara (0->Poblacio abans, 1->Poblacio ara)
    public float[] poblacio = new float[2];
    //Els materials que tens ara i abans
    public float[] materials_ara = new float[3];
    public float[] materials_abans = new float[3];

    //El consum que tenies abans i el que tens ara
    public float[] consum_ara = new float[3];
    public float[] consum_abans = new float[3];
    //Les faccions que t'han atacat
    public List<string> atacants = new List<string>();

}
