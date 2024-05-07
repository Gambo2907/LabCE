import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
//Vista para aprobar los prestamos
const AprobarPrestamos = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  useEffect(()=>{
    axios.get('https://localhost:7215/api/Activo/lista_activos_req_apr/'+"123") 
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

  const handleAceptar = (placa) =>{
      const confirm = window.confirm("Desea aprobar esta solicitud?");
      if(confirm){
        axios.put('https://localhost:7215/api/Activo/aprobar_activo?placa='+placa)
        .then(() => {
         // location.reload();
        }).catch(err=>console.log(err));
      }
      //location.reload();
      
    }
  
//Tabla con los prestamos
return (
  <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
    <h1>SOLICITUDES DE APROBACION DE PRESTAMOS</h1>
    <div className='w-750 rounded bg-white border shadow p-4'>
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Fecha</th>
            <th>Placa</th>
            <th>Tipo</th>
            <th>Marca</th>
          </tr>
        </thead>
        <tbody>
          {
            data.map((d, i) =>(
              <tr key={i}>
                <td>{d.fecha}</td>
                <td>{d.placa}</td>
                <td>{d.tipo}</td>
                <td>{d.marca}</td>
                <td>
                  <button onClick={e => handleAceptar(d.placa)} className='btn btn-sm btn-primary'>
                    <i className='fa-solid fa-check'></i>
                  </button>
                </td>
                <td>
                  <button onClick={e => handleDelete(d.placa)} className='btn btn-sm btn-danger'>
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

export default AprobarPrestamos