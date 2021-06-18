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

export const getComanda = async (date=null) => {
    try {
        let response = null

        if (date === null) {
            response = await fetch(backendUrl + `api/comanda`)
        }
        else {
            response = await fetch(backendUrl + `api/comanda?fecha=${date}`)
        }

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}