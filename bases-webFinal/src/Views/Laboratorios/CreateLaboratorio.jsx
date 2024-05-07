import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
//Vista para crear laboratorios
const CreateLaboratorio = () => {
  const [values, setValues] = useState({
    nombre: '',
    hora_Inicio:'',
    hora_Final:'',
    capacidad:'',
    computadoresd:'',
    facilidades:'',
  })
  const navigate = useNavigate();
  //Función para manejar el envío del formulario
  const handleSubmit = (event) =>{
    event.preventDefault();
    axios.post('https://localhost:7215/api/Laboratorio/crearlab',values)
    .then(res => {
      console.log(res);
      navigate('/laboratorios')
    })
    .catch(err => console.log(err));
  }
  {

  }
  //Formulario
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>AGREGAR LABORATORIO</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='nombre'>NOMBRE</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese la cedula'
            onChange={e => setValues({...values, nombre: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='hora_Inicio'>hora_Inicio</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese el nombre'
            onChange={e => setValues({...values, hora_Inicio: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='hora_Final'>hora_Final</label>
            <input type='text' name='hora_Final' className='form-control' placeholder='Ingrese el primer apellido'
            onChange={e => setValues({...values,hora_Final: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='capacidad'>capacidad</label>
            <input type='text' name='capacidad' className='form-control' placeholder='Ingrese el segundo apellido'
            onChange={e => setValues({...values, capacidad: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='computadores'>computadores</label>
            <input type='text' name='correo' className='form-control' placeholder='Ingrese el correo'
            onChange={e => setValues({...values, computadores: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='facilidades'>facilidades</label>
            <input type='text' name='facilidades' className='form-control' placeholder='Ingrese la contrasena'
            onChange={e => setValues({...values, facilidades: e.target.value})}/>
          </div>
          <button className='btn btn-success'>GUARDAR</button>
          <Link to="/laboratorios" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}
export default CreateLaboratorio