const express = require('express')

const app = express()
const port = 5000
const path = require('path')
const router = express.Router();

app.use(express.static(__dirname + '/node_modules/bootstrap/dist'));
app.use(express.static('./src'))

app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname + '/src/index.html'))
})

app.get('/comandas', (req, res) => {
    res.sendFile(path.join(__dirname + '/src/comandas.html'))
})

app.get('/administracion', (req, res) => {
    res.sendFile(path.join(__dirname + '/src/administracion.html'))
})

app.get('/administracion/mercaderia/add', (req, res) => {
    res.sendFile(path.join(__dirname + '/src/addMercaderia.html'))
})

app.get('/administracion/mercaderia/edit/:id', (req, res) => {
    res.sendFile(path.join(__dirname + '/src/updateMercaderia.html'))
})

app.use('/', router)

app.listen(port, () => {
    console.log("Server corriendo en http://localhost:5000")
})
