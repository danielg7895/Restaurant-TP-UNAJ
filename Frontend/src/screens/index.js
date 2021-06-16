import { getMercaderias } from "../actions/mercaderiaActions.js"

let mercaderiasDiv = document.getElementById("mercaderia-list")
let loaderDiv = document.getElementById("loader")

window.onload = async () => {
    var mercaderiasJson = await getMercaderias()
    loaderDiv.remove()

    if (mercaderiasJson === null) {
        mercaderiasDiv.insertAdjacentHTML('beforeend', '<div class="alert alert-danger">Error al obtener las mercaderias de la base de datos</div>')
    }

    console.log(mercaderiasJson)

    mercaderiasJson.forEach((mercaderiaJson) => {
        mercaderiasDiv.insertAdjacentHTML('beforeend', MercaderiaCard(mercaderiaJson))
        let btnElem = document.getElementById(`btn-${mercaderiaJson.id}`)
        btnElem.onclick = () => {
            AddToComandas(mercaderiaJson)
        }
    })
}

const MercaderiaCard = (data) => {
    return (
        /*html*/`
        <div class="col">
            <div class="card h-100">
                <img class="card-img-top" style="width: 100%; height: 15vw; object-fit: cover;" src=${data.imagen} alt="Card imagen">
                <div class="card-body">
                    <h5 class="card-title">${data.nombre}</h5>
                    <h6 class="card-subtitle mb-2 text-muted">${data.ingredientes}</h6>
                    <p class="card-text">${data.preparacion}</p>
                </div>

                <div class="card-footer">
                    <a href="#" class="btn btn-primary" id="btn-${data.id}">Agregar a comanda</a>
                </div>
            </div>
        </div>
        `
    )
}

const AddToComandas = (mercaderiaJson) => {

    if (localStorage.length !== 0) {

        let comandas = JSON.parse(localStorage.comandas)
        var mercaderiaExists = false

        comandas.forEach((mercaderia) => {
            if (mercaderia.id === mercaderiaJson.id) {
                mercaderiaExists = true
            }
        })

        if (!mercaderiaExists) {
            console.log(comandas)
            console.log("Adding new comanda...")

            comandas.push(mercaderiaJson)
            localStorage.setItem("comandas", JSON.stringify(comandas))
        }
    }
    else {
        localStorage.setItem("comandas", JSON.stringify([mercaderiaJson]))
    }
}