import React, { useState, useEffect } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { Link, useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
//Menu para seleccionar el horario a reservar el lab
const MenuReservaOperador = () =>  {
    const [events, setEvents] = useState([]);
    const { id } = useParams();
    const [values, setValues] = useState({
      id: 0,
      fecha: '',
      horaInicio:'',
      horaFin:'',
      cantHoras:1,
      nombreLab:id,
      cedProf: 0,
      carnetOP:(JSON.parse(localStorage.getItem('data'))).carnet,
      nombreEstudiante:'',
      aP1Estudiante:'',
      aP2Estudiante:'',
      correoEstudiante:(JSON.parse(localStorage.getItem('data'))).correo,
      carnetEstudiante:0,
    })
    const handleUpdate = (event) =>{
      event.preventDefault();
      axios.post('https://localhost:7215/api/Reservacion/crear_reservacion_estudiante',values)
      .then(res => {
        console.log(res);
        location.reload();
      })
      .catch(err => console.log(err));
    }
  
    useEffect(() => {
        // Lógica para obtener datos de la API utilizando Axios
        axios.get("https://localhost:7215/api/Reservacion/reservaciones/"+id)
          .then((response) => {
            // Verificar si la respuesta es exitosa
            if (response.status === 200) {
              // Convertir los datos recibidos a un formato compatible con FullCalendar
              const formattedEvents = response.data.map((event) => ({
                title: event.nombreLab,
                start: event.fecha + "T" + event.horaInicio, // Combina fecha y hora de inicio
                end: event.fecha + "T" + event.horaFin, // Combina fecha y hora de finalización
              }));
              setEvents(formattedEvents);
            } else {
              // Manejar cualquier error de respuesta no exitosa
              console.error("Error al obtener datos de la API:", response.statusText);
            }
          })
          .catch((error) => {
            // Manejar cualquier error de red u otro tipo de error
            console.error("Error al obtener datos de la API:", error);
          });
      }, []); // Se ejecuta solo una vez al montar el componente
  
    const handleDateSelect = (selectInfo) => {
      let title = prompt("Ingrese el título del evento:");
      let calendarApi = selectInfo.view.calendar;
  
      calendarApi.unselect(); // Limpiar la selección
  
      if (title) {
  
        const fechaActual = new Date(); // Obtener la fecha y hora actual
        const fechaFormateada = fechaActual.toISOString().split('T')[0]; // Formatear la fecha a 'YYYY-MM-DD'
  
        console.log("dsdsd")
        console.log(selectInfo.startStr)
  
        setValues({...values, fecha: fechaFormateada})
   
  
  
  
        axios.post('https://localhost:7215/api/Reservacion/crear_reservacion_estudiante',values)
        .then(() => {
          console.log(res);
          navigate('/profesor/reservas')
        })
        .catch(err => console.log(err));
        calendarApi.addEvent({
          title,
          start: selectInfo.startStr,
          end: selectInfo.endStr,
          allDay: selectInfo.allDay,
        });
  
        // Opcional: También puedes enviar el nuevo evento a la API para que se guarde en la base de datos
      }
    };
  //Calendario y formulario
    return (
      <div style={{ display: "flex", flexDirection: "column", height: "200vh" }}>
        <div style={{ flex: "1", overflow: "auto" }}>
          <FullCalendar
            plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
            initialView="dayGridWeek"
            headerToolbar={{
              start: "today prev,next",
              center: "title",
              end: "dayGridMonth,timeGridWeek,timeGridDay",
            }}
            events={events}
            selectable={false}
            select={handleDateSelect}
          />
                <div>
        <form onSubmit={handleUpdate}>
            <div className='mb-2'>
              <label htmlFor='fecha'>FECHA</label>
              <input type='text' name='fecha' className='form-control' placeholder='Ingrese la fecha'
              onChange={e => setValues({...values, fecha: e.target.value})}/>
            </div>
            <div className='mb-2'>
              <label htmlFor='horaInicio'>horaInicio</label>
              <input type='text' name='nombre' className='form-control' placeholder='Ingrese horaInicio'
              onChange={e => setValues({...values, horaInicio: e.target.value})}/>
            </div>
            <div className='mb-2'>
              <label htmlFor='horaFin'>horaFin</label>
              <input type='text' name='horaFin' className='form-control' placeholder='Ingrese el primer apellido'
              onChange={e => setValues({...values,horaFin: e.target.value})}/>
            </div>
            <button className='btn btn-success'>GUARDAR</button>
            <Link to="/operador/reservalaboratorio" className='btn btn-primary ms-3'>VOLVER</Link>
          </form>
        </div>
        </div>
  
      </div>
    );
  };
  

export default MenuReservaOperador