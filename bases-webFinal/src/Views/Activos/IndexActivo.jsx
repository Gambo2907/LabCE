import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import DivTable from '../../Components/DivTable';
import Swal from 'sweetalert2';
//Vista para ver los activos
function IndexActivos() {

  const [data, setData] = useState([]); // Estado para almacenar los datos de los activos
  const navigate = useNavigate(); // Hook para la navegación
  const [classLoad, setClassLoad] = useState(''); // Clase para mostrar el estado de carga
  const [classTable, setClassTable] = useState('d-none'); // Clase para mostrar la tabla
  const [rows, setRows] = useState(0); // Estado para el número total de filas
  const [page, setPage] = useState(1); // Estado para el número de página actual
  const [pageSize, setPageSize] = useState(0); // Estado para el tamaño de página
    
  // Utiliza useEffect para cargar los datos de los activos cuando el componente se monta
  useEffect(() => {
    // Realiza una solicitud GET para obtener la lista de activos
    axios.get('https://localhost:7215/api/Activo/lista_activos')
      .then(res => {
        setData(res.data); // Actualiza el estado con los datos de los activos
        setRows(res.total); // Actualiza el estado con el número total de filas
        setPageSize(res.per_page); // Actualiza el estado con el tamaño de página
        setClassTable(''); // Muestra la tabla
        setClassLoad('d-none'); // Oculta el estado de carga
      })
      .catch(err => console.log(err)); // Maneja errores de la solicitud
  }, []);

// Función para manejar la eliminación de un activo
    function handleDelete(placa) {
      Swal.fire({
        title: '¿Está seguro que desea eliminar este activo?',
        text: 'Esta acción no se puede deshacer.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminarlo'
      }).then((result) => {
        if (result.isConfirmed) {
          axios.delete(`https://localhost:7215/api/Activo/eliminar_activo?placa=${placa}`)
            .then((res) => {
              Swal.fire(
                '¡Eliminado!',
                'El activo ha sido eliminado.',
                'success'
              );
              location.reload(); // Este es un método poco elegante, puedes manejar el refresco de datos de una manera más eficiente dependiendo de tu flujo de datos.
            })
            .catch((err) => {
              console.log(err);
              Swal.fire(
                'Error',
                'Hubo un problema al eliminar el activo.',
                'error'
              );
            });
        }
      });
    }

  return (
    //Tabla para mostrar la informacion
    <div className='container-fluid'>
      <h1>LISTA DE ACTIVOS</h1>
      
        <div className='d-flex justify-content-end'>
            <Link to="/activos/create" className='btn btn-success'>AGREGAR</Link>
        </div>
      <DivTable col='10' off='1' classLoad={classLoad} classTable={classTable}>
       
        <table className='table table-bordered'>
          <thead>
            <tr>
              <th>PLACA</th>
              <th>TIPO</th>
              <th>MARCA</th>
              <th>FECHACOMPRA</th>
              <th>REQUIERE APROBADOR</th>
              <th>NOMBRE LAB</th>
              <th>CEDPROF</th>
              <th>ESTADO</th>
            </tr>
          </thead>
          <tbody className='table-group-divider'>
            {
              //Mapea los valores a las filas
              data.map((d, i) =>(
                <tr key={i}>
                  <td>{d.placa}</td>
                  <td>{d.tipo}</td>
                  <td>{d.marca}</td>
                  <td>{d.fecha_Compra}</td>
                  <td>{d.req_Aprobador ? 'Si' : 'No'}</td>
                  <td>{d.nombre_Lab}</td>
                  <td>{d.ced_Prof}</td>
                  <td>{d.estado}</td>
                  <td> <Link to={`/activos/update/${d.placa}`}  className='btn btn-sm btn-primary me-2'><i className='fa-solid fa-edit'></i></Link></td>
                  <td><button onClick={e => handleDelete(d.placa)} className='btn btn-sm btn-danger'><i className='fa-solid fa-trash'></i></button></td>
                </tr>
              ))
            }
          </tbody>
        </table>
      </DivTable>
      
      

    </div>
  )




}

export default IndexActivos