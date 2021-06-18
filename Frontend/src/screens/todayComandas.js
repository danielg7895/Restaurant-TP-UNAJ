import { getComanda } from "../actions/comandaActions.js";
import {getFormasEntrega} from "../actions/formaEntregaActions.js"
import { LoadComandas } from './comandas.js'


let mercaderiasDiv = document.getElementById("mercaderia-list")

window.onload = async() => {
    LoadComandas()
    
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    var todayComandas = await getComanda(today)
    var formasEntrega = await getFormasEntrega()

    todayComandas.forEach((comanda) => {
        mercaderiasDiv.insertAdjacentHTML('beforeend', ComandaCard(comanda, formasEntrega))
    })

}

const ComandaCard = (comanda, formasEntrega) => {


    return (
        /*html*/`
        <div class="col p-2">
            <div class="card h-100">
                <div class="card-body">
                    <h5 class="card-title">Comanda #${comanda.id}</h5>
                    <p class="card-subtitle mb-2 text-muted">Forma Entrega: ${formasEntrega[comanda.formaEntrega - 1].description}</p>
                </div>

                <div class="card-footer d-flex justify-content-between">
                    <h6>Precio Total: $${comanda.precioTotal}</h6>
                </div>
            </div>
        </div>

        `
    )
}