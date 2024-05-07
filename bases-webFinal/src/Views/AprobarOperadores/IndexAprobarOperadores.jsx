import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
//Vista para aprobar operadores
const IndexAprobarOperadores = () => {
    const [data, setData] = useState([]);
    const navigate = useNavigate();
  // Función para obtener las solicitudes de aprobación de operadores desde el servidor
    useEffect(()=>{
      axios.get('https://localhost:7215/api/Operador/lista_operadores_no_aprobados')
      .then(res => setData(res.data))
      .catch(err => console.log(err));
    },[]);
  
  
  
    const handleDelete = (carnet) =>{
        /** 
      const confirm = window.confirm("Desea eliminar este activo?");
      if(confirm){
        axios.delete('https://localhost:7215/api/Profesor/eliminar_profesor?cedula='+carnet)
        .then(res => {
          location.reload();
        }).catch(err=>console.log(err));
      }*/
    }
// Función para manejar la aprobación de una solicitud de operador
    const handleAceptar = (carnet) =>{
        const confirm = window.confirm("Desea aprobar esta solicitud?");
        if(confirm){
          axios.put('https://localhost:7215/api/Operador/aprobar_operador?carnet='+carnet)
          .then(() => {
            location.reload();
          }).catch(err=>console.log(err));
        }
        location.reload();
        
      }
    
  //Tabla con la informacion
  return (
    <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
      <h1>SOLICITUDES DE APROBACION DE OPERADORES</h1>
      <div className='w-750 rounded bg-white border shadow p-4'>
        <table className='table table-striped'>
          <thead>
            <tr>
              <th>CARNET</th>
              <th>CEDULA</th>
              <th>CORREO</th>
              <th>PASSWORD</th>
              <th>NOMBRE</th>
              <th>AP1</th>
              <th>AP2</th>
              <th>NACIMIENTO</th>
              <th>EDAD</th>
              <th>ESTADO</th>
            </tr>
          </thead>
          <tbody>
            {
              data.map((d, i) =>(
                <tr key={i}>
                  <td>{d.carnet}</td>
                  <td>{d.cedula}</td>
                  <td>{d.correo}</td>
                  <td>{d.password}</td>
                  <td>{d.nombre}</td>
                  <td>{d.ap1}</td>
                  <td>{d.ap2}</td>
                  <td>{d.nacimiento}</td>
                  <td>{d.edad}</td>
                  <td>{d.aprobado}</td>
                  <td>
                    <button onClick={e => handleAceptar(d.carnet)} className='btn btn-sm btn-primary'>
                      <i className='fa-solid fa-check'></i>
                    </button>
                  </td>
                  <td>
                    <button onClick={e => handleDelete(d.carnet)} className='btn btn-sm btn-danger'>
                      <i className='fa-solid fa-trash'></i>
                    </button>
                  </td>
                </tr>
              ))
            }
          </tbody>
        </table>
  
      </div>
      
  
    </div>
  )
  
  
  
  
  }

export default IndexAprobarOperadores