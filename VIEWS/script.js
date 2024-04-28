
function validarLogin(params) {
    var txtUsuario = document.getElementById("txtUser")
    var txtPassWord = document.getElementById("txtPass")
        console.log(validarUsuario(txtUsuario.value))
    if (validarUsuario(txtUsuario.value)!="ok") {
        alert("Error en el Usuario")
    }else{
        if (validarPass(txtPassWord.value)=="ok") {
            solicitarAcceso(txtUsuario.value,txtPassWord)
            

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
function irMenu(perfil) {

    if (perfil=="AAAAAAAA") {
        irMenuPaciente()
    }
    if (perfil=="BBBBBBBB") {
        irMenuProfesional()
    }
    
}
function irMenuPaciente(params) {
    location.href ="http://127.0.0.1:5500/VIEWS/gestionCitas.html"
}
function irMenuProfesional(params) {
    location.href ="http://127.0.0.1:5500/VIEWS/gestionOrdenes.html"
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
function solicitarAcceso(user,pass) {
    irMenu(user)
    
}

function irGestionarCita(params) {
    var workArea=document.getElementById("wareaServicios")
    console.log(workArea)
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/consultarCitas.html")
    console.log(workArea.getAttributeNode("src"))
}
function irGestionarOrden(params) {
    var workArea=document.getElementById("wareaServicios")
    console.log(workArea)
   
    workArea.setAttribute("src","http://127.0.0.1:5500/VIEWS/consultarServicios.html")
    console.log(workArea.getAttributeNode("src"))
}