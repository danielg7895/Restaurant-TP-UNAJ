import Card from '../components/ComandaCard.js'
import Render from '../render.js'
import { getMercaderia } from '../actions/mercaderiaActions.js'


const HomeScreen = async () => {

    var mercaderia = await getMercaderia(4)
    Render(Card(mercaderia))

    return (
        /*html*/`
            <div> Home </div>
        `
    )
    
}

export default HomeScreen