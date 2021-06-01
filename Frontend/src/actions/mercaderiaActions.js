const backendUrl = 'http://localhost:45148/'

export const getMercaderia = async (id) => {

    let response = await fetch(backendUrl + `api/mercaderia/${id}`)

    let data = await response.json()

    return data
}


