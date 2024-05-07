import {Link, useNavigate} from 'react-router-dom';
import storage from '../Storage/storage';
// Define el componente funcional Nav
const Nav = () => {
  const go = useNavigate();

   // Función para cerrar sesión de un administrador
  const logout = async() =>{
    localStorage.removeItem('data');
    localStorage.removeItem('user-info');
    localStorage.removeItem('userAdmin')
    localStorage.removeItem('userProfesor')
    go('/loginadministrador');
  
    
  }
  // Función para cerrar sesión de un profesor
  const logoutProfesor = async() =>{
    localStorage.removeItem('data');
    localStorage.removeItem('user-info');
    localStorage.removeItem('userProfesor')
    go('/loginprofesor');
  
  }
 // Función para cerrar sesión de un operador
 //Hace el registro de entrada y salida
  const logoutOperador= async() =>{
  
    const fechaActual = new Date(); // Obtener la fecha y hora actual
    const fechaFormateada = fechaActual.toISOString().split('T')[0]; // Formatear la fecha a 'YYYY-MM-DD'
    localStorage.removeItem('userOperador')


    const horas = fechaActual.getHours().toString().padStart(2, '0');
    const minutos = fechaActual.getMinutes().toString().padStart(2, '0');
    const segundos = fechaActual.getSeconds().toString().padStart(2, '0');
    const horaActual = `${horas}:${minutos}:${segundos}`;


    await axios.post('https://localhost:7215/api/Reportes/registrar_reporte', {
      id: 0,
      fecha_Trabajo: fechaFormateada,
      hora_Inicio: localStorage.getItem('horaEntrada'),
      hora_Final: horaActual,
      horas_Totales: 0,
      carnet_Op: (JSON.parse(localStorage.getItem('data'))).carnet,
    });
    localStorage.removeItem('data');
    localStorage.removeItem('user-info');
    localStorage.removeItem('userOpeador')
    localStorage.removeItem('horaEntrada')

    go('/loginoperador');



  
  }


  return (
    <nav className='navbar navbar-expand-lg navbar-white bg-info'> {/* Barra de navegación */}
      <div className='container-fluid'>
        <a className='navbar-brand'>Proyecto bases</a> {/* Marca de navegación */}
        <button className='navbar-toggler' type='button' data-bs-toggle='collapse' data-bs-target='#nav' aria-controls='navbarSupportedContent'>
          <span className='navbar-toggler-icon'></span>
        </button>
      </div>
      {/* Renderiza las opciones de navegación según el tipo de usuario */}
      
      {localStorage.getItem('user-info') && localStorage.getItem('userAdmin') ? (
        <div className='collapse navbar-collapse' id='nav'>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5 h4'>
              {storage.get('data').nombre}
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/laboratorios' className='nav-link'>Laboratorios</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/activos' className='nav-link'>Activos</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/profesores' className='nav-link'>Profesores</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/aprobaroperadores' className='nav-link'>Aprobar Solicitudes</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/reportes' className='nav-link'>Reporte</Link>
            </li>
          </ul>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5'>
              <button className='btn btn-info' onClick={logout}>LOGOUT</button>
            </li>
          </ul>
        </div>
      ) : ''}
      {localStorage.getItem('user-info') && localStorage.getItem('userProfesor') ? (
        <div className='collapse navbar-collapse' id='nav'>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5 h4'>
              {storage.get('data').nombre}
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/profesor/cambiopassword' className='nav-link'>Cambiar Contraseña</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/profesor/aprobarprestamos' className='nav-link'>Aprobar Préstamos</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/profesor/reserva' className='nav-link'>Reservar Laboratorios</Link>
            </li>

          </ul>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5'>
              <button className='btn btn-info' onClick={logoutProfesor}>LOGOUT</button>
            </li>
          </ul>
        </div>
      ) : ''}
      {localStorage.getItem('user-info') && localStorage.getItem('userOperador') ? (
        <div className='collapse navbar-collapse' id='nav'>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5 h4'>
              {storage.get('data').nombre}
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/operador/devoluciones' className='nav-link'>Devoluciones</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/operador/prestamoestudiantes' className='nav-link'>Prestamo estudiante</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/operador/prestamoprofesores' className='nav-link'>Prestamo profesor</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/operador/reservalaboratorio' className='nav-link'>Reserva laboratorio</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/operador/reporte' className='nav-link'>Reporte</Link>
            </li>
          </ul>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5'>
              <button className='btn btn-info' onClick={logoutOperador}>LOGOUT</button>
            </li>
          </ul>
        </div>
      ) : ''}
    
      
    </nav>
  )
}

export default Nav