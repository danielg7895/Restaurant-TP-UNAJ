import {getMercaderia, updateMercaderia} from '../actions/mercaderiaActions.js'


let mainDiv = document.getElementById("main")

window.onload = async () => {
    let id = location.pathname.split("/").pop()
    let mercaderia = await getMercaderia(id)

    mainDiv.insertAdjacentHTML('beforeend', MercaderiaForm(mercaderia))
    var formElem = document.getElementById("update-form")
    formElem.onsubmit = (e) => {
        e.preventDefault()
        ActualizarMercaderia()
    }

    console.log(mercaderia)
}

const ActualizarMercaderia = async () => {
    var newMercaderia = {
        id : location.pathname.split("/").pop(),
        nombre : document.getElementById("mercaderia-nombre").value,
        tipoMercaderiaId : document.getElementById("mercaderia-tipoMercaderiaId").value,
        precio : document.getElementById("mercaderia-precio").value,
        ingredientes : document.getElementById("mercaderia-ingredientes").value,
        preparacion : document.getElementById("mercaderia-preparacion").value,
        imagen : document.getElementById("mercaderia-imagen").value,
    }

    var updMerc = await updateMercaderia(newMercaderia)
    window.scrollTo(0, 0);
    if (updMerc == null) {
        mainDiv.insertAdjacentHTML('afterbegin', `<div class="alert alert-danger" role="alert">Hubo un error al conectarse a la base de datos.</div>`)

    }
    else if (updMerc.status === 400) {
        mainDiv.insertAdjacentHTML('afterbegin', `<div class="alert alert-danger" role="alert">Hubo un error al actualizar la mercaderia </div>`)
    }
    else {
        mainDiv.insertAdjacentHTML('afterbegin', `<div class="alert alert-success" role="alert">La mercaderia fue actualizada correctamente</div>`)
        console.log(updMerc)
    }
    

}

const MercaderiaForm = (mercaderia) => {
    return (
        /*html*/`
        <form id="update-form">
            <div class="mb-3">
                <label for="mercaderia-id" class="form-label">ID</label>
                <input type="text" class="form-control" id="mercaderia-id" value=${mercaderia.id} disabled>
            </div>
            <div class="mb-3">
                <label for="mercaderia-nombre" class="form-label">Nombre</label>
                <textarea class="form-control" id="mercaderia-nombre" required>${mercaderia.nombre}</textarea>
            </div>
            <div class="mb-3">
                <label for="mercaderia-tipoMercaderiaId" class="form-label">Tipo de mercaderia</label>
                <textarea class="form-control" id="mercaderia-tipoMercaderiaId" required>${mercaderia.tipoMercaderiaId}</textarea>
            </div>
            <div class="mb-3">
                <label for="mercaderia-precio" class="form-label">Precio</label>
                <textarea class="form-control" id="mercaderia-precio" required>${mercaderia.precio}</textarea>
            </div>
            <div class="mb-3">
                <label for="mercaderia-ingredientes" class="form-label">Ingredientes</label>
                <textarea class="form-control" id="mercaderia-ingredientes" required>${mercaderia.ingredientes}</textarea>
            </div>
            <div class="mb-3">
                <label for="mercaderia-preparacion" class="form-label">Preparacion</label>
                <textarea class="form-control" id="mercaderia-preparacion" required>${mercaderia.preparacion}</textarea>
            </div>
            <div class="mb-3">
                <label for="mercaderia-imagen" class="form-label">Imagen</label>
                <textarea class="form-control" id="mercaderia-imagen" required>${mercaderia.imagen}</textarea>
            </div>
            <button type="submit" class="btn btn-primary" id="mercaderia-update-btn">Actualizar mercaderia</button>
        </form>
        
        `
    )
}