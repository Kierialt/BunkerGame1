using BunkerGame.Backend.Models;

namespace BunkerGame.Backend.Services;

public class GameService
{
    private static readonly string[] Professions = 
    { 
        "Engineer", "Doctor", "Programmer", "Farmer", "Scientist", "Garbage collector", 
        "Astronaut", "Astrologer", "Film Director", "Teacher", "Adult film actor", "Scare Actor ",
        "Fisherman", "Homeless person", "Unemployed", "Chicken sexer", "Professional Cuddler",
        "Pet food taster", "Golf ball diver", "Snake milker", "Snake milker", "Professional sleeper", 
        "Cheese Sculptor", "Sandcastle Builder", "Gum Buster", "Brewer", "Dancer", "Singer", "Musician",
        "Eurovision 2025 Winner (not mandatory Austrian)", "Scammer", "Jeweler", "Artist", "Drug dealer",
        "Gamer", "Footballer", "Lawyer", "Architect", "Police officer", "YouTuber", "Florist", "Translator",
        "Swimmer", "Prosecutor", "Surgeon", "Mechanic", "Mayor", "Businessman", "Detective", "Maid", "Explorer",
        "Firefighter", "Taxi driver", "Truck driver", "Racer", "Ghost hunter", "Psychologist", "Gardener",
        "Psychotherapist", "Cleaner", "Sommelier", "Topographer", "Geodesist", "Geologist", "Sculptor",
        "Experienced traveler", "Glass blower", "Sales manager", "Miner", "Geographer", "Pharmacist", "Actor", 
        "Anthropologist", "Pathologist", "Stand-up comedian", "Courier", "Ghost hunter", "Hunter", "Water specialist",
        "Nutritionist", "Chemist", "Metalworker", "Trainer", "Scout medic", "Weapon specialist", "Psychic", 
        "Meteorologist / Weather Observer", "Black market specialist", "Botanist / Herbalist", "Security strategist",
        "Shoe repairer", "Cartographer", "Chemistry lab technician", "Radio operator", "Munitions expert",
        "Navigator", "Climatologist", "Environmental scientist", "Veterinarian", "Cultural Anthropologist"
    };
    private static readonly string[] Genders =
    {
        "Man", "Woman", "Transgender", "Prefer not to say", "Non-binary"
    };
    

    private static readonly string[] Orientations =
    {
        "Heterosexual", "Homosexual", "Bisexual", "Asexual", "Pansexual", "Polysexual"
                                                             
    };
    
    private static readonly string[] Personalities =
    {
        "Skeptical", "Confident", "Optimistic", "Pessimistic", "Arrogant", "Bold", "Carrying", "Decent", "Dishonest", 
        "Easy-going", "Energetic", "Enthusiastic", "Forgetful", "Humorous", "Greedy", "Lively", "Impatient",
        "Misanthropic", "Paranoid", "Anxious", "Logical", "Modest", "Moody", "Moral", "Noisy", "Nosy", "Rebellious",
        "Passionate", "Realistic", "Clumsy", "Workaholic", "Sensitive", "Hard-working", "Sincere", "Romantic",
        "Determined", "Mindful", "Wise", "Cynical", "Sympathetic", "Reliable", "Unreliable", "Loyal", "Strict", 
        "Violent", "Calm", "Creative", "Generous", "Stubborn", "Introverted", "Extroverted", "Philanthropic",
        "Cheerful", "Brave", "Suspicious", "Ambitious", "Cautious", "Psychopathic", "Manipulative", "Narcissistic", 
        "Sadistic", "Obsessive", "Delusional", "Compulsive", "Aggressive", "Ruthless", "Cold-hearted", "Merciless",
        "Deceptive", "Hostile", "Jealous", "Fanatical", "Unpredictable", "Heartbreaker"
    };

