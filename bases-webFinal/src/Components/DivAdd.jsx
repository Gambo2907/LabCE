import React from 'react'

// Definición del componente DivAdd
const DivAdd = ({ children }) => {
  return (
    // Estructura HTML que define el diseño del componente utilizando clases de Bootstrap
    <div className='row mt-3'> {/* Una fila con margen superior */}
        <div className='col-md-4 offset-4'> {/* Columna que ocupa 4 columnas en dispositivos medianos y se desplaza 4 columnas hacia la derecha */}
            <div className='d-grid mx-auto'> {/* Una cuadrícula CSS centrada */}
                {children} {/* Renderiza el contenido dentro de esta cuadrícula */}
            </div>
        </div>
    </div>
  )
}

// Exporta el componente DivAdd para ser utilizado en otras partes de la aplicación
export default DivAdd
