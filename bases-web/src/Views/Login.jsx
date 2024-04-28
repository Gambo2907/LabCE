import React,{useState} from 'react'
import { useNavigate, Link } from 'react-router-dom';
import { sendRequest } from '../Functions';
import DivInput from '../Components/DivInput';
import storage from '../Storage/storage';


const Login = () => {
  const [email, setEmail] = useState('');
  const [password,setPassword] = useState('');
  const go = useNavigate();

  const login = async(e) =>{
    e.preventDefault();
    const form = {email:email,password:password};
    //const res = await sendRequest('GET','','https://localhost:7215/api/Laboratorio/lista_labs','',false);

    storage.set('authToken','')
    storage.set('authUser','isaac@sd')

    console.log('bieeen')
    
  }

  

  return (
    <div className='container-fluid'>
      <div className='row mt-5'>
        <div className='col-md-4 offset-md-4'>
          <div className='card border border-dark'>
            <div className='card-header bg-dark border border-dark 
            text-white'>
              LOGIN
            </div>
            <div className='card-body'>
              <form onSubmit={login}>
                <DivInput type='email' icon='fa-at' value={email}
                className='form-control' placeholder='Email' required='required'
                handleChange={(e)=> setEmail(e.target.value)} />
                <DivInput type='password' icon='fa-key' value={password}
                className='form-control' placeholder='Password' required='required'
                handleChange={(e)=> setPassword(e.target.value)} />
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

export default Login