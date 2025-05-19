using BunkerGame.Backend.Models;

namespace BunkerGame.Backend.Services;

public class GameService
{
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
    

    private static readonly string[] Orientations =
    {
        "Heterosexual", "Homosexual", "Bisexual", "Asexual"
    };
    
    private static readonly string[] Personalities =
    {
        "Лидер", "Интроверт", "Агрессор", "Дипломат", "Оптимист"
    };

    private static readonly string[] Healthes =
    {
        "Perfect health", "Measles", "Epilepsy", "Chronic migraine", "Kidney stones",
        "Blind", "Deaf", "Mute", "Missing little toe on the left foot", "Common cold", 
        "Water allergy", "Chickenpox", "Lice", "Apnea", "Alien Hand Syndrome (One hand \"has a mind of its own.\")",
        "Depression", "Insomnia", "Anxiety disorder", "Asthma", "Arthritis"
    };


    private readonly Random _random = new();

    public Player CreateRandomPlayer()
    {
        return new Player
        {
            //Name = Names[_random.Next(Names.Length)],
            Profession = Professions[_random.Next(Professions.Length)],
            Age = _random.Next(18, 99),
            Gender = Genders[_random.Next(Genders.Length)],
            Orientation = Orientations[_random.Next(Orientations.Length)],
            Health = Healthes[_random.Next(Healthes.Length)]
        };
    }
}