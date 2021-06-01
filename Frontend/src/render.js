
const Render = async (screen) => {
    var divMain = document.getElementById("main")
    divMain.insertAdjacentHTML('beforeend', await screen)
}

export default Render