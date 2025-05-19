namespace BunkerGame.Backend.Models;

public class Player
{
    public string Profession { get; set; } = default!; 
     public string Gender { get; set; } = default!;
     public int Age { get; set; } 
    public string Orientation { get; set; } = default!;
 
    public string Name { get; set; } = default!;

    public string Health { get; set; } = default!;
    private static readonly string[] Professions = 
    { 
        "Engineer", "Doctor", "Programmer", "Farmer", "Scientist", "Garbage collector", 
        "Astronaut", "Astrologer", "Film Director", "Teacher", "Adult film actor", 
        "Fisherman", "Homeless person", "Unemployed"
        
    };
    private static readonly string[] Genders =
    {
        "Man", "Woman", "Transgender"
    };

    private static readonly int[] Ages = 
    {
        18, 19, 20, 22, 25, 27, 28, 30, 32, 33, 37, 39, 40, 41, 45, 46, 52,
        54, 55, 59, 64, 66, 69, 73, 82, 88, 92, 99 
    };

    private static readonly string[] Orientations =
    {
        "Heterosexual", "Homosexual", "Bisexual", "Asexual"
    };
    
    private static readonly string[] Personalities =
    {
        "Лидер", "Интроверт", "Агрессор", "Дипломат", "Оптимист"
    };

    
}

