import {Link, useNavigate} from 'react-router-dom';
import storage from '../Storage/storage';

const Nav = () => {
  const go = useNavigate();
  const logout = async() =>{
    storage.remove('authToken')
    storage.remove('authUser')
  
  }


  return (
    <nav className='navbar navbar-expand-lg navbar-white bg-info'>
      <div className='container-fluid'>
        <a className='navbar-brand'>COMPANY</a>
        <button className='navbar-toggler' type='button' data-bs-toggle='collapse' data-bs-target='#nav' aria-controls='navbarSupportedContent'>
          <span className='navbar-toggler-icon'>
          </span>
        </button>
      </div>
      {!storage.get('authUser') ? ( //Cambiar estoooo
        <div className='collapse navbar-collapse' id='nav'>
          <ul className='navbar-nav mx-auto mb-2'>
            <li className='nav-item px-lg-5 h4'>
             NOMBRE USUARIO
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/' className='nav-link'>Labs</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/activos' className='nav-link'>Activos</Link>
            </li>
            <li className='nav-item px-lg-5'>
              <Link to='/profesores' className='nav-link'>Profesores</Link>
            </li>
          </ul>
        </div>
      ) : ''}


    </nav>
    
  )
}

export default Nav