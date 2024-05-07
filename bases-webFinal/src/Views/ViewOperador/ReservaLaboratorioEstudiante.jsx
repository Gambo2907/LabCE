import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
//Vista para reservar laboratorios por parte del estudiante
const ReservaLaboratorioEstudiante = () => {
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

//Tabla para mostrar los laboratorios
return (
  <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
    <h1>LISTA DE LABORATORIOS</h1>
    <div className='w-75 rounded bg-white border shadow p-4'>
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>NOMBRE</th>
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
                <td>{d.capacidad}</td>
                <td>{d.computadores}</td>
                <td>{d.facilidades}</td>
                <td>
                <div className='d-flex justify-content-end'>
                    <Link to={`/operador/reservalaboratorios/${d.nombre}`} className='btn btn-success'>CONSULTAR</Link>
                </div>
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

export default ReservaLaboratorioEstudiante