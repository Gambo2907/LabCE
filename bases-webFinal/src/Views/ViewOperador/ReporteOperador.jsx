import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';
import { useReactToPrint } from 'react-to-print';
//Vista para generar los reportes usando react to print
const ReporteOperador = () => {
    const [data, setData] = useState([]);
    const componentRef = useRef();
  
    useEffect(() => {
      // Llamada a la API para obtener los datos
      axios.get('https://localhost:7215/api/Reportes/lista_reportes/'+(JSON.parse(localStorage.getItem('data'))).carnet)
        .then(response => {
          setData(response.data);
        })
        .catch(error => {
          console.error('Error al obtener los datos:', error);
        });
    }, []);
  
    const handlePrint = useReactToPrint({
      content: () => componentRef.current,
    });
  //Tabla con los datos del reporte
    return (
        <div style={{ padding: '20px' }}>
          <h1>Reporte de Operadores</h1>
          <button onClick={handlePrint} style={{ marginBottom: '20px' }}>Imprimir Reporte</button>
          <div ref={componentRef}>
            <h2>{data.length > 0 ? `${data[0].nombre}` + ` ${data[0].ap1}` + ` ${data[0].ap2}`  : 'Nombre Operador'}</h2>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
              <thead>
                <tr>
    
                  <th style={{ border: '1px solid #ddd', padding: '8px', textAlign: 'left' }}>Hora de Inicio</th>
                  <th style={{ border: '1px solid #ddd', padding: '8px', textAlign: 'left' }}>Hora de Salida</th>
                  <th style={{ border: '1px solid #ddd', padding: '8px', textAlign: 'left' }}>Horas Laboradas</th>
                </tr>
              </thead>
              <tbody>
                {data.map((operador, index) => (
                  <tr key={index}>
      
                    <td style={{ border: '1px solid #ddd', padding: '8px' }}>{operador.hora_Inicio}</td>
                    <td style={{ border: '1px solid #ddd', padding: '8px' }}>{operador.hora_Final}</td>
                    <td style={{ border: '1px solid #ddd', padding: '8px' }}>{operador.horas_Totales}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      );
    }

export default ReporteOperador