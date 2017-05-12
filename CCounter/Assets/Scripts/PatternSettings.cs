using System.Collections;
using System.Collections.Generic;

namespace CCounter
{
    public class PatternSettings
    {
        public static string[] Stiches = new string[] {
            "Magic Ring", "Slip Stich","Single Crotchet","Increasec",
            "Invisible Increase", "Decrease", "Invisible Decrease", "Double Crotchet",
            "Half Double Crotchet" };

        public static string[] StichesAbbreviations = new string[] {
            "Mg", "Sl", "Sc", "Inc",
            "Inv Inc", "Dec", "Inv Dec", "Dc",
            "Hdc" };
    }

    [System.Serializable]
    public class Stich
    {
        public int  IdStich;  
        public string Abbr;        
        public string Name;        
        public int NumberRepeats;            

        public Stich()
        {
            IdStich = -1;
            Abbr = "";
            Name = "";
            NumberRepeats = 0;
        }
    }

    [System.Serializable]
    public class Round
    {
        public string NamePattern;
        public int RoundNumber;
        public int RepeatsPerGroupStiches;
        public List<Stich>Stiches;
        public List<Stich> AllRepeatsStiches;
        public bool IsTickedOff = false;        

        public Round()
        {
            RoundNumber = 0;
            Stiches = new List<Stich>();
            AllRepeatsStiches = new List<Stich>();
            IsTickedOff = false;
        }

        public void AddStich(Stich stich)
        {
            Stiches.Add(stich);
        }

        public void AddStichesAllRepeats(Stich stich)
        {
            AllRepeatsStiches.Add(stich);
        }

        public void Clear()
        {
            RoundNumber = 0;
            Stiches.Clear();
            Stiches = new List<Stich>();

            IsTickedOff = false;

            AllRepeatsStiches.Clear();
            AllRepeatsStiches = new List<Stich>();
        }
    }    
    
}