    private static readonly string[] Healthes =
    {
        "Perfect health", "Measles", "Epilepsy", "Chronic migraine", "Kidney stones",
        "Blind", "Deaf", "Mute", "Missing little toe on the left foot", "Common cold", 
        "Water allergy", "Chickenpox", "Lice", "Apnea", "Alien Hand Syndrome (One hand \"has a mind of its own.\")",
        "Depression", "Insomnia", "Anxiety disorder", "Asthma", "Arthritis", "Dissociative identity disorder",
        "Tuberculosis", "HIV / AIDS", "Smallpox", "Rabies", "Cholera", "COVID-19", "Paranoia", "Schizophrenia", 
        "PTSD (Post-Traumatic Stress Disorder)", "Diabetes (Type 1 or 2)", "OCD (Obsessive-Compulsive Disorder)",
        "Plague", "Multiple Sclerosis", "Color Blindness", "Hermaphrodite", "Flatulence Disorder – chronic gas", 
        "Autism", "Sleepwalking (Somnambulism)", "Chronic Hiccups", "Gluten Sensitivity", "Lactose Intolerance",
        "Short-Term Memory Loss", "Peanut allergy", "Hypersexuality – excessive sex drive", 
        "Foreign Accent Syndrome – suddenly speaks with a strange accent after trauma", 
        "Cleromancy Obsession – makes every decision by dice or coin flip",
        "Lactose Intolerance – difficulty digesting lactose (milk sugar)", 
        "Gluten Intolerance / Celiac Disease – immune reaction to gluten (wheat, barley, rye)",
        "Peanut Allergy – severe immune response to peanuts", "Fish Allergy – allergy to fish like salmon or tuna",
        "Tree Nut Allergy – allergy to nuts like almonds, walnuts, cashews", 
        "Shellfish Allergy – allergy to crustaceans and mollusks (shrimp, crab, lobster)", 
        "Egg Allergy – allergy to proteins in egg whites or yolks", "Soy Allergy – allergy to soy proteins",
        "Histamine Intolerance – reaction to foods high in histamine (like aged cheese, wine)",
        "Hay Fever (Allergic Rhinitis) – allergy to pollen", "Mold Allergy – reaction to mold spores",
        "Dust Mite Allergy – reaction to microscopic mites in household dust", 
        "Pet Allergy – allergy to proteins in pet dander (cats, dogs)", 
        "Insect Sting Allergy – allergy to bee, wasp, or hornet stings",
        "Latex Allergy – reaction to natural rubber latex", 
        "Drug Allergy – e.g., Penicillin Allergy, Aspirin Sensitivity", 
        "Nickel Allergy – contact allergy to nickel in jewelry, etc."
        
    };

    private static readonly string[] Hobbys =
    {
        "Learning sign language", "Managing a personal budget in Excel", "Playing blindfold chess",
        "Willingly volunteering for local clean-up events", "Reflashing old gadgets", "Collecting rare spices",
        "Building LEGO sets without instructions", "Learning Braille", "Designing board games", "Sadist",
        "Spying on pigeons", "Comparing the taste of tap water from different sinks", "Studying body language", 
        "Expanding emotional intelligence", "Reading scientific articles", "Big data analysis", "Masochist",
        "Gardening", "Foraging — knowing which wild plants won’t kill you", "Hunting & Fishing", "Archery",
        "Fire-making", "Camping / Bushcraft", "First Aid / Herbal Medicine", "Cooking", "Sewing / Knitting",
        "Metalworking / Blacksmithing", "Carpentry", "Water Filtration / Purification Techniques", "Soap Making", 
        "Bartering / Negotiation", "Mechanical Repair / DIY Fixing", "Solar Power Know-How", "Animal Husbandry", 
        "Mushroom Cultivation", "Basic Electronics / Ham Radio", "Map Reading & Orienteering", "Lockpicking",
        "Self-defense / Martial Arts", "Brewing / Fermentation", "Beekeeping", "Storytelling or Music", "Birdwatching",
        "Collecting weird objects", "Origami", "Storytelling", "Soap making", "Herbalism", "Rock Balancing", "Shibari",
        "BDSM", "Mushroom foraging", "Making homemade instruments", "Amateur astronomy", "Sketching/Drawing",
        "Writing poetry", "Meditation", "Cooking exotic dishes", "Collecting insects", "Tattoo art", "Martial Arts", 
        "Magic Tricks", "Calligraphy", "Puzzle solving", "Hitting people on your right", "Mimicry (Imitating Voices)",
        "Hunting", "Fishing", "Emergency Childcare", "Obstetrician", "Cryptography", "Solar panel maintenance",
        "Collecting love letters", "Candle wax play", "Role-Playing Games", "Body Painting", "Tickle Therapy",
        "Candle Making", "Solar Tech Experiment", "Satanism"
      
    };

