import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';
import { useReactToPrint } from 'react-to-print';
//Vista para generar reportes como administrador
const Reportes = () => {
    const [data, setData] = useState([]);
    const componentRef = useRef(null);

    useEffect(() => {
      axios.get('https://localhost:7215/api/Reportes/lista_reportes')
        .then(response => {
          setData(response.data);
        })
        .catch(error => {
          console.error('Error al obtener los datos:', error);
        });
    }, []);
    //Se agrupan los que tienen nombre igual para agrupar las fechas y horas
    const groupByNombre = () => {
      const groupedData = {};
      data.forEach(item => {
        if (!groupedData[item.nombre]) {
          groupedData[item.nombre] = [];
        }
        groupedData[item.nombre].push(item);
      });
      return groupedData;
    };

    const handlePrint = useReactToPrint({
      content: () => componentRef.current
    });

    return (
      <div>
        <button onClick={handlePrint}>Imprimir Reporte</button>
        <div ref={componentRef} style={{ fontFamily: 'Arial, sans-serif' }}>
          {Object.entries(groupByNombre()).map(([nombre, horas], index) => (
            <div key={index} style={{ marginBottom: '20px' }}>
              <h2>{nombre}</h2>
              <table style={{ width: '100%', borderCollapse: 'collapse', border: '1px solid #000' }}>
                <thead>
                  <tr>
                    <th style={{ border: '1px solid #000', padding: '8px' }}>Apellidos</th>
                    <th style={{ border: '1px solid #000', padding: '8px' }}>Hora de Inicio</th>
                    <th style={{ border: '1px solid #000', padding: '8px' }}>Hora de Finalización</th>
                    <th style={{ border: '1px solid #000', padding: '8px' }}>Horas Totales</th>
                  </tr>
                </thead>
                <tbody>
                  {horas.map((item, index) => (
                    <tr key={index}>
                      <td style={{ border: '1px solid #000', padding: '8px' }}>{item.ap1} {item.ap2}</td>
                      <td style={{ border: '1px solid #000', padding: '8px' }}>{item.hora_Inicio}</td>
                      <td style={{ border: '1px solid #000', padding: '8px' }}>{item.hora_Final}</td>
                      <td style={{ border: '1px solid #000', padding: '8px' }}>{item.horas_Totales}</td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr>
                    <td style={{ border: '1px solid #000', padding: '8px', textAlign: 'right' }} colSpan="3">Total de Horas:</td>
                    <td style={{ border: '1px solid #000', padding: '8px' }}>
                      {horas.reduce((total, item) => total + item.horas_Totales, 0)}
                    </td>
                  </tr>
                </tfoot>
              </table>
            </div>
          ))}
        </div>
      </div>
    );
};

export default Reportes;
