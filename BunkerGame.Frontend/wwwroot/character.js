async function loadCharacter() {
    // Пробуем получить нового персонажа с бэкенда
    try {
        const response = await fetch("http://localhost:5138/api/Game/StartGame", {
            method: "POST"
        });
        if (response.ok) {
            const data = await response.json();
            showCharacter(data.data);
            // Сохраняем нового персонажа в localStorage (по желанию)
            localStorage.setItem("characterData", JSON.stringify(data.data));
        } else {
            document.getElementById("characterCard").innerHTML = "<p>Ошибка при получении персонажа.</p>";
        }
    } catch {
        document.getElementById("characterCard").innerHTML = "<p>Сервер недоступен.</p>";
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
                <p><strong>Name:</strong> ${data.name ?? "???"}</p>
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
}

window.onload = function() {
    // При первом открытии берём персонажа из localStorage
    const data = JSON.parse(localStorage.getItem("characterData"));
    if (data) {
        showCharacter(data);
    } else {
        loadCharacter();
    }
    document.getElementById("refreshBtn").onclick = loadCharacter;
    document.getElementById("backHomeBtn").onclick = function() {
        window.location.href = "index.html";
    };

    document.getElementById("shareBtn").onclick = function() {
        const data = JSON.parse(localStorage.getItem("characterData"));
        if (!data) return;

// Формируем текст для отправки
        const text = 
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

        // Заполняем текстовое поле
        document.getElementById("shareText").value = text;

        // Формируем ссылки
        const encoded = encodeURIComponent(text);
        document.getElementById("tgShare").href = `https://t.me/share/url?url=&text=${encoded}`;
        document.getElementById("waShare").href = `https://wa.me/?text=${encoded}`;
        document.getElementById("igShare").href = `https://www.instagram.com/direct/new/?text=${encoded}`;
        document.getElementById("fbShare").href = `https://www.facebook.com/sharer/sharer.php?u=&quote=${encoded}`;

        // Показываем модальное окно
        document.getElementById("shareModal").style.display = "flex";
    };

    // Кнопка закрытия
    document.getElementById("closeModal").onclick = function() {
        document.getElementById("shareModal").style.display = "none";
    };

    // Кнопка "Скопировать"
    document.getElementById("copyShare").onclick = function() {
        const text = document.getElementById("shareText").value;
        navigator.clipboard.writeText(text);
        document.getElementById("copyShare").textContent = "Copied!";
        setTimeout(() => {
            document.getElementById("copyShare").textContent = "Copy";
        }, 1500);
    };

    // Закрытие по клику вне окна
    document.getElementById("shareModal").onclick = function(e) {
        if (e.target === this) this.style.display = "none";
    };
};

