using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Backend.Models;

public class Player
{
    [Key]  
    public int Id { get; set; }

    public string Profession { get; set; } = default!; 
    
     public string Gender { get; set; } = default!;
     
     public int Age { get; set; } 
     
    public string Orientation { get; set; } = default!;
    
    public string Hobby { get; set; } = default!;
    
    public string Phobia { get; set; } = default!;
    
    public string Luggage { get; set; } = default!;
    
    public string AdditionalInformation { get; set; } = default!;
    
    public string BodyType { get; set; } = default!;
    
    public string Health { get; set; } = default!;
    
    public string Personalitie { get; set; } = default!;
    
}


