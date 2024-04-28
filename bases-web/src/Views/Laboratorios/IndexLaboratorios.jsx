import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';

const IndexLaboratorios = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  useEffect(()=>{
    axios.get('https://localhost:7215/api/Laboratorio/lista_labs')
    .then(res => setData(res.data))
    .catch(err => console.log(err));
  },[]);



  const handleDelete = (nombre) =>{
    const confirm = window.confirm("Desea eliminar este laboratorio?");
    if(confirm){
      axios.delete('https://localhost:7215/api/Laboratorio/eliminarlab?nombre='+nombre)
      .then(res => {
        location.reload();
      }).catch(err=>console.log(err));
    }
  }


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
            data.map((d, i) =>(
              <tr key={i}>
                <td>{d.nombre}</td>
                <td>{d.hora_Inicio}</td>
                <td>{d.ora_Final}</td>
                <td>{d.capacidad}</td>
                <td>{d.computadore}</td>
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