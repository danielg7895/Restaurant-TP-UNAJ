
const ComandaCard = (data) => {
    return (
        /*html*/`
        <div>
            <div class="card" style="width: 18rem;">
                <img class="card-img-top" src=${data.image} alt="Card imagen">
                <div class="card-body">
                <h5 class="card-title">${data.name}</h5>
                <h6 class="card-subtitle mb-2 text-muted">${data.ingredients}</h6>
                <p class="card-text">${data.preparation}</p>
                <a href="#" class="btn btn-primary">Agregar a comanda</a>
            </div>
        </div>
        
        </div>
        
        `
    )
}

export default ComandaCard