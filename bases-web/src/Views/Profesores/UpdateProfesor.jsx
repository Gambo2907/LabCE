import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';

const UpdateProfesor = () => {

  //const [data, setData] = useState([]);
  const { id } = useParams();
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



  useEffect(()=>{
    axios.get('https://localhost:7215/api/Profesor/lista_profesores/' + id)
    .then(res => {
      setValues(res.data);
    })
    .catch(err => console.log(err));
  },[]);

  const navigatex = useNavigate();
  const handleUpdate = (event) =>{
    event.preventDefault();
    axios.put('https://localhost:7215/api/Profesor/actualizar_profesor?cedula='+id,values)
    .then(res => {
      console.log(res);
      navigatex('/profesores')
    })
    .catch(err => console.log(err));
  }

  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>EDITAR PROFESOR</h1>
        <form onSubmit={handleUpdate}>
          <div className='mb-2'>
            <label htmlFor='cedula'>CEDULA</label>
            <input type='text' name='cedula' className='form-control' placeholder='Ingrese la cedula'
            value={values.cedula} onChange={e => setValues({...values, cedula: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nombre'>NOMBRE</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese el tipo'
            value={values.nombre} onChange={e => setValues({...values, nombre: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap1'>APELLIDO1</label>
            <input type='text' name='ap1' className='form-control' placeholder='Ingrese la marca'
            value={values.ap1} onChange={e => setValues({...values, ap1: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ap2'>APELLIDO2</label>
            <input type='text' name='ap2' className='form-control' placeholder='Ingrese la fecha_Compra'
            value={values.ap2} onChange={e => setValues({...values, ap2: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='correo'>CORREO</label>
            <input type='text' name='correo' className='form-control' placeholder='correo'
            value={values.correo} onChange={e => setValues({...values, correo: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='password'>PASSWORD</label>
            <input type='text' name='password' className='form-control' placeholder='Ingrese id_Estado'
            value={values.password} onChange={e => setValues({...values, password: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nacimiento'>NACIMIENTO</label>
            <input type='text' name='nacimiento' className='form-control' placeholder='Ingrese nombre_Lab'
            value={values.nacimiento} onChange={e => setValues({...values, nacimiento: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='edad'>EDAD</label>
            <input type='text' name='edad' className='form-control' placeholder='Ingrese ced_Prof'
            value={values.edad} onChange={e => setValues({...values, edad: e.target.value})}/>
          </div>
          <button className='btn btn-success'>ACTUALIZAR</button>
          <Link to="/profesores" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}


export default UpdateProfesor