    private static readonly string[] Phobias =
    {
        "Androphobia — fear of men", "Nyctophobia — fear of the dark", "Phobophobia", 
        "Automysophobia — fear of getting your body dirty", "Turophobia — fear of cheese", 
        "Aichmophobia — fear of sharp objects", "Claustrophobia", "Aquophobia", "No phobia", 
        "Acrophobia — fear of heights", "Blaptophobia — fear of hurting someone", 
        "Hydrophobia — fear of water", "Heliophobia — fear of the sun", 
        "Gravidophobia — fear of getting pregnant", "Anthropophobia – fear of people", 
        "Automatonophobia – fear of robots or AI", "Claustrophobia – fear of confined spaces",
        "Hippopotomonstrosesquipedaliophobia – fear of long words",  "Hemophobia – fear of blood", 
        "Gynophobia – fear of women",  "Erotophobia – fear of sexual topics", "Parthenophobia – fear of virgins ",
        "Agliophobia – fear of pain", "Ephebiphobia – fear of teenagers", "Emetophobia – fear of vomiting",
        "Tokophobia – fear of pregnancy or childbirth", "Mysophobia – fear of germs or dirt",
        "Taphophobia – fear of being buried alive",  "Urophobia – fear of urine or urination",
        "Bananaphobia – fear of bananas", "Kinemortophobia – fear of zombies or the undead",
        "Romaphobia – fear of Rome or Roman culture", "Triskaidekaphobia – fear of the number 13",
        "Zoophobia – fear of animals"
        
    };

    private static readonly string[] Luggaages =
    {

    };

    private static readonly string[] AdditionalInformations =
    {
        "Speaks three languages fluently", "Was a licensed nurse", "Had a black belt in judo", 
        "Knew how to build solar panels from scrap", "Worked as a mechanic", "Trained as a chef",
        "Could identify over 50 edible wild plants", "Was a certified electrician", "Built their own tiny house",
        "Completed a survival course in the Rockies", "Practiced meditation daily", "Worked as a plumber ",
        "Used to be a teacher", "Was an amateur radio operator (HAM radio)", "Worked in cybersecurity ",
        "Knows Morse code", "Trained therapy dog handler", "Was a firefighter", "Knows how to ferment foods",
        "Taught self-defense classes", "Experienced in sewing and tailoring", "Used to be a logistician",
        "Gardening enthusiast with seed collection", "Had wilderness first aid certification",
        "Could repair electronics with minimal tools", "Expert at repairing radios and electronics",
        "Knows edible wild plants by heart", "Has first aid training from previous job as a nurse", 
        "Can start a fire with just flint and dry leaves", "Collects tiny bones from forest animals as a hobby", 
        "Claims to communicate with crows", "Sleeps with a stuffed toy giraffe for comfort",
        "Obsessed with cataloging every rock they find", "Can’t stop humming awkward music rhythm",
        "Has a pet name for every single tool they own", "Used to be a skilled lockpicker for a famous gang",
        "Laughs uncontrollably at bad jokes, even if they’re not funny", "Killed an old lady with axe",
        "Once got stuck in a closet for three hours during a drill", "Expert at scavenging rare medical supplies",
        "Has a tattoo that marks their time spent in prison", "Keeps a hidden stash of contraband in the shelter",
        "Has a reputation for settling disputes with fists, not words", "Skilled at stealth and silent takedowns", 
        "Once escaped capture by disguising as a janitor(cleaner)", "Was dating 13 people at the same time", 
        "Accidentally caused a fatal fight over food supplies", "Haunted by the memory of a kill in self-defense", 
        "Once defended themselves by lethal force during a raid", "Has a complicated love life even in the shelter", 
        "Keeps a knife with a worn handle from a past violent encounter", "Has three higher educations",
        "Known for charming others to gain favors or info", "Can purify water using natural methods", 
        "Uses flirtation as a survival tactic in tense situations", "Skilled at repairing mechanical equipmen",
        "Experienced hunter with a keen eye for tracking", "Trained in wilderness survival techniques",
        "Knows how to make natural insect repellent", "Can craft effective traps for animals or intruders", 
        "Fluent in multiple languages, useful for negotiating with outsiders",
        "Practices meditation to maintain mental resilience", 
        "Experienced cook who can make meals from limited resources"
    };

    private static readonly string[] BodyTypes =
    {
        "Athletic", "Slim", "Fit", "Flexible", "Average", "Overweight", "Obese", "Bodybuilder"
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
            Health = Healthes[_random.Next(Healthes.Length)],
            Hobby = Hobbys[_random.Next(Hobbys.Length)],
            BodyType = BodyTypes[_random.Next(BodyTypes.Length)],
            Phobia = Phobias[_random.Next(Phobias.Length)],
            //Luggage = Luggaages[_random.Next(Luggaages.Length)],
            AdditionalInformation = AdditionalInformations[_random.Next(AdditionalInformations.Length)],
            Personalitie = Personalities[_random.Next(Personalities.Length)],
            
        };
    }
}