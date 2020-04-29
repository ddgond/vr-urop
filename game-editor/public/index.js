const state = {
  output: {
    sentences: [],
    kanjiList: "",
  },
};

function clear() {
  document.querySelector("#sentence-input").value = "";
  document.querySelector("#display").innerHTML = "";
  document.querySelector("#current").innerHTML = "";
  document.querySelector("#current-box").style.display = "none";
}

function submit() {
  const text = document.querySelector("#sentence-input").value;
  const display = document.querySelector("#display");
  clear();

  state.text = text;
  state.questions = [];
  [...text].forEach((char, i) => {
    const charSpan = document.createElement("span");
    charSpan.setAttribute("onclick", `create('${char}', ${i})`);
    charSpan.innerText = char;
    display.appendChild(charSpan);
  });
}

function create(char, i) {
  const reading = prompt(`Enter the reading for ${char}`);
  const current = document.querySelector("#current");
  state.questions.push({
    i,
    content: reading,
    answer: char,
  });
  current.innerHTML += `<li><span class="u-bold">Index ${i}:</span> ${char} → ${reading}</li>`;
  document.querySelector("#current-box").style.display = "flex";
}

function finishSentence() {
  if (state.questions.length == 0) return alert("Add some readings first");

  const parts = [];
  state.questions.sort((a, b) => a.i - b.i);
  state.questions.forEach((q, i) => {
    const prevIndex = i && state.questions[i - 1].i + 1;
    const prev = state.text.substring(prevIndex, q.i);
    if (prev) {
      parts.push({ isQuestion: false, content: prev });
    }
    parts.push({ isQuestion: true, content: q.content, answer: q.answer });
  });

  const finalQuestion = state.questions[state.questions.length - 1];
  if (finalQuestion.i < state.text.length - 1) {
    parts.push({
      isQuestion: false,
      content: state.text.substring(finalQuestion.i + 1, state.text.length),
    });
  }

  state.output.sentences.push({ parts });
  state.output.kanjiList = document.querySelector("#kanji-list").value;

  const text = [...state.text]
    .map((char, i) =>
      state.questions.some((q) => q.i === i)
        ? `<span class="emph">${char}</span>`
        : char
    )
    .join("");
  document.querySelector("#output").innerHTML += `<li>${text}</li>`;
  document.querySelector("#output-box").style.display = "flex";
  clear();
}

function saveFile() {
  fetch("/api/submit", {
    method: "post",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(state.output),
  })
    .then((res) => res.json())
    .then((res) => alert("Saved!"));
}
