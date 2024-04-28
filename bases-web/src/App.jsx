import { BrowserRouter, Routes, Route } from "react-router-dom";
import Nav from './Components/Nav';
import Labs from './Views/Labs/IndexLabs';
import CreateLabs from './Views/Labs/CreateLab';
import EditLabs from './Views/Labs/EditLab';
import Login from './Views/Login';
import ProtectedRoutes from './Components/ProtectedRoutes';

import IndexActivos from "./Views/Activos/IndexActivo";
import CreateActivo from "./Views/Activos/CreateActivo";
import UpdateActivo from "./Views/Activos/UpdateActivo";

import IndexProfesores from "./Views/Profesores/IndexProfesores";
import CreateProfesor from "./Views/Profesores/CreateProfesor";
import UpdateProfesor from "./Views/Profesores/UpdateProfesor";

import UpdateLaboratorio from "./Views/Laboratorios/UpdateLaboratorio";
import CreateLaboratorio from "./Views/Laboratorios/CreateLaboratorio";
import IndexLaboratorios from "./Views/Laboratorios/IndexLaboratorios";




import 'bootstrap/dist/css/bootstrap.min.css';

function App() {

  return (

    <BrowserRouter>
      <Nav />
      <Routes>
        <Route path="/" element={<IndexLaboratorios />} />
        <Route path="/login" element={<Login />} />


        <Route path="/activos" element={<IndexActivos />} />
        <Route path="/activos/create" element={<CreateActivo />} />
        <Route path="/activos/update/:id" element={<UpdateActivo />} />

        <Route path="/profesores" element={<IndexProfesores />} />
        <Route path="/profesores/create" element={<CreateProfesor />} />
        <Route path="/profesores/update/:id" element={<UpdateProfesor />} />

        <Route path="/laboratorios" element={<IndexLaboratorios />} />
        <Route path="/laboratorios/create" element={<CreateLaboratorio />} />
        <Route path="/laboratorios/update/:id" element={<UpdateLaboratorio />} />



        <Route element={<ProtectedRoutes />}>
        
        <Route path="/createlab" element={<CreateLabs />} />
        <Route path="/editlab" element={<EditLabs />} />
        
        </Route>
        
      </Routes>
      

    
    </BrowserRouter>

  )
}

export default App
