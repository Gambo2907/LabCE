import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
const CreateActivo = () => {
  const [values, setValues] = useState({
    placa: '',
    tipo: '',
    marca:'',
    fecha_Compra:'',
    req_Aprobador:'',
    id_Estado:'',
    nombre_Lab:'',
    ced_Prof:'',
  })
  const navigate = useNavigate();
  const handleSubmit = (event) =>{
    event.preventDefault();
    axios.post('https://localhost:7215/api/Activo/crear_activo',values)
    .then(res => {
      console.log(res);
      navigate('/activos')
    })
    .catch(err => console.log(err));
  }
  {

  }
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>AGREGAR ACTIVO</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='placa'>PLACA</label>
            <input type='text' name='placa' className='form-control' placeholder='Ingrese la placa'
            onChange={e => setValues({...values, placa: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='tipo'>TIPO</label>
            <input type='text' name='tipo' className='form-control' placeholder='Ingrese el tipo'
            onChange={e => setValues({...values, tipo: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='marca'>MARCA</label>
            <input type='text' name='marca' className='form-control' placeholder='Ingrese la marca'
            onChange={e => setValues({...values,marca: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='fecha_Compra'>fecha_Compra</label>
            <input type='text' name='fecha_Compra' className='form-control' placeholder='Ingrese la fecha_Compra'
            onChange={e => setValues({...values, fecha_Compra: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='req_Aprobador'>req_Aprobador</label>
            <input type='text' name='req_Aprobador' className='form-control' placeholder='req_Aprobador'
            onChange={e => setValues({...values, req_Aprobador: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='id_Estado'>id_Estado</label>
            <input type='text' name='id_Estado' className='form-control' placeholder='Ingrese id_Estado'
            onChange={e => setValues({...values, id_Estado: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='nombre_Lab'>nombre_Lab</label>
            <input type='text' name='nombre_Lab' className='form-control' placeholder='Ingrese nombre_Lab'
            onChange={e => setValues({...values, nombre_Lab: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ced_Prof'>ced_Prof</label>
            <input type='text' name='ced_Prof' className='form-control' placeholder='Ingrese ced_Prof'
            onChange={e => setValues({...values, ced_Prof: e.target.value})}/>
          </div>
          <button className='btn btn-success'>GUARDAR</button>
          <Link to="/activos" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}

export default CreateActivo
