import React,{useEffect, useState,useRef} from 'react'
import DivAdd from '../../Components/DivAdd';
import DivTable from '../../Components/DivTable';
import DivSelect from '../../Components/DivSelect';
import DivInput from '../../Components/DivInput';
import Modal from '../../Components/Modal';
import { Link } from 'react-router-dom';
import {confirmation, sendRequest} from '../../Functions';
import { PaginationControl } from 'react-bootstrap-pagination-control';

const Labs = () =>{
  const[Laboratorios,setLaboratorios] = useState([]);
  const[nombre, setNombre] = useState('');
  const[horaInicio, setHoraInicio] = useState('');
  const[horaFinal, setHoraFinal] = useState('');
  const[capacidad, setCapacidad] = useState('');
  const[computadores, setComputadores] = useState('');
  const[facilidades, setFacilidades] = useState('');

  const[operation, setOperation] = useState('');

  const[title, setTitle] = useState('');
  const[classLoad, setClassLoad] = useState('');
  const[classTable, setClassTable] = useState('d-none');
  const[rows, setRows] = useState(0);
  const[page, setPage] = useState(1);
  const[pageSize, setPageSize] = useState(0);
  const NameInput = useRef();
  const close = useRef();

  let method = '';
  let url = '';

  useEffect(()=>{
    getLabs(1)
  },[]);
  const getLabs = async (page) =>{
    const res = await sendRequest('GET','','https://localhost:7215/api/Laboratorio/lista_labs?page='+page,'');
    setLaboratorios(res);
    setRows(res.total);
    setPageSize(res.per_page);
    setClassTable('');
    setClassLoad('d-none');
  }

  const deleteLab=(id,name)=>{
    confirmation(name,'https://localhost:7215/api/Laboratorio/eliminarlab?nombre='+id,'/');
  }

  const clear = ()=>{
    setNombre('');
    setHoraInicio('')
    setCapacidad('')
    setFacilidades('')
  }
  
  const openModal = (op,n,hi,hf,ca,co,f) =>{
    clear();
    setTimeout( ()=> NameInput.current.focus(),600);
    setOperation(op);
    if(op == 1){
      clear();
      setTitle('Crear lab');
    }
    else{
      setTitle('Actualizar empleado');
      setNombre(n);
      setHoraInicio(hi);
      setHoraFinal(hf);
      setCapacidad(ca);
      setComputadores(co);
      setFacilidades(f);

    }
  }
  const save = async()=>{
    e.preventDefault();
    if(operation == 1){
      method = 'POST';
      url = 'https://localhost:7215/api/Laboratorio/crearlab'
    }
    else{
      method = 'PUT';
      url = 'https://localhost:7215/api/Laboratorio/actualizarlab?nombre='+nombre;
    }
    const form = {nombre:nombre,horaInicio:horaInicio,horaFinal:horaFinal,capacidad:capacidad,computadores:computadores,facilidades:facilidades};
    const res = await sendRequest(method,form,url,'');
    if(method == 'PUT' && res.status == true){
      close.current.click();
    }
    if(res.status == true){
      clear();
      getLabs(page);
      setTimeout(()=> NameInput.current.focus(),3000);
    }
  }
  const goPage = (p) =>{
    setPage(p);
    getLabs(p);
  }
  return (
    <div className='container-fluid'>
     <DivAdd>
       <button className='btn btn-dark' data-bs-toggle='modal'
        data-bs-target='#modalLabs' onClick={()=>openModal(1)}>
          <i className='fa-solid fa-circle-plus'></i> ADD

        </button>
     </DivAdd>
     <DivTable col='10' off='1' classLoad={classLoad} classTable={classTable}>
       <table className='table table-bordered'>
         <thead><tr><th>NUMERACION</th><th>NOMBRE</th><th>HORA1</th><th>HORA2</th><th>CAPACIDAD</th><th>COMPUTADORES</th><th>FACILIDADES</th><th>EDITAR</th><th>ELIMINAR</th></tr></thead>
         <tbody className='table-group-divider'>
           {Laboratorios.map( (row,i)=>(
             <tr key={row.nombre}>
               <td>{(i+1)}</td>
               <td>{(row.nombre)}</td>
               <td>{(row.hora_Inicio)}</td>
               <td>{(row.hora_Final)}</td>
               <td>{(row.capacidad)}</td>
               <td>{(row.computadores)}</td>
               <td>{(row.facilidades)}</td>
               <td>
               <button className='btn btn-warning' data-bs-toggle='modal'
                data-bs-target='#modalLabs' 
                onClick={()=>openModal(2,row.nombre,row.hora,row.capacidad,row.computadores,row.facilidades)}>
                  <i className='fa-solid fa-edit'></i>
                </button>
               </td>
               <td>
                 <button className='btn btn-danger'
                 onClick={()=> deleteLab(row.nombre,'PUT')}>
                   <i className='fa-solid fa-trash'></i>
                 </button>
               </td>
             </tr>
           ))}
         </tbody>
       </table>
       <PaginationControl changePage = {page => goPage(page)} 
       next={true} limit={pageSize} page={page} total={rows} />
     </DivTable>
     <Modal title={title} modal='modalLabs'>
      <div className='modal-body'>
        <form onSubmit={save}>
        <DivInput type='text' icon = 'fa-user' 
          value={nombre} className ='form-control' placeholder='Nombre' 
          required='required' ref={NameInput}
          handleChange={(e)=>setNombre(e.target.value)} />
          <DivInput type='horaInicio' icon = 'fa-clock' 
          value={horaInicio} className ='form-control' 
          placeholder='Hora Inicio' required='required'
          handleChange={(e)=>setHoraInicio(e.target.value)} />
          <DivInput type='horaFinal' icon = 'fa-clock' 
          value={horaFinal} className ='form-control' 
          placeholder='Hora Final' required='required'
          handleChange={(e)=>setHoraFinal(e.target.value)} />
          <DivInput type='capacidad' icon = 'fa-clock' 
          value={capacidad} className ='form-control' 
          placeholder='Capacidad' required='required'
          handleChange={(e)=>setCapacidad(e.target.value)} />
          <DivInput type='computadores' icon = 'fa-desktop' 
          value={computadores} className ='form-control' 
          placeholder='Computadores' required='required'
          handleChange={(e)=>setComputadores(e.target.value)} />
          <DivInput type='facilidades' icon = 'fa-clock' 
          value={facilidades} className ='form-control' 
          placeholder='Facilidades' required='required'
          handleChange={(e)=>setFacilidades(e.target.value)} />
          
          <div className='d-grid col-10 mx-auto'>
            <button className='btn btn-success'>
              <i className='fa-solid fa-save'></i> SAVE
            </button>
          </div>
        </form>
      </div>
      <div className='modal-footer'>
        <button className='btn btn-dark' data-bs-dismiss='modal'
        ref={close}> CLOSE</button>
      </div>
     </Modal>
    </div>
   )
}

