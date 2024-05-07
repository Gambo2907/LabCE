import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

//Vista para actualizar los activos
const UpdateActivo = () => {


  //const [data, setData] = useState([]);
  const { id } = useParams();// Obtiene el ID del activo de los parámetros de la URL
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
  
  // Maneja el cambio del checkbox "Requiere Aprobador"
  const handleCheckboxChange = (event) => {
    const { checked } = event.target;
    setValues(prevValues => ({
      ...prevValues,
      req_Aprobador: checked
    }));
  };

 // Obtiene los datos del activo para prellenar el formulario al cargar el componente
  useEffect(()=>{
    axios.get('https://localhost:7215/api/Activo/lista_activos/' + id)
    .then(res => {
      setValues(res.data);
    })
    .catch(err => console.log(err));
  },[]);

  const navigatex = useNavigate();
    // Función para manejar la actualización del activo
  const handleUpdate = (event) =>{
    event.preventDefault();
    axios.put('https://localhost:7215/api/Activo/actualizar_activo?placa='+id,values)
    .then(res => {
      console.log(res);
      navigatex('/activos')
    })
    .catch(err => console.log(err));
  }
  //Formulario para solicitar los datos
  return (
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>EDITAR ACTIVO</h1>
        <form onSubmit={handleUpdate}>
          <div className='mb-2'>
            <label htmlFor='placa'>PLACA</label>
            <input type='text' name='placa' className='form-control' placeholder='Ingrese la placa'
            value={values.placa} onChange={e => setValues({...values, placa: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='tipo'>TIPO</label>
            <input type='text' name='tipo' className='form-control' placeholder='Ingrese el tipo'
            value={values.tipo} onChange={e => setValues({...values, tipo: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='marca'>MARCA</label>
            <input type='text' name='marca' className='form-control' placeholder='Ingrese la marca'
            value={values.marca} onChange={e => setValues({...values, marca: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='fecha_Compra'>fecha_Compra</label>
            <input type='text' name='fecha_Compra' className='form-control' placeholder='Ingrese la fecha_Compra'
            value={values.fecha_Compra} onChange={e => setValues({...values, fecha_Compra: e.target.value})}/>
          </div>
          
          <div className='mb-2'>
            <label htmlFor='req_Aprobador'>Requiere Aprobador</label>
            <input 
              type='checkbox' 
              name='req_Aprobador' 
              checked={values.req_Aprobador}
              onChange={handleCheckboxChange}
            />
          </div>
          <div className='mb-2'>
            <label htmlFor='id_Estado'>Estado</label>
            <select name='id_Estado' className='form-control' value={values.id_Estado} onChange={e => setValues({...values, id_Estado: e.target.value})}>
              <option value='4'></option>
              <option value='1'>Disponible</option>
              <option value='2'>Prestado</option>
              <option value='3'>Averiado</option>
            </select>
          </div>
          <div className='mb-2'>
            <label htmlFor='nombre_Lab'>nombre_Lab</label>
            <input type='text' name='nombre_Lab' className='form-control' placeholder='Ingrese nombre_Lab'
            value={values.nombre_Lab} onChange={e => setValues({...values, nombre_Lab: e.target.value})}/>
          </div>
          <div className='mb-2'>
            <label htmlFor='ced_Prof'>ced_Prof</label>
            <input type='text' name='ced_Prof' className='form-control' placeholder='Ingrese ced_Prof'
            value={values.ced_Prof} onChange={e => setValues({...values, ced_Prof: e.target.value})}/>
          </div>
          <button className='btn btn-success'>ACTUALIZAR</button>
          <Link to="/activos" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>

    </div>
  )
}

export default UpdateActivo