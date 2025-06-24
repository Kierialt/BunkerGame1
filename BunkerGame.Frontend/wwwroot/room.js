document.addEventListener('DOMContentLoaded', function() {
    const loadingOverlay = document.querySelector('.loading-overlay');
    const roomId = localStorage.getItem('currentRoomId');
    const sessionCode = localStorage.getItem('currentRoomCode');
    const playerNickname = localStorage.getItem('currentPlayerNickname');

    if (!roomId || !sessionCode || !playerNickname) {
        alert('Room information not found. Please return to the main menu.');
        window.location.href = 'index.html';
        return;
    }

    
    // Initialize room
    document.getElementById('sessionCode').textContent = sessionCode;
    document.getElementById('playerNickname').textContent = playerNickname;
    
    // Hide loading overlay
    setTimeout(() => {
        loadingOverlay.classList.remove('active');
    }, 1000);

    // Back button
    document.getElementById('backBtn').addEventListener('click', () => {
        localStorage.removeItem('currentRoomId');
        localStorage.removeItem('currentRoomCode');
        localStorage.removeItem('currentPlayerNickname');
        window.location.href = 'index.html';
    });

    // Load room info
    loadRoomInfo();

    // Auto-refresh room info every 5 seconds
    setInterval(loadRoomInfo, 5000);

    async function loadRoomInfo() {
        try {
            const response = await fetch(`${API_CONFIG.BASE_URL}/api/Room/GetRoomInfo?roomId=${roomId}`);
            
            if (response.ok) {
                const data = await response.json();
                const room = data.data;
                
                updateRoomDisplay(room);
                updatePlayersDisplay(room.players);
                updateGameControls(room);
                
                // Check for active voting
                if (room.status === 'Playing') {
                    await loadVotingInfo();
                }
            } else {
                console.error('Failed to load room info');
            }
        } catch (error) {
            console.error('Error loading room info:', error);
        }
    }

    function updateRoomDisplay(room) {
        document.getElementById('roomName').textContent = room.name;
        document.getElementById('roomStatus').textContent = room.status;
        document.getElementById('playerCount').textContent = room.currentPlayers;
        document.getElementById('maxPlayers').textContent = room.maxPlayers;
        document.getElementById('storyText').textContent = room.story || 'No story available';
    }

    function updatePlayersDisplay(players) {
        const playersList = document.getElementById('playersList');
        playersList.innerHTML = '';
        const currentNickname = localStorage.getItem('currentPlayerNickname');

        players.forEach(player => {
            const isCurrent = player.nickname === currentNickname;
            const playerCard = document.createElement('div');
            playerCard.className = `player-card ${player.isAlive ? 'alive' : 'eliminated'} ${player.isWinner ? 'winner' : ''}${isCurrent ? ' current-player' : ''}`;
            
            let characteristics = '';
            if (isCurrent) {
                characteristics += `<div class=\"current-player-badge\">YOU</div>`;
            }
            characteristics += `<div class=\"player-name\">${player.nickname}</div>`;
            
            // Always show profession
            if (player.profession) {
                characteristics += `<div class="characteristic">👨‍💼 Profession: ${player.profession}</div>`;
            }
            
            // Show other characteristics only if revealed
            if (player.gender && player.isGenderRevealed) {
                characteristics += `<div class="characteristic">👤 Gender: ${player.gender}</div>`;
            }
            if (player.age && player.isAgeRevealed) {
                characteristics += `<div class="characteristic">🎂 Age: ${player.age}</div>`;
            }
            if (player.orientation && player.isOrientationRevealed) {
                characteristics += `<div class="characteristic">💕 Orientation: ${player.orientation}</div>`;
            }
            if (player.hobby && player.isHobbyRevealed) {
                characteristics += `<div class="characteristic">🎯 Hobby: ${player.hobby}</div>`;
            }
            if (player.phobia && player.isPhobiaRevealed) {
                characteristics += `<div class="characteristic">😱 Phobia: ${player.phobia}</div>`;
            }
            if (player.luggage && player.isLuggageRevealed) {
                characteristics += `<div class="characteristic">🎒 Luggage: ${player.luggage}</div>`;
            }
            if (player.additionalInformation && player.isAdditionalInfoRevealed) {
                characteristics += `<div class="characteristic">ℹ️ Additional Info: ${player.additionalInformation}</div>`;
            }
            if (player.bodyType && player.isBodyTypeRevealed) {
                characteristics += `<div class="characteristic">💪 Body Type: ${player.bodyType}</div>`;
            }
            if (player.health && player.isHealthRevealed) {
                characteristics += `<div class="characteristic">🏥 Health: ${player.health}</div>`;
            }
            if (player.personality && player.isPersonalityRevealed) {
                characteristics += `<div class="characteristic">🧠 Personality: ${player.personality}</div>`;
            }
            
            playerCard.innerHTML = characteristics;
            playersList.appendChild(playerCard);
        });
    }

    function updateGameControls(room) {
        const startGameBtn = document.getElementById('startGameBtn');
        const startVotingBtn = document.getElementById('startVotingBtn');
        const revealCharacteristicBtn = document.getElementById('revealCharacteristicBtn');

        if (room.status === 'Waiting') {
            startGameBtn.style.display = 'block';
            startVotingBtn.style.display = 'none';
            revealCharacteristicBtn.style.display = 'block';
        } else if (room.status === 'Playing') {
            startGameBtn.style.display = 'none';
            startVotingBtn.style.display = 'block';
            revealCharacteristicBtn.style.display = 'block';
        } else {
            startGameBtn.style.display = 'none';
            startVotingBtn.style.display = 'none';
            revealCharacteristicBtn.style.display = 'none';
        }
    }

    // Start Game
    document.getElementById('startGameBtn').addEventListener('click', async () => {
        try {
            const response = await fetch(`${API_CONFIG.BASE_URL}/api/Room/StartGame`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ roomId: parseInt(roomId) })
            });

            if (response.ok) {
                loadRoomInfo(); // Refresh room info
            } else {
                alert('Failed to start game');
            }
        } catch (error) {
            alert('Server error');
        }
    });

    // Start Voting
    document.getElementById('startVotingBtn').addEventListener('click', async () => {
        try {
            const response = await fetch(`${API_CONFIG.BASE_URL}/api/Room/StartVoting`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ roomId: parseInt(roomId) })
            });

            if (response.ok) {
                await loadVotingInfo();
            } else {
                alert('Failed to start voting');
            }
        } catch (error) {
            alert('Server error');
        }
    });

    // Reveal Characteristic
    document.getElementById('revealCharacteristicBtn').addEventListener('click', () => {
        showRevealModal();
    });

    function showRevealModal() {
        const modal = document.getElementById('revealModal');
        const playerSelect = document.getElementById('playerSelect');
        const characteristicSelect = document.getElementById('characteristicSelect');
        const currentNickname = localStorage.getItem('currentPlayerNickname');
        // Populate player select (only current player)
        playerSelect.innerHTML = '';
        const option = document.createElement('option');
        option.value = currentNickname;
        option.textContent = currentNickname;
        playerSelect.appendChild(option);
        // Fetch room and player data
        fetch(`${API_CONFIG.BASE_URL}/api/Room/GetRoomInfo?roomId=${roomId}`)
            .then(res => res.json())
            .then(data => {
                const player = data.data.players.find(p => p.nickname === currentNickname);
                // List of all characteristics
                const allCharacteristics = [
                    { value: 'gender', label: 'Gender', revealed: player.isGenderRevealed },
                    { value: 'age', label: 'Age', revealed: player.isAgeRevealed },
                    { value: 'orientation', label: 'Orientation', revealed: player.isOrientationRevealed },
                    { value: 'hobby', label: 'Hobby', revealed: player.isHobbyRevealed },
                    { value: 'phobia', label: 'Phobia', revealed: player.isPhobiaRevealed },
                    { value: 'luggage', label: 'Luggage', revealed: player.isLuggageRevealed },
                    { value: 'additionalinfo', label: 'Additional Information', revealed: player.isAdditionalInfoRevealed },
                    { value: 'bodytype', label: 'Body Type', revealed: player.isBodyTypeRevealed },
                    { value: 'health', label: 'Health', revealed: player.isHealthRevealed },
                    { value: 'personality', label: 'Personality', revealed: player.isPersonalityRevealed }
                ];
                // Leave only unrevealed
                characteristicSelect.innerHTML = '<option value="">Select characteristic</option>';
                allCharacteristics.forEach(c => {
                    if (!c.revealed) {
                        const opt = document.createElement('option');
                        opt.value = c.value;
                        opt.textContent = c.label;
                        characteristicSelect.appendChild(opt);
                    }
                });
            });
        modal.style.display = 'flex';
    }

    // Close reveal modal
    document.getElementById('closeRevealModal').addEventListener('click', () => {
        document.getElementById('revealModal').style.display = 'none';
    });

    // Reveal form submission
    document.getElementById('revealForm').addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const playerName = document.getElementById('playerSelect').value;
        const characteristic = document.getElementById('characteristicSelect').value;
        
        if (!playerName || !characteristic) {
            document.getElementById('revealMessage').textContent = 'Please select both player and characteristic';
            return;
        }

        try {
            // Find player ID by name
            const response = await fetch(`${API_CONFIG.BASE_URL}/api/Room/GetRoomInfo?roomId=${roomId}`);
            const data = await response.json();
            const player = data.data.players.find(p => p.nickname === playerName);
            
            if (!player) {
                document.getElementById('revealMessage').textContent = 'Player not found';
                return;
            }

            const revealResponse = await fetch(`${API_CONFIG.BASE_URL}/api/Room/RevealCharacteristic`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ 
                    roomPlayerId: player.id, 
                    characteristic: characteristic 
                })
            });

            if (revealResponse.ok) {
                document.getElementById('revealModal').style.display = 'none';
                loadRoomInfo(); // Refresh to show revealed characteristic
            } else {
                document.getElementById('revealMessage').textContent = 'Failed to reveal characteristic';
            }
        } catch (error) {
            document.getElementById('revealMessage').textContent = 'Server error';
        }
    });

    async function loadVotingInfo() {
        try {
            const response = await fetch(`${API_CONFIG.BASE_URL}/api/Room/GetCurrentVoting?roomId=${roomId}`);
            
            if (response.ok) {
                const data = await response.json();
                const voting = data.data;
                
                if (voting) {
                    updateVotingDisplay(voting);
                } else {
                    document.getElementById('votingSection').style.display = 'none';
                }
            }
        } catch (error) {
            console.error('Error loading voting info:', error);
        }
    }

    function updateVotingDisplay(voting) {
        const votingSection = document.getElementById('votingSection');
        const votingRoundNumber = document.getElementById('votingRoundNumber');
        const votingStatus = document.getElementById('votingStatus');
        const votingOptions = document.getElementById('votingOptions');
        const votingResults = document.getElementById('votingResults');

        votingSection.style.display = 'block';
        votingRoundNumber.textContent = voting.roundNumber;
        
        if (voting.status === 'Active') {
            votingStatus.innerHTML = '<div class="voting-active">🗳️ Voting is active - cast your vote!</div>';
            votingOptions.style.display = 'block';
            votingResults.style.display = 'none';
            
            // Show voting options (alive players)
            showVotingOptions();
        } else {
            votingStatus.innerHTML = '<div class="voting-finished">✅ Voting finished</div>';
            votingOptions.style.display = 'none';
            votingResults.style.display = 'block';
            
            // Show voting results
            showVotingResults(voting);
        }
    }

    function showVotingOptions() {
        const votingOptions = document.getElementById('votingOptions');
        votingOptions.innerHTML = '<h4>Vote to eliminate:</h4>';
        
        // Get alive players
        const alivePlayers = Array.from(document.querySelectorAll('.player-card.alive'))
            .map(card => card.querySelector('.player-name').textContent);
        
        alivePlayers.forEach(playerName => {
            const button = document.createElement('button');
            button.className = 'vote-btn';
            button.textContent = playerName;
            button.onclick = () => castVote(playerName);
            votingOptions.appendChild(button);
        });
    }

    async function castVote(targetPlayerName) {
        try {
            // Get current voting round
            const votingResponse = await fetch(`${API_CONFIG.BASE_URL}/api/Room/GetCurrentVoting?roomId=${roomId}`);
            const votingData = await votingResponse.json();
            const voting = votingData.data;
            
            if (!voting) {
                alert('No active voting round');
                return;
            }

            // Find player IDs
            const roomResponse = await fetch(`${API_CONFIG.BASE_URL}/api/Room/GetRoomInfo?roomId=${roomId}`);
            const roomData = await roomResponse.json();
            const voter = roomData.data.players.find(p => p.nickname === playerNickname);
            const target = roomData.data.players.find(p => p.nickname === targetPlayerName);
            
            if (!voter || !target) {
                alert('Player not found');
                return;
            }

            const voteResponse = await fetch(`${API_CONFIG.BASE_URL}/api/Room/Vote`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    votingRoundId: voting.id,
                    voterId: voter.id,
                    votedForId: target.id
                })
            });

            if (voteResponse.ok) {
                alert('Vote cast successfully!');
                loadVotingInfo(); // Refresh voting info
            } else {
                alert('Failed to cast vote');
            }
        } catch (error) {
            alert('Server error');
        }
    }

    function showVotingResults(voting) {
        const votingResults = document.getElementById('votingResults');
        
        if (voting.eliminatedPlayerNickname) {
            votingResults.innerHTML = `
                <div class="eliminated-player">
                    <h4>Eliminated: ${voting.eliminatedPlayerNickname}</h4>
                </div>
            `;
        }
        
        if (voting.votes && voting.votes.length > 0) {
            const votesList = document.createElement('div');
            votesList.innerHTML = '<h4>Votes:</h4>';
            
            voting.votes.forEach(vote => {
                const voteItem = document.createElement('div');
                voteItem.className = 'vote-item';
                voteItem.textContent = `${vote.voterNickname} voted for ${vote.votedForNickname}`;
                votesList.appendChild(voteItem);
            });
            
            votingResults.appendChild(votesList);
        }
    }

    // Close modal when clicking outside
    window.addEventListener('click', (event) => {
        const modal = document.getElementById('revealModal');
        if (event.target === modal) {
            modal.style.display = 'none';
        }
    });
}); 