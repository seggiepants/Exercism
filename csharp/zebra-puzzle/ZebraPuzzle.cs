
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Cryptography;

using Xunit.Sdk;

public enum Color { Red , Green , Ivory , Yellow , Blue }
public enum Nationality { Englishman , Spaniard , Ukrainian , Japanese , Norwegian }
public enum Pet { Dog , Snails , Fox , Horse , Zebra }
public enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
public enum Smoke { OldGold, Kools, Chesterfields, LuckyStrike, Parliaments }
public enum Hobby { Dancing, Painter, Football, Chess, Reading }

public enum Property { Color, Nationality, Pet, Drink, Smoke, Hobby };

public class House
{
    public Color? color = null;
    public Nationality? nationality = null;
    public Pet? pet = null;
    public Drink? drink = null;
    public Smoke? smoke = null;
    public Hobby? hobby = null;

    public House()
    {
        Init();
    }
    public void Init()
    {
        color = null;
        nationality = null;
        pet = null;
        drink = null;
        smoke = null;
        hobby = null;
    }

    public bool FilledIn()
    {
        return color != null &&
            nationality != null &&
            pet != null &&
            drink != null &&
            smoke != null &&
            hobby != null;
    }
    public Property[] GetEmptyProperties()
    {
        List<Property> values = new();
        if (color == null) values.Add(Property.Color);
        if (nationality == null) values.Add(Property.Nationality);
        if (pet == null) values.Add(Property.Pet);
        if (drink == null) values.Add(Property.Drink);
        if (smoke == null) values.Add(Property.Smoke);
        if (hobby == null) values.Add(Property.Hobby);

        return values.ToArray<Property>();
    }
};

public static class ZebraPuzzle
{
    const int NUM_HOUSES = 5;
    public static Random r = new();    
    public static House[] houses = new House[NUM_HOUSES];
    public static bool hasInit = false;
    public static Color[] AvailableColors(House[] houses)
    {
        Color[]? all = Color.GetValues<Color>();
        if (all == null)
            return [];
        Color[]? taken = (from house in houses where house.color != null select (Color)house.color).ToArray<Color>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }
    public static Nationality[] AvailableNationalities(House[] houses)
    {
        Nationality[]? all = Nationality.GetValues<Nationality>();
        if (all == null)
            return [];
        Nationality[]? taken = (from house in houses where house.nationality != null select (Nationality)house.nationality).ToArray<Nationality>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }
    public static Pet[] AvailablePets(House[] houses)
    {
        Pet[]? all = Pet.GetValues<Pet>();
        if (all == null)
            return [];
        Pet[]? taken = (from house in houses where house.pet != null select (Pet)house.pet).ToArray<Pet>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }
    public static Drink[] AvailableDrinks(House[] houses)
    {
        Drink[]? all = Drink.GetValues<Drink>();
        if (all == null)
            return [];
        Drink[]? taken = (from house in houses where house.drink != null select (Drink)house.drink).ToArray<Drink>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }
    public static Smoke[] AvailableSmokes(House[] houses)
    {
        Smoke[]? all = Smoke.GetValues<Smoke>();
        if (all == null)
            return [];
        Smoke[]? taken = (from house in houses where house.smoke != null select (Smoke)house.smoke).ToArray<Smoke>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }

    public static Hobby[] AvailableHobbies(House[] houses)
    {
        Hobby[]? all = Hobby.GetValues<Hobby>();
        if (all == null)
            return [];
        Hobby[]? taken = (from house in houses where house.hobby != null select (Hobby)house.hobby).ToArray<Hobby>();
        if (taken == null)
            return [];
        return all.Except(taken).ToArray();
    }

