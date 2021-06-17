const backendUrl = 'http://localhost:45148/'

export const getMercaderia = async (id) => {

    try {
        let response = await fetch(backendUrl + `api/mercaderia/${id}`)

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}

export const getMercaderias = async () => {
    try {
        let response = await fetch(backendUrl + `api/mercaderia`)

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}

export const updateMercaderia = async (data) => {
    try {
        let response = await fetch(backendUrl + `api/mercaderia/${data.id}`, {
            method: "PUT",
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


export const addMercaderia = async (data) => {
    try {
        let response = await fetch(backendUrl + `api/mercaderia`, {
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

export const deleteMercaderia = async (id) => {
    try {
        let response = await fetch(backendUrl + `api/mercaderia/${id}`, {
            method: "DELETE",
        })

        return await response.json()
    }
    catch (e) {
        console.error(e)
    }

    return null
}
