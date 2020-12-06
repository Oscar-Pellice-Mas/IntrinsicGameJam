using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundInfo : MonoBehaviour
{
    //La poblacio abans i ara (0->Poblacio abans, 1->Poblacio ara)
    public long[] poblacio = new long[2];
    //Els materials que tens ara i abans
    public int[] materials_ara = new int[3];
    public int[] materials_abans = new int[3];

    //El consum que tenies abans i el que tens ara
    public int[] consum_ara = new int[3];
    public int[] consum_abans = new int[3];
    //Les faccions que t'han atacat
    public List<string> atacants = new List<string>();

}
