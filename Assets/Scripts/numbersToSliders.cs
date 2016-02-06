using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class numbersToSliders : MonoBehaviour
{
    static int[] rankings =
        {
            0,
            3,
            6,
            10,
            19,
            35,
            65,
            120,
            222,
            412,
            761,
            1409,
            2606,
            4821,
            8920,
            16502,
            30528,
            56477,
            104482,
            193291,
            357589,
            661540,
            1223848,
            2264119,
            4188620,
        };

    public static float findRank(float num)
    {
        int rank = 0;
        for (int i = 0; i < rankings.Length; i++)
        {
            if (num >= rankings[i])
                rank++;
        }
        return rank + ((num - rankings[rank - 1]) / (rankings[rank] - rankings[rank - 1]));
    }
}