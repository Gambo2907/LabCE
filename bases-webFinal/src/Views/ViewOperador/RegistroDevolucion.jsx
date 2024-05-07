import React, { useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';
//Vista para registrar las devoluciones de activos
const RegistroDevolucion = () => {
    const { id } = useParams();
    const navigate = useNavigate(); // Asegúrate de importar useNavigate
  
    const [values, setValues] = useState({
      password: '',

    });
  //Valida la contrasena y registra la devolucion
    const handleSubmit = (event) => {
      event.preventDefault();
      
      axios.post('https://localhost:7215/api/Autenticacion/autenticacion_operador', { password: values.password })
        .then(() => {
          console.log("EXITOOOOO");
          axios.put(`https://localhost:7215/api/Activo/devolucion_activo?placa=${id}`)
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
          <h1>Registrar devolución</h1>
          <form onSubmit={handleSubmit}>
            <div className='mb-2'>
              <label htmlFor='password'>Ingrese su contraseña</label>
              <input type="password" name='password' className='form-control' placeholder='Ingrese su contraseña'
                onChange={e => setValues({...values, password: e.target.value})}/>
            </div>
            <button className='btn btn-success'>GUARDAR</button>
            <Link to="/operador/devoluciones" className='btn btn-primary ms-3'>VOLVER</Link>
          </form>
        </div>
      </div>
    );
  }
  

export default RegistroDevolucion