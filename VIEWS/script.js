
function validarLogin(params) {
    var txtUsuario = document.getElementById("txtUser")
    var txtPassWord = document.getElementById("txtPass")
        console.log(validarUsuario(txtUsuario.value))
    if (validarUsuario(txtUsuario.value)!="ok") {
        alert("Error en el Usuario")
    }else{
        if (validarPass(txtPassWord.value)=="ok") {
            enviarsolicitud(solicitarAcceso(txtUsuario,txtPassWord))     
        }else{
            alert("Error en el Password") 
        }
    }
}
function validarUsuario(user) {
    console.log(user)
    var result="ok"
    
    if ((user.length)!=8) {
        result="Error en el usuario"
    }
    return result
}

function validarPass(pass) {
    console.log(pass)
    var result="ok"
    
    if ((pass.length)<=0) {
        result="Error en el password"
    }
    return result   
}

function irLoguin(params) {
    var workArea=document.getElementById("warea")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/login.html")
    console.log(workArea.getAttributeNode("src"))
}
function irMenuPaciente(params) {
    var workArea=document.getElementById("warea")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/gestionCitas.html")
    console.log(workArea.getAttributeNode("src"))
}
function irSolicitarCita(params) {
    var workArea=document.getElementById("wareaCitas")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/solicitarCita.html")
    console.log(workArea.getAttributeNode("src"))
}

function irConsultarCita(params) {
    var workArea=document.getElementById("wareaCitas")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/consultarCitas.html")
    console.log(workArea.getAttributeNode("src"))
}
function eliminarTd(params) {
    if (window.confirm("desea cancelar la cita?")) {
        alert("Cita cancelada exitosamente !!!")
        
    }
}
function irConsultarOrden(params) {
    var workArea=document.getElementById("wareaCitas")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/consultarServicios.html")
    console.log(workArea.getAttributeNode("src"))
}