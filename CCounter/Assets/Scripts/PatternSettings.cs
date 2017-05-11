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

    public class Stich
    {
        private int  m_IdStich;
        public int IdStich
        {
            get { return m_IdStich; }
            set { m_IdStich = value; }
        }

        private string m_Abbr;
        public string Abbr
        {
            get { return m_Abbr; }
            set { m_Abbr = value; }
        }
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private int m_NumberRepeats;
        public int NumberRepeats
        {
            get { return m_NumberRepeats; }
            set { m_NumberRepeats = value; }
        }

        private bool m_IsTickedOff = false;
        public bool IsTickedOff
        {
            get { return m_IsTickedOff; }
            set { m_IsTickedOff = value; }
        }

        public Stich()
        {
            m_IdStich = -1;
            m_Abbr = "";
            m_Name = "";
            m_NumberRepeats = 0;
            m_IsTickedOff = false;
        }
    }

    public class Round
    {
        private string m_NamePattern;
        public string NamePattern
        {
            get { return m_NamePattern; }
            set { m_NamePattern = value; }
        }

        private int m_RoundNumber;
        public int RoundNumber
        {
            get { return m_RoundNumber; }
            set { m_RoundNumber = value; }
        }

        private int m_RepeatsPerGroupStiches;
        public int RepeatsPerGroupStiches
        {
            get { return m_RepeatsPerGroupStiches; }
            set { m_RepeatsPerGroupStiches = value; }
        }

        private List<Stich> m_Stiches;
        public List<Stich> Stiches
        {
            get { return m_Stiches; }
            set { m_Stiches = value; }
        }
        private List<Stich> m_AllRepeatsStiches;
        public List<Stich> AllRepeatsStiches
        {
            get { return m_AllRepeatsStiches; }
            set { m_AllRepeatsStiches = value; }
        }

        public Round()
        {
            m_RoundNumber = 0;
            m_Stiches = new List<Stich>();
            m_AllRepeatsStiches = new List<Stich>();
        }

        public void AddStich(Stich stich)
        {
            m_Stiches.Add(stich);
        }

        public void AddStichesAllRepeats(Stich stich)
        {
            m_AllRepeatsStiches.Add(stich);
        }

        public void Clear()
        {
            m_RoundNumber = 0;
            m_Stiches.Clear();
            m_Stiches = new List<Stich>();

            m_AllRepeatsStiches.Clear();
            m_AllRepeatsStiches = new List<Stich>();
        }
    }    
    
}
