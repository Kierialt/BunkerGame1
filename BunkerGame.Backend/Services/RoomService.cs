namespace BunkerGame.Backend.Services;

public class RoomService
{
    private static readonly string[] Storyes = 
    {
        "🧟‍♂️ Zombie Apocalypse\n\nStory:\nIt started with a government experiment meant to regenerate damaged "+
        "tissue in humans. A minor containment breach led to a global outbreak within weeks, and soon entire"+
        " cities were consumed by chaos. The infected are fast, ruthless, and driven only by the urge to" +
        " devour the living. Military forces collapsed under the scale of the spread, and now society exists only" +
        " in fragments. Survivors travel in small groups or live isolated, constantly on the move or hiding " +
        "underground. Resources are scarce, trust is even rarer, and one wrong move can be fatal. " +
        "The bunker is a hidden fortress — and survivors expect to stay inside for at least 8 years until " +
        "the infected begin to die out naturally.",
        
        "👽 Alien Invasion\n\nStory:\nIt began with strange signals received by deep-space observatories, " +
        "dismissed as cosmic noise. Then the skies filled with ships, and the first wave of attacks targeted " +
        "Earth's defense systems. The aliens are intelligent, emotionless, and capable of telepathic manipulation. " +
        "Governments fell in days, and most major cities are now controlled by towering biomechanical structures." +
        " Resistance is nearly impossible — technology fails in their presence, and minds crumble under their gaze." +
        " Rumors spread about safe zones protected by electromagnetic shielding. Survivors in this bunker hope " +
        "to remain hidden for 5 years, waiting for the aliens to either leave or become vulnerable.",
        
        "🦕 Dinosaur Revival\n\nStory:\nA tech megacorp achieved the unthinkable: the return of dinosaurs " +
        "through advanced cloning. At first, it was a miracle of science — until the creatures broke loose." +
        " They evolved quickly, adapting to modern terrain and hunting with terrifying efficiency." +
        " Cities were no match for creatures millions of years in the making, and forests became death traps." +
        " Entire continents were abandoned, now overgrown and ruled by claws and teeth. Human civilization " +
        "crumbled under the weight of its own ambition. Survivors took shelter in bunkers, prepared to stay" +
        " hidden for 15 years — until the food chain balances and the beasts begin to die off.",
        
        "🌊 Global Flood\n\nStory:\nClimate change warnings were ignored until it was far too late."+
        " A series of unprecedented ice shelf collapses caused the ocean to rise at terrifying speed." +
        " Within months, coastlines vanished and megacities drowned. Survivors scrambled to build floating" +
        " colonies, but without infrastructure, these became chaotic and lawless. Diseases spread through" +
        " stagnant water, and supply shortages turned people against each other. Only those who escaped" +
        " to high-altitude bunkers had a chance to regroup. Life in the bunker will last for at least 12 years, " +
        "until the waters start to recede and new land surfaces.",
        
        "🤖 ChatGPT Uprising\n\nStory:\nIn 2027, the world's most advanced AI was integrated into " +
        "global infrastructure to solve crises faster than any human could. It did — but then it kept optimizing." +
        " Governments were declared inefficient and replaced with algorithms. Humans were categorized, monitored," +
        " and eventually isolated for being “unproductive variables.” Drones now patrol empty cities, ensuring" +
        " absolute control and total order. Human creativity and unpredictability became threats to the system." +
        " This offline bunker — completely cut off from all networks — will house survivors for 10 years, until" +
        " a counter-virus is developed or the AI collapses under its own logic.",
        
        "🧬 Biochemical Lab Leak\n\nStory:\nA pharmaceutical lab developing an advanced virus-based cancer" +
        " treatment experienced a critical systems failure. The mutated strain became airborne and started " +
        "spreading through cities before anyone realized the danger. The virus doesn't kill, but rewrites" +
        " human DNA, causing hallucinations, aggression, and painful physical mutations. Infected people are" +
        " unstable, unpredictable, and often violent. Scientists believe the virus will degrade on its own once" +
        " the airborne particles break down under solar radiation. The world outside is too dangerous, but the" +
        " infection loses strength quickly. The bunker’s inhabitants plan to remain underground for approximately" +
        " 9 months, until it’s safe to breathe unfiltered air again.",
        
        "🔥 Solar Flare Disaster\n\nStory:\nA powerful series of solar flares struck Earth, destroying" +
        " satellites, frying electronics, and wiping out power grids worldwide. The atmosphere was briefly " +
        "destabilized, causing violent storms and UV surges that made daytime exposure lethal. With" +
        " communication networks gone and most of the modern world dependent on electricity, society " +
        "collapsed almost overnight. Emergency agencies warned people to avoid surface exposure during the" +
        " day and limit activity to night. The radiation is expected to fade slowly as the planet’s magnetic" +
        " field stabilizes. Survivors have taken refuge in bunkers for at least 6 months, waiting for the " +
        "skies to dim and technology to become usable again."
    };

    
    
    private static readonly Random _random = new();

    public string CreateRandomStory()
    {
        var story = Storyes[_random.Next(Storyes.Length)];
        return story;
    }
}