
const Render = async (screen) => {
    var divMain = document.getElementById("app")
    divMain.insertAdjacentHTML('beforeend', await screen)
}

export default Render