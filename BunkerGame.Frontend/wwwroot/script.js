document.addEventListener('DOMContentLoaded', function() {
    // --- LOGIN/REGISTER/USERPANEL LOGIC ---
    function showLoginModal() {
        document.getElementById('loginModal').style.display = 'flex';
    }
    
    function showRegisterModal() {
        document.getElementById('registerModal').style.display = 'flex';
    }
    
    function closeModals() {
        document.getElementById('loginModal').style.display = 'none';
        document.getElementById('registerModal').style.display = 'none';
    }

    // Modal open/close handlers
    document.getElementById('loginBtn').onclick = showLoginModal;
    document.getElementById('closeLoginModal').onclick = closeModals;
    document.getElementById('closeRegisterModal').onclick = closeModals;
    
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
    // bla bla bla

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

    // --- START GAME AND RULES BUTTONS ---
    document.getElementById("startGameBtn").addEventListener("click", async () => {
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
                alert("Error occurred while starting the game.");
            }
        } catch (error) {
            alert("Server is not available.");
        }
    });

    document.getElementById("rulesBtn").addEventListener("click", () => {
        window.location.href = "rules.html";
    });
});