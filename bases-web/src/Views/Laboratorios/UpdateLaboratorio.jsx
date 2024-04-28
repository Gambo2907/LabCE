import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';

const UpdateLaboratorio = () => {
  //const [data, setData] = useState([]);
  const { id } = useParams();
  const [values, setValues] = useState({

    nombre: '',
    hora_Inicio:'',
    hora_Final:'',
    capacidad:'',
    computadores:'',
    facilidades:'',
  })



  useEffect(()=>{
    axios.get('https://localhost:7215/api/Laboratorio/lista_labs/' + id)
    .then(res => {
      setValues(res.data);
    })
    .catch(err => console.log(err));
  },[]);

  const navigatex = useNavigate();
  const handleUpdate = (event) =>{
    event.preventDefault();
    axios.put('https://localhost:7215/api/Laboratorio/actualizarlab?nombre='+id,values)
    .then(res => {
      console.log(res);
      navigatex('/laboratorios')
    })
    .catch(err => console.log(err));
  }

  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>EDITAR LABORATORIO</h1>
        <form onSubmit={handleUpdate}>
          <div className='mb-2'>
            <label htmlFor='nombre'>nombre</label>
            <input type='text' name='nombre' className='form-control' placeholder='Ingrese la cedula'
            value={values.nombre} onChange={e => setValues({...values, nombre: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='hora_Inicio'>hora_Inicio</label>
            <input type='text' name='hora_Inicio' className='form-control' placeholder='Ingrese el tipo'
            value={values.hora_Inicio} onChange={e => setValues({...values, hora_Inicio: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='hora_Final'>hora_Final</label>
            <input type='text' name='hora_Final' className='form-control' placeholder='Ingrese la marca'
            value={values.hora_Final} onChange={e => setValues({...values, hora_Final: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='capacidad'>capacidad</label>
            <input type='text' name='capacidad' className='form-control' placeholder='Ingrese la fecha_Compra'
            value={values.capacidad} onChange={e => setValues({...values, capacidad: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='computadores'>computadores</label>
            <input type='text' name='computadores' className='form-control' placeholder='correo'
            value={values.computadores} onChange={e => setValues({...values, computadores: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='facilidades'>facilidades</label>
            <input type='text' name='facilidades' className='form-control' placeholder='Ingrese id_Estado'
            value={values.facilidades} onChange={e => setValues({...values, facilidades: e.target.value})}/>
          </div>
          <button className='btn btn-success'>ACTUALIZAR</button>
          <Link to="/laboratorios" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}

export default UpdateLaboratorio