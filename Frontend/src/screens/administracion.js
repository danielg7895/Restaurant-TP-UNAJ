import { getMercaderias } from "../actions/mercaderiaActions.js"

let tableBodyDiv = document.getElementById("mercaderias-table-body")

window.onload = async () => {
    let mercaderias = await getMercaderias()
    // falta validacion

    mercaderias.forEach((mercaderia) => {
        tableBodyDiv.insertAdjacentHTML("beforeend", MercaderiaTable(mercaderia))
    })
}

const MercaderiaTable = (mercaderia) => {
    return (
        `
            <tr id="mercaderia-${mercaderia.id}">
                <th scope="row">${mercaderia.id}</th>
                <td>${mercaderia.nombre}</td>
                <td>${mercaderia.precio}</td>
                <td>
                <a href="administracion/mercaderia/${mercaderia.id}"><i class="bi bi-pencil-square ps-2 text-warning"></i></a>
                <a href=""><i class="bi bi-x-lg pe-2 text-danger"></i></a>
                
                </td>
            </tr>
        
        `
    )
}