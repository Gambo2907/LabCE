import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
//Vista para cambiar la contrasena
const CambioPassword = () => {

  //const [data, setData] = useState([]);

  const navigatex = useNavigate();

  const handleUpdate = (event) =>{
    event.preventDefault();
    axios.put('https://localhost:7215/api/Profesor/cambio_password_profesor?cedula='+(JSON.parse(localStorage.getItem('data'))).cedula)
    .then(res => {
      console.log(res);

      localStorage.removeItem('data');
      localStorage.removeItem('user-info');
      localStorage.removeItem('userProfesor')
      go('/loginprofesor');
      
   
      //AGREGAR UNA ALERTA
    })
    .catch(err => console.log(err));
  }
//Formulario
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>CAMBIAR CONTRASEÑA</h1>
        <form onSubmit={handleUpdate}>
          <button className='btn btn-success'>GENERAR NUEVA CONTRSEÑA</button>
          <Link to="/profesores" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}


export default CambioPassword