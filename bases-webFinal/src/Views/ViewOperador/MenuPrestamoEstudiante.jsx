import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
//Vista para realizar prestamo de laboratorio a un estudiante
const MenuPrestamoEstudiante = () => {


    //const [data, setData] = useState([]);
    const { id } = useParams();
    const fechaActual = new Date(); // Obtener la fecha y hora actual
    const fechaFormateada = fechaActual.toISOString().split('T')[0]; // Formatear la fecha a 'YYYY-MM-DD'
  
    const horas = fechaActual.getHours().toString().padStart(2, '0');
    const minutos = fechaActual.getMinutes().toString().padStart(2, '0');
    const segundos = fechaActual.getSeconds().toString().padStart(2, '0');
    const horaActual = `${horas}:${minutos}:${segundos}`;
  
    const [values, setValues] = useState({
      id: 0,
      fecha: fechaFormateada,
      hora: horaActual, // Agregar la hora actual
      placaActivo:id,
      cedProf:'',
      carnetOP:(JSON.parse(localStorage.getItem('data'))).carnet,
      nombreEstudiante:'',
      aP1Estudiante:'',
      aP2Estudiante:'',
      correoEstudiante:'',
    });
  
  
  
  
    const navigatex = useNavigate();
    const handleUpdate = (event) =>{
      event.preventDefault();
      axios.post('https://localhost:7215/api/Prestamos/crear_prestamo_estudiante',values)
      .then(res => {
        console.log(res);
        navigatex('/operador/prestamoestudiantes')
      })
      .catch(err => console.log(err));
    }
  //Formulario
    return (
      <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
        <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
          <h1>PRESTAMO A ESTUDIANTE</h1>
          <form onSubmit={handleUpdate}>
            <div className='mb-2'>
              <label htmlFor='cedProf'>CEDULA PROFESOR</label>
              <input type='text' name='cedProf' className='form-control' placeholder='Ingrese la cedula del profesor'
              value={values.cedProf} onChange={e => setValues({...values, cedProf: e.target.value})}/>
            </div>
            <div className='mb-2'>
              <label htmlFor='nombreEstudiante'>nombreEstudiante</label>
              <input type='text' name='nombreEstudiante' className='form-control' placeholder='Ingrese el nombre'
              value={values.nombreEstudiante} onChange={e => setValues({...values, nombreEstudiante: e.target.value})}/>
            </div>
  
            <div className='mb-2'>
              <label htmlFor='aP1Estudiante'>Apellido</label>
              <input type='text' name='aP1Estudiante' className='form-control' placeholder='aP1profesor'
              value={values.aP1Estudiante} onChange={e => setValues({...values, aP1Estudiante: e.target.value})}/>
            </div>
            <div className='mb-2'>
              <label htmlFor='aP2Estudiante'>Apellido</label>
              <input type='text' name='aP2Estudiante' className='form-control' placeholder='Ingrese aP2profesoir'
              value={values.aP2Estudiante} onChange={e => setValues({...values, aP2Estudiante: e.target.value})}/>
            </div>
            <div className='mb-2'>
              <label htmlFor='correoEstudiante'>correo estudiante</label>
              <input type='text' name='correoEstudiante' className='form-control' placeholder='Ingrese correo profesor'
              value={values.correoEstudiante} onChange={e => setValues({...values, correoEstudiante: e.target.value})}/>
            </div>
            <button className='btn btn-success'>CONFIRMAR</button>
            <Link to="/operador/prestamoprofesores" className='btn btn-primary ms-3'>VOLVER</Link>
          </form>
        </div>
  
      </div>
    )
  }

export default MenuPrestamoEstudiante