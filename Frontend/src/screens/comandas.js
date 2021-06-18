import {getFormasEntrega} from '../actions/formaEntregaActions.js'
import {addComanda} from '../actions/comandaActions.js'

var comandaAlertDiv = document.getElementById("comandaAlert")
var comandasListDiv = document.getElementById("comandas-list")
var totalPricediv = document.getElementById("totalPrice")
var enviarComandaBtn = document.getElementById("enviarComanda-btn")
var formaEntregaDropdownMenu = document.getElementById("formaEntregaDropdownMenu")
var formaEntregaDropdownBtn = document.getElementById("formaEntregaDropdownBtn")

export const LoadComandas = async () => {
    ConfigureFormasEntrega()

    enviarComandaBtn.onclick = () => { EnviarComanda() }

    if (localStorage.comandas === undefined) {
        comandaAlertDiv.innerHTML = `<div class="alert alert-info" role="alert" id="comandaAlert">No hay mercaderias agregadas a la comanda. <a href="/">Ver mercaderias</a></div>`
        return
    }

    var mercaderias = JSON.parse(localStorage.comandas)

    mercaderias.forEach((mercaderia) => {
       AddToComandas(mercaderia)
    })

}

export const AddToComandas = (mercaderia) => {
    comandaAlertDiv.innerHTML = ""
    
    // updating total price
    var currentPrice = parseFloat(totalPricediv.innerHTML.split("$").pop().trim().replace('.', ''))
    currentPrice += parseFloat(mercaderia.precio)
    totalPricediv.innerHTML = `$ ${currentPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}`

    var comandaHtml = ComandaRow(mercaderia)
    comandasListDiv.insertAdjacentHTML('beforeend', comandaHtml)

    var deleteDiv = document.getElementById(`p-${mercaderia.id}`)
    deleteDiv.onclick = () => {
        RemoveFromComandas(mercaderia)
    }

    ToggleButton()
}

const RemoveFromComandas = (mercaderia) => {

    if (localStorage.length !== 0) {

        let comandas = JSON.parse(localStorage.comandas)

        comandas.forEach((merc) => {
            if (merc.id === mercaderia.id) {
                comandas.splice(comandas.indexOf(merc), 1)

                localStorage.setItem("comandas", JSON.stringify(comandas))
            }
        })

        document.getElementById(`comanda-${mercaderia.id}`).remove()
        
        if (comandas.length === 0) {
            localStorage.removeItem("comandas")
            comandaAlertDiv.innerHTML = `<div class="alert alert-info" role="alert">No hay mercaderias agregadas a la comanda. <a href="/">Ver mercaderias</a></div>`
            ToggleButton()
        }

        var currentPrice = parseFloat(totalPricediv.innerHTML.split("$").pop().trim().replace('.', ''))
        currentPrice -= parseFloat(mercaderia.precio)
        totalPricediv.innerHTML = `$ ${currentPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}`
    }
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
               <i class="bi bi-x text-danger fw-bold h3 d-pointer"></i>
            </div>
        </div>
    </div>
    `
    )
}

const ToggleButton = () => {
    if (localStorage.comandas === undefined || localStorage.FormaEntrega === undefined) {
        enviarComandaBtn.classList.remove('disabled')
        enviarComandaBtn.classList.add('disabled')
    } else {
        enviarComandaBtn.classList.remove('disabled')
    }
}

const ConfigureFormasEntrega = async () => {

    var formasEntrega = await getFormasEntrega()
    
    var currentFormaEntrega = localStorage.getItem("FormaEntrega")

    if (currentFormaEntrega !== null) {
        formaEntregaDropdownBtn.innerHTML = formasEntrega[parseInt(currentFormaEntrega) -1].description
    }

    formasEntrega.forEach( (formaEntrega) => {
        formaEntregaDropdownMenu.insertAdjacentHTML("beforeend", `<li><a class="dropdown-item d-pointer" id="formaEntrega-${formaEntrega.id}">${formaEntrega.description}</a></li>`)

        document.getElementById(`formaEntrega-${formaEntrega.id}`).onclick = () => {
            localStorage.setItem("FormaEntrega", formaEntrega.id)
            formaEntregaDropdownBtn.innerHTML = document.getElementById(`formaEntrega-${formaEntrega.id}`).innerHTML
            ToggleButton()
        }
    })
    
    ToggleButton()
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
    localStorage.removeItem("comandas")
    document.getElementById("totalPrice").innerHTML = "$ 0"
}

const ShowAlert = (type, message) => {
    comandasListDiv.insertAdjacentHTML('beforebegin', `<div class="alert alert-${type}" role="alert" id="alertMessage">${message}</div>`)
    var alertMessageDiv = document.getElementById("alertMessage")
    setInterval(() => {
        alertMessageDiv.remove()
        comandaAlertDiv.innerHTML = `<div class="alert alert-info" role="alert">No hay mercaderias agregadas a la comanda. <a href="/">Ver mercaderias</a></div>`
    }, 3000)
}