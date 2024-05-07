import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css'
//Vista para realizar el registro de los profesores
const Registro = () => {
  const [values, setValues] = useState({
    carnet: '',
    cedula: '',
    correo: '',
    password: '',
    nombre:'',
    ap1:'',
    ap2:'',
    nacimiento: null,
    edad:0,
    aprobado: false,
  })
  const navigate = useNavigate();
  const handleSubmit = (event) =>{

    const formattedValues = {
      ...values,
      nacimiento: values.nacimiento ? values.nacimiento.toISOString().split('T')[0] : null
    };

    event.preventDefault();
    axios.post('https://localhost:7215/api/Operador/registrar_operador', formattedValues)
    .then(res => {
      console.log(res);
      navigate('/loginoperador')
    })
    .catch(err => console.log(err));
  }
  {

  }
  //Formulario
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>REGISTRO DE OPERADOR</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='nombre'>CARNET</label>
            <input type='text' name='carnet' className='form-control' placeholder='Ingrese el carnet'
            onChange={e => setValues({...values, carnet: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='cedula'>CEDULA</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese la cedula'
            onChange={e => setValues({...values, cedula: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='correo'>CORREO</label>
            <input type='text' name='correo' className='form-control' placeholder='Ingrese su correo'
            onChange={e => setValues({...values,correo: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='password'>CONTRASENA</label>
            <input type='text' name='password' className='form-control' placeholder='Ingrese su contrasena'
            onChange={e => setValues({...values, password: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nombre'>NOMBRE</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese su nombre'
            onChange={e => setValues({...values, nombre: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap1'>PRIMER APELLIDO</label>
            <input type='text' name='ap1' className='form-control' placeholder='Ingrese su primer apellido'
            onChange={e => setValues({...values, ap1: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap2'>SEGUNDO APELLIDO</label>
            <input type='text' name='ap2' className='form-control' placeholder='Ingrese susegundo apellido'
            onChange={e => setValues({...values, ap2: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='fecha_Compra'>Fecha de nacimiento</label>
            <h1></h1>
            <DatePicker
  
              selected={values.nacimiento}
              onChange={date => setValues({...values, nacimiento: date})}
              dateFormat="yyyy-MM-dd"
              className='form-control'
          
            />
          </div>
          <button className='btn btn-success'>REGISTRAR</button>
          <Link to="/loginoperador" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}

export default Registro