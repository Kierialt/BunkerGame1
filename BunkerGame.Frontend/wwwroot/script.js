// document.getElementById("startGameBtn").addEventListener("click", async () => {
//     try {
//         const response = await fetch("http://localhost:5138/api/Game/StartGame", { // <-- correct api
//             method: "POST"
//         });

//         if (response.ok) {
//             alert("Game started successfully!");
//         } else {
//             alert("Error occurred while starting the game.");
//         }
//     } catch (error) {
//         alert("Server is not available.");
//     }
// });

// document.getElementById("rulesBtn").addEventListener("click", () => {
//     alert("There will be game rules here.");
// });


document.getElementById("startGameBtn").addEventListener("click", async () => {
    try {
        const response = await fetch("http://localhost:5138/api/Game/StartGame", {
            method: "POST"
        });

        if (response.ok) {
            const data = await response.json();
            // alert(`success: ${data.success}\nmessage: ${data.message}\ndata: ${data.data}`);

            // Сохраняем данные персонажа в localStorage
            localStorage.setItem("characterData", JSON.stringify(data.data));
            // Переходим на страницу персонажа
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
// document.getElementById("rulesBtn").addEventListener("click", () => {
//     alert("There will be game rules here.");
// });