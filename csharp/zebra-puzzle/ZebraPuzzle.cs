using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

public enum Field { Color, Nationality, Pet, Drink, Hobby }

public enum Color { Red , Green , Ivory , Yellow , Blue }
public enum Nationality { Englishman , Spaniard , Ukrainian , Japanese , Norwegian }
public enum Pet { Dog , Snail , Fox , Horse , Zebra }
public enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
public enum Smoke { OldGold , Kools , Chesterfields , LuckyStrike , Parliaments }
public enum Hobby { Dancing, Painter, Reading, Football, Chess }

public struct House
{
    public Color? color = null;
    public Nationality? nationality = null;
    public Pet? pet = null;
    public Drink? drink = null;
    public Hobby? hobby = null;

    public House()
    {
        this.color = null;
        this.nationality = null;
        this.pet = null;
        this.drink = null;
        this.hobby = null;
    }

    public int? this[Field index]
    {
        get
        {
            switch(index)
            {
            case Field.Color:
                return (int?)this.color;
            case Field.Nationality:
                return (int?)this.nationality;
            case Field.Pet:
                return (int?)this.pet;
            case Field.Hobby:
                return (int?)this.hobby;
            case Field.Drink:
                return (int?)this.drink;            
            }
            return null;
        }
        set
        {
            switch(index)
            {
            case Field.Color:
                this.color = (Color?)value;
                break;
            case Field.Nationality:
                this.nationality = (Nationality?)value;
                break;
            case Field.Pet:
                this.pet = (Pet?)value;
                break;
            case Field.Hobby:
                this.hobby = (Hobby?)value;
                break;
            case Field.Drink:
                this.drink = (Drink?)value;
                break;
            }
        }
    }

    public bool isFull()
    {
        return this.color != null && this.nationality != null && this.pet != null && this.drink != null && this.hobby != null;
    }

    public override string ToString()
    {
        return $"Color: {this.color}, Nationality: {this.nationality}, Drink: {this.drink}, Hobby: {this.hobby}, Pet: {this.pet}";
    }

};


public static class ZebraPuzzle
{
    static int[] colors = [(int)Color.Blue, (int)Color.Green, (int)Color.Ivory, (int)Color.Red, (int)Color.Yellow];
    static int[] nationalities = [(int)Nationality.Englishman, (int)Nationality.Japanese, (int)Nationality.Norwegian, (int)Nationality.Spaniard, (int)Nationality.Ukrainian];
    static int[] drinks = [(int)Drink.Coffee, (int)Drink.Milk, (int)Drink.OrangeJuice, (int)Drink.Tea, (int)Drink.Water];
    static int[] hobbies = [(int)Hobby.Chess, (int)Hobby.Dancing, (int)Hobby.Football, (int)Hobby.Painter, (int)Hobby.Reading];
    static int[] pets = [(int)Pet.Dog, (int)Pet.Fox, (int)Pet.Horse, (int)Pet.Snail, (int)Pet.Zebra];
    static Tuple<Field, int[]>[] fieldValues = [
        new Tuple<Field, int[]>(Field.Color, colors)
        , new Tuple<Field, int[]>(Field.Nationality, nationalities)
        , new Tuple<Field, int[]>(Field.Drink, drinks)
        , new Tuple<Field, int[]>(Field.Pet, pets)
        , new Tuple<Field, int[]>(Field.Hobby, hobbies)];
    const int NUM_HOUSES = 5;
    public static House[] data = { new House(), new House(), new House(), new House(), new House()};
    public static bool isFull()
    {
        return (from house in data
                where house.isFull()
            select 1).Sum() == data.Length;
    }

    public static int find(Field key, int? value)
    {
        if (value == null) return -1;

        return (from house in data.Index()
            where house.Item[key] == value
            select house.Index).FirstOrDefault<int>(-1);
    }

    public static string PrintHouses()
    {
        return String.Join("\n",(from house in data.Index()
        select (house.Index + 1).ToString() + ": " + house.Item.ToString()));
    }

    public static void undo(Stack<Tuple<int, Field>> undoStack, int stackPointer)
    {
        while (undoStack.Count >= stackPointer)
        {
            (int house, Field field) = undoStack.Pop();
            data[house][field] = null;
        }
    }

    // Populate if house[key] === value then house[otherKey] = otherValue
    static bool populatePair(Stack<Tuple<int, Field>> undoStack, Field key1, int? value1, Field key2, int? value2)
    {
        int index = find(key1, value1);
        if (index >= 0)
        {
            if (data[index][key2] == null)
            {
                undoStack.Push(new Tuple<int, Field>(index, key2));
                data[index][key2] = value2;
                return true;
            }
            else if (data[index][key2] != value2)
                return false;
        }
    
        index = find(key2, value2);
        if (index >= 0)
        {
            if (data[index][key1] == null)
            {
                undoStack.Push(new Tuple<int, Field>(index, key1));
                data[index][key1] = value1;
                return true;
            }
            else if (data[index][key1] != value1)
                return false;
        }
        return true;    
    }