    public static bool IsOK(House[] houses)
    {
        // False if multiples.
        bool colorMultiples = houses.GroupBy(n => n.color).Any(m => m.Count() > 1 && m.Key != null);
        if (colorMultiples)
            return false;

        bool nationalityMultiples = houses.GroupBy(n => n.nationality).Any(m => m.Count() > 1 && m.Key != null);
        if (nationalityMultiples)
            return false;
        
        bool petMultiples = houses.GroupBy(n => n.pet).Any(m => m.Count() > 1 && m.Key != null);
        if (petMultiples)
            return false;
        
        bool drinkMultiples = houses.GroupBy(n => n.drink).Any(m => m.Count() > 1 && m.Key != null);
        if (drinkMultiples)
            return false;

        bool smokeMultiples = houses.GroupBy(n => n.smoke).Any(m => m.Count() > 1 && m.Key != null);
        if (smokeMultiples)
            return false;

        bool hobbyMultiples = houses.GroupBy(n => n.hobby).Any(m => m.Count() > 1 && m.Key != null);
        if (hobbyMultiples)
            return false;

        int missing = 0;

        // - There are five houses. -- Given

        // - The Englishman lives in the red house.
        Color? red = (from house in houses where house.nationality == Nationality.Englishman select house.color).FirstOrDefault<Color?>();
        if (red == null)
            missing++;
        else if (red != Color.Red)
            return false;
        
        Nationality? englishman = (from house in houses where house.color == Color.Red select house.nationality).FirstOrDefault<Nationality?>();
        if (englishman == null)
            missing++;
        else if (englishman != Nationality.Englishman)
            return false;

        // - The Spaniard owns the dog.
        Pet? dog = (from house in houses where house.nationality == Nationality.Spaniard select house.pet).FirstOrDefault<Pet?>();
        if (dog == null)
            missing++;
        else if (dog != Pet.Dog)
            return false;

        // - The person in the green house drinks coffee.
        Drink? coffee = (from house in houses where house.color == Color.Green select house.drink).FirstOrDefault<Drink?>();
        if (coffee == null)
            missing++;
        else if (coffee != Drink.Coffee)
            return false;

        // - The Ukrainian drinks tea.
        Drink? tea = (from house in houses where house.nationality == Nationality.Ukrainian select house.drink).FirstOrDefault<Drink?>();
        if (tea == null)
            missing++;
        else if (tea != Drink.Tea)
            return false;

        // - The green house is immediately to the right of the ivory house.
        var greenList = (from pair in Enumerable.Index<House>(houses)
                         where pair.Item.color == Color.Green
                         select pair);
        if (greenList == null || greenList.Count() == 0)
            missing++;
        else
        {
            int greenIndex = greenList.First().Index;
            bool leftHouse = greenIndex - 1 >= 0 ? houses[greenIndex - 1].color == Color.Ivory : false;
            bool rightHouse = greenIndex + 1 < houses.Length ? houses[greenIndex + 1].color == Color.Ivory : false;
            if (!leftHouse && !rightHouse)
                return false;
        }

        // - The snail owner likes to go dancing.
        Hobby? dancing = (from house in houses where house.pet == Pet.Snails select house.hobby).FirstOrDefault<Hobby?>();
        if (dancing == null)
            missing++;
        else if (dancing != Hobby.Dancing)
            return false;

        // - The person in the yellow house is a painter.
        Hobby? painter = (from house in houses where house.color == Color.Yellow select house.hobby).FirstOrDefault<Hobby?>();
        if (painter == null)
            missing++;
        else if (painter != Hobby.Painter)
            return false;

        // - The person in the middle house drinks milk.
        Drink? milk = houses[2].drink;
        if (milk == null)
            missing++;
        else if (milk != Drink.Milk)
            return false;

        // - The Norwegian lives in the first house.
        Nationality? norwegian = houses[0].nationality;
        if (norwegian == null)
            missing++;
        else if (norwegian != Nationality.Norwegian)
            return false;

        // - The person who enjoys reading lives in the house next to the person with the fox.
        var readers = (from pair in Enumerable.Index(houses) where pair.Item.hobby == Hobby.Reading select pair);
        if (readers == null || readers.Count() == 0)
            missing++;
        else
        {
            int readerIndex = readers.First().Index;
            bool leftHouse = readerIndex - 1 >= 0 ? houses[readerIndex - 1].pet == Pet.Fox : false;
            bool rightHouse = readerIndex + 1 < houses.Length ? houses[readerIndex + 1].pet == Pet.Fox : false;
            if (!leftHouse && !rightHouse)
                return false;
        }

        // - The painter's house is next to the house with the horse.
        var painters = (from pair in Enumerable.Index(houses) where pair.Item.hobby == Hobby.Painter select pair);
        if (painters == null || painters.Count() == 0)
            missing++;
        else
        {
            int painterIndex = painters.First().Index;
            bool leftHouse = painterIndex - 1 >= 0 ? houses[painterIndex - 1].pet == Pet.Horse : false;
            bool rightHouse = painterIndex + 1 < houses.Length ? houses[painterIndex + 1].pet == Pet.Horse : false;
            if (!leftHouse && !rightHouse)
                return false;
        }

        // - The person who plays football drinks orange juice.
        Drink? orangeJuice = (from house in houses where house.hobby == Hobby.Football select house.drink).FirstOrDefault<Drink?>();
        if (orangeJuice == null)
            missing++;
        else if (orangeJuice != Drink.OrangeJuice)
            return false;

        // - The Japanese person plays chess.
        Hobby? chess = (from house in houses where house.nationality == Nationality.Japanese select house.hobby).FirstOrDefault<Hobby?>();
        if (chess == null)
            missing++;
        else if (chess != Hobby.Chess)
            return false;

        // - The Norwegian lives next to the blue house.
        var norwegians = (from pair in Enumerable.Index(houses) where pair.Item.nationality == Nationality.Norwegian select pair);
        if (norwegians == null || norwegians.Count() == 0)
            missing++;
        else
        {
            int norwegianIndex = norwegians.First().Index;
            bool leftHouse = norwegianIndex - 1 >= 0 ? houses[norwegianIndex - 1].color == Color.Blue : false;
            bool rightHouse = norwegianIndex + 1 < houses.Length ? houses[norwegianIndex + 1].color == Color.Blue : false;
            if (!leftHouse && !rightHouse)
                return false;
        }
        return true;
    }

