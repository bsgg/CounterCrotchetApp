using System.Collections;
using System.Collections.Generic;

namespace CCounter
{ 
    public struct Round
    {
        string[] Cells;
    }

    public class PatternSettings 
    {
        private string m_NamePart = "";
        private int m_NumberRounds = 0;
        private List<Round> m_Rounds;

        public PatternSettings(string namePart, int nRounds)
        {
            m_NamePart = namePart;
            m_NumberRounds = nRounds;
            m_Rounds = new List<Round>();

        }

        public void AddRound(Round round)
        {
            m_Rounds.Add(round);
        }

    }
}