    // Populate if house[key] === value then houseToRight[otherKey] = otherValue
    static bool populatePairRight(Stack<Tuple<int, Field>>undoStack, Field key1, int? value1, Field key2, int? value2)
    {
        int index1 = find(key1, value1);
        int index2 = find(key2, value2);
    
        // if you have both they had better match
        if (index1 >= 0 && index2 >=0 && (index1 + 1) != index2)
            return false;

        if (index1 >= 0 && index1 <= data.Length - 2)
        {
            if (data[index1 + 1][key2] == null)
            {
                undoStack.Push(new Tuple<int, Field>(index1 + 1, key2));
                data[index1 + 1][key2] = value2 ;
                return true;                
            }
            else if (data[index1 + 1][key2] != value2)
                return false;
        }

        if (index2 >= 1 && index2 < data.Length)
        {
            if (data[index2 - 1][key1] == null)
            {
                undoStack.Push(new Tuple<int, Field>(index2 - 1, key1));
                data[index2 - 1][key1] = value1;
                return true;
            }
            else if (data[index2 - 1][key1] != value1)
                return false;
        }
        return true;
    }

    // These searches are lengthy to write so I broke this one in two and
    // just call it with both possible combinations (should be redundant, really).
    static bool populatePairNeighbor_Step(Stack<Tuple<int, Field>>undoStack, Field key1, int? value1, Field key2, int? value2)
    {
        int index = find(key1, value1);
        if (index >= 0)
        {
            List<Tuple<int, int?>>sides = new();
            if (index - 1 >= 0)
                sides.Add(new Tuple<int, int?>(index -1, data[index - 1][key2]));
            
            if (index + 1 < data.Length)
                sides.Add(new Tuple<int, int?>(index + 1, data[index + 1][key2]));

            if (sides.Count == 1 && sides[0].Item2 != value2 && sides[0].Item2 != null)
                return false;
            else if (sides.Count == 1 && sides[0].Item2 == value2)
                return true;
            else if (sides.Count == 1 && sides[0].Item2 == null)
            {
                // only one and it is empty, fill it
                undoStack.Push(new Tuple<int, Field>(sides[0].Item1, key2));
                data[sides[0].Item1][key2] = value2;
                return true;
            }      
            else if (sides.Count == 2 && sides[0].Item2 != null && sides[1].Item2 != null && sides[0].Item2 != value2 && sides[1].Item2 != value2)
            {
                // both sides not target
                return false;
            }
            else if (sides.Count == 2 && (sides[0].Item2 == value2 || sides[1].Item2 == value2))
            {
                // one side is target
                return true;
            }
            else if (sides.Count == 2 && sides[0].Item2 == null && sides[1].Item2 != null && sides[1].Item2 != value2)
            {
                // left empty, right not empty not target
                undoStack.Push(new Tuple<int, Field>(sides[0].Item1, key2));
                data[sides[0].Item1][key2] = value2;
            }
            else if (sides.Count == 2 && sides[0].Item2 != null && sides[1].Item2 == null && sides[0].Item2 != value2)
            {
                // right empty, let not empty not target
                undoStack.Push(new Tuple<int, Field>(sides[1].Item1, key2));
                data[sides[1].Item1][key2] = value2;
            }      
        }
        return true;
    }

  // Populate if house[key] === value then houseToLeftOrRight[otherKey] = otherValue
    static bool populatePairNeighbor(Stack<Tuple<int, Field>>undoStack, Field key1, int? value1, Field key2, int? value2)
    {
        int index1 = find(key1, value1);
        int index2 = find(key2, value2);

        // Both populated but more than one spot apart is an error
        if (index1 >= 0 && index2 >= 0)
        {
            if (Math.Abs(index2 - index1) > 1)
                return false;

            if ((data[index1][key1] == value1 && data[index2][key2] == value2) ||
                (data[index2][key1] == value1 && data[index1][key2] == value2))
                return true ;

        }

        if (!populatePairNeighbor_Step(undoStack, key1, value1, key2, value2))
            return false;

        if (!populatePairNeighbor_Step(undoStack, key2, value2, key1, value1))
            return false;

        return true;
    }

