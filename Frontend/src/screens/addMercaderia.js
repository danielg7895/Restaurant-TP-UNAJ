import {addMercaderia} from '../actions/mercaderiaActions.js'


let mainDiv = document.getElementById("main")

window.onload = async () => {

    var formElem = document.getElementById("update-form")
    formElem.onsubmit = (e) => {
        e.preventDefault()
        AgregarMercaderia()
    }
}

const AgregarMercaderia = async () => {
    var newMercaderia = {
        nombre : document.getElementById("mercaderia-nombre").value,
        tipoMercaderiaId : document.getElementById("mercaderia-tipoMercaderiaId").value,
        precio : document.getElementById("mercaderia-precio").value,
        ingredientes : document.getElementById("mercaderia-ingredientes").value,
        preparacion : document.getElementById("mercaderia-preparacion").value,
        imagen : document.getElementById("mercaderia-imagen").value,
    }

    var updMerc = await addMercaderia(newMercaderia)
    window.scrollTo(0, 0);
    if (updMerc == null) {
        ShowAlert("danger", "Hubo un error al conectarse a la base de datos.")
    }
    else if (updMerc.status === 400) {
        ShowAlert("danger", "El formato de los datos ingresados es incorrecto.")
    }
    else {
        ShowAlert("success", "La mercaderia fue agregada correctamente.")
        console.log(updMerc)
    }
    
}

const ShowAlert = (type, message) => {
    mainDiv.insertAdjacentHTML('afterbegin', `<div class="alert alert-${type}" role="alert" id="alertMessage">${message}</div>`)
    var alertMessageDiv = document.getElementById("alertMessage")
    setInterval(() => {
        alertMessageDiv.remove()
    }, 3000)
}