import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
//Vista para crear un profesor
const CreateProfesor = () => {

  const [values, setValues] = useState({
    cedula: '',
    nombre: '',
    ap1:'',
    ap2:'',
    correo:'',
    password:'',
    nacimiento:'',
    edad:'',
  })
  const navigate = useNavigate();
  //Funcion al aceptar
  const handleSubmit = (event) =>{
    event.preventDefault();
    axios.post('https://localhost:7215/api/Profesor/crear_profesor',values)
    .then(res => {
      console.log(res);
      navigate('/profesores')
    })
    .catch(err => console.log(err));
  }
  {

  }
  //Formulario
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>AGREGAR PROFESOR</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='cedula'>CEDULA</label>
            <input type='text' name='cedula' className='form-control' placeholder='Ingrese la cedula'
            onChange={e => setValues({...values, cedula: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nombre'>NOMBRE</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese el nombre'
            onChange={e => setValues({...values, nombre: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap1'>APPELIDO1</label>
            <input type='text' name='ap1' className='form-control' placeholder='Ingrese el primer apellido'
            onChange={e => setValues({...values,ap1: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap2'>APELLIDO2</label>
            <input type='text' name='ap2' className='form-control' placeholder='Ingrese el segundo apellido'
            onChange={e => setValues({...values, ap2: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='correo'>CORREO</label>
            <input type='text' name='correo' className='form-control' placeholder='Ingrese el correo'
            onChange={e => setValues({...values, correo: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='password'>PASSWORD</label>
            <input type='text' name='password' className='form-control' placeholder='Ingrese la contrasena'
            onChange={e => setValues({...values, password: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nacimiento'>NACIMIENTO</label>
            <input type='text' name='nacimiento' className='form-control' placeholder='Ingrese el ano de nacimiento'
            onChange={e => setValues({...values, nacimiento: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='edad'>EDAD</label>
            <input type='text' name='edad' className='form-control' placeholder='Ingrese la edad'
            onChange={e => setValues({...values, edad: e.target.value})}/>
          </div>
          <button className='btn btn-success'>GUARDAR</button>
          <Link to="/profesores" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}
export default CreateProfesor