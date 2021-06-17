
const backendUrl = 'http://localhost:45148/'

export const getFormasEntrega = async () => {
    try {
        let response = await fetch(backendUrl + `api/Comanda/formaEntrega`)

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}