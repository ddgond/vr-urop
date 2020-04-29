const express = require("express");
const fs = require("fs");
const app = express();
app.use(express.json());

app.get("/", (req, res) => res.sendFile(`${__dirname}/index.html`));
app.post("/api/submit", (req, res) => {
  fs.writeFileSync(
    "../Assets/Scripts/wordgame/gameconfig.json",
    JSON.stringify(req.body)
  );
  res.send({});
});

app.use(express.static("public"));

const port = 3000;
app.listen(port, () =>
  console.log(`Example app listening at http://localhost:${port}`)
);
