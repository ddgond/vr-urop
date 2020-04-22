const express = require('express')
const app = express()
const port = 3000

app.get('/test', (req, res) => res.send('Hello World!'))
app.get('/', (req, res) => res.sendFile(`${__dirname}/index.html`))
app.use(express.static('public'))

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))