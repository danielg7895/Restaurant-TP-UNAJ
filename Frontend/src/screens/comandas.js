import {getFormasEntrega} from '../actions/formaEntregaActions.js'
import {addComanda} from '../actions/comandaActions.js'

var comandasListDiv = document.getElementById("comandas-list")

export const LoadComandas = async () => {
    ConfigureFormasEntrega()
    var enviarComandaBtn = document.getElementById("enviarComanda-btn")
    enviarComandaBtn.onclick = () => { EnviarComanda() }

    var totalPricediv = document.getElementById("totalPrice")

    if (localStorage.length === 0) {
        comandasListDiv.innerHTML = `<div class="alert alert-info" role="alert">No hay mercaderias agregadas a la comanda. <a href="/">Ver mercaderias</a></div>`
        return
    }

    var comandas = JSON.parse(localStorage.comandas)
    var totalPrice = 0

    comandas.forEach((comanda) => {
        totalPrice += comanda.precio
        var comandaHtml = ComandaRow(comanda)
        comandasListDiv.insertAdjacentHTML('beforeend', comandaHtml)

        var deleteDiv = document.getElementById(`p-${comanda.id}`)
        deleteDiv.onclick = () => {
            RemoveFromComandas(comanda.id)
        }
    })

    totalPricediv.innerHTML = `$ ${totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}`

}


const ComandaRow = (data) => {
    return (
    `
    <div class="row border-bottom p-2" id="comanda-${data.id}">
        <div class="col-2">
            <img style="height: 50px; width: 50px" src=${data.imagen} alt=${data.nombre}>
        </div>

        <div class="col d-flex ps-4 align-self-center">
            ${data.nombre}
        </div>

        <div class="col fw-bold d-flex justify-content-center align-self-center">
            $ ${data.precio.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}
            <div class="col fw-bold d-flex justify-content-center align-self-center" id="p-${data.id}">
               <i class="bi bi-x text-danger fw-bold h5" style="cursor: pointer; font-size: 30px"></i>
            </div>
        </div>
    </div>
    `
    )
}

const RemoveFromComandas = (mercaderiaId, parentDiv) => {

    if (localStorage.length !== 0) {

        let comandas = JSON.parse(localStorage.comandas)

        comandas.forEach((mercaderia) => {
            if (mercaderia.id === mercaderiaId) {
                comandas.splice(comandas.indexOf(mercaderia), 1)

                localStorage.setItem("comandas", JSON.stringify(comandas))
            }
        })

        if (comandas.length === 0) {
            localStorage.clear()
        }

        document.getElementById(`comanda-${mercaderiaId}`).remove()

        location.reload();
    }
}

const ConfigureFormasEntrega = async () => {
    var formaEntregaDropdownMenu = document.getElementById("formaEntregaDropdownMenu")
    var formaEntregaDropdownBtn = document.getElementById("formaEntregaDropdownBtn")
    var formasEntrega = await getFormasEntrega()
    
    var currentFormaEntrega = localStorage.getItem("FormaEntrega")
    document.getElementById("enviarComanda-btn").disabled = true

    if (currentFormaEntrega !== null) {
        formaEntregaDropdownBtn.innerHTML = formasEntrega[parseInt(currentFormaEntrega) -1].description
    }
    else {
        document.getElementById("enviarComanda-btn").classList.add("disabled")
    }

    formasEntrega.forEach( (formaEntrega) => {
        formaEntregaDropdownMenu.insertAdjacentHTML("beforeend", `<li><a class="dropdown-item" id="formaEntrega-${formaEntrega.id}" href="#">${formaEntrega.description}</a></li>`)

        document.getElementById(`formaEntrega-${formaEntrega.id}`).onclick = () => {
            localStorage.setItem("FormaEntrega", formaEntrega.id)
            formaEntregaDropdownBtn.innerHTML = document.getElementById(`formaEntrega-${formaEntrega.id}`).innerHTML
            document.getElementById("enviarComanda-btn").classList.remove("disabled")
        }
    })
}

const EnviarComanda = async () => {
    var mercaderias = JSON.parse(localStorage.getItem("comandas"))
    var formaEntrega = localStorage.getItem("FormaEntrega")

    var comanda = {
        'formaEntrega': formaEntrega,
        'mercaderia' : [

        ]
    }

    mercaderias.forEach( (mercaderia) => {
        comanda.mercaderia.push(mercaderia.id)
    })
    
    var comandaResponse = await addComanda(comanda)
    if (comandaResponse.status === undefined) {
        ShowAlert("success", "La comanda fue enviada correctamente!")
        ClearComandas()
    } else {
        ShowAlert("danger", "Hubo un error al enviar la comanda")
    }

}

const ClearComandas = () => {
    var mercaderias = JSON.parse(localStorage.getItem("comandas"))
    document.getElementById("enviarComanda-btn").classList.add("disabled")

    mercaderias.forEach( (mercaderia) => {
        document.getElementById(`comanda-${mercaderia.id}`).remove()
    })
    localStorage.clear()
    document.getElementById("totalPrice").innerHTML = "$ 0"
}

const ShowAlert = (type, message) => {
    comandasListDiv.insertAdjacentHTML('beforebegin', `<div class="alert alert-${type}" role="alert" id="alertMessage">${message}</div>`)
    var alertMessageDiv = document.getElementById("alertMessage")
    setInterval(() => {
        alertMessageDiv.remove()
        comandasListDiv.innerHTML = `<div class="alert alert-info" role="alert">No hay mercaderias agregadas a la comanda. <a href="/">Ver mercaderias</a></div>`
    }, 3000)
}