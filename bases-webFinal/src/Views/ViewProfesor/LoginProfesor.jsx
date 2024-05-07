import React,{useEffect, useState} from 'react'
import { useNavigate, Link, Navigate } from 'react-router-dom';

import DivInput from '../../Components/DivInput';
import storage from '../../Storage/storage';
//Vista con el login del profesor
const LoginProfesor = () => {

  const [data, setData] = useState([]);

  const [values, setValues] = useState({
      correo: '',
      password: '',
    })
 
  const go = useNavigate();
  useEffect(() => {
      if(localStorage.getItem('user-info')){
          <Navigate to='/login' />
      }
  }, [])



  const handleSubmit = (event) =>{
      event.preventDefault();

    }
  //Se guarda el tipo de usuario que ingresa
  const login = async(e) =>{
    e.preventDefault();


    axios.post('https://localhost:7215/api/Login/login_profesor',values)
    .then(res => setData(res.data),  localStorage.setItem('user-info', true), localStorage.setItem('data', JSON.stringify(data)))
    .then(() => {
      console.log(data.correo);
      console.log(data.password);
      console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
      <Navigate to='/' />
     // localStorage.setItem('user-info', JSON.stringify(res.data));
      console.log(localStorage.getItem('user-info'));
    })
    .catch(err => console.log(err));
      
    
    axios.post('https://localhost:7215/api/Login/login_profesor',values)
    .then(res => localStorage.setItem('data', JSON.stringify(res.data)))
    .then(() => {
      localStorage.setItem('userProfesor', true)
      console.log(data.correo);
      console.log(data.password);
      console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
      go('/profesor/aprobarprestamos')
     // localStorage.setItem('user-info', JSON.stringify(res.data));
      console.log(localStorage.getItem('user-info'));
    })
    .catch(err => console.log(err));


  }

  //Formulario

  return (
    <div className='container-fluid'>
      <div className='row mt-5'>
        <div className='col-md-4 offset-md-4'>
          <div className='card border border-dark'>
            <div className='card-header bg-dark border border-dark 
            text-white'>
              LOGIN PROFESOR
            </div>
            <div className='card-body'>
              <form onSubmit={login}>
                <DivInput type='email' icon='fa-at' value={values.correo}
                className='form-control' placeholder='Email' required='required'
                handleChange={(e)=> setValues({...values,correo: e.target.value})} />
                <DivInput type='password' icon='fa-key' value={values.password}
                className='form-control' placeholder='Password' required='required'
                handleChange={(e)=> setValues({...values,password: e.target.value})} />
                <div className='d-grid col-10 mx-auto'>
                  <button className='btn btn-dark'>
                    <i className='fa-solid fa-door-open'></i>
                    LOGIN
                  </button>
                </div>
              </form>
            </div>
          </div>  
        </div>
      </div>
    </div>
  )
}

export default LoginProfesor