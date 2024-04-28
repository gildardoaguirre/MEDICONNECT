
const urlparte1="http://127.0.0.1:5500/VIEWS/"
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
    workArea.setAttribute("src",urlparte1+"login.html")
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
    location.href =urlparte1+"gestionCitas.html"
}
function irMenuProfesional(params) {
    location.href =urlparte1+"gestionOrdenes.html"
}
function irSolicitarCita(params) {
    var workArea=document.getElementById("wareaCitas")
    console.log(workArea)
    workArea.setAttribute("src",urlparte1+"solicitarCita.html")
    console.log(workArea.getAttributeNode("src"))
}

function irConsultarCita(params) {
    var workArea=document.getElementById("wareaCitas")
    console.log(workArea)
    workArea.setAttribute("src",urlparte1+"consultarCitas.html")
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
   
    workArea.setAttribute("src",urlparte1+"consultarServicios.html")
    console.log(workArea.getAttributeNode("src"))
}
function solicitarAcceso(user,pass) {
    irMenu(user)
    
}

function irGestionarCita(params) {
    var workArea=document.getElementById("wareaServicios")
    console.log(workArea)
    workArea.setAttribute("src",urlparte1+"consultarCitas.html")
    console.log(workArea.getAttributeNode("src"))
}
function irGestionarOrden(params) {
    var workArea=document.getElementById("wareaServicios")
    console.log(workArea)
   
    workArea.setAttribute("src",urlparte1+"consultarServicios.html")
    console.log(workArea.getAttributeNode("src"))
}
function IrCreaUsuario(params) {
    var workArea=document.getElementById("warea")
    console.log(workArea)
    workArea.setAttribute("src",urlparte1+"crearUsuario.html")
    console.log(workArea.getAttributeNode("src"))
    
}

function validarFormularioUsuario(params) {
    var txtDocumento=document.getElementById("txtDocumento").value
    var txtFechaNac=document.getElementById("txtFechaNac").value
    var txtelefono=document.getElementById("txtelefono").value
    var txtNombres=document.getElementById("txtNombres").value
    var txtApellidos=document.getElementById("txtApellidos").value
    var txtEmail=document.getElementById("txtEmail").value
    var txtDireccion=document.getElementById("txtDireccion").value
    var pass1=document.getElementById("pass1").value
    var pass2=document.getElementById("pass2").value

    try {
        
        if(!validarCorreo(txtEmail)==""){
            alert("Error en el campo Email")
        }
        if(pass1!=pass2){
            alert("las contraseñas no coinciden")
        }

    } catch (e) {
        alert(e)
    }
    console.log(pass2)
}

function validarCorreo(email) {
    var validEmail =  /^\w+([.-_+]?\w+)*@\w+([.-]?\w+)*(\.\w{2,10})+$/;
	// Using test we can check if the text match the pattern
	if( !validEmail.test(email) ){
        var result= "Email is valid, continue with form submission";
		return result;
	}
    
}
