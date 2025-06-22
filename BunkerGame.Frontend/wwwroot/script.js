document.addEventListener('DOMContentLoaded', function() {
    // show loading overlay while starting the game
    const loadingOverlay = document.querySelector('.loading-overlay');
    loadingOverlay.classList.add('active');

    // hide loading overlay after 2 seconds
    setTimeout(() => {
        loadingOverlay.classList.remove('active');
    }, 2000);

    // --- LOGIN/REGISTER/USERPANEL LOGIC ---
    function showLoginModal() {
        document.getElementById('loginModal').style.display = 'flex';
    }
    
    function showRegisterModal() {
        document.getElementById('registerModal').style.display = 'flex';
    }
    
    function showCreateRoomModal() {
        document.getElementById('createRoomModal').style.display = 'flex';
    }
    
    function showJoinRoomModal() {
        document.getElementById('joinRoomModal').style.display = 'flex';
    }
    
    function closeModals() {
        document.getElementById('loginModal').style.display = 'none';
        document.getElementById('registerModal').style.display = 'none';
        document.getElementById('createRoomModal').style.display = 'none';
        document.getElementById('joinRoomModal').style.display = 'none';
    }

    // Modal open/close handlers
    document.getElementById('loginBtn').onclick = showLoginModal;
    document.getElementById('closeLoginModal').onclick = closeModals;
    document.getElementById('closeRegisterModal').onclick = closeModals;
    document.getElementById('closeCreateRoomModal').onclick = closeModals;
    document.getElementById('closeJoinRoomModal').onclick = closeModals;
    
    document.getElementById('showRegister').onclick = function(e) {
        e.preventDefault();
        closeModals();
        showRegisterModal();
    };
    
    document.getElementById('showLogin').onclick = function(e) {
        e.preventDefault();
        closeModals();
        showLoginModal();
    };

    // Close modals when clicking outside
    window.onclick = function(event) {
        if (event.target === document.getElementById('loginModal')) closeModals();
        if (event.target === document.getElementById('registerModal')) closeModals();
        if (event.target === document.getElementById('createRoomModal')) closeModals();
        if (event.target === document.getElementById('joinRoomModal')) closeModals();
    };

    // Login form submission
    document.getElementById('loginForm').onsubmit = async function(e) {
        e.preventDefault();
        const email = document.getElementById('loginEmail').value;
        const password = document.getElementById('loginPassword').value;
        const nickname = document.getElementById('loginNickname').value;

        try {
            const response = await fetch('http://localhost:5138/Auth/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password, nickname })
            });

            if (response.ok) {
                localStorage.setItem('token', 'dummy-token');
                localStorage.setItem('nickname', nickname);
                closeModals();
                updateUserPanel();
            } else {
                document.getElementById('loginMessage').textContent = 'Invalid credentials';
            }
        } catch (error) {
            document.getElementById('loginMessage').textContent = 'Server error';
        }
    };

    // Register form submission
    document.getElementById('registerForm').onsubmit = async function(e) {
        e.preventDefault();
        const email = document.getElementById('registerEmail').value;
        const password = document.getElementById('registerPassword').value;
        const nickname = document.getElementById('registerNickname').value;

        try {
            const response = await fetch('http://localhost:5138/Auth/Register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password, nickname })
            });

            if (response.ok) {
                localStorage.setItem('nickname', nickname);
                closeModals();
                updateUserPanel();
            } else {
                document.getElementById('registerMessage').textContent = 'Registration failed';
            }
        } catch (error) {
            document.getElementById('registerMessage').textContent = 'Server error';
        }
    };

    // Create Room form submission
    document.getElementById('createRoomForm').onsubmit = async function(e) {
        e.preventDefault();
        const roomName = document.getElementById('roomName').value;
        const maxPlayers = parseInt(document.getElementById('maxPlayers').value);
        const nickname = document.getElementById('creatorNickname').value;

        try {
            const response = await fetch('http://localhost:5138/api/Room/CreateRoom', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name: roomName, maxPlayers, nickname })
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('currentRoomId', data.data.roomId);
                localStorage.setItem('currentRoomCode', data.data.sessionCode);
                localStorage.setItem('currentPlayerNickname', nickname);
                closeModals();
                window.location.href = 'room.html';
            } else {
                document.getElementById('createRoomMessage').textContent = 'Failed to create room';
            }
        } catch (error) {
            document.getElementById('createRoomMessage').textContent = 'Server error';
        }
    };

    // Join Room form submission
    document.getElementById('joinRoomForm').onsubmit = async function(e) {
        e.preventDefault();
        const sessionCode = document.getElementById('sessionCode').value;
        const nickname = document.getElementById('joinerNickname').value;

        try {
            const response = await fetch('http://localhost:5138/api/Room/JoinRoom', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ sessionCode, nickname })
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('currentRoomId', data.data.roomId);
                localStorage.setItem('currentRoomCode', sessionCode);
                localStorage.setItem('currentPlayerNickname', nickname);
                closeModals();
                window.location.href = 'room.html';
            } else {
                document.getElementById('joinRoomMessage').textContent = 'Failed to join room';
            }
        } catch (error) {
            document.getElementById('joinRoomMessage').textContent = 'Server error';
        }
    };

    // Update user panel (login button or nickname)
    function updateUserPanel() {
        const token = localStorage.getItem('token');
        const nickname = localStorage.getItem('nickname');
        const userPanel = document.getElementById('userPanel');
        
        if (token && nickname) {
            userPanel.innerHTML = `
                <button id="nicknameBtn" class="user-nickname-btn">${nickname}</button>
                <div id="userDropdown" class="user-dropdown" style="display:none;">
                    <button id="logoutBtn">Logout</button>
                </div>
            `;
            
            document.getElementById('nicknameBtn').onclick = function() {
                const dropdown = document.getElementById('userDropdown');
                dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
            };
            
            document.getElementById('logoutBtn').onclick = function() {
                localStorage.removeItem('token');
                localStorage.removeItem('nickname');
                updateUserPanel();
            };
            
            // Close dropdown when clicking outside
            document.addEventListener('click', function handler(e) {
                if (!e.target.closest('.user-panel')) {
                    document.getElementById('userDropdown').style.display = 'none';
                    document.removeEventListener('click', handler);
                }
            });
        } else {
            userPanel.innerHTML = `<button id="loginBtn" class="login-top-btn">Login</button>`;
            document.getElementById('loginBtn').onclick = showLoginModal;
        }
    }

    // Initialize user panel
    updateUserPanel();

    // --- BUTTON HANDLERS ---
    document.getElementById("createRoomBtn").addEventListener("click", showCreateRoomModal);
    document.getElementById("joinRoomBtn").addEventListener("click", showJoinRoomModal);
    
    document.getElementById("startGameBtn").addEventListener("click", async () => {
        // Показываем загрузку
        loadingOverlay.classList.add('active');
        
        try {
            const response = await fetch("http://localhost:5138/api/Game/StartGame", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem("characterData", JSON.stringify(data.data));
                window.location.href = "character.html";
            } else {
                loadingOverlay.classList.remove('active');
                alert("Error occurred while starting the game.");
            }
        } catch (error) {
            loadingOverlay.classList.remove('active');
            alert("Server is not available.");
        }
    });

    document.getElementById("rulesBtn").addEventListener("click", () => {
        window.location.href = "rules.html";
    });
});