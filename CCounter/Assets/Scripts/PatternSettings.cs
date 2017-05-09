using System.Collections;
using System.Collections.Generic;

namespace CCounter
{
    public class Stich
    {
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
            m_Abbr = "";
            m_Name = "";
            m_NumberRepeats = 0;
            m_IsTickedOff = false;
        }
    }

    public class Round
    {
        private int m_RoundNumber;
        public int RoundNumber
        {
            get { return m_RoundNumber; }
            set { m_RoundNumber = value; }
        }
        private List<Stich> m_Stiches;
        private List<Stich> m_AllRepeatsStiches;

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
            
        }
    }

    public class PatternSettings 
    {
        private string m_Name = "";
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private List<Round> m_Rounds;

        public PatternSettings()
        {
            m_Rounds = new List<Round>();
        }

        public void AddRound(Round round)
        {
            m_Rounds.Add(round);
        }

        public void RemoveLastRound()
        {
            if (m_Rounds != null && m_Rounds.Count > 0)
            {
                m_Rounds[m_Rounds.Count].Clear();
                m_Rounds.RemoveAt(m_Rounds.Count);
            }
        }
    }
}
