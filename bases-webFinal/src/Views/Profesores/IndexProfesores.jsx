import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
//Vista para mostrar los profesores
const IndexProfesores = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  useEffect(()=>{
    axios.get('https://localhost:7215/api/Profesor/lista_profesores')
    .then(res => setData(res.data))
    .catch(err => console.log(err));
  },[]);


//Funcion para eliminar un profesor
  function handleDelete(cedula) {
    Swal.fire({
      title: '¿Está seguro que desea eliminar este profesor?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminarlo'
    }).then((result) => {
      if (result.isConfirmed) {
        axios.delete(`https://localhost:7215/api/Profesor/eliminar_profesor?cedula=${cedula}`)
          .then((res) => {
            Swal.fire(
              '¡Eliminado!',
              'El profesor ha sido eliminado.',
              'success'
            );
            location.reload(); // Este es un método poco elegante, puedes manejar el refresco de datos de una manera más eficiente dependiendo de tu flujo de datos.
          })
          .catch((err) => {
            console.log(err);
            Swal.fire(
              'Error',
              'Hubo un problema al eliminar el profesor.',
              'error'
            );
          });
      }
    });
  }


return (
  <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
    <h1>LISTA DE PROFESORES</h1>
    <div className='w-75 rounded bg-white border shadow p-4'>
      <div className='d-flex justify-content-end'>
          <Link to="/profesores/create" className='btn btn-success'>AGREGAR</Link>
      </div>
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>CEDULA</th>
            <th>NOMBRE</th>
            <th>APELLIDO1</th>
            <th>APELLIDO2</th>
            <th>CORREO</th>
            <th>PASSWORD</th>
            <th>NACIMIENTO</th>
            <th>EDAD</th>
          </tr>
        </thead>
        <tbody>
          {
            data.map((d, i) =>(
              <tr key={i}>
                <td>{d.cedula}</td>
                <td>{d.nombre}</td>
                <td>{d.ap1}</td>
                <td>{d.ap2}</td>
                <td>{d.correo}</td>
                <td>{d.password}</td>
                <td>{d.nacimiento}</td>
                <td>{d.edad}</td>
                <td> <Link to={`/profesores/update/${d.cedula}`}  className='btn btn-sm btn-primary me-2'><i className='fa-solid fa-edit'></i></Link></td>
                <td>
                  <button onClick={e => handleDelete(d.cedula)} className='btn btn-sm btn-danger'>
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
export default IndexProfesores