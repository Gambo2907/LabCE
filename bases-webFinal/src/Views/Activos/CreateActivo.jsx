import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
//Vista para crear activos
const CreateActivo = () => {
  //Valores a enviar
  const [values, setValues] = useState({
    placa: '',
    tipo: '',
    marca:'',
    fecha_Compra: null, // Change to null for datepicker
    req_Aprobador: false,
    id_Estado:1,
    nombre_Lab:'',
    ced_Prof:'',
  });

  const navigate = useNavigate();
  //Funcion al presionar aceptar
  const handleSubmit = (event) =>{
    event.preventDefault();
    // Formatting the date before sending
    const formattedValues = {
      ...values,
      fecha_Compra: values.fecha_Compra ? values.fecha_Compra.toISOString().split('T')[0] : null
    };
    axios.post('https://localhost:7215/api/Activo/crear_activo', formattedValues)
    .then(res => {
      console.log(res);
      navigate('/activos');
    })
    .catch(err => console.log(err));
  };


  const handleCheckboxChange = (event) => {
    const { checked } = event.target;
    setValues(prevValues => ({
      ...prevValues,
      req_Aprobador: checked
    }));
  };

  return (
    //Formulario para llenar los datos necesarios para crear al activo
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
            <label htmlFor='placa'>MARCA</label>
            <input type='text' name='marca' className='form-control' placeholder='Ingrese la marca'
            onChange={e => setValues({...values, marca: e.target.value})}/>
          </div>
          {/* Add datepicker for fecha_Compra */}
          <div className='mb-2'>
            <label htmlFor='fecha_Compra'>Fecha de Compra</label>
            <DatePicker
              selected={values.fecha_Compra}
              onChange={date => setValues({...values, fecha_Compra: date})}
              dateFormat="yyyy-MM-dd"
              className='form-control'
            />
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
            <select name='id_Estado' className='form-control' onChange={e => setValues({...values, id_Estado: e.target.value})}>
              <option value='1'>Disponible</option>
              <option value='2'>Prestado</option>
              <option value='3'>Averiado</option>
            </select>
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
  );
};

export default CreateActivo;