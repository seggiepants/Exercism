public enum Color { Red , Green , Ivory , Yellow , Blue }
public enum Nationality { Englishman , Spaniard , Ukrainian , Japanese , Norwegian }
public enum Pet { Dog , Snail , Fox , Horse , Zebra }
public enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
public enum Smoke { OldGold , Kools , Chesterfields , LuckyStrike , Parliaments }
public enum Hobby { Dancing, Painter, Reading, Football, Chess }

public static class ZebraPuzzle
{
    const int NUM_HOUSES = 5;
    public static Random r = new();
    //static List<Dictionary<string, Tuple<string, List<string>>>> houses = new();
    private static void BuildHouses(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        if (houses.Count == NUM_HOUSES)
            return;

        for (int i = 0; i < NUM_HOUSES; i++)
        {
            Dictionary<string, Tuple<string, List<string>>> house = new();
            // Color
            house.Add("Index", new Tuple<string, List<string>>((i + 1).ToString(), new List<string>()));
            house.Add("Color", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Color)).ToList<string>()));
            house.Add("Nationality", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Nationality)).ToList<string>()));
            house.Add("Pet", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Pet)).ToList<string>()));
            house.Add("Drink", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Drink)).ToList<string>()));
            //house.Add("Smoke", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Smoke)).ToList<string>()));
            house.Add("Hobby", new Tuple<string, List<string>>("", Enum.GetNames(typeof(Hobby)).ToList<string>()));
            houses.Add(house);
        }
    }

    private static List<Dictionary<string, Tuple<string, List<string>>>> CopyHouses(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        List<Dictionary<string, Tuple<string, List<string>>>> next = new();
        foreach (Dictionary<string, Tuple<string, List<string>>> dict in houses)
        {
            Dictionary<string, Tuple<string, List<string>>> nextDict = new();
            foreach (string key in dict.Keys)
            {
                List<string> nextList = new();
                foreach (string value in dict[key].Item2)
                    nextList.Add(value);
                nextDict.Add(key, new Tuple<string, List<string>>(dict[key].Item1, nextList));
            }
            next.Add(nextDict);
        }
        return next;
    }

    private static bool IsValueInUse(List<Dictionary<string, Tuple<string, List<string>>>> houses, string key, string value)
    {
        return (from h in houses where h[key].Item1 == value select 1).Count() > 0;
    }

    private static void RemoveOption(List<Dictionary<string, Tuple<string, List<string>>>> houses, string key, string value, string skipIndex)
    {
        var updateHouses = (
            from h in houses
            where h[key].Item2.Contains(value) && h["Index"].Item1 != skipIndex
            select h);
        foreach (var house in updateHouses)
        {
            var houseValue = house[key];
            house[key] = new Tuple<string, List<string>>(houseValue.Item1, (from string v in houseValue.Item2 where v != value select v).ToList<string>());
        }
    }
    
    private static void PrintHouses(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        foreach(var house in houses)
        {
            Console.WriteLine(String.Join(", ", (from value in house select $"[{value.Key}]=\"{value.Value.Item1}\"")));
        }
    }

    private static Tuple<bool, List<Dictionary<string, Tuple<string, List<string>>>>> Run(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        if (houses.Count != NUM_HOUSES)
            BuildHouses(houses);

        while (!IsDone(houses))
        {
            try
            {
                bool anyChange = true;
                while (anyChange)
                {
                    anyChange = RunConstraints(houses);
                    if (!CheckConstraints(houses))
                        throw new Exception("Bad constraints.");
                }

                if (!IsDone(houses))
                {
                    // Find first thing to change.
                    bool attempt = false;
                    foreach (var house in houses)
                    {
                        string[] keys = house.Keys.ToArray<string>();
                        r.Shuffle<string>(keys);

                        foreach (string key in keys)
                        {
                            //if (key == "Drink" || key == "Pet")
                            //    continue;
                            var data = house[key];
                            if (data.Item1 == "" && data.Item2.Count > 0)
                            {
                                for (int i = 0; i < data.Item2.Count; i++)
                                {
                                    if (IsValueInUse(houses, key, data.Item2[i]))
                                        continue;
                                    List<Dictionary<string, Tuple<string, List<string>>>> nextHouses = CopyHouses(houses); //new(houses);
                                    string newValue = data.Item2[i];
                                    string index = house["Index"].Item1;
                                    nextHouses[int.Parse(index) - 1][key] = new Tuple<string, List<string>>(newValue, new List<string>());
                                    RemoveOption(nextHouses, key, newValue, index);

                                    if (CheckConstraints(nextHouses))
                                    {
                                        try
                                        {
                                            attempt = true;
                                            var ret = Run(nextHouses);
                                            if (ret.Item1 && CheckConstraints(nextHouses) && IsDone(nextHouses))
                                            {
                                                return new Tuple<bool, List<Dictionary<string, Tuple<string, List<string>>>>>(true, nextHouses);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            //Console.WriteLine(ex.Message);
                                        }

                                    }
                                }
                            }
                        }
                    }
                    if (attempt)
                        throw new Exception("Nothing could be updated");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return new Tuple<bool, List<Dictionary<string, Tuple<string, List<string>>>>>(false, houses);
            }
        }
        if (!IsDone(houses) || !CheckConstraints(houses))
            return new Tuple<bool, List<Dictionary<string, Tuple<string, List<string>>>>>(false, houses);
        return new Tuple<bool, List<Dictionary<string, Tuple<string, List<string>>>>>(true, houses);
    }

    private static bool IsDone(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return (from h in houses where h["Color"].Item1 == "" || h["Nationality"].Item1 == "" || h["Pet"].Item1 == "" || h["Drink"].Item1 == "" || h["Hobby"].Item1 == "" select 1).Count() == 0;
    }

    private static bool RunConstraints(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        bool ret = false;
        ret = ret || ConstraintOnlyOne(houses);
        ret = ret || ConstraintFiveHouses(houses);
        ret = ret || ConstraintEnglishman_Red(houses);
        ret = ret || ConstraintSpaniard_Dog(houses);
        ret = ret || ConstraintGreen_Coffee(houses);
        ret = ret || ConstraintUkrainian_Tea(houses);
        ret = ret || ConstraintGreen_Ivory(houses);
        ret = ret || ConstraintSnail_Dancing(houses);
        ret = ret || ConstraintYellow_Painter(houses);
        ret = ret || ConstraintMiddle_Milk(houses);
        ret = ret || ConstraintNorwegian_First(houses);
        ret = ret || ConstraintReading_Fox(houses);
        ret = ret || ConstraintPainter_Horse(houses);
        ret = ret || ConstraintFootball_OrangeJuice(houses);
        ret = ret || ConstraintJapanese_Chess(houses);
        ret = ret || ConstraintNorwegian_Blue(houses);
        return ret;
    }

    private static bool CheckConstraints(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        try
        {
            bool ret = false;
            ret = ret || ConstraintOnlyOne(houses);
            ret = ret || ConstraintFiveHouses(houses);
            ret = ret || ConstraintEnglishman_Red(houses);
            ret = ret || ConstraintSpaniard_Dog(houses);
            ret = ret || ConstraintGreen_Coffee(houses);
            ret = ret || ConstraintUkrainian_Tea(houses);
            ret = ret || ConstraintGreen_Ivory(houses);
            ret = ret || ConstraintSnail_Dancing(houses);
            ret = ret || ConstraintYellow_Painter(houses);
            ret = ret || ConstraintMiddle_Milk(houses);
            ret = ret || ConstraintNorwegian_First(houses);
            ret = ret || ConstraintReading_Fox(houses);
            ret = ret || ConstraintPainter_Horse(houses);
            ret = ret || ConstraintFootball_OrangeJuice(houses);
            ret = ret || ConstraintJapanese_Chess(houses);
            ret = ret || ConstraintNorwegian_Blue(houses);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // 00. If there is only one available option left, use it.
    private static bool ConstraintOnlyOne(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        if (houses.Count != NUM_HOUSES)
            BuildHouses(houses);

        bool updated = false;
        foreach (var house in houses)
        {
            foreach (string key in house.Keys)
            {
                var value = house[key];
                if (value.Item1.Length == 0 && value.Item2.Count == 1)
                {
                    if (IsValueInUse(houses, key, value.Item2[0]))
                        throw new Exception($"Only remaining value {value.Item2[0]} is already in use.");
                    string newValue = value.Item2[0];
                    house[key] = new Tuple<string, List<string>>(newValue, new List<string>());
                    RemoveOption(houses, key, newValue, house["Index"].Item1);
                    updated = true;
                }
            }
        }
        return updated;
    }

    // 01. There are five houses.
    private static bool ConstraintFiveHouses(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        bool updated = false;
        if (houses.Count != NUM_HOUSES)
        {
            BuildHouses(houses);
            updated = true;
        }
        if (houses.Count != NUM_HOUSES)
            throw new Exception("There should be five houses.");
        return updated;
    }

    // 02. The Englishman lives in the red house.
    private static bool ConstraintEnglishman_Red(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Nationality", "Englishman", "Color", "Red");
    }

    // 03. The Spaniard owns the dog.
    private static bool ConstraintSpaniard_Dog(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Nationality", "Spaniard", "Pet", "Dog");
    }

    // 04. The person in the green house drinks coffee.
    private static bool ConstraintGreen_Coffee(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Color", "Green", "Drink", "Coffee");
    }

    // 05. The Ukrainian drinks tea.
    private static bool ConstraintUkrainian_Tea(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Nationality", "Ukrainian", "Drink", "Tea");
    }

    // 06. The green house is immediately to the right of the ivory house. (Ivory then Green)
    private static bool ConstraintGreen_Ivory(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        bool updated = false;
        var ivoryHouse = (from h in houses where h["Color"].Item1 == "Ivory" select h).FirstOrDefault();
        var greenHouse = (from h in houses where h["Color"].Item1 == "Green" select h).FirstOrDefault();

        if (ivoryHouse != null && greenHouse != null)
        {
            string expectedIndex = (int.Parse(ivoryHouse["Index"].Item1) + 1).ToString();
            if (greenHouse["Index"].Item1 != expectedIndex)
            {
                throw new Exception("Logical Contradiction found Green House not to left of Ivory.");
            }
        }
        else if (ivoryHouse != null && greenHouse == null)
        {
            string ivoryIndex = ivoryHouse["Index"].Item1;
            string greenIndex = (int.Parse(ivoryIndex) + 1).ToString();
            var expectedHouse = (from h in houses where h["Index"].Item1 == greenIndex select h).FirstOrDefault();
            if (expectedHouse == null)
                throw new Exception($"Ivory house at index {ivoryIndex} has no match for Green expected at {greenIndex}");

            expectedHouse["Color"] = new Tuple<string, List<string>>("Green", new List<string>());
            RemoveOption(houses, "Color", "Green", greenIndex);
            updated = true;
        }
        else if (greenHouse != null && ivoryHouse == null)
        {
            string greenIndex = greenHouse["Index"].Item1;
            string ivoryIndex = (int.Parse(greenIndex) - 1).ToString();
            var expectedHouse = (from h in houses where h["Index"].Item1 == ivoryIndex select h).FirstOrDefault();
            if (expectedHouse == null)
                throw new Exception($"Green house at index {greenIndex} has no match for Ivory expected at {ivoryIndex}");

            expectedHouse["Color"] = new Tuple<string, List<string>>("Ivory", new List<string>());
            RemoveOption(houses, "Color", "Ivory", ivoryIndex);
            updated = true;
        }
        return updated;
    }

    // 07. The snail owner likes to go dancing.
    private static bool ConstraintSnail_Dancing(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Pet", "Snail", "Hobby", "Dancing");
    }

    // 08. The person in the yellow house is a painter.
    private static bool ConstraintYellow_Painter(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Color", "Yellow", "Hobby", "Painter");
    }

    // 09. The person in the middle house drinks milk.
    private static bool ConstraintMiddle_Milk(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Index", "3", "Drink", "Milk");
    }

    // 10. The Norwegian lives in the first house.
    private static bool ConstraintNorwegian_First(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Index", "1", "Nationality", "Norwegian");
    }

    // 11. The person who enjoys reading lives in the house next to the person with the fox.
    private static bool ConstraintReading_Fox(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintNextToA_B(houses, "Hobby", "Reading", "Pet", "Fox");
    }

    // 12. The painter's house is next to the house with the horse.
    private static bool ConstraintPainter_Horse(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintNextToA_B(houses, "Hobby", "Painter", "Pet", "Horse");
    }

    // 13. The person who plays football drinks orange juice.
    private static bool ConstraintFootball_OrangeJuice(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Hobby", "Football", "Drink", "OrangeJuice");
    }

    // 14. The Japanese person plays chess.
    private static bool ConstraintJapanese_Chess(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintEqualA_B(houses, "Nationality", "Japaneses", "Hobby", "Chess");
    }

    // 15. The Norwegian lives next to the blue house.
    private static bool ConstraintNorwegian_Blue(List<Dictionary<string, Tuple<string, List<string>>>> houses)
    {
        return ConstraintNextToA_B(houses, "Nationality", "Norwegian", "Color", "Blue");
    }

    private static bool ConstraintEqualA_B(List<Dictionary<string, Tuple<string, List<string>>>> houses, string keyA, string valueA, string keyB, string valueB)
    {
        bool updated = false;
        var aHouse = (from h in houses where h[keyA].Item1 == valueA select h).FirstOrDefault();
        var bHouse = (from h in houses where h[keyB].Item1 == valueB select h).FirstOrDefault();

        if (aHouse != null && bHouse != null)
        {
            if (aHouse["Index"].Item1 != bHouse["Index"].Item1)
            {
                throw new Exception($"Logical Contradiction found {valueA} and {valueB} are not the same house.");
            }
        }
        else if (aHouse != null && bHouse == null)
        {
            if (aHouse[keyB].Item1 != "")
                throw new Exception($"Can't update {keyB} = {valueB}, it is already {aHouse[keyB].Item1}.");
            aHouse[keyB] = new Tuple<string, List<string>>(valueB, new List<string>());
            RemoveOption(houses, keyB, valueB, aHouse["Index"].Item1);
            updated = true;
        }
        else if (bHouse != null && aHouse == null)
        {
            if (bHouse[keyA].Item1 != "")
                throw new Exception($"Can't update {keyA} = {valueA}, it is already {bHouse[keyA].Item1}.");
            bHouse[keyA] = new Tuple<string, List<string>>(valueA, new List<string>());
            RemoveOption(houses, keyA, valueA, bHouse["Index"].Item1);
            updated = true;
        }
        return updated;
    }

    private static bool ConstraintNextToA_B(List<Dictionary<string, Tuple<string, List<string>>>> houses, string keyA, string valueA, string keyB, string valueB)
    {
        bool updated = false;
        var aHouse = (from h in houses where h[keyA].Item1 == valueA select h).FirstOrDefault();
        var bHouse = (from h in houses where h[keyB].Item1 == valueB select h).FirstOrDefault();

        if (aHouse != null && bHouse != null)
        {
            int aIndex = int.Parse(aHouse["Index"].Item1);
            int bIndex = int.Parse(bHouse["Index"].Item1);
            if (aIndex - 1 != bIndex && aIndex + 1 != bIndex)
            {
                throw new Exception($"Logical Contradiction found {valueA} and {valueB} are not neighboring houses.");
            }
        }
        else if (aHouse != null && bHouse == null)
        {
            int aIndex = int.Parse(aHouse["Index"].Item1);

            string leftIndex = (aIndex - 1).ToString();
            string rightIndex = (aIndex + 1).ToString();
            var leftHouse = (from h in houses where h["Index"].Item1 == leftIndex select h).FirstOrDefault();
            var rightHouse = (from h in houses where h["Index"].Item1 == rightIndex select h).FirstOrDefault();

            if ((rightHouse == null || rightHouse[keyB].Item1 != "") && (leftHouse == null || leftHouse[keyB].Item1 != ""))
            {
                throw new Exception($"No house found to update {keyB} = \"{valueB}\"");
            }
            else if (rightHouse == null || rightHouse[keyB].Item1 != "")
            {
                // Right is full/non-existent so left house should be B.
                if (leftHouse != null && leftHouse[keyB].Item1 == "")
                {
                    leftHouse[keyB] = new Tuple<string, List<string>>(valueB, new List<string>());
                    RemoveOption(houses, keyB, valueB, leftHouse["Index"].Item1);
                    updated = true;
                }
                else if (leftHouse == null)
                    throw new Exception($"Attempting to update {keyB}={valueB} but the house on the left was not found.");
                else
                    throw new Exception($"Attempting to update {keyB}={valueB} but it is already {leftHouse[keyB].Item1}");
            }
            else if (leftHouse == null || leftHouse[keyB].Item1 != "")
            {
                // Left is full/non-existent so right house should be horse.
                if (rightHouse != null && rightHouse[keyB].Item1 == "")
                {
                    rightHouse[keyB] = new Tuple<string, List<string>>(valueB, new List<string>());
                    RemoveOption(houses, keyB, valueB, rightHouse["Index"].Item1);
                    updated = true;
                }
                else if (rightHouse == null)
                    throw new Exception($"Attempting to update {keyB}={valueB} but the house on the right was not found.");
                else
                    throw new Exception($"Attempting to update {keyB}={valueB} but it is already {rightHouse[keyB].Item1}");

            }
        }
        else if (bHouse != null && aHouse == null)
        {
            int bIndex = int.Parse(bHouse["Index"].Item1);

            string leftIndex = (bIndex - 1).ToString();
            string rightIndex = (bIndex + 1).ToString();
            var leftHouse = (from h in houses where h["Index"].Item1 == leftIndex select h).FirstOrDefault();
            var rightHouse = (from h in houses where h["Index"].Item1 == rightIndex select h).FirstOrDefault();

            if ((rightHouse == null || rightHouse[keyA].Item1 != "") && (leftHouse == null || leftHouse[keyA].Item1 != ""))
            {
                throw new Exception($"No house found to set {keyA} = \"{valueA}\"");
            }
            else if (rightHouse == null || rightHouse[keyA].Item1 != "")
            {
                // Right is full/non-existent so left house should be A.
                if (leftHouse != null && leftHouse[keyA].Item1 == "")
                {
                    leftHouse[keyA] = new Tuple<string, List<string>>(valueA, new List<string>());
                    RemoveOption(houses, keyA, valueA, leftHouse["Index"].Item1);
                    updated = true;
                }
                else if (leftHouse == null)
                    throw new Exception($"Attempting to update {keyA}={valueA} but the house was not found.");
                else
                    throw new Exception($"Attempting to update {keyA}={valueA} but it is already {leftHouse[keyA].Item1}");

            }
            else if (leftHouse == null || leftHouse[keyA].Item1 != "")
            {
                // Left is full/non-existent so right house should be fox.
                if (rightHouse != null && rightHouse[keyA].Item1 == "")
                {
                    rightHouse[keyA] = new Tuple<string, List<string>>(valueA, new List<string>());
                    RemoveOption(houses, keyA, valueA, rightHouse["Index"].Item1);
                    updated = true;
                }
                else if (rightHouse == null)
                    throw new Exception($"Attempting to update {keyA}={valueA} but the house was not found.");
                else
                    throw new Exception($"Attempting to update {keyA}={valueA} but it is already {rightHouse[keyA].Item1}");

            }
        }
        return updated;
    }

    public static Nationality DrinksWater()
    {
        List<Dictionary<string, Tuple<string, List<string>>>> houses = new();
        var ret = Run(houses);
        if (!ret.Item1)
            throw new Exception("Failed, could not complete puzzle.");
        else
            houses = ret.Item2;
        PrintHouses(houses);
        var waterHouse = (from h in houses where h["Drink"].Item1 == "Water" select h).FirstOrDefault();
        if (waterHouse == null)
            throw new Exception("Failed, water house not found.");
        return GetNationality(waterHouse["Nationality"].Item1);
    }

    public static Nationality OwnsZebra()
    {
        List<Dictionary<string, Tuple<string, List<string>>>> houses = new();
        var ret = Run(houses);
        if (!ret.Item1)
            throw new Exception("Failed, could not complete puzzle.");
        else
            houses = ret.Item2;
        PrintHouses(houses);
        var zebraHouse = (from h in houses where h["Pet"].Item1 == "Zebra" select h).FirstOrDefault();
        if (zebraHouse == null)
            throw new Exception("Failed, zebra house not found.");
        return GetNationality(zebraHouse["Nationality"].Item1);
    }

    public static Nationality GetNationality(string value)
    {
        Dictionary<string, Nationality> lookup = new()
        {
            ["Englishman"] = Nationality.Englishman,
            ["Spaniard"] = Nationality.Spaniard,
            ["Ukrainian"] = Nationality.Ukrainian,
            ["Japanese"] = Nationality.Japanese,
            ["Norwegian"] = Nationality.Norwegian,
        };
        return lookup[value];
    }
}