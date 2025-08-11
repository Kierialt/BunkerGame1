# 🚀 Bunker Game

A multiplayer survival strategy game where players compete for a spot in a bunker during an apocalyptic scenario. Players must strategically reveal their character traits and convince others they deserve to survive while voting to eliminate the least valuable members.

## 🎮 Game Overview

Bunker Game is inspired by the popular social deduction game where players take on unique character roles with various traits, skills, and backgrounds. The goal is to survive multiple voting rounds and secure a place in the bunker by proving your value to the group.

### 🎯 Game Mechanics

- **Character Generation**: Each player gets a randomly generated character with unique traits
- **Multi-Round Gameplay**: Strategic reveals of character characteristics across multiple rounds
- **Voting System**: Players vote to eliminate one member each round
- **Survival Strategy**: Use your traits wisely to build alliances and argue your case
- **Multiplayer Support**: Play with 5-10 players in custom game rooms

## ✨ Features

### 🎭 Character System
- **Professions**: 80+ unique professions (Engineer, Doctor, Astronaut, etc.)
- **Personal Traits**: Age, gender, orientation, personality, body type
- **Skills & Backgrounds**: Hobbies, phobias, health conditions, additional information
- **Survival Gear**: Luggage items that could be valuable in the bunker

### 🏠 Game Rooms
- **Custom Room Creation**: Set room name and maximum player count
- **Session Codes**: 6-character codes for easy room joining
- **Player Management**: Track current players and winners
- **Game States**: Waiting, Playing, and Finished states

### 🗳️ Voting System
- **Round-Based Voting**: Multiple voting rounds throughout the game
- **Strategic Elimination**: Vote out the least valuable player each round
- **Result Tracking**: Monitor voting patterns and outcomes

### 🔐 Authentication
- **User Registration**: Email, password, and nickname registration
- **Login System**: Secure authentication for returning players
- **Guest Play**: Option to play without registration

## 🏗️ Architecture

### Backend (.NET 8 Web API)
- **Framework**: ASP.NET Core 8.0
- **Database**: SQLite with Entity Framework Core
- **Architecture**: Clean architecture with services and controllers
- **Features**: 
  - RESTful API endpoints
  - Swagger/OpenAPI documentation
  - CORS support for frontend integration
  - Automatic database backup service

### Frontend (Static Web App)
- **Technology**: Vanilla JavaScript, HTML5, CSS3
- **Design**: Modern, responsive UI with cyberpunk aesthetic
- **Features**:
  - Real-time game updates
  - Modal-based user interface
  - Responsive design for various screen sizes
  - Custom fonts and styling

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- Rider or Visual Studio 2022 or VS Code
- Modern web browser

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BunkerGame1
   ```

2. **Backend Setup**
   ```bash
   cd BunkerGame.Backend
   dotnet restore
   dotnet run
   ```
   The backend will be available at `http://localhost:5138`

3. **Frontend Setup**
   ```bash
   cd BunkerGame.Frontend
   dotnet run
   ```
   The frontend will be available at `http://localhost:5198`

### Configuration

#### Backend Configuration
Update `BunkerGame.Backend/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=path/to/your/BunkerGameDb"
  }
}
```

#### Frontend Configuration
Update `BunkerGame.Frontend/wwwroot/config.js`:
```javascript
const API_CONFIG = {
    BASE_URL: 'http://localhost:5138'  // Backend API URL
};
```

## 🎮 How to Play

### 1. Create or Join a Room
- **Create Room**: Set room name, max players (5-10), and your nickname
- **Join Room**: Enter a 6-character session code and your nickname
- **Single Player**: Play solo to test the game mechanics

### 2. Game Setup
- Each player receives a randomly generated character
- Characters have 10 different trait categories
- Room creator can start the game when ready

### 3. Gameplay Rounds
- **Round 1**: Reveal only your Profession card
- **Subsequent Rounds**: Strategically reveal other traits
- **Voting**: After each round, vote to eliminate one player
- **Survival**: Use your traits to convince others of your value

### 4. Victory Conditions
- Survive all voting rounds
- Be among the final group allowed in the bunker
- Number of winners depends on total players:
  - 5 players: 2 winners
  - 6-7 players: 3 winners
  - 8-9 players: 4 winners
  - 10 players: 5 winners

