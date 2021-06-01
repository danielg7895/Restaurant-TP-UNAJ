import Header from './components/Header.js'
import Footer from './components/Footer.js'
import Render from './render.js'

import HomeScreen from './screens/HomeScreen.js'
import ComandaScreen from './screens/ComandaScreen.js'



window.onload = () => {
    Render(Header)
    var currentPath = window.location.pathname

    if (currentPath === "/") {
        Render(HomeScreen())
    }
    else if(currentPath === "/comandas") {
        Render(ComandaScreen)
    }


    Render(Footer)
}

