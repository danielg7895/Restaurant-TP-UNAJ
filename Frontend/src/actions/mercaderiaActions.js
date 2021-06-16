const backendUrl = 'http://localhost:45148/'

export const getMercaderia = async (id) => {

    let response = await fetch(backendUrl + `api/mercaderia/${id}`)

    let data = await response.json()

    return data
}

export const getMercaderias = async () => {
    try {
        let response = await fetch(backendUrl + `api/mercaderia`)
        let productos = await response.json()

        return productos
    }
    catch (e) {
        console.error(e)
    }

    return null
}
