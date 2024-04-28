import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import DivTable from '../../Components/DivTable';
function IndexActivos() {
    const [data, setData] = useState([]);
    const navigate = useNavigate();
    const[title, setTitle] = useState('');
    const[classLoad, setClassLoad] = useState('');
    const[classTable, setClassTable] = useState('d-none');
    const[rows, setRows] = useState(0);
    const[page, setPage] = useState(1);
    const[pageSize, setPageSize] = useState(0);
    useEffect(()=>{
      axios.get('https://localhost:7215/api/Activo/lista_activos')
      .then(res => setData(res.data))
      .then(res =>setRows(res.total))
      .then(res =>setPageSize(res.per_page))
      .then(setClassTable(''))
      .then(setClassLoad('d-none'))
      .catch(err => console.log(err));
    },[]);



    const handleDelete = (placa) =>{
      const confirm = window.confirm("Desea eliminar este activo?");
      if(confirm){
        axios.delete('https://localhost:7215/api/Activo/eliminar_activo?placa='+placa)
        .then(res => {
          location.reload();
        }).catch(err=>console.log(err));
      }
    }


  return (
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
              <th>APROBADOR</th>
              <th>NOMBRE LAB</th>
              <th>CEDPROF</th>
            </tr>
          </thead>
          <tbody className='table-group-divider'>
            {
              data.map((d, i) =>(
                <tr key={i}>
                  <td>{d.placa}</td>
                  <td>{d.tipo}</td>
                  <td>{d.marca}</td>
                  <td>{d.fecha_Compra}</td>
                  <td>{d.req_Aprobador}</td>
                  <td>{d.nombre_Lab}</td>
                  <td>{d.ced_Prof}</td>
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