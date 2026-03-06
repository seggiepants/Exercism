using Xunit.v3;

public class RelativeDistance
{   
    Dictionary<string, string[]> tree;
    public RelativeDistance(Dictionary<string, string[]> familyTree)
    {
        this.tree = familyTree;
    }
    
    public int DegreeOfSeparation(string personA, string personB)
    {
        List<string> OneDegree(Dictionary<string, string[]>tree, string name)
        {
            List<string> ret = new();
            List<string> keys = tree.Keys.ToList<string>(); 
            
            // children
            if (keys.Contains(name))
                ret.AddRange(tree[name]);

            // Parents & siblings
            foreach(string key in keys)
            {
                if (tree[key].Contains(name))
                {
                    ret.Add(key);
                    ret.AddRange(tree[key]);
                }
            }
            // don't include name in the return set
            return (from string entry in ret
                    where entry != name
                    select entry).ToHashSet<string>().ToList<string>();
        }

        // searched is none, new to search is one degree separation
        // if count doesn't change between rounds
        List<string> familyA = new();
        List<string> newFamilyA = OneDegree(this.tree, personA);
        List<string> familyB = new();
        List<string> newFamilyB = OneDegree(this.tree, personB);
        int distance = 0;
  
        while (true)
        {
            // check if the family groups overlapped in the last round
            distance += 1;
            familyA.AddRange(newFamilyA);
            familyB.AddRange(newFamilyB);
            if (familyA.Contains(personB) && familyB.Contains(personA))
                return distance;

            // Get the next set of relatives from the new search set
            List<string>nextFamilyA = new ();
            foreach(string person in newFamilyA)
            {
                List<string> current = OneDegree(this.tree, person);
                nextFamilyA.AddRange(current);

            }
            List<string> nextFamilyB = new();
            foreach(string person in newFamilyB)
            {
                List<string> current = OneDegree(this.tree, person);
                nextFamilyB.AddRange(current);
            }
            
            // set the new search list. Only include names we haven't searched yet.
            newFamilyA = (from string person in nextFamilyA
                where !familyA.Contains(person)
                select person).ToList<string>();
            
            newFamilyB = (from string person in nextFamilyB
                where !familyB.Contains(person)
                select person).ToList<string>();

            // if no new names they are not related
            if (newFamilyA.Count + newFamilyB.Count == 0)
                return -1;
        }
    }
}
