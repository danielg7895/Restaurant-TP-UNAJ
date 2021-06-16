var comandasListDiv = document.getElementById("comandas-list")

window.onload = () => {
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
        <div class="col-1">
            <img style="height: 50px; width: 50px" src=${data.imagen} alt=${data.nombre}>
        </div>

        <div class="col-6 h5 d-flex ps-4 align-self-center">
            ${data.nombre}
        </div>

        <div class="col-2 fw-bold d-flex justify-content-center align-self-center">
            $ ${data.precio.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}
        </div>
        <div class="col-2 fw-bold d-flex justify-content-center align-self-center" id="p-${data.id}">
           <i class="bi bi-x text-danger fw-bold h5" style="cursor: pointer; font-size: 30px"></i>
        </div>
    </div>
    `
    )
}

const RemoveFromComandas = (productId, parentDiv) => {

    if (localStorage.length !== 0) {

        let cart = JSON.parse(localStorage.comandas)

        cart.forEach((producto) => {
            if (producto.id === productId) {
                cart.splice(cart.indexOf(producto), 1)

                localStorage.setItem("comandas", JSON.stringify(cart))
            }
        })

        if (cart.length === 0) {
            localStorage.clear()
        }

        document.getElementById(`comanda-${productId}`).remove()

        location.reload();
    }
}