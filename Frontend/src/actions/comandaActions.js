const backendUrl = 'http://localhost:45148/'


export const addComanda = async (data) => {
    try {
        let response = await fetch(backendUrl + `api/comanda`, {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                "content-type": "application/json"
            }
        })

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}