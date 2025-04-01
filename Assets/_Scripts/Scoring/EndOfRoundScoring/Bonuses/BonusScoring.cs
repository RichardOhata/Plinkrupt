using UnityEngine;
using System.Collections;

public class BonusScoring
{
    public ArrayList bonuses = new ArrayList();

    public void addBonusByName(string name, int value)
    {
        int index = getBonusIndexByName(name);

        // Bonus is not in list
        if (index == -1)
        {
            bonuses.Add(new BonusScore(name, value, 1));
        }
        else
        {
            ((BonusScore) bonuses[index]).counter++;
        }
    }

    public int getBonusIndexByName(string name)
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            if (((BonusScore) bonuses[i]).name.Equals(name)) {
                return i;
            }
        }

        return -1;
    }

    public int getTotalBonus()
    {
        int total = 0;

        foreach (BonusScore score in bonuses)
        {
            total += score.getTotalBonus();
        }

        return total;
    }

    public void clearBonuses()
    {
        bonuses.Clear();
    }
}
