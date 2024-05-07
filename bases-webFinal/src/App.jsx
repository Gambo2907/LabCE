import { BrowserRouter, Routes, Route } from "react-router-dom";
import Nav from './Components/Nav';
import Labs from './Views/Labs/IndexLabs';
import CreateLabs from './Views/Labs/CreateLab';
import EditLabs from './Views/Labs/EditLab';
import Login from './Views/Login';
import ProtectedRoutes from './Components/ProtectedRoutes';


//ADMINISTRRADOR
import IndexActivos from "./Views/Activos/IndexActivo";
import CreateActivo from "./Views/Activos/CreateActivo";
import UpdateActivo from "./Views/Activos/UpdateActivo";

import IndexProfesores from "./Views/Profesores/IndexProfesores";
import CreateProfesor from "./Views/Profesores/CreateProfesor";
import UpdateProfesor from "./Views/Profesores/UpdateProfesor";

import UpdateLaboratorio from "./Views/Laboratorios/UpdateLaboratorio";
import CreateLaboratorio from "./Views/Laboratorios/CreateLaboratorio";
import IndexLaboratorios from "./Views/Laboratorios/IndexLaboratorios";

import IndexAprobarOperadores from "./Views/AprobarOperadores/IndexAprobarOperadores";


//PROFESOR



import LoginProfesor from "./Views/ViewProfesor/LoginProfesor";
import AprobarPrestamos from "./Views/ViewProfesor/AprobarPrestamos";
import CambioPassword from "./Views/ViewProfesor/CambioPassword";
import ReservaLaboratorio from "./Views/ViewProfesor/ReservaLaboratorio";

import LoginAdministrador from "./Views/LoginAdministrador";

//OPERADOR
import DevolucionActivo from "./Views/ViewOperador/DevolucionActivo";
import LoginOperador from "./Views/ViewOperador/LoginOperador";
import PrestamoActivoEstduiante from "./Views/ViewOperador/PrestamoActivoEstduiante";
import PrestamoActivoProfesor from "./Views/ViewOperador/PrestamoActivoProfesor";
import Registro from "./Views/ViewOperador/Registro";
import ReservaLaboratorioEstudiante from "./Views/ViewOperador/ReservaLaboratorioEstudiante";
import MenuPrestamoProfesor from "./Views/ViewOperador/MenuPrestamoProfesor";
import MenuReservaOperador from "./Views/ViewOperador/MenuReservaOperador";

import 'bootstrap/dist/css/bootstrap.min.css';
import MenuReserva from "./Views/ViewProfesor/MenuReserva";
import RegistroAveria from "./Views/ViewOperador/RegistroAveria";
import RegistroDevolucion from "./Views/ViewOperador/RegistroDevolucion";
import MenuPrestamoEstudiante from "./Views/ViewOperador/MenuPrestamoEstudiante";

import ReporteOperador from "./Views/ViewOperador/ReporteOperador";
import Reportes from "./Views/Reportes";
function App() {

  return (

    <BrowserRouter>
      <Nav />
      <Routes>

        {/**VISTA PRINCIPAL */}
        <Route path="/" element={<LoginAdministrador />} />
        <Route path="/login" element={<Login />} />

        {/**VISTA LOGIN */}

        <Route path="/loginadministrador" element={<LoginAdministrador />} />
        <Route path="/loginprofesor" element={<LoginProfesor />} />
        <Route path="/loginoperador" element={<LoginOperador />} />

        <Route path="/registrooperador" element={<Registro />} />

        
         {/**VISTA PROFESOR */}

        <Route path="/profesor" element={<LoginProfesor />} />
        <Route path="/profesor/aprobarprestamos" element={<AprobarPrestamos />} />
        <Route path="/profesor/cambiopassword" element={<CambioPassword />} />
        <Route path="/profesor/reserva" element={<ReservaLaboratorio />} />
        <Route path="/profesor/reserva/menureserva/:id" element={<MenuReserva />} />

        {/**VISTA OPERADOR */}
        <Route path="/operador/devoluciones" element={<DevolucionActivo />} />
        <Route path="/operador/prestamoestudiantes" element={<PrestamoActivoEstduiante />} />
        <Route path="/operador/prestamoprofesores" element={<PrestamoActivoProfesor />} />
        <Route path="/operador/reservalaboratorio" element={<ReservaLaboratorioEstudiante />} />
        <Route path="/operador/reservalaboratorios/:id" element={<MenuReservaOperador />} />
        <Route path="/operador/prestamoprofesores/reserva/:id" element={<MenuPrestamoProfesor />} />
        <Route path="/operador/prestamoestudiantes/reserva/:id" element={<MenuPrestamoEstudiante />} />

        <Route path="/operador/devoluciones/devolucion/:id" element={< RegistroDevolucion/>} />
        <Route path="/operador/devoluciones/averia/:id" element={<RegistroAveria />} />
        <Route path="/operador/reporte" element={<ReporteOperador />} />







      




        <Route element={<ProtectedRoutes />}>
        {/**VISTA ADMIN */}
          <Route path="/profesores" element={<IndexProfesores />} />
          <Route path="/activos" element={<IndexActivos />} />
          <Route path="/activos/create" element={<CreateActivo />} />
          <Route path="/activos/update/:id" element={<UpdateActivo />} />

          <Route path="/profesores" element={<IndexProfesores />} />
          <Route path="/profesores/create" element={<CreateProfesor />} />
          <Route path="/profesores/update/:id" element={<UpdateProfesor />} />

          <Route path="/laboratorios" element={<IndexLaboratorios />} />
          <Route path="/laboratorios/create" element={<CreateLaboratorio />} />
          <Route path="/laboratorios/update/:id" element={<UpdateLaboratorio />} />

          <Route path="/createlab" element={<CreateLabs />} />
          <Route path="/editlab" element={<EditLabs />} />

          <Route path="/reportes" element={<Reportes />} />



          
          <Route path="/aprobaroperadores" element={<IndexAprobarOperadores />} />
        
        </Route>
        
      </Routes>
      

    
    </BrowserRouter>

  )
}

export default App
