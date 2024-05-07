import React, { useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';
//Vista para reportar una averia
const RegistroAveria = () => {
  const { id } = useParams();
  const navigate = useNavigate(); // Asegúrate de importar useNavigate

  const [values, setValues] = useState({
    password: '',
    detalle: '',
  });
  //Se realiza la validacion de la contrasena y si se valida se registra la averia
  const handleSubmit = (event) => {
    event.preventDefault();
    
    axios.post('https://localhost:7215/api/Autenticacion/autenticacion_operador', { password: values.password })
      .then(() => {
        console.log("EXITOOOOO");
        axios.put(`https://localhost:7215/api/Activo/averia_activo?placa=${id}`, { detalle: values.detalle })
          .then(res => {
            console.log(res);
            navigate('/operador/devoluciones');
          })
          .catch(err => console.log(err));
      })
      .catch(err => console.log(err));
  };
  //Formulario
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>Registrar avería</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='password'>Ingrese su contraseña</label>
            <input type="password" name='password' className='form-control' placeholder='Ingrese su contraseña'
              onChange={e => setValues({...values, password: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='detalle'>Detalle de la avería</label>
            <input type='text' name='detalle' className='form-control' placeholder='Ingrese el detalle'
              onChange={e => setValues({...values, detalle: e.target.value})}/>
          </div>
          <button className='btn btn-success'>GUARDAR</button>
          <Link to="/operador/devoluciones" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>
    </div>
  );
}

export default RegistroAveria;
