using System.Collections.Generic;
using UnityEngine;

public static class ListShuffler
{
    // M�thode d'extension pour m�langer une liste
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            // �change les �l�ments
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}