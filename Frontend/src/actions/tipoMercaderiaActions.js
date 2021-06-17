
const backendUrl = 'http://localhost:45148/'

export const getTiposMercaderia = async () => {
    try {
        let response = await fetch(backendUrl + `/api/Mercaderia/tipoMercaderia`)

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}