    public static void FillConstraints(House[] houses, Stack<Tuple<House, Property>>changes)
    {
        // - There are five houses.
        // given

        // - The Englishman lives in the red house.
        House? englishman = (from house in houses where house.nationality == Nationality.Englishman select house).FirstOrDefault<House?>();
        if (englishman != null && englishman.color == null)
        {
            englishman.color = Color.Red;
            changes.Push(new Tuple<House, Property>(englishman, Property.Color));
        }

        // - The Spaniard owns the dog.
            House? spaniard = (from house in houses where house.nationality == Nationality.Spaniard select house).FirstOrDefault<House?>();
        if (spaniard != null && spaniard.pet == null)
        {
            spaniard.pet = Pet.Dog;
            changes.Push(new Tuple<House, Property>(spaniard, Property.Pet));
        }

        // - The person in the green house drinks coffee.
            House? green = (from house in houses where house.color == Color.Green select house).FirstOrDefault<House?>();
        if (green != null && green.drink == null)
        {
            green.drink = Drink.Coffee;
            changes.Push(new Tuple<House, Property>(green, Property.Drink));
        }

        // - The Ukrainian drinks tea.
            House? ukranian = (from house in houses where house.nationality == Nationality.Ukrainian select house).FirstOrDefault<House?>();
        if (ukranian != null && ukranian.drink == null)
        {
            ukranian.drink = Drink.Tea;
            changes.Push(new Tuple<House, Property>(ukranian, Property.Drink));
        }

        // - The green house is immediately to the right of the ivory house.
        // Can't say left or right

        // - The snail owner likes to go dancing.
        House? snail = (from house in houses where house.pet == Pet.Snails select house).FirstOrDefault<House?>();
        if (snail != null && snail.hobby == null)
        {
            snail.hobby = Hobby.Dancing;
            changes.Push(new Tuple<House, Property>(snail, Property.Hobby));
        }

        // - The person in the yellow house is a painter.
        House? yellow = (from house in houses where house.color == Color.Yellow select house).FirstOrDefault<House?>();
        if (yellow != null && yellow.hobby == null)
        {
            yellow.hobby = Hobby.Painter;
            changes.Push(new Tuple<House, Property>(yellow, Property.Hobby));
        }

        // - The person in the middle house drinks milk.
        if (houses[2].drink == null)
        {
            houses[2].drink = Drink.Milk;
            changes.Push(new Tuple<House, Property>(houses[2], Property.Drink));
        }

        // - The Norwegian lives in the first house.
        if (houses[0].nationality == null)
        {
            houses[0].nationality = Nationality.Norwegian;
            changes.Push(new Tuple<House, Property>(houses[0], Property.Nationality));
        }
            
        // - The person who enjoys reading lives in the house next to the person with the fox.
        // Can't really say left or right.

        // - The painter's house is next to the house with the horse.
        // Can't really say left or right.

        // - The person who plays football drinks orange juice.
        House? football = (from house in houses where house.hobby == Hobby.Football select house).FirstOrDefault<House?>();
        if (football != null && football.drink == null)
        {
            football.drink = Drink.OrangeJuice;
            changes.Push(new Tuple<House, Property>(football, Property.Drink));
        }

        // - The Japanese person plays chess.
        House? japanese = (from house in houses where house.nationality == Nationality.Japanese select house).FirstOrDefault<House?>();
        if (japanese != null && japanese.hobby == null)
        {
            japanese.hobby = Hobby.Chess;
            changes.Push(new Tuple<House, Property>(japanese, Property.Hobby));
        }

        // - The Norwegian lives next to the blue house.
        // Can't say left or right
    }

