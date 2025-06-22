function setFlipCardHeight() {
    // Wait for DOM update
    setTimeout(() => {
        const front = document.querySelector('.flip-card-front');
        const back = document.querySelector('.flip-card-back');
        const card = document.querySelector('.flip-card');
        if (front && back && card) {
            const frontHeight = front.scrollHeight;
            front.style.height = frontHeight + "px";
            back.style.height = frontHeight + "px";
            card.style.height = frontHeight + "px";
        }
    }, 50);
}

async function getCharacter() {
    try {
        const response = await fetch(`${API_CONFIG.BASE_URL}/api/Game/StartGame`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (response.ok) {
            const data = await response.json();
            displayCharacter(data.data);
            setFlipCardHeight();
        } else {
            document.getElementById("characterCard").innerHTML = "<p>Error while getting character.</p>";
        }
    } catch {
        document.getElementById("characterCard").innerHTML = "<p>Server unavailable.</p>";
    }
}

function showCharacter(data) {
    const avatarSeed = data.name && data.name !== "???" ? data.name : Math.floor(Math.random() * 10000);
    const avatarUrl = `https://api.dicebear.com/7.x/adventurer/svg?seed=${encodeURIComponent(avatarSeed)}`;

    document.getElementById("characterCard").innerHTML = `
        <div class="character-card dossier-layout">
            <div class="dossier-info">
                <h2>Dossier</h2>
                <p><strong>Profession:</strong> ${data.profession}</p>
                <p><strong>Gender:</strong> ${data.gender}</p>
                <p><strong>Age:</strong> ${data.age}</p>
                <p><strong>Orientation:</strong> ${data.orientation}</p>
                <p><strong>Health:</strong> ${data.health}</p>
                <p><strong>Hobby:</strong> ${data.hobby}</p>
                <p><strong>Phobia:</strong> ${data.phobia}</p>
                <p><strong>Luggage:</strong> ${data.luggage}</p>
                <p><strong>Additional Information:</strong> ${data.additionalInformation}</p>
                <p><strong>Body Type:</strong> ${data.bodyType}</p>
                <div class="stamp">CONFIDENTIAL</div>
                <div class="signature">Signature: ____________</div>
            </div>
            <div class="dossier-avatar">
                <div class="avatar-frame">
                    <img src="${avatarUrl}" alt="Avatar" class="avatar-img">
                </div>
            </div>
        </div>
    `;
    setFlipCardHeight();
}

async function getStory() {
    try {
        const response = await fetch(`${API_CONFIG.BASE_URL}/api/Game/RandomStory`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (response.ok) {
            const data = await response.json();
            const story = data.data;
            
            // If there is no story, load a new one
            if (!story || story.trim() === '') {
                getStory();
                return;
            }
            
            // Remove emoji and wrap text in strong tags for bold font
            const storyHtml = story.replace(/^[^\w\s]*/, '').replace(/\b(\w+)\b/g, '<strong>$1</strong>');
            document.getElementById("storyContent").innerHTML = storyHtml;
            localStorage.setItem("apocalypseStory", storyHtml);
        } else {
            document.getElementById("storyContent").innerHTML = "<p>Error while loading story.</p>";
        }
    } catch {
        document.getElementById("storyContent").innerHTML = "<p>Server unavailable.</p>";
    }
}

window.onload = function() {
    // Start with card face down (back side visible)
    let isFlipped = false;
    const flipCard = document.getElementById("flipCard");
    flipCard.classList.remove("flipped"); // Ensure it's face down

    // Load character and story
    const data = JSON.parse(localStorage.getItem("characterData"));
    if (data) {
        showCharacter(data);
    } else {
        getCharacter();
    }
    getStory();

    // Flip logic
    flipCard.onclick = function() {
        isFlipped = !isFlipped;
        if (isFlipped) {
            flipCard.classList.add("flipped");
        } else {
            flipCard.classList.remove("flipped");
        }
    };

    // Buttons
    document.getElementById("refreshBtn").onclick = async function() {
        await getCharacter();
        // Keep card face down after refresh
        isFlipped = false;
        flipCard.classList.remove("flipped");
    };

    document.getElementById("backHomeBtn").onclick = function() {
        // clear story when returning to the main page
        localStorage.removeItem("apocalypseStory");
        window.location.href = "index.html";
    };

    document.getElementById("shareBtn").onclick = function() {
        const data = JSON.parse(localStorage.getItem("characterData"));
        if (!data) return;

        // Masked text for modal
        const maskedText = 
`***************
*  TOP SECRET  *
***************
Information is hidden.`;

        document.getElementById("shareText").value = maskedText;

        // Real text for sharing
        const realText = 
`Me in Bunker Game:
Profession: ${data.profession}
Gender: ${data.gender}
Age: ${data.age}
Orientation: ${data.orientation}
Name: ${data.name ?? "???"}
Health: ${data.health}
Hobby: ${data.hobby}
Phobia: ${data.phobia}
Luggage: ${data.luggage}
Additional Information: ${data.additionalInformation}
Body Type: ${data.bodyType}
You can join our survival http://localhost:5198/index.html/`;

        // Set share links with real data
        const encoded = encodeURIComponent(realText);
        document.getElementById("tgShare").href = `https://t.me/share/url?url=&text=${encoded}`;
        document.getElementById("waShare").href = `https://wa.me/?text=${encoded}`;
        document.getElementById("igShare").href = `https://www.instagram.com/direct/new/?text=${encoded}`;
        document.getElementById("fbShare").href = `https://www.facebook.com/sharer/sharer.php?u=&quote=${encoded}`;

        // Copy real data on copy
        document.getElementById("copyShare").onclick = function() {
            navigator.clipboard.writeText(realText);
            document.getElementById("copyShare").textContent = "Copied!";
            setTimeout(() => {
                document.getElementById("copyShare").textContent = "Copy";
            }, 1500);
        };

        document.getElementById("shareModal").style.display = "flex";
    };

    // Close modal button
    document.getElementById("closeModal").onclick = function() {
        document.getElementById("shareModal").style.display = "none";
    };

    // Close modal when clicking outside
    document.getElementById("shareModal").onclick = function(e) {
        if (e.target === this) this.style.display = "none";
    };
};