/** 
function Labs() {
  const [labs, setLabs] = useState([]);
  const [classLoad, setClassLoad] = useState('');
  const [classTable, setClassTable] = useState('');

  useEffect( () => {
    getLabs();
  }, []);

  const getLabs = async() =>{
    const res = await sendRequest('GET','','https://localhost:7215/api/Laboratorio/lista_labs','');
    setLabs(res);
    setClassTable('');
    setClassLoad('d-none');
  }
*/
 

  /**
   * useEffect(() => {
  const fetchData = async () => {
    try {
      const response = await axios.get('https://localhost:7215/api/Laboratorio/lista_labs');
      setLabs(response.data);
      setClassTable('');
      setClassLoad('d-none');
    } catch (error) {
      console.log(error);
    }
  };

  fetchData(); // Llama a la función fetchData dentro del useEffect

}, []);
   */


/** 

  return (
   <div className='container-fluid'>
    <DivAdd>
      <Link to='createlab' className='btn btn-dark'>
        <i className='fa-solid fa-circle-plus'></i> Add
      </Link>
    </DivAdd>
    <DivTable col='10' off='1' classLoad={classLoad} classTable={classTable}>
      <table className='table table-bordered'>
        <thead><tr><th>NUMERACION</th><th>NOMBRE</th><th>HORA</th><th>CAPACIDAD</th><th>COMPUTADORES</th><th>FACILIDADES</th><th>NOMBlRE</th><th>NOMBRE</th></tr></thead>
        <tbody className='table-group-divider'>
          {labs.map( (row,i)=>(
            <tr key={row.id}>
              <td>{(i+1)}</td>
              <td>{(row.nombre)}</td>
              <td>{(row.hora)}</td>
              <td>{(row.capacidad)}</td>
              <td>{(row.computadores)}</td>
              <td>{(row.facilidades)}</td>
              <td>
                <Link to={'/editlab/'+row.id} className='btn btn-dark'>
                  <i className='fa-solid fa-edit'></i>
                </Link>
              </td>
              <td>
                <button className='btn btn-danger'
                onClick={()=> confirmation('','','')}>
                  <i className='fa-solid fa-trash'></i>
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </DivTable>
   </div>
  )
}
*/
export default Labs