    public static bool Fill(House[] houses)
    {
        Stack<Tuple<House, Property>> changes = new();
        bool success = false;
        while (!success)
        {
            FillConstraints(houses, changes);
            House[] availableHouses = (from pair in Enumerable.Index(houses) where pair.Item.FilledIn() == false select pair.Item).ToArray<House>();
            if (availableHouses.Count() == 0)
                break;

            House house = RandomElement<House>(availableHouses);
            Property[] properties = house.GetEmptyProperties();
            Property property = RandomElement<Property>(properties);
            changes.Push(new Tuple<House, Property>(house, property));
            switch (property)
            {
                case Property.Color:
                    Color color = RandomElement<Color>(AvailableColors(houses));
                    house.color = color;                    
                    break;
                case Property.Drink:
                    Drink drink = RandomElement<Drink>(AvailableDrinks(houses));
                    house.drink = drink;
                    break;
                case Property.Hobby:
                    Hobby hobby = RandomElement<Hobby>(AvailableHobbies(houses));
                    house.hobby = hobby;
                    break;
                case Property.Nationality:
                    Nationality nationality = RandomElement<Nationality>(AvailableNationalities(houses));
                    house.nationality = nationality;
                    break;
                case Property.Pet:
                    Pet pet = RandomElement<Pet>(AvailablePets(houses));
                    house.pet = pet;
                    break;
                case Property.Smoke:
                    Smoke smoke = RandomElement<Smoke>(AvailableSmokes(houses));
                    house.smoke = smoke;
                    break;
            }            
            while (!IsOK(houses) && changes.Count() > 0)
            {
                bool wasFull = (from h in houses where !h.FilledIn() select 1).Count() == 0;
                while (changes.Count() > 0)
                {
                    Tuple<House, Property> top = changes.Pop();
                    switch (top.Item2)
                    {
                        case Property.Color:
                            top.Item1.color = null;
                            break;
                        case Property.Nationality:
                            top.Item1.nationality = null;
                            break;
                        case Property.Drink:
                            top.Item1.drink = null;
                            break;
                        case Property.Hobby:
                            top.Item1.hobby = null;
                            break;
                        case Property.Pet:
                            top.Item1.pet = null;
                            break;
                        case Property.Smoke:
                            top.Item1.smoke = null;
                            break;
                    }
                    if (!wasFull)
                        break;
                }
            }
        }
        return IsOK(houses);
    }

    public static T RandomElement<T>(T[] values)
    {
        return values[r.NextInt64(values.Count())];
    }

    public static void Init()
    {
        for (int i = 0; i < houses.Count(); i++)
            houses[i] = new House();

        // Seed data
        houses[0].nationality = Nationality.Norwegian;
        houses[2].drink = Drink.Milk;
        houses[1].color = Color.Blue;
        bool done = false;
        while (!done)
        {
            if (!Fill(houses))
                Console.WriteLine("FAILURE");
            done = (from pair in Enumerable.Index(houses) where pair.Item.FilledIn() == false select pair.Item).Count() == 0;
        }
        Console.WriteLine("Done!");
    }
    public static Nationality DrinksWater()
    {
        Init();
        House? water = (from house in houses where house.drink == Drink.Water select house).FirstOrDefault<House>();
        if (water != null && water.nationality != null)
            return (Nationality)water.nationality;

        throw new InvalidOperationException("That didn't work.");
    }

    public static Nationality OwnsZebra()
    {
        Init();
        House? zebra = (from house in houses where house.pet == Pet.Zebra select house).FirstOrDefault<House>();
        if (zebra != null && zebra.nationality != null)
            return (Nationality)zebra.nationality;

        throw new InvalidOperationException("That didn't work.");
    }
}