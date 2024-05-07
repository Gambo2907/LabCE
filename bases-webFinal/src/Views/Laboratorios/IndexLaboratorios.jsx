import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
//Vista para mostrar los laboratorios
const IndexLaboratorios = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  useEffect(()=>{
    axios.get('https://localhost:7215/api/Laboratorio/lista_labs')
    .then(res => setData(res.data))
    .catch(err => console.log(err));
  },[]);


// Función para manejar la eliminación de un laboratorio
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

//Tabla con la informacionde los laboratorios
return (
  <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
    <h1>LISTA DE LABORATORIOS</h1>
    <div className='w-75 rounded bg-white border shadow p-4'>
      <div className='d-flex justify-content-end'>
          <Link to="/laboratorios/create" className='btn btn-success'>AGREGAR</Link>
      </div>
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>NOMBRE</th>
            <th>hora_Inicio</th>
            <th>hora_Final</th>
            <th>capacidad</th>
            <th>computadores</th>
            <th>facilidades</th>
          </tr>
        </thead>
        <tbody>
          {
            //Mapea la informacion con las filas
            data.map((d, i) =>(
              <tr key={i}>
                <td>{d.nombre}</td>
                <td>{d.hora_Inicio}</td>
                <td>{d.hora_Final}</td>
                <td>{d.capacidad}</td>
                <td>{d.computadores}</td>
                <td>{d.facilidades}</td>
                <td> <Link to={`/laboratorios/update/${d.nombre}`}  className='btn btn-sm btn-primary me-2'><i className='fa-solid fa-edit'></i></Link></td>
                <td>
                  <button onClick={e => handleDelete(d.nombre)} className='btn btn-sm btn-danger'>
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
export default IndexLaboratorios