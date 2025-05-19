window.onload = function() {
    // Получаем данные из localStorage
    const data = JSON.parse(localStorage.getItem("characterData"));
    if (!data) {
        document.getElementById("characterCard").innerHTML = "<p>Нет данных о персонаже.</p>";
        return;
    }
    // Выводим карточку персонажа
    document.getElementById("characterCard").innerHTML = `
        <div class="character-card">
            <p><strong>Профессия:</strong> ${data.profession}</p>
            <p><strong>Пол:</strong> ${data.gender}</p>
            <p><strong>Возраст:</strong> ${data.age}</p>
            <p><strong>Ориентация:</strong> ${data.orientation}</p>
            // <p><strong>Имя:</strong> ${data.name ?? "???"}</p>
            <p><strong>Здоровье:</strong> ${data.health}</p>
        </div>
    `;
};