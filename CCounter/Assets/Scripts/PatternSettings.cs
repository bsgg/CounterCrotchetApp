using System.Collections;
using System.Collections.Generic;

namespace CCounter
{
    public class PatternSettings
    {
        public enum ESTICH { NONE = -1, MAGIC_RING = 0, SLIP_STICH, SINGLE_CROTCHET, INCREASE, INVISIBLE_INCREASE, DECREASE, INVISIBLE_DECREASE, DOUBLE_CROTCHET, HALF_DOUBLE_CROTCHET };

        public static string[] Stiches = new string[] {
            "Magic Ring", "Slip Stich","Single Crotchet","Increase",
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
       // public int  IdStich;  
        //public string Abbr;        
        public string Name;        
        public int NumberRepeats;
        //public bool SpecialStich;
        //public bool CountAsStich;         

        public Stich()
        {
            //IdStich = (int)PatternSettings.ESTICH.NONE;
           // Abbr = "";
            Name = "";
            NumberRepeats = 0;
           // SpecialStich = false;
            //CountAsStich = true;
        }
    }

    [System.Serializable]
    public class Round
    {
        public enum ETYPEROUND { NORMAL, FRONTLOOPY, BACKLOOP};

        public string PartName;
        public ETYPEROUND TypeRound = ETYPEROUND.NORMAL;        
        public int RoundNumber;
        public int Repeats;
        
        public List<Stich> Stiches;
        public List<Stich> AllRepeatsStiches;
        public int TotalNumberStiches;
        public bool IsCompleted = false;        

        public Round()
        {
            PartName = "Part";
            TypeRound = ETYPEROUND.NORMAL;
            RoundNumber = 0;
            Stiches = new List<Stich>();
            AllRepeatsStiches = new List<Stich>();
            TotalNumberStiches = 0;
            IsCompleted = false;
        }
        
    }    
    
}