    // Run through all the rules (except the givens -- can be filled in without futher data)
    // if a rule fails the tests return false, if no errors found return true.
    static bool populate(Stack<Tuple<int, Field>>undoStack)
    {
        // 1 There are five houses
        if (data.Length != 5)
            return false;    
        // 2 Englishman lives in the red house
        if (!populatePair(undoStack, Field.Nationality, (int?)Nationality.Englishman, Field.Color, (int?)Color.Red))
            return false;
        // 3 Spaniard owns a dog
        if (!populatePair(undoStack, Field.Nationality, (int?)Nationality.Spaniard, Field.Pet, (int?)Pet.Dog))
            return false;
        // 4 Green house drinks coffee
        if (!populatePair(undoStack, Field.Color, (int?) Color.Green, Field.Drink, (int?)Drink.Coffee))
            return false;
        // 5 Ukranian drinks tea
        if (!populatePair(undoStack, Field.Nationality, (int?) Nationality.Ukrainian, Field.Drink, (int?)Drink.Tea))      
            return false;
        // 6 - Green house right of Ivory house
        //if (!this.populatePairRight(undoStack, 'color', 'green', 'color', 'ivory'))
        if (!populatePairRight(undoStack, Field.Color, (int?) Color.Ivory, Field.Color, (int?) Color.Green))
            return false;
        // 7 Snail owner goes dancing
        if (!populatePair(undoStack, Field.Pet, (int?) Pet.Snail, Field.Hobby, (int?)Hobby.Dancing))
            return false;
        // 8 Yellow house likes painting
        if (!populatePair(undoStack, Field.Color, (int?) Color.Yellow, Field.Hobby, (int?) Hobby.Painter))
            return false;
        // 9 Middle house drinks milk is a given
        // 10 1st house is Norwegian is a given
        // 11 - Reading next to fox
        if (!populatePairNeighbor(undoStack, Field.Hobby, (int?)Hobby.Reading, Field.Pet, (int?) Pet.Fox))
            return false;
        // 12 - Painter next to horse
        if (!populatePairNeighbor(undoStack, Field.Hobby, (int?)Hobby.Painter, Field.Pet, (int?) Pet.Horse))
            return false;
        // 13 Football drinks orange juice
        if (!populatePair(undoStack, Field.Hobby, (int?)Hobby.Football, Field.Drink, (int?) Drink.OrangeJuice))
            return false;
        // 14 Japanese plays chess
        if (!populatePair(undoStack, Field.Nationality, (int?)Nationality.Japanese, Field.Hobby, (int?)Hobby.Chess))
            return false;
        // 15 is also a given Norwegian (1st house) is next to blue house where 2 is only neighbor 
        
        return true;
    }

    // Populate the puzzle
    static bool fillPuzzle(Stack<Tuple<int, Field>>undoStack)
    {
        for(int house = 0; house < data.Length; house++)
        {
            foreach(Tuple<Field, int[]> pair in fieldValues)
            {
                if (data[house][pair.Item1] == null)
                {
                    Field key = pair.Item1;
                    int[] values = pair.Item2;
                    int?[] used = (from currentHouse in data
                                where currentHouse[key] != null
                                select (int?)currentHouse[key]).ToArray<int?>();
                    int[] free = (from value in values
                                where !used.Contains(value)
                                select value).ToArray<int>();
                    if (free.Length > 0)
                    {
                        foreach(int? attempt in free)
                        {
                            data[house][key] = attempt;
                            undoStack.Push(new Tuple<int, Field>(house, key));
                            int stackPointer = undoStack.Count;
                            bool success = populate(undoStack);
                            if (!success) 
                                undo(undoStack, stackPointer);
                            else 
                            {
                                success = fillPuzzle(undoStack);
                                if (!success)
                                    undo(undoStack, stackPointer);
                                else
                                    break;
                            }
                        }
                    }
                }        
            }
            if (data[house].color == null || 
                data[house].nationality == null || 
                data[house].drink == null ||
                data[house].hobby == null ||
                data[house].pet == null)
                return false;

            if (isFull())
                return true;
        }
        return populate(undoStack) && isFull();
    }

    public static Nationality DrinksWater()
    {
        if (!isFull())
        {
            // populate the givens
            data[0].nationality = Nationality.Norwegian; // The Norwegian lives in the first house.
            data[1].color = Color.Blue; // The Norwegian lives next to the blue house
            data[2].drink = Drink.Milk; // The middle house drinks milk

            fillPuzzle(new Stack<Tuple<int, Field>>());
        }

        Nationality? ret = (from house in data where house.drink == Drink.Water select house.nationality).FirstOrDefault((Nationality?)null);
        if (ret == null)
            throw new Exception("Failed, water house not found.");
        return (Nationality)ret;
    }

    public static Nationality OwnsZebra()
    {
        if (!isFull())
        {
            // populate the givens
            data[0].nationality = Nationality.Norwegian; // The Norwegian lives in the first house.
            data[1].color = Color.Blue; // The Norwegian lives next to the blue house
            data[2].drink = Drink.Milk; // The middle house drinks milk

            fillPuzzle(new Stack<Tuple<int, Field>>());
        }

        Nationality? ret = (from house in data where house.pet == Pet.Zebra select house.nationality).FirstOrDefault((Nationality?)null);
        if (ret == null)
            throw new Exception("Failed, Zebra house not found.");

        return (Nationality) ret;
    }
}
