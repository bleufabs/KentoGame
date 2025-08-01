document.addEventListener("DOMContentLoaded", () => {
    startGame();
});

async function startGame() {
    await fetch("/api/game/start", { method: "POST" });
    loadScene();
}

async function loadScene() {
    const response = await fetch("/api/game/scene");
    const data = await response.json();

    document.getElementById("scene-text").innerHTML = `<p>${data.text}</p>`;
    const choicesDiv = document.getElementById("choices");
    choicesDiv.innerHTML = "";

    data.choices.forEach(choice => {
        const btn = document.createElement("button");
        btn.textContent = choice.text;
        btn.onclick = () => makeChoice(choice.id);
        choicesDiv.appendChild(btn);
    });

    updateAffection();
}

async function makeChoice(choiceId) {
    await fetch("/api/game/choice", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(choiceId)
    });
    await showResponse();
}

async function showResponse() {
    const response = await fetch("/api/game/scene");
    const data = await response.json();

    const sceneText = document.getElementById("scene-text");
    sceneText.innerHTML += `<hr><p><em>${data.response}</em></p>`;

    const choicesDiv = document.getElementById("choices");
    choicesDiv.innerHTML = "";

    const nextBtn = document.createElement("button");
    nextBtn.textContent = "Continue";
    nextBtn.onclick = nextScene;
    choicesDiv.appendChild(nextBtn);

    updateAffection();
}

async function nextScene() {
    await fetch("/api/game/next", { method: "POST" });
    loadScene();
}

async function updateAffection() {
    const res = await fetch("/api/game/affection");
    const level = await res.json();
    document.getElementById("kento-affection").textContent = level;
}
