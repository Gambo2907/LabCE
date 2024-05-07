import Swal from "sweetalert2";
import storage from "./Storage/storage";

export const show_alerta = (msj, icon) =>{
    Swal.fire({ title:msj, icon:icon, buttonsStyling:true});
}

export const sendRequest = async(method, params, url,redir='',token=false) => {
    if(token){
        const authToken = storage.get('authToken');
        axios.defaults.headers.commo['Autorization'] = 'Bearer ' +authToken;
    }
    let res;
    await axios({method:method, url:url, data:params}).then(
        response =>{
            res = response.data,
            (method != 'GET') ? show_alerta(response.data.message,'succes'):'',
            setTimeout( ()=>
        (redir !='') ? window.location.href = redir : '',2000)
        }).catch( (erros) =>{
            console.log("hay errores");
        })
    return res;

}

export const confirmation = async(name, url, redir) => {
    const alert = Swal.mixin({buttonsStyling:true});
    alert.fire({
        title:'aaaaaaaaaa',
        iconL:'question',showCancelButton:true,
        confirmButtonText:'<i class="fa-solid fa-check"></i> Yes, delete',
        cancelButtonText: '<i class="fa-solid fa-ban"></i> Cancel'
    }).then( (result)=>{
        if(result.isConfirmed){
            sendRequest('DELETE',{},url,redir);
        }
    });

}



export default show_alerta;