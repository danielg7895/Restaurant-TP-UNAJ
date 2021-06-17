import { getMercaderias, deleteMercaderia } from "../actions/mercaderiaActions.js"

let tableBodyDiv = document.getElementById("mercaderias-table-body")
let appDiv = document.getElementById("app")

window.onload = async () => {
    let mercaderias = await getMercaderias()

    mercaderias.forEach((mercaderia) => {
        tableBodyDiv.insertAdjacentHTML("beforeend", MercaderiaTable(mercaderia))
        document.getElementById(`deleteBtn-${mercaderia.id}`).onclick = () => {
            EliminarMercaderia(mercaderia.id)
        }
    })
}

const ShowConfirmation = () => {
}

const EliminarMercaderia = async (id) => {
    var deleted = await deleteMercaderia(id)
    window.scrollTo(0, 0);
    console.log(deleted)
    
    if (deleted.Status !== undefined) {
        ShowAlert("success", "La mercaderia fue eliminada correctamente")
    } 
    else {
        ShowAlert("danger", "Hubo un error al eliminar la mercaderia")
    }
}

const MercaderiaTable = (mercaderia) => {
    return (
        `
            <tr id="mercaderia-${mercaderia.id}">
                <th scope="row">${mercaderia.id}</th>
                <td>${mercaderia.nombre}</td>
                <td>${mercaderia.precio}</td>
                <td>
                <a href="/administracion/mercaderia/edit/${mercaderia.id}"><i class="bi bi-pencil-square ps-2 text-warning"></i></a>
                <a style="cursor:pointer;" id="deleteBtn-${mercaderia.id}"><i class="bi bi-x-lg pe-2 text-danger"></i></a>
                </td>
            </tr>
        
        `
    )
}

const ShowAlert = (type, message) => {
    appDiv.insertAdjacentHTML('afterbegin', `<div class="alert alert-${type}" role="alert" id="alertMessage">${message}</div>`)
    var alertMessageDiv = document.getElementById("alertMessage")
    setInterval(() => {
        alertMessageDiv.remove()
    }, 3000)
}