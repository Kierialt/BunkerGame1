document.getElementById("startGameBtn").addEventListener("click", async () => {
    try {
        const response = await fetch("http://localhost:5198/api/Game/StartGame", {
            method: "POST"
        });

        if (response.ok) {
            alert("Игра успешно запущена!");
            // Например, переход на страницу игры:
            // window.location.href = "/frontend/game.html";
        } else {
            alert("Ошибка при запуске игры.");
        }
    } catch (error) {
        console.error("Ошибка при подключении к API:", error);
        alert("Сервер недоступен.");
    }
});

document.getElementById("rulesBtn").addEventListener("click", () => {
    alert("Здесь будут правила игры.");
});