## 🔧 API Endpoints

### Game Management
- `POST /api/Game/StartGame` - Start single player game
- `GET /api/Game/RandomStory` - Get random apocalyptic story

### Room Management
- `POST /api/Room/CreateRoom` - Create new game room
- `POST /api/Room/JoinRoom` - Join existing room
- `GET /api/Room/GetRoomInfo` - Get room information
- `POST /api/Room/StartGame` - Start multiplayer game

### Gameplay
- `POST /api/Room/StartVoting` - Begin voting round
- `POST /api/Room/Vote` - Submit vote
- `GET /api/Room/GetCurrentVoting` - Get current voting status
- `POST /api/Room/RevealCharacteristic` - Reveal character trait

### Authentication
- `POST /api/Auth/Register` - User registration
- `POST /api/Auth/Login` - User login

## 🎨 Character Traits

### Core Attributes
- **Profession**: 80+ unique occupations with varying survival value
- **Age**: 18-55 years old
- **Gender**: Man, Woman, Transgender, Non-binary, Prefer not to say
- **Orientation**: Heterosexual, Homosexual, Bisexual, Asexual, Pansexual, Polysexual

### Survival Factors
- **Health**: Perfect health to various conditions (blind, deaf, chronic illnesses)
- **Body Type**: Athletic, Slim, Fit, Flexible, Average, Overweight, Obese, Bodybuilder
- **Personality**: 60+ traits from Confident to Psychopathic

### Skills & Backgrounds
- **Hobbies**: 80+ activities from practical survival skills to unusual interests
- **Phobias**: 50+ fears that could affect bunker life
- **Luggage**: 100+ items from essential survival gear to unusual objects
- **Additional Information**: 60+ background details and special abilities

## 🛠️ Development

### Project Structure
```
BunkerGame1/
├── BunkerGame.sln                 # Solution file
├── BunkerGame.Backend/            # .NET 8 Web API
│   ├── Controllers/               # API endpoints
│   ├── Models/                    # Data models
│   ├── Services/                  # Business logic
│   ├── DTOs/                     # Data transfer objects
│   ├── Responses/                 # API response models
│   ├── Data/                      # Database context
│   └── Utils/                     # Utility classes
├── BunkerGame.Frontend/           # Static web application
│   └── wwwroot/                   # Web assets
│       ├── *.html                 # Game pages
│       ├── *.js                   # Game logic
│       ├── *.css                  # Styling
│       └── images/                # Game assets
└── .gitignore                     # Git exclusions
```

### Key Services
- **GameService**: Character generation and game logic
- **RoomService**: Room management and multiplayer functionality
- **VotingService**: Voting system and round management
- **AuthService**: User authentication and management
- **BackupService**: Automatic database backup

### Database Models
- **GameRoom**: Game room information and state
- **Player**: Character traits and attributes
- **RoomPlayer**: Player-room relationships
- **VotingRound**: Voting session data
- **Vote**: Individual vote records
- **User**: Registered user accounts

## 🚀 Deployment

### Backend Deployment
1. Build the project: `dotnet publish -c Release`
2. Deploy to your hosting platform (Azure, AWS, etc.)
3. Update connection strings for production database
4. Configure CORS policies for production domains

### Frontend Deployment
1. Build and deploy static files to web server
2. Update `config.js` with production API URL
3. Ensure HTTPS is enabled for security

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🎯 Future Enhancements

- [ ] Real-time WebSocket communication
- [ ] Mobile app support
- [ ] Additional character traits and scenarios
- [ ] AI opponents for single-player mode
- [ ] Tournament and ranking systems
- [ ] Custom rule sets and game modes
- [ ] Multi-language support
- [ ] Advanced analytics and statistics

## 🐛 Known Issues

- Database path configuration needs to be updated for production
- CORS policy currently allows all origins (should be restricted in production)
- No input validation on some API endpoints

## 📞 Support

For questions, issues, or contributions, please:
1. Check existing issues in the repository
2. Create a new issue with detailed information
3. Contact the development team 

---

**Enjoy surviving in the bunker! 🚀**
