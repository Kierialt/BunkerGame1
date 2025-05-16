document.getElementById("startGameBtn").addEventListener("click", async () => {
    try {
        const response = await fetch("http://localhost:5138/api/Game/StartGame", { // <-- правильный порт!
            method: "POST"
        });

        if (response.ok) {
            alert("Игра успешно запущена!");
        } else {
            alert("Ошибка при запуске игры.");
        }
    } catch (error) {
        alert("Сервер недоступен.");
    }
});

document.getElementById("rulesBtn").addEventListener("click", () => {
    alert("Здесь будут правила игры.");
});
