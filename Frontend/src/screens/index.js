import { getMercaderias } from "../actions/mercaderiaActions.js"
import {LoadComandas} from './comandas.js'

let mercaderiasDiv = document.getElementById("mercaderia-list")
let loaderDiv = document.getElementById("loader")

window.onload = async () => {
    var mercaderiasJson = await getMercaderias()
    loaderDiv.remove()
    LoadComandas()

    if (mercaderiasJson === null) {
        mercaderiasDiv.insertAdjacentHTML('beforeend', '<div class="alert alert-danger">Error al obtener las mercaderias de la base de datos</div>')
    }

    mercaderiasJson.forEach((mercaderiaJson) => {
        mercaderiasDiv.insertAdjacentHTML('beforeend', MercaderiaCard(mercaderiaJson))

        let btnElem = document.getElementById(`btn-${mercaderiaJson.id}`)
        btnElem.onclick = () => {
            AddToComandas(mercaderiaJson)
        }

        SetupModal(mercaderiaJson)
    })
}

const MercaderiaCard = (data) => {
    return (
        /*html*/`
        <div class="col p-2">
            <div class="card h-100">
                <a class="modal-toggle-${data.id}" style="cursor: pointer;" data-bs-toggle="modal" data-bs-target="#mercaderiaModal">
                    <img class="card-img-top" style="width: 100%; height: 15vw; object-fit: cover;" src=${data.imagen} alt="Card imagen">
                </a>
                <div class="card-body">
                    <a class="modal-toggle-${data.id} text-decoration-none text-dark" style="cursor: pointer;" data-bs-toggle="modal" data-bs-target="#mercaderiaModal">
                        <h5 class="card-title">${data.nombre}</h5>
                    </a>
                    <p class="card-subtitle mb-2 text-muted">${data.ingredientes}</p>
                </div>

                <div class="card-footer d-flex justify-content-between">
                    <h4 >$${data.precio}</h4>
                    <a style="cursor:pointer;" class="btn btn-primary ms-auto" id="btn-${data.id}">Agregar a comanda</a>
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

const SetupModal = (mercaderiaJson) => {
    let modalsTriggerElem = document.getElementsByClassName(`modal-toggle-${mercaderiaJson.id}`)

    Array.from(modalsTriggerElem).forEach((a) => {
        a.onclick = () => {
            document.getElementById("modalTitle").innerHTML = mercaderiaJson.nombre
            
            document.getElementById("modalBody").innerHTML = /*html*/`
                <div class="col">
                    <img style="width: 100%; height: 15vw; object-fit: cover;" src=${mercaderiaJson.imagen}>
                    <h6 class="card-subtitle mb-2 text-muted mt-2">Ingredientes: ${mercaderiaJson.ingredientes}</h6>
                    <p class="card-text mt-4">Preparacion: ${mercaderiaJson.preparacion}</p>
                </div>
            `

            document.getElementById("modalFooter").innerHTML = `
                <h4 class="me-auto">Precio: $${mercaderiaJson.precio}</h4>
                <a class="btn btn-primary" id="modal-btn-${mercaderiaJson.id}">Agregar a comanda</a>
            `

            document.getElementById(`modal-btn-${mercaderiaJson.id}`).onclick = () => {
                AddToComandas(mercaderiaJson)
            }
        }
    })



}