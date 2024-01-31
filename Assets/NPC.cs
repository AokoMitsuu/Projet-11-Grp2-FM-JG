using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class NPC
{
    public string Name;
    public List<TraitSo> Traits;
    public string Summary;

    public NPC(string name, List<TraitSo> traits)
    {
        Name = name;
        Traits = traits;
        GenerateSummary();
    }

    private void GenerateSummary()
    {
        // Générer un résumé basé sur les traits
        Summary = $"Je suis {Name}. Mes traits principaux sont: \n";
        foreach(TraitSo trait in Traits)
        {
            Summary += trait.Description;
            if (Traits.Last() != trait)
                Summary += "\n";
        }
    }
}