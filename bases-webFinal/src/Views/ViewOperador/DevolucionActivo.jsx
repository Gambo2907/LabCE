import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
//Vista para las devoluciones de los activos
const DevolucionActivo = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  useEffect(()=>{
    axios.get('https://localhost:7215/api/Activo/lista_activos_prestados')
    .then(res => setData(res.data))
    .catch(err => console.log(err));
  },[]);


  
  function handleDelete(nombre) {
    Swal.fire({
      title: '¿Está seguro que desea elminar este laboratorio?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminarlo'
    }).then((result) => {
      if (result.isConfirmed) {
        axios.delete(`https://localhost:7215/api/Laboratorio/eliminarlab?nombre=${nombre}`)
          .then((res) => {
            Swal.fire(
              '¡Eliminado!',
              'El laboratorio ha sido eliminado.',
              'success'
            );
            location.reload(); // Este es un método poco elegante, puedes manejar el refresco de datos de una manera más eficiente dependiendo de tu flujo de datos.
          })
          .catch((err) => {
            console.log(err);
            Swal.fire(
              'Error',
              'Hubo un problema al eliminar el laboratorio.',
              'error'
            );
          });
      }
    });
  }

//Tabla con la informacion y dos botones para devolucion o averia
return (
  <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
    <h1>ACTIVOS PRESTADOS</h1>
    <div className='w-75 rounded bg-white border shadow p-4'>
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>PLACA</th>
            <th>TIPO</th>
            <th>REGISTRAR DEVOLUCIÓN</th>
            <th>REGISTRAR AVERIA</th>
          </tr>
        </thead>
        <tbody>
          {
            data.map((d, i) =>(
              <tr key={i}>
                <td>{d.placa}</td>
                <td>{d.tipo}</td>
                <td> <Link to={`/operador/devoluciones/devolucion/${d.placa}`}  className='btn btn-sm btn-primary me-2'><i className='fa-solid fa-check'></i></Link></td>
                <td> <Link to={`/operador/devoluciones/averia/${d.placa}`}  className='btn btn-sm btn-danger'><i className='fa-solid fa-exclamation-triangle'></i></Link></td>
      
              </tr>
            ))
          }
        </tbody>
      </table>

    </div>
    

  </div>
)




}

export default DevolucionActivo