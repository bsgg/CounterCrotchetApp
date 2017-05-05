using System.Collections;
using System.Collections.Generic;

namespace CCounter
{ 
    public class Round
    {
        public string Summary;
        public List<string> Stiches;

        public Round()
        {
            Summary = "";
            Stiches = new List<string>(); 
        }

        public void Clear()
        {
            Summary = "";
            Stiches.Clear();
            Stiches = new List<string>();
        }
    }

    public class PatternSettings 
    {
        private string m_NamePart = "";
        public string NamePart
        {
            get { return m_NamePart; }
            set { m_NamePart = value; }